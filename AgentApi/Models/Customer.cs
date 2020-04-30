using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AgentApi.Models
{
    public class Customer
    {
        /// <summary>
        /// The id of the Customer
        /// </summary>
        [JsonProperty("_id")]
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        /// <summary>
        /// The Id of the Agent the Customer belongs to
        /// </summary>
        [JsonProperty("agent_id")]
        [JsonPropertyName("agent_id")]
        public int AgentId { get; set; }

        /// <summary>
        /// The Customers GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Is the Customer Active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The Customers Balance
        /// </summary>
        public string Balance { get; set; }

        /// <summary>
        /// The Customers Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The Customers Eye Color
        /// </summary>
        public string EyeColor { get; set; }

        /// <summary>
        /// The Name of the Customer
        /// </summary>
        public PersonName Name { get; set; }

        /// <summary>
        /// The Company Name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The Customers Email Address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The Customers Phone Number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The Customers Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The Date the Customer Registered
        /// </summary>
        public DateTime Registered { get; set; }

        /// <summary>
        /// The Customers Associated Latitude
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The Customers Associated Longitude
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// The Customers Tags
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
