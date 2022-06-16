namespace PlotApp.MVVM.Models.Dot {
    internal class Point {
        public Point() {
            this.X = null;
            this.Y = null;
        }

        public Point(double x, double y) {
            this.X = x;
            this.Y = y;
        }

        public double? X { get; set; }
        public double? Y { get; set; }
    }
}
