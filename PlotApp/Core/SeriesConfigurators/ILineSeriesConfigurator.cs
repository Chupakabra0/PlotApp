using OxyPlot.Series;

namespace PlotApp.Core.SeriesConfigurators {
    interface ILineSeriesConfigurator {
        public LineSeries           Config(LineSeries series);
        public ILineSeriesConfigurator? NextConfigurator { get; set; }
    }
}
