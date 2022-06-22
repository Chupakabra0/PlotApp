using OxyPlot;
using OxyPlot.Series;

namespace PlotApp.Core.SeriesConfigurators {
    class AddCirclesConfigurator : ILineSeriesConfigurator {
        public AddCirclesConfigurator(ILineSeriesConfigurator? nextConfigurator = null) {
            this.NextConfigurator = nextConfigurator;
        }

        public LineSeries Config(LineSeries series) {
            series.MarkerType = MarkerType.Circle;

            return this.NextConfigurator?.Config(series) ?? series;
        }

        public ILineSeriesConfigurator? NextConfigurator { get; set; }
    }
}
