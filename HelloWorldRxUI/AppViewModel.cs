﻿using System;
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

        public AppViewModel()
        {
            this.ObservableForProperty(x => x.Name, false, false)
                .Subscribe(x =>
                {
                    Greeting = "Hello " + (string.IsNullOrEmpty(x.Value) ? "stranger" : x.Value);
                });

            ContinueCommand = ReactiveCommand.CreateAsyncTask(
                this.ObservableForProperty(x => x.Name, false, false).Select(x => !string.IsNullOrEmpty(x.Value)),
                (x, ct) => MakeGreetingAsync(Name, ct));
        }

        private async Task<string> MakeGreetingAsync(string name, CancellationToken ct)
        {
            return await Task.Run(() =>
            {
                AsyncGreeting = "Working...";
                for (int i = 0; i < 10; ++i)
                {
                    Thread.Sleep(1000);
                    Progress = (i + 1)*10/100.0;
                }
                var greeting = "Hello " + (string.IsNullOrEmpty(name) ? "stranger" : name);
                AsyncGreeting = greeting;
                return greeting;
            }, ct);
        }
    }
}
