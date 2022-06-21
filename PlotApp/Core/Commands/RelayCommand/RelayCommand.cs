﻿using System;
using System.Windows.Input;

namespace PlotApp.Core.Commands.RelayCommand {
    public class RelayCommand : ICommand {
        public RelayCommand(Action<object?>? function, Func<object?, bool>? canExecute = null) {
            this.canExecute = canExecute;
            this.function   = function;
        }

        public bool CanExecute(object? parameter) => this.canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => this.function?.Invoke(parameter);

        public event EventHandler? CanExecuteChanged {
            add    => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        protected Func<object?, bool>? canExecute;
        protected Action<object?>?     function;
    }
}
