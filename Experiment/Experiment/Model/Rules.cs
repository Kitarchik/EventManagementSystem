using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SQLite;

namespace Experiment.Model
{
    [Serializable, Table("Rules")]
    public class Rules
    {
        [PrimaryKey, JsonProperty(PropertyName = "rulesId")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "active")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "project")]
        public int ProjectId { get; set; }
        [JsonProperty(PropertyName = "parent")]
        public int? ParentId { get; set; }
        [JsonProperty(PropertyName = "creation")]
        public DateTime CreationDate { get; set; }
        [JsonProperty(PropertyName="lastEdit")]
        public DateTime? LastEditDate { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [XmlArrayItem("ChildRules"), Ignore]
        public List<Rules> ChildRules { get; set; }
        [XmlIgnore, Ignore]
        public List<ImageSigned> Images { get; set; }
    }
}