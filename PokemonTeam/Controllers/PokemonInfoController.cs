using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PokemonTeam.Models.PokemonDBModel;
using PokemonTeam.Repository;

namespace PokemonTeam.Controllers {
    public class PokemonInfoController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult AddPokemon(PokemonInfo item) {
            using (var myEntity = new PokemonPartyDbContext()) {
                try {
                    item.DateCreated = DateTime.Now;
                    myEntity.PokemonInfos.Add(item);
                    myEntity.SaveChanges();
                    return Json(new {
                        Success = true,
                        Message = "Pokemon Added Successfully!"
                    });

                }
                catch (Exception ex) {
                    return Json(new {
                        Success = false,
                        Message = ex.Message,
                    });
                }
            }
        }
    }
}
