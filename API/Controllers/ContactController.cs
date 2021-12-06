using Domain.Domain;
using Domain.Services;
using Infraestructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        
        private readonly ContactService<ContactViewModel> contactService;

        public ContactController(ContactService<ContactViewModel> service)
        {
            this.contactService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = contactService.GetAll();
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = contactService.GetOne(id);
            return Ok(result);
        }

        [HttpPost("")]
        public IActionResult Search([FromBody] QueryOptions  queryOptions)
        {
            var result = contactService.GetAll(queryOptions.Page,queryOptions.Limit,queryOptions.Order,queryOptions.Search);
            return Ok(result);
        }



        [HttpPost("")]
        public IActionResult Create([FromBody] ContactViewModel entity)
        {
            var newEntity = contactService.Add(entity);
            return Created("New", newEntity);   
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ContactViewModel entity)
        {
            if (contactService.Update(entity))
            {
                return Ok(entity);
            }
            else
            {
                return StatusCode(304);     
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {

            if (contactService.Remove(id))
            {
                return NoContent();           
            }
            else
            {
                return NotFound();           
            }

        }

    }
}
