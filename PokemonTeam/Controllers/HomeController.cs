using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonTeam.Models;
using PokemonTeam.Repository;
using System.Diagnostics;

namespace PokemonTeam.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            var myEntity = new PokemonPartyDbContext();
            var myParties = myEntity.Parties.Include(c=>c.PartyDetails).ThenInclude(d=>d.Pokemon).ToList();
            return View(myParties);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
