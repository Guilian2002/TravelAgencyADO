using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyADO.ASPCore.Clients;
using TravelAgencyADO.ASPCore.Models.TravelAgency;

namespace TravelAgencyADO.ASPCore.Controllers.TravelAgency
{
    public class DestinationController : Controller
    {
        private readonly TravelAgencyApiClient _agency;

        public DestinationController(TravelAgencyApiClient agency)
        {
            _agency = agency;
        }

        // GET: DestinationController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                List<DestinationViewModel> destinations = (List<DestinationViewModel>) await _agency.GetAllDestinationAsync();
                return View(destinations);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: DestinationController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var destination = await _agency.GetDestinationByIdAsync(id);

                if (destination == null)
                {
                    return NotFound();
                }

                return View(destination);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: DestinationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DestinationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DestinationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                DestinationViewModel destination = await _agency.DestinationCreateAsync(
                    model.Country,
                    model.City,
                    model.Description
                );

                if (destination != null)
                {
                    return RedirectToAction("Details", new { id = destination.Id });
                }

                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création.");
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //// GET: DestinationController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: DestinationController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DestinationController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DestinationController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
