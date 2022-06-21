namespace PlotApp.MVVM.Models.Dot {
    internal class Point {
        public Point() {
            this.X = 0;
            this.Y = 0;
        }

        //public Point(Point xy) {
        //    this.X = xy.X;
        //    this.Y = xy.Y;
        //}

        public Point(decimal x, decimal y) {
            this.X = x;
            this.Y = y;
        }

        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
