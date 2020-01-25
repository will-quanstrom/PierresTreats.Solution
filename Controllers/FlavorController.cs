using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using PierresTreats.Models;

namespace PierresTreats.Controllers
{
    public class FlavorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PierresTreatsContext _db;

        public FlavorController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Flavors.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(Flavor flavor, int TreatId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser =  await _userManager.FindByIdAsync(userId);
            flavor.User = currentUser;
            if (TreatId != 0)
            {
                _db.TreatFlavor.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var thisFlavor = _db.Flavors
                .Include(flavor => flavor.Treats)
                .ThenInclude(join => join.Flavor)
                .FirstOrDefault(flavor => flavor.FlavorId == id);
                return View(thisFlavor);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
            return View(thisFlavor);
        }

        [Authorize]
        public ActionResult AddTreat(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
            ViewBag.TreatId = new SelectList(_db.TreatFlavor, "TreatId", "Name");
            return View(thisFlavor);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddTreat(Flavor flavor, int TreatId)
        {
            if (TreatId != 0)
            {
            _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = flavor.FlavorId, TreatId = TreatId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var thisFlavor = _db.Treats.FirstOrDefault(flavors => flavors.FlavorId == id);
            return View(thisFlavor);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
            _db.Flavors.Remove(thisFlavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteTreat(int joinId)
        {
            var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
            _db.TreatFlavor.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}