using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SweetAndSavory.Data;

namespace SweetAndSavory.Controllers
{
    public class TreatsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TreatsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Treats.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Treat treat, int FlavorId)
        {
            _db.Treats.Add(treat);
            if (FlavorId != 0)
            {
                _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Treat thisTreat = _db.Treats
            .Include(treat => treat.Flavors)
            .ThenInclude(join => join.Flavor)
            .FirstOrDefault(treats => treats.TreatId == id);
            return View(thisTreat);
        }

        public ActionResult Edit(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
            return View(thisTreat);
        }

        [HttpPost]
        public ActionResult Edit(Treat treat, int FlavorId)
        {
            if (FlavorId != 0)
            {
                _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
            }
            _db.Entry(treat).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddFlavor(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
            return View(thisTreat);
        }

        [HttpPost]
        public ActionResult AddFlavor(Treat treat, int FlavorId)
        {
            if (FlavorId != 0)
            {
                _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
            return View(thisTreat);
        }

        [HttpPost]
        public ActionResult DeleteFlavor(int joinId)
        {
            var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
            _db.FlavorTreat.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
            _db.Treats.Remove(thisTreat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}