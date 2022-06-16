using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.DataManipulators.IDataManipulator {
    internal class JsonDataManipulator : IDataManipulator {
        public JsonDataManipulator() {
            
        }

        public string DataSerialize(ICollection<Point> points) =>
           JsonConvert.SerializeObject(points);
        

        public ICollection<Point> DataDeserialize(string data) =>
            JsonConvert.DeserializeObject<ICollection<Point>>(data) ?? throw new Exception("FUCK");
    }
}
