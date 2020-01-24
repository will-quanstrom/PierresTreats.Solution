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
    public class TreatController : Controller
    {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly PierresTreatsContext _db;

      public TreatController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
      {
          _userManager = userManager;
          _db = db;
      }

      public ActionResult Index()
      {
          return View(_db.Treats.ToList());
      }

      [Authorize]
      public ActionResult Create()
      {
          ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
          return View();
      }

      [Authorize]
      [HttpPost]
      public async Task<ActionResult> Create(Treat treat, int FlavorId)
      {
          var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          var currentUser = await _userManager.FindByIdAsync(userId);
          treat.User = currentUser;
          _db.Treats.Add(treat);
          if (FlavorId != 0)
          {
              _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
          }
          _db.SaveChanges();
          return RedirectToAction("Index");
      }

      [Authorize]
      public ActionResult Details(int id)
      {
        var thisTreat = _db.Treats
          .Include(treat => treat.Flavors)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(treat => treat.TreatId == id);
          return View(thisTreat);
      }

      [Authorize]
      public ActionResult Edit(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View(thisTreat);
      }
      
      [Authorize]
      [HttpPost]
      public ActionResult Edit(Treat treat, int FlavorId)
      {
        if (FlavorId != 0)
        {
          _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
        }
        _db.Entry(treat).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      [Authorize]
      public ActionResult AddFlavor(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
        ViewBag.FlavorId = new SelectList(_db.TreatFlavor, "FlavorId", "Name");
        return View(thisTreat);
      }

      [Authorize]
      [HttpPost]
      public ActionResult AddFlavor(Treat treat, int FlavorId)
      {
        if (FlavorId != 0)
        {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      [Authorize]
      public ActionResult Delete(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
        return View(thisTreat);
      }

      [Authorize]
      [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
        _db.Treats.Remove(thisTreat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      [Authorize]
      [HttpPost]
      public ActionResult DeleteFlavor(int joinId)
      {
        var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
        _db.TreatFlavor.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      }
}