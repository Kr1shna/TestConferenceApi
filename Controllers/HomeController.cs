using ConferenceApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Encodings.Web;

namespace ConferenceApi.Controllers
{
    [Route("home/")]
    public class HomeController : Controller
    {
        private readonly SectionContext _context;

        public HomeController(SectionContext context)
        {
            _context = context;

            if (_context.Sections.Count() == 0) {
                _context.Sections.Add(new SectionItem {
                    SectionName = "GIS",
                    Name = "Geoinformation Systems",
                    City = "Tomsk",
                    Location = "Lenina 2, 404"
                });
                _context.Sections.Add(new SectionItem {
                    SectionName = "CS",
                    Name = "Computer Science",
                    City = "Tomsk",
                    Location = "Lenina 30, 206"
                });
                _context.SaveChanges();
            }
        }
        
        public IActionResult Index()
        {
            return View(_context.Sections.ToList());
        }

        [HttpPost]
        public IActionResult PostNewSection(string registerSection, string registerName, string registerCity, string registerLocation)
        {
            var hasSection = false;
            foreach (var s in _context.Sections) {
                if (!hasSection && s.SectionName == registerSection) {
                    hasSection = true;
                }
            }

            if (!hasSection) {
                _context.Sections.Add(new SectionItem {
                    SectionName = registerSection,
                    Name = registerName,
                    City = registerCity,
                    Location = registerLocation
                });
                _context.SaveChanges();
            }

            return View("~/Views/Home/Index.cshtml", _context.Sections.ToList());
        }
    }
}