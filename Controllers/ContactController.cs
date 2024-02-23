using DewavesAPI.Data;
using DewavesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DewavesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    { private readonly DewavesAPIDbContext _dbContext;
        public ContactController( DewavesAPIDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet] 
        public async Task <IActionResult> GetAllContacts()
        {
            try
            {
                return Ok(await _dbContext.Contacted.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the Database");
            }
        }

        [HttpGet]
        [Route("{id:int}")]

         public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            try
            {
                var contact = await _dbContext.Contacted.FindAsync(id);

                if (contact == null)
                {

                    return NotFound();

                }
                return Ok(contact);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the Database");
            }
        }

       

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            try
            {
                var Contact = new Contact()
                {
                    Name = addContactRequest.Name,
                    Email = addContactRequest.Email,
                    Phone = addContactRequest.Phone,
                    Address = addContactRequest.Address,

                };

                await _dbContext.Contacted.AddAsync(Contact);
                await _dbContext.SaveChangesAsync();
                return Ok(Contact);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating/Adding  new contacts record ");
            }

        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateContact([FromRoute] int id ,UpdateContactRequest updateContactRequest)
        {
            try
            {

                var contact = await _dbContext.Contacted.FindAsync(id);
                if (contact != null)
                {

                    contact.Name = updateContactRequest.Name;
                    contact.Email = updateContactRequest.Email;
                    contact.Phone = updateContactRequest.Phone;
                    contact.Address = updateContactRequest.Address;

                    await _dbContext.SaveChangesAsync();
                    return Ok(contact);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating  contacts record ");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            try
            {
                var contact= await _dbContext.Contacted.FindAsync(id);
                if(contact != null)
                {
                    _dbContext.Contacted.Remove(contact);
                    await _dbContext.SaveChangesAsync();  
                    return Ok(contact);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting  contact record ");
            }

        }
    }
}
