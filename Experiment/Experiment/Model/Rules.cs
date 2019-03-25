using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Experiment.Model
{
    [Serializable]
    public class Rules
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [XmlArrayItem("ChildRules")]
        public List<Rules> ChildRules { get; set; }
        [XmlIgnore]
        public List<ImageSigned> Images { get; set; }
    }
}