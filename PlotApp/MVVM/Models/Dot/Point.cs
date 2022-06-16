namespace PlotApp.MVVM.Models.Dot {
    internal class Point {
        public Point() {
            this.X = 0.0;
            this.Y = 0.0;
        }

        public Point(double x, double y) {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
