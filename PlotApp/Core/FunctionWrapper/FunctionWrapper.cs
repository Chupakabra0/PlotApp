﻿using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Series;
using PlotApp.Core.FunctionModifiers;
using PlotApp.Core.SeriesConfigurators;
using PlotApp.Core.Utils;
using PlotApp.MVVM.Models.Dot;
using PlotApp.MVVM.Models.Function;

using FT = PlotApp.Core.FunctionType.FunctionType;

namespace PlotApp.Core.FunctionWrapper {
    internal class FunctionWrapper {
        public FunctionWrapper(Function function, FT type = FT.Line, decimal tension = 0.5M) {
            this.Function = function;
            this.Name     = this.Function.Name;
            this.ScaleX   = this.Function.ScaleX;
            this.ScaleY   = this.Function.ScaleY;
            this.WrapX    = this.Function.WrapX;
            this.WrapY    = this.Function.WrapY;
            this.Tension  = tension;
            this.Type     = type;

            this.modifier_ = new ScaleXModifier(this.ScaleX,
                new ScaleYModifier(this.ScaleY,
                    new WrapXModifier(this.WrapX,
                        new WrapYModifier(this.WrapY))));
        }

        public LineSeries GetLineSeries() {
            this.UpdateAll();

            ILineSeriesConfigurator? configurator
                = this.Type switch {
                    FT.Line  => null,
                    FT.Point => new RemoveLineConfigurator(new AddCirclesConfigurator()),
                    _        => null
                };

            var series = new LineSeries {
                InterpolationAlgorithm = new CanonicalSpline(DoubleDecimalCast.CastToDouble(this.Tension)),
                Title = this.Name
            };
            series = configurator?.Config(series) ?? series;

            var points = this.PointConstruct(this.Function.Points);

            foreach (var point in points) {
                series.Points.Add(new(DoubleDecimalCast.CastToDouble(point.X), DoubleDecimalCast.CastToDouble(point.Y)));
            }

            return series;
        }

        public Function Function { get; set; }

        public string  Name    { get; set; }
        public decimal ScaleX  { get; set; }
        public decimal ScaleY  { get; set; }
        public decimal WrapX   { get; set; }
        public decimal WrapY   { get; set; }
        public decimal Tension { get; set; }
        public FT      Type    { get; set; }

        private List<Point> PointConstruct(IEnumerable<Point> points) {
            //var result = new List<Point>(points);

            var enumerable = points.ToList();
            var result     = new List<Point>(enumerable.Count);
            result.AddRange(enumerable.Select(e => new Point(e.X, e.Y)));

            return this.modifier_.Modify(result);
        }

        private void UpdateAll() {
            //this.Function.Name   = this.Name;
            //this.Function.ScaleX = this.ScaleX;
            //this.Function.ScaleY = this.ScaleY;
            //this.Function.WrapX  = this.WrapX;
            //this.Function.WrapY  = this.WrapY;

            this.modifier_ = new ScaleXModifier(this.ScaleX,
                new ScaleYModifier(this.ScaleY,
                    new WrapXModifier(this.WrapX,
                        new WrapYModifier(this.WrapY))));
        }

        private IFunctionModifier modifier_;
    }
}
