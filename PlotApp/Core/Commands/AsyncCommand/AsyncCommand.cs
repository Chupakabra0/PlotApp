using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlotApp.Core.Utils;

namespace PlotApp.Core.Commands.AsyncCommand {
    public interface IAsyncCommand : ICommand {
        public Task ExecuteAsync();
        public bool CanExecute();
    }

    public class AsyncCommand : IAsyncCommand {
        public event EventHandler? CanExecuteChanged;

        public AsyncCommand(Func<Task>? execute, Func<bool>? canExecute = null) {
            this.execute    = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute() {
            return!this.isExecuting && (this.canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync() {
            if (this.CanExecute()) {
                try {
                    this.IsExecuting = true;
                    await this.execute?.Invoke()!;
                }
                finally {
                    this.IsExecuting = false;
                }
            }

            this.RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged() {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object? parameter) => this.CanExecute();
        void ICommand.Execute(object?    parameter) => this.ExecuteAsync().FireAndForgetSafeAsync();

        private bool isExecuting;

        public bool IsExecuting {
            get => this.isExecuting;
            set {
                if (value == this.isExecuting) return;
                this.isExecuting = value;
                this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private readonly Func<Task>? execute;
        private readonly Func<bool>? canExecute;
    }
}