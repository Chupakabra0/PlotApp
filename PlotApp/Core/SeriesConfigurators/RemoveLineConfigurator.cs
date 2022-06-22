using OxyPlot;
using OxyPlot.Series;

namespace PlotApp.Core.SeriesConfigurators {
    class RemoveLineConfigurator : ILineSeriesConfigurator {
        public RemoveLineConfigurator(ILineSeriesConfigurator? nextConfigurator = null) {
            this.NextConfigurator = nextConfigurator;
        }

        public LineSeries Config(LineSeries series) {
            series.LineStyle = LineStyle.None;

            return this.NextConfigurator?.Config(series) ?? series;
        }

        public ILineSeriesConfigurator? NextConfigurator { get; set; }
    }
}
