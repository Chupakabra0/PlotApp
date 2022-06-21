﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using PlotApp.Core.FunctionModifiers;
using PlotApp.MVVM.Models.Dot;
using PlotApp.MVVM.Models.Function;

namespace PlotApp.Core.FunctionWrapper {
    internal class FunctionWrapper {
        public FunctionWrapper(Function function, double tension/* = 0.5*/) {
            this.Function = function;
            this.Name     = this.Function.Name;
            this.ScaleX   = this.Function.ScaleX;
            this.ScaleY   = this.Function.ScaleY;
            this.WrapX    = this.Function.WrapX;
            this.WrapY    = this.Function.WrapY;
            this.Tension  = tension;

            this.modifier_ = new ScaleXModifier(this.ScaleX,
                new ScaleYModifier(this.ScaleY,
                    new WrapXModifier(this.WrapX,
                        new WrapYModifier(this.WrapY))));
        }

        public LineSeries GetLineSeries() {
            this.UpdateAll();

            var series = new LineSeries {
                InterpolationAlgorithm = new CanonicalSpline(this.Tension)
            };
            var points = this.PointConstuct(this.Function.Points);

            foreach (var point in points) {
                series.Points.Add(new(point.X, point.Y));
            }

            return series;
        }

        public Function Function { get; set; }

        public string Name    { get; set; }
        public double ScaleX  { get; set; }
        public double ScaleY  { get; set; }
        public double WrapX   { get; set; }
        public double WrapY   { get; set; }
        public double Tension { get; set; }

        private List<Point> PointConstuct(IEnumerable<Point> points) {
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
