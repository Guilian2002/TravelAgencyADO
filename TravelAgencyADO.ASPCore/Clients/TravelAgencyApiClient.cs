using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using TravelAgencyADO.ASPCore.Models.TravelAgency;

namespace TravelAgencyADO.ASPCore.Clients
{
    public class TravelAgencyApiClient
    {
        private readonly HttpClient _http;

        public TravelAgencyApiClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://localhost:7091/");
        }

        public async Task<ActivityViewModel> ActivityCreateAsync(string title, string description, decimal price, Guid destinationId)
        {
            var payload = new
            {
                Title = title,
                Description = description,
                Price = price,
                DestinationId = destinationId
            };

            var response = await _http.PostAsJsonAsync("/api/activities", payload);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ActivityViewModel>();

                return data ?? throw new Exception("L'API a retourné un objet vide.");
            }

            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Erreur API ({response.StatusCode}): {errorDetails}");
        }
        public async Task<ICollection<ActivityViewModel>> GetAllActivitiesAsync(Guid destinationId)
        {
            var response = await _http.GetAsync($"/api/activities?destinationId={destinationId}");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ICollection<ActivityViewModel>>();
                return content!.ToList() ?? throw new ArgumentNullException("Pas d'activités");
            }

            throw new ArgumentNullException("Pas d'activités");
        }

        public async Task<ActivityViewModel> GetActivityByIdAsync(Guid id)
        {
            var response = await _http.GetAsync($"/api/activities/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ActivityViewModel>();
                return content ?? throw new ArgumentNullException("Pas d'activités");
            }

            throw new ArgumentNullException("Pas d'activités");
        }

        public async Task<BookingViewModel> BookingCreateAsync(DateTime? bookingDate, string clientName, ICollection<Guid> activityIds)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { bookingDate, clientName, activityIds }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("/api/bookings", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<BookingViewModel>();
                return data! ?? throw new ArgumentNullException("Pas de réservations reçu");
            }
            throw new ArgumentNullException("Pas de réservations reçu");
        }

        public async Task<bool> BookingDeleteAsync(Guid bookingId)
        {
            var response = await _http.DeleteAsync($"/api/bookings/{bookingId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<ICollection<BookingViewModel>> GetAllBookingsAsync()
        {
            var response = await _http.GetAsync("/api/bookings");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ICollection<BookingViewModel>>();
                return content!.ToList() ?? throw new ArgumentNullException("Pas de réservations");
            }

            throw new ArgumentNullException("Pas de réservations");
        }

        public async Task<BookingViewModel> GetBookingByIdAsync(Guid id)
        {
            var response = await _http.GetAsync($"/api/bookings/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<BookingViewModel>();
                return content ?? throw new ArgumentNullException("Pas de réservation");
            }

            throw new ArgumentNullException("Pas de réservation");
        }

        public async Task<DestinationViewModel> DestinationCreateAsync(string country, string city, string description)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { country, city, description }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("/api/destinations", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<DestinationViewModel>();
                return data! ?? throw new ArgumentNullException("Pas de destination reçu");
            }
            throw new ArgumentNullException("Pas de destination reçu");
        }

        public async Task<ICollection<DestinationViewModel>> GetAllDestinationAsync()
        {
            var response = await _http.GetAsync("/api/destinations");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ICollection<DestinationViewModel>>();
                return content!.ToList() ?? throw new ArgumentNullException("Pas de destinations");
            }
            throw new ArgumentNullException("Pas de destinations");
        }

        public async Task<DestinationViewModel> GetDestinationByIdAsync(Guid id)
        {
            var response = await _http.GetAsync($"/api/destinations/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<DestinationViewModel>();
                return content ?? throw new ArgumentNullException("Pas de destinations");
            }

            throw new ArgumentNullException("Pas de destinations");
        }
    }
}
