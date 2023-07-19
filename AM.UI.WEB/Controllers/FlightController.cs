using AM.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using AM.Core.Domain;
using AM.Data.Migrations;


namespace AM.UI.WEB.Controllers
{
    public class FlightController : Controller
    {
        readonly IFlightService flightService;
        readonly IPlaneService planeService;
        public FlightController(IFlightService flightService, IPlaneService planeService)
        {
            this.flightService = flightService;
            this.planeService = planeService;   

        }
        // GET: FlightController

        public ActionResult Index(DateTime? flightdate)
        {
            if(flightdate!=null)
                return View(flightService.GetAll().Where(f=>f.FlightDate==flightdate).ToList());
            return View(flightService.GetAll());
        }

        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightController/Create
        public ActionResult Create()
        {
            var planes = planeService.GetAll();
            ViewBag.Planes = new SelectList(planes, "PlaneId", "PlaneId");
            return View();
        }
        public ActionResult Sort()
        {
            return View("Index", flightService.SortFlights());
        }
        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight flight,IFormFile PilotImage)
        {
            try
            {
                if (PilotImage != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                   "wwwroot", "uploads", PilotImage.FileName);
                    Stream stream = new FileStream(path, FileMode.Create);
                    PilotImage.CopyTo(stream);
                    flight.Pilot = PilotImage.FileName;
                }
                flightService.Add(flight);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(flightService.Get(id));
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight)
        {
            try
            {
                flight.FlightId = id;
                flightService.update(flight);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(flightService.Get(id));
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Flight flight)
        {
            try
            {
                flight.FlightId = id;
                flightService.Remove(flight.FlightId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
