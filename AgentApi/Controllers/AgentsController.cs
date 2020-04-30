using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgentApi.Models;

namespace AgentApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentContext _context;

        public AgentsController(AgentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All Agents
        /// </summary>
        /// <returns>A List of all the Agents</returns>
        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            await _context.Phones.ToListAsync();
            return await _context.Agents.ToListAsync();
        }

        /// <summary>
        /// Get Agent Details by the Agent Id
        /// </summary>
        /// <param name="id">The Agent Id</param>
        /// <returns>The Agent Details</returns>
        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            await _context.Phones.ToListAsync();
            var agent = await _context.Agents.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return agent;
        }

        /// <summary>
        /// Update the Agent Details
        /// </summary>
        /// <param name="id">The Id of the Agent</param>
        /// <param name="agent">The Agent Details</param>
        /// <returns></returns>
        /// <response code="204">Returns Success</response>
        /// <response code="400">If the Agent Id details do not match</response>            
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // PUT: api/Agents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgent(int id, Agent agent)
        {
            if (id != agent.Id)
            {
                return BadRequest();
            }

            _context.Entry(agent).State = EntityState.Modified;
            
            if (agent.Phone != null)
            {
                _context.Entry(agent.Phone).State = EntityState.Modified;
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await _context.Phones.ToListAsync();
            var existingAgents = await _context.Agents.FindAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Create Agent
        /// </summary>
        /// <param name="agent">The Agent Details</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or Agent Id already exisits</response>            
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<Agent>> CreateAgent(Agent agent)
        {
            if (agent != null && !AgentExists(agent.Id))
            {
                _context.Agents.Add(agent);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAgent), new { id = agent.Id }, agent);
            }
            else
            {
                return BadRequest();
            }

        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.Id == id);
        }
    }
}
