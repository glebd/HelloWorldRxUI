﻿using System;
using System.Reactive.Linq;
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

        public ReactiveCommand<object> ContinueCommand { get; set; }

        public AppViewModel()
        {
            this.ObservableForProperty(x => x.Name, false, false)
                .Subscribe(x =>
                {
                    Greeting = "Hello " + (string.IsNullOrEmpty(x.Value) ? "stranger" : x.Value);
                });

            ContinueCommand = ReactiveCommand.Create(
                this.ObservableForProperty(x => x.Name, false, false).Select(x => !string.IsNullOrEmpty(x.Value)));
        }
    }
}
