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
                if (this.CheckAllStrings()) {
                    this.FillPoints();
                }
                else {
                    MessageBox.Show("Wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }, _ => this.CheckAllStrings());


        public ObservableCollection<Point> Points { get; set; } = new();

        private Function functionWrapper_ => new($"F(x) = {this.FunctionString}");

        private Func<double, double> function_ =>
            x => new Expression($"F({x.ToString(CultureInfo.InvariantCulture)})", this.functionWrapper_).calculate();

        private double lowLimitX_  => double.Parse(this.LowLimitXString,  CultureInfo.InvariantCulture);
        private double highLimitX_ => double.Parse(this.HighLimitXString, CultureInfo.InvariantCulture);
        private double lowLimitY_  => double.Parse(this.LowLimitYString,  CultureInfo.InvariantCulture);
        private double highLimitY_ => double.Parse(this.HighLimitYString, CultureInfo.InvariantCulture);
        private double step_       => double.Parse(this.StepString,       CultureInfo.InvariantCulture);

        private bool CheckAllStrings() => 
            DoubleValidation.IsDouble(this.LowLimitXString) && DoubleValidation.IsDouble(this.HighLimitXString) 
            && DoubleValidation.IsDouble(this.LowLimitYString) && DoubleValidation.IsDouble(this.HighLimitYString)
            && DoubleValidation.IsDouble(this.StepString)
            && this.functionWrapper_.checkSyntax();

        private void FillPoints() {
            var pb = new PointsBuilder(this.function_, new(this.lowLimitX_, this.highLimitX_),
                                       new(this.lowLimitY_, this.highLimitY_), this.step_);

            this.Points = pb.BuildPoints();
        }
    }
}