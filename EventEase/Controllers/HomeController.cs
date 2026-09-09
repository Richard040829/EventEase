using EventEase.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["VenueCount"] = await _context.Venues.CountAsync();
            ViewData["EventCount"] = await _context.Events.CountAsync();
            ViewData["BookingCount"] = await _context.Bookings.CountAsync();
            return View();
        }
    }
}
