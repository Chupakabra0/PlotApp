using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.DataManipulators.IDataManipulator {
    internal interface IDataManipulator {
        public string DataSerialize(ICollection<Point> points);
        public ICollection<Point> DataDeserialize(string data);
    }
}
