using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using PokemonTeam.Models.PokemonDBModel;
using PokemonTeam.Repository;
using PokemonTeam.Controllers;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeam.Controllers {
    public class PokemonPartyController : Controller {
        public IActionResult Index() {
            return View(GetPokemonInfo());
        }
        public IActionResult DeleteParty(int id) {
            using (var myEntity = new PokemonPartyDbContext()) {
                using (var trans = myEntity.Database.BeginTransaction()) {
                    try {         
                        var tbl = GetPartyById(id);
                        myEntity.PartyDetails.RemoveRange(tbl.PartyDetails);
                        myEntity.Parties.Remove(tbl);
                        myEntity.SaveChanges();
                        trans.Commit();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex) {
                        trans.Rollback();
                        return Json(new {
                            success = true,
                            message = ex.Message
                        });
                    }
                }
            }
        }
        public IActionResult Edit(int id) {
            var myParties = GetPartyById(id);
            var myPokemonList = GetPokemonInfo();
            ViewData["PartyInfo"] = myParties;
            ViewData["PokemonList"] = myPokemonList;
            return View();
        }
        public IActionResult EditParty(Party item, List<int> itemID) {
            using (var myEntity = new PokemonPartyDbContext()) {
                using (var trans = myEntity.Database.BeginTransaction()) {
                    try {
                        var classified = new List<string>();
                        var tbl = GetPartyById(item.PartyId);
                        myEntity.PartyDetails.RemoveRange(tbl.PartyDetails);
                        foreach (var id in itemID) {
                            var itemD = new PartyDetail {
                                PokemonId = id,
                            };
                            tbl.PartyDetails.Add(itemD);
                            classified.Add(GetPokemonType(id));
                        }
                        tbl.Classification = classified.GroupBy(c => c).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key;
                        tbl.Name = item.Name;
                        myEntity.Parties.Update(tbl);
                        //
                        myEntity.SaveChanges();
                        trans.Commit();
                        return Json(new {
                            success = true,
                            message = "Party Updated Successfully!"
                        });
                    }
                    catch (Exception ex) {
                        trans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                }
            }
        }
        [HttpPost]
        public IActionResult AddParty(Party item, List<int> itemID) {
            using (var myEntity = new PokemonPartyDbContext()) {
                using (var trans = myEntity.Database.BeginTransaction()) {
                    try {
                        var classified = new List<string>();
                        foreach (var id in itemID) {
                            var itemD = new PartyDetail {
                                PokemonId = id,
                            };
                            item.PartyDetails.Add(itemD);
                            classified.Add(GetPokemonType(id));
                        }
                        item.Classification = classified.GroupBy(c => c).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key;
                        item.DateCreated = DateTime.Now;
                        myEntity.Parties.Add(item);
                        //
                        myEntity.SaveChanges();
                        trans.Commit();
                        return Json(new {
                            success = true,
                            message = "Party Created Successfully!"
                        });
                    }
                    catch (Exception ex) {
                        trans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                }
            }
        }
        private IEnumerable<SelectListItem> GetPokemonInfo() {
            using (var myEntity = new PokemonPartyDbContext()) {
                var item = myEntity.PokemonInfos.ToList();
                return item.Select(c => new SelectListItem {
                    Value = c.PokemonId.ToString(),
                    Text = c.Name
                });
            }
        }
        private string GetPokemonType(int id) {
            using (var myEntity = new PokemonPartyDbContext()) {
                return myEntity.PokemonInfos.FirstOrDefault(c => c.PokemonId == id).Type;
            }
        }
        private Party GetPartyById(int id) {
            var myEntity = new PokemonPartyDbContext();
            return myEntity.Parties.Include(c => c.PartyDetails).ThenInclude(d => d.Pokemon).FirstOrDefault(e => e.PartyId == id);
        }
    }
}
