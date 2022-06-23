using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.Formulas;
using PlotApp.Core.PointsBuilder;
using PlotApp.Core.Utils;
using PlotApp.Core.Validations.ModelValidations;

using Expression = org.mariuszgromada.math.mxparser.Expression;
using Point      = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.FillFunctionViewModel {
    internal class FillFunctionViewModel : BaseViewModel.BaseViewModel {
        public FillFunctionViewModel() {
            this.myFormula_ = new SofiaFormula();
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

        public ICommand FillByMyFunctionCommand =>
            new RelayCommand(_ => {
                if (this.CheckAllStrings()) {
                    this.FillByMyFunction();
                }
                else {
                    MessageBox.Show("Wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error,
                                    MessageBoxResult.OK);
                }
            }, _ => this.CheckAllStrings()); // TODO: another check rool

        public ObservableCollection<Point> Points { get; set; } = new();

        private Function functionWrapper_ => new($"F(x) = {this.FunctionString}");

        private readonly IFormula myFormula_;

        private Func<double, double> function_ =>
            x => new Expression($"F({x.ToString(CultureInfo.InvariantCulture)})", this.functionWrapper_).calculate();

        private Func<double, double> myFunction_ => x => this.myFormula_.Calculate(x);

        private decimal lowLimitX_ 
            => DoubleDecimalCast.CastToDecimal(double.Parse(this.LowLimitXString,  CultureInfo.InvariantCulture));
        private decimal highLimitX_
            => DoubleDecimalCast.CastToDecimal(double.Parse(this.HighLimitXString, CultureInfo.InvariantCulture));
        private decimal lowLimitY_ 
            => DoubleDecimalCast.CastToDecimal(double.Parse(this.LowLimitYString,  CultureInfo.InvariantCulture));
        private decimal highLimitY_
            => DoubleDecimalCast.CastToDecimal(double.Parse(this.HighLimitYString, CultureInfo.InvariantCulture));
        private decimal step_
            => DoubleDecimalCast.CastToDecimal(double.Parse(this.StepString,       CultureInfo.InvariantCulture));

        private bool CheckAllStrings() => 
            decimalValidation.IsDouble(this.LowLimitXString) && decimalValidation.IsDouble(this.HighLimitXString) 
            && decimalValidation.IsDouble(this.LowLimitYString) && decimalValidation.IsDouble(this.HighLimitYString)
            && decimalValidation.IsDouble(this.StepString)
            && this.functionWrapper_.checkSyntax();

        private void FillPoints() {
            var pb = new PointsBuilder(this.function_, new(this.lowLimitX_, this.highLimitX_),
                                       new(this.lowLimitY_, this.highLimitY_), this.step_);

            this.Points = pb.BuildPoints();
        }

        private void FillByMyFunction() {
            var pb = new PointsBuilder(this.myFunction_, new(this.lowLimitX_, this.highLimitX_),
                                       new(this.lowLimitY_, this.highLimitY_), this.step_);

            this.Points = pb.BuildPoints();
        }
    }
}