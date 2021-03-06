﻿using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace HelloWorldRxUI
{
    public class AppViewModel : ReactiveObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private string _greeting;
        public string Greeting
        {
            get { return _greeting; }
            set { this.RaiseAndSetIfChanged(ref _greeting, value); }
        }

        private string _asyncGreeting;
        public string AsyncGreeting
        {
            get { return _asyncGreeting; }
            set { this.RaiseAndSetIfChanged(ref _asyncGreeting, value); }
        }

        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set { this.RaiseAndSetIfChanged(ref _progress, value); }
        }

        public ReactiveCommand<string> ContinueCommand { get; set; }

        public ReactiveCommand<object> StopCommand { get; set; }

        public AppViewModel()
        {
            this.ObservableForProperty(x => x.Name, false, false)
                .Subscribe(x =>
                {
                    Greeting = "Hello " + (string.IsNullOrEmpty(x.Value) ? "stranger" : x.Value);
                });

            ContinueCommand = ReactiveCommand.CreateAsyncObservable(
                this.ObservableForProperty(x => x.Name, false, false).Select(x => !string.IsNullOrEmpty(x.Value)),
                _ => Observable.FromAsync(LongTask).TakeUntil(StopCommand));

            StopCommand = ReactiveCommand.Create(ContinueCommand.IsExecuting);

            ContinueCommand.ObserveOn(RxApp.MainThreadScheduler).Subscribe(s => AsyncGreeting = s);
        }

        private async Task<string> LongTask(CancellationToken ct)
        {
            Progress = 0;
            return await Task.Run(() =>
            {
                for (int i = 0; i < 10 && !ct.IsCancellationRequested; ++i)
                {
                    Thread.Sleep(1000);
                    Progress = (i + 1)*10/100.0;
                }
                if (ct.IsCancellationRequested)
                {
                    Debug.WriteLine("Cancellation requested");
                    Progress = 0;
                }
                var greeting = ct.IsCancellationRequested ? "Cancelled!" : "Finished";
                return greeting;
            }, ct);
        }
    }
}
