using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.PointsBuilder;
using PlotApp.Core.Validations.ModelValidations;
using Expression = org.mariuszgromada.math.mxparser.Expression;
using Point = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.FillFunctionViewModel {
    internal class FillFunctionViewModel : BaseViewModel.BaseViewModel {
        public FillFunctionViewModel() {

        }

        public string FunctionString   { get; set; } = "x^2";
        public string LowLimitXString  { get; set; } = "-10.0";
        public string HighLimitXString { get; set; } = "10.0";
        public string LowLimitYString  { get; set; } = double.NegativeInfinity.ToString(CultureInfo.InvariantCulture);
        public string HighLimitYString { get; set; } = double.PositiveInfinity.ToString(CultureInfo.InvariantCulture);
        public string StepString       { get; set; } = "0.1";

        public ICommand EvaluateCommand =>
            new RelayCommand(_ => {
                if (this.CheckAllString()) {
                    this.FillPoints();
                }
                else {
                    MessageBox.Show("Wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }, _ => this.CheckAllString());


        public ObservableCollection<Point> Points { get; set; } = new();

        private Function   FunctionWrapper   => new($"F(x) = {this.FunctionString}");
        private Func<double, double> Function =>
            x => new Expression($"F({x.ToString(CultureInfo.InvariantCulture)})", this.FunctionWrapper).calculate();

        private double LowLimitX  => double.Parse(this.LowLimitXString,  CultureInfo.InvariantCulture);
        private double HighLimitX => double.Parse(this.HighLimitXString, CultureInfo.InvariantCulture);
        private double LowLimitY  => double.Parse(this.LowLimitYString,  CultureInfo.InvariantCulture);
        private double HighLimitY => double.Parse(this.HighLimitYString, CultureInfo.InvariantCulture);
        private double Step       => double.Parse(this.StepString,       CultureInfo.InvariantCulture);

        private bool CheckAllString() => 
            DoubleValidation.IsDouble(this.LowLimitXString) && DoubleValidation.IsDouble(this.HighLimitXString) 
            && DoubleValidation.IsDouble(this.LowLimitYString) && DoubleValidation.IsDouble(this.HighLimitYString)
            && DoubleValidation.IsDouble(this.StepString)
            && this.FunctionWrapper.checkSyntax();

        private void FillPoints() {
            var pb = new PointsBuilder(this.Function, new(this.LowLimitX, this.HighLimitX),
                                       new(this.LowLimitY, this.HighLimitY), this.Step);

            this.Points = pb.BuildPoints();
        }
    }
}