using Newtonsoft.Json;
using System;

namespace Experiment.Model
{
    public class Project
    {
        [JsonProperty(PropertyName= "projectId")]
        public int ProjectId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end")]
        public DateTime? EndDate { get; set; }
        public Rules Rules { get; set; }
    }
}