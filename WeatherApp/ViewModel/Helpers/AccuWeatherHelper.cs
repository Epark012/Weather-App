using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    /// <summary>
    /// Helper class to request information from Accuweather API
    /// </summary>
    public class AccuWeatherHelper
    {
        // User Key
        public const string API_KEY = "XnVs75DWUY6mGSUwOOUqdeT8o4akNdGq";

        // Endpoints
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRNET_CONDITION_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        
        /// <summary>
        /// Request city list
        /// </summary>
        /// <param name="key">Key value to search</param>
        /// <returns></returns>
        public static async Task<List<City>> GetCities(string key)
        {
            var cities = new List<City>();

            var url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, key);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        /// <summary>
        /// Request current condition by city key
        /// </summary>
        /// <param name="cityKey">City key to check current condition</param>
        /// <returns></returns>
        public static async Task<CurrentCondition> GetCurrentCondition(string cityKey)
        {
            var currentCondition = new CurrentCondition();

            var url = BASE_URL + string.Format(CURRNET_CONDITION_ENDPOINT, cityKey, API_KEY);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                currentCondition = JsonConvert.DeserializeObject<List<CurrentCondition>>(json).FirstOrDefault();
            }

            return currentCondition;
        }
    }
}
