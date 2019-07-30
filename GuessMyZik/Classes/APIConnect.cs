using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GuessMyZik.Classes;
using Windows.UI.Xaml.Media.Animation;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.Web.Http;
using System.Net.NetworkInformation;

namespace GuessMyZik.Classes
{
    class APIConnect
    {
        private string response;

        public APIConnect(){ } //Empty constructor 

        /// <summary>
        /// Function convert item paramter to JSON and send him to the url with HttpClient.
        /// </summary>
        /// <param name="item">Object to convert to JSON.</param>
        /// <param name="url">URL of the API for the HttpRequest.</param>
        public async Task<string> PostAsJsonAsync<T>(T item, string url)
        {
            HttpClient httpClient = new HttpClient();   //Instanciation of HttpClient.
            var itemAsJson = JsonConvert.SerializeObject(item); //Convert the object item to JSON Object.
            var httpResponseMessage = await httpClient.PostAsync(new Uri(url), new HttpStringContent(itemAsJson)); //Send to the API Server (url) the JSON Object.
            return this.response = await httpResponseMessage.Content.ReadAsStringAsync(); //Recover display content of the php files and convert to string.
        }

        /// <summary>
        /// Function send string to the url with HttpClient.
        /// </summary>
        /// <param name="item">String to sent.</param>
        /// <param name="url">URL of the API for the HttpRequest.</param>
        public async Task<string> PostAsJsonAsync(string item, string url)
        {
            HttpClient httpClient = new HttpClient(); //Instanciation of HttpClient.
            var httpResponseMessage = await httpClient.PostAsync(new Uri(url), new HttpStringContent(item)); //Send to the API Server (url) the string.
            return this.response = await httpResponseMessage.Content.ReadAsStringAsync(); //Recover display content of the php files and convert to string.
        }


        /// <summary>
        /// Function send string to the url with HttpClient.
        /// </summary>
        /// <param name="item">String to sent.</param>
        /// <param name="url">URL of the API for the HttpRequest.</param>
        public async Task<string> GetAsJsonAsync(string url)
        {
            HttpClient httpClient = new HttpClient(); //Instanciation of HttpClient.
            var httpResponseMessage = await httpClient.GetAsync(new Uri(url)); //Send to the API Server (url) the string.
            return this.response = await httpResponseMessage.Content.ReadAsStringAsync(); //Recover display content of the php files and convert to string.
        }

    }
}
