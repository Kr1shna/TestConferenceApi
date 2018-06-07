using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ConferenceApi.Models;
using System.Linq;
using Newtonsoft.Json;

namespace ConferenceApi.Controllers
{
    [Route("conference/")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly SectionContext _context;

        public ConferenceController(SectionContext context)
        {
            _context = context;
        }

        [HttpGet("info")]
        public IActionResult GetAll()
        {
            var result = "[";
            var l = _context.Sections.Count();
            foreach(var section in _context.Sections) {
                l--;
                result += section.Serialize();
                if (l > 0) {
                    result += ",";
                }
            }
            result += "]";
            return Ok(result);
        }

        [HttpGet("{context}/info")]
        public IActionResult GetSectionInfo(string context)
        {
            var sections = _context.Sections.ToList();
            foreach (var s in sections) {
                if (s.SectionName == context) {
                    return Ok(s.Serialize());
                }
            }

            return NotFound(context);
        }

        [HttpPost("{context}/info")]
        public IActionResult AddSectionInfo(string context, SectionItem item)
        {
            foreach (var s in _context.Sections) {
                if (s.SectionName == context) {
                    return BadRequest(context);
                }
            }

            _context.Sections.Add(new SectionItem {
                SectionName = context,
                Name = item.Name,
                City = item.City,
                Location = item.Location
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{context}/info")]
        public IActionResult ChangeSectionInfo(string context, SectionItem item)
        {
            foreach (var s in _context.Sections) {
                if (s.SectionName == context) {
                    s.SectionName = context;
                    s.Name = item.Name;
                    s.City = item.City;
                    s.Location = item.Location;

                    _context.Sections.Update(s);
                    _context.SaveChanges();
                    return NoContent();
                }
            }

            return BadRequest(context);
        }
    }
}
