

namespace LiveAdd.Helpers
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class GoogleReverseGeolovationService
    {
        private const string ApiKey = "AIzaSyBFmLRuqZ8zpwJb9O8Bpk8_F0EAuKKtKFo";

        public string GetAddressFromCoordinates(double latitude, double longitude)
        {
            string addresss = string.Empty;
            var response = this.GoogleApiAddress(latitude, longitude);
            addresss = this.ParseAddressFromResponse(response);

            return addresss;
        }

        private string GoogleApiAddress(double latitude, double longitude)
        {
            string urlBase = "https://maps.googleapis.com/maps/api/geocode/json";
            string location = "?latlng=" + latitude + "," + longitude;
            string key = "&key=" + ApiKey;
            string url = urlBase + location + key;

            string responseString = string.Empty;
            using (var client = new HttpClient())
            {
                // ask a trainer - why when we use the Windows.Net.Http.dll ot this one and we use await it sleeps
                var response = client.GetAsync(new Uri(url)).Result;
                responseString = response.Content.ReadAsStringAsync().Result;
            }
            return responseString;
        }

        private string ParseAddressFromResponse(string response)
        {
            string result = string.Empty;
            var jsonData = JObject.Parse(response);

            result = (string)jsonData["results"].First["formatted_address"];

            return result;
        }
    }
}
