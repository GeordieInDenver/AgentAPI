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
    public class CustomersController : ControllerBase
    {
        private readonly AgentContext _context;

        public CustomersController(AgentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns>A List of all the Customers</returns>
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            await _context.Names.ToListAsync();
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Get All Customers associated to the Agent
        /// </summary>
        /// <param name="agentId">The Agent Id</param>
        /// <returns>A List of all Agents Customers</returns>
        // GET: api/Customers/5
        [HttpGet("{agentId}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersByAgentId(int agentId)
        {
            // get list of all the customers
            await _context.Names.ToListAsync();

            // return those customers that match the agent id
            return await _context.Customers.Where(x => x.AgentId == agentId).ToListAsync();
        }

        /// <summary>
        /// Update the Cusomter Details
        /// </summary>
        /// <param name="id">The Id of the Customer</param>
        /// <param name="customer">The Customer Details</param>
        /// <returns></returns>
        /// <response code="204">Returns Success</response>
        /// <response code="400">If the Customer Id details do not match</response>            
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            if (customer.Name != null)
            {
                _context.Entry(customer.Name).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer">The Customer Details</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or the customer id already exists</response>            
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            if (customer != null && !CustomerExists(customer.Id))
            {

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return new JsonResult(customer)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete the Customer by Id
        /// </summary>
        /// <param name="id">The Id of the customer</param>
        /// <returns></returns>
        /// <response code="204">Returns success if customer deleted</response>
        /// <response code="404">If Customer is not found</response>
        // DELETE: api/Customers/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
