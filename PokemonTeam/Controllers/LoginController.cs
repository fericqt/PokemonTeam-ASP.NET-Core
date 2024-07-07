using Microsoft.AspNetCore.Mvc;
using PokemonTeam.Models.PokemonDBModel;
using PokemonTeam.Repository;

namespace PokemonTeam.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult CheckCredentials(User item) {
            try {
                using (var myEntity = new PokemonPartyDbContext()) {
                    var temp = myEntity.Users.FirstOrDefault(c => c.Email == item.Email && c.Password == item.Password);
                    if (temp == null) {
                        return Json(new {
                            success = false,
                            message = "Email or Password incorrect!"
                        });
                    }
                    return Json(new {
                        success = true,
                        message = ""
                    });
                }
            }
            catch (Exception) {

                throw;
            }
        }
    }
}
