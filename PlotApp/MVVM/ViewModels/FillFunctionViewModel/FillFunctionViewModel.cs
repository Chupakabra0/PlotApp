using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.PointsBuilder;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.MVVM.ViewModels.FillFunctionViewModel {
    internal class FillFunctionViewModel : BaseViewModel.BaseViewModel {
        public FillFunctionViewModel() {

        }

        public string FunctionString { get; set; } = "x^2";
        public double LowLimitX      { get; set; } = 0;
        public double HighLimitX     { get; set; } = 10;
        public double LowLimitY      { get; set; } = -100;
        public double HighLimitY     { get; set; } = 100;
        public double Step           { get; set; } = 0.1;

        public ICommand EvaluateCommand =>
            new RelayCommand(_ => {
                var a = this.Function(0.1);
                var b = $"F(x) = {this.FunctionString}";

                var pb = new PointsBuilder(this.Function, new(this.LowLimitX, this.HighLimitX),
                                           new(this.LowLimitY, this.HighLimitY), this.Step);

                this.Points = pb.BuildPoints();
            });


        public ObservableCollection<Point> Points { get; set; }

        private Func<double, double> Function =>
            x => new Expression($"F({x.ToString(CultureInfo.InvariantCulture)})", new Function($"F(x) = {this.FunctionString}")).calculate();
    }
}