using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using AgentApi.Models;

namespace AgentApi.Utilities
{
    public static class Utilities
    {
        private const string agentFileName = "agents.txt";
        private const string customersFileName = "customers.txt";

        /// <summary>
        /// Load the test data from the json files
        /// </summary>
        /// <param name="context"></param>
        public static void LoadData(AgentContext context)
        {
            var agents = JsonConvert.DeserializeObject<List<Agent>>(File.ReadAllText(agentFileName));

            foreach(var agent in agents)
            {
                try
                {
                    context.Agents.Add(agent);
                    context.SaveChanges();
                }
                catch (Exception)
                { }

            }

            var customers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customersFileName));

            foreach (var customer in customers)
            {
                try
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                catch(Exception )
                { }
            }
        }
    }
}
