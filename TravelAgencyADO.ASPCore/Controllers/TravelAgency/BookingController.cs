using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TravelAgencyADO.ASPCore.Clients;
using TravelAgencyADO.ASPCore.Models.TravelAgency;

namespace TravelAgencyADO.ASPCore.Controllers.TravelAgency
{
    public class BookingController : Controller
    {
        private readonly TravelAgencyApiClient _agency;

        public BookingController(TravelAgencyApiClient agency)
        {
            _agency = agency;
        }

        // GET: BookingController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                List<BookingViewModel> bookings = (List<BookingViewModel>) await _agency.GetAllBookingsAsync();
                return View(bookings);
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Home");
            }
        }

        // GET: BookingController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var booking = await _agency.GetBookingByIdAsync(id);
                if (booking == null) return NotFound();

                var activities = new List<ActivityViewModel>();
                foreach (Guid activityId in booking.ActivityIds)
                {
                    activities.Add(await _agency.GetActivityByIdAsync(activityId));
                }

                DestinationViewModel destination = null;
                if (activities.Any())
                {
                    destination = await _agency.GetDestinationByIdAsync(activities.First().DestinationId);
                }

                var viewModel = new BookingDetailsViewModel
                {
                    Booking = booking,
                    Activities = activities,
                    Destination = destination!
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create(string selectedCountry)
        {
            var destinations = await _agency.GetAllDestinationAsync();
            var model = new BookingCreateViewModel { SelectedCountry = selectedCountry };

            model.AvailableCountries = destinations.Select(d => d.Country).Distinct().OrderBy(c => c).ToList();

            if (!string.IsNullOrEmpty(selectedCountry))
            {
                foreach (var dest in destinations.Where(d => d.Country == selectedCountry))
                {
                    var activities = await _agency.GetAllActivitiesAsync(dest.Id);
                    foreach (var act in activities)
                    {
                        model.AvailableActivities.Add(new ActivityRowViewModel
                        {
                            ActivityId = act.Id,
                            Title = act.Title,
                            Price = act.Price,
                            Country = dest.Country,
                            City = dest.City
                        });
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookingCreateViewModel model)
        {
            if (model.SelectedActivityIds == null || !model.SelectedActivityIds.Any())
            {
                ModelState.AddModelError("", "Veuillez cocher au moins une activité.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var booking = await _agency.BookingCreateAsync(
                    model.BookingDate,
                    model.ClientName,
                    model.SelectedActivityIds
                );
                return RedirectToAction("Details", new { id = booking.Id });
            }

            return View();
        }

        //// GET: BookingController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BookingController/Edit/5
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

        // GET: BookingController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var booking = await _agency.GetBookingByIdAsync(id);

                if (booking == null)
                {
                    return NotFound();
                }

                return View(booking);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: BookingController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _agency.BookingDeleteAsync(id);
                return RedirectToAction("Index", "Booking");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
