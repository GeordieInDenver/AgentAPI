using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AgentApi.Models
{
    public class Agent
    {
        /// <summary>
        /// The Agent Id
        /// </summary>
        [JsonProperty("_id")]
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        /// <summary>
        /// The Agents Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Agents Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The Agents City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The Agents State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The Agents Zip Code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// The Agents Tier Level
        /// </summary>
        public int Tier { get; set; }

        /// <summary>
        /// The Agents contact numbers
        /// </summary>
        public ContactPhone Phone { get; set; }
 
    }
}
