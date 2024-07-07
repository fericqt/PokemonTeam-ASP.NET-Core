using Microsoft.AspNetCore.Mvc;
using PokemonTeam.Models.PokemonDBModel;
using PokemonTeam.Repository;

namespace PokemonTeam.Controllers {
    public class RegisterController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult CreateAccount(User item) {
            try {
                using (var myEntity = new PokemonPartyDbContext()) {
                    item.DateCreated = DateTime.Now;
                    item.Status = "Active";
                    item.UserLevel = "Administrator";
                    myEntity.Add(item);
                    myEntity.SaveChanges();
                    return Json(new {
                        success = true,
                        message = "Account Created Successfully!"
                    });
                }
            }
            catch (Exception ex) {
                return Json(new {
                    success = false,
                    message = ex.Message
                });
            }
            
        }
    }
}
