using Newtonsoft.Json;
using System;
using SQLite;

namespace Experiment.Model
{
    [Table("Projects")]
    public class Project
    {
        [JsonProperty(PropertyName= "projectId"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end")]
        public DateTime? EndDate { get; set; }

        [Ignore]
        public Rules Rules { get; set; }
    }
}