using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyADO.ASPCore.Clients;
using TravelAgencyADO.ASPCore.Models.TravelAgency;

namespace TravelAgencyADO.ASPCore.Controllers.TravelAgency
{
    public class ActivityController : Controller
    {
        private readonly TravelAgencyApiClient _agency;

        public ActivityController(TravelAgencyApiClient agency)
        {
            _agency = agency;
        }

        // GET: ActivityController
        [HttpGet]
        public async Task<ActionResult> Index(Guid destinationId)
        {
            try
            {
                var activities = await _agency.GetAllActivitiesAsync(destinationId);
                ViewBag.DestinationId = destinationId;
                return View(activities);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ActivityController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var activity = await _agency.GetActivityByIdAsync(id);

                if (activity == null)
                {
                    return NotFound();
                }

                return View(activity);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ActivityController/Create
        public ActionResult Create(Guid destinationId)
        {
            try
            {
                var activity = new ActivityViewModel { DestinationId = destinationId };

                return View(activity);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // POST: ActivityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                ActivityViewModel activity = await _agency.ActivityCreateAsync(
                    model.Title,
                    model.Description,
                    model.Price,
                    model.DestinationId
                );

                if (activity != null)
                {
                    return RedirectToAction("Details", new { id = activity.Id });
                }

                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création.");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //// GET: ActivityController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ActivityController/Edit/5
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

        //// GET: ActivityController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ActivityController/Delete/5
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
