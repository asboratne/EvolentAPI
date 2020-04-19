using ContactManagement.Core;
using ContactManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ContactManagement.APIHelpers
{
    /// <summary>
    /// API Helper
    /// </summary>
    public static class APIHelper
    {
        /// <summary>
        /// Get Contacts List
        /// </summary>
        /// <param name="token">api token</param>
        /// <param name="baseUrl">base url</param>
        /// <returns>contact list</returns>
        internal static List<ContactsModel> GetContactsList(string token, string baseUrl)
        {
            List<ContactsModel> contactsList = new List<ContactsModel>();
            try
            {
                string apiURL = baseUrl + Constants.GetContactsList;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiURL));
                httpRequest.ContentType = "application/json";
                httpRequest.Method = "GET";
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
                string json = string.Empty;
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        json = (new StreamReader(stream)).ReadToEnd();
                        contactsList = JsonConvert.DeserializeObject<List<ContactsModel>>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contactsList;
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="contactsModel">contacts model</param>
        /// <param name="url">url</param>
        /// <param name="token">token</param>
        internal static void CreateContact(ContactsModel contactsModel, string url, string token)
        {
            try
            {
                string apiUrl = url + Constants.SaveContact;
                HttpClient client = new HttpClient();
                Uri baseAddress = new Uri(url);
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl, contactsModel).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Contact Details
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="url">url</param>
        /// <param name="token">token</param>
        /// <returns>Contacts details for given </returns>
        internal static ContactsModel GetContactDetails(int id, string url, string token)
        {
            ContactsModel contactModel = new ContactsModel();
            try
            {
                string apiURL = url + Constants.GetContactDetails + "/" + id;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiURL));
                httpRequest.ContentType = "application/json";
                httpRequest.Method = "GET";
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
                string json = string.Empty;
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        json = (new StreamReader(stream)).ReadToEnd();
                        contactModel = JsonConvert.DeserializeObject<ContactsModel>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contactModel;
        }

        /// <summary>
        /// Delete Contactco
        /// </summary>
        /// <param name="id">contact Id</param>
        /// <param name="url">base url</param>
        /// <param name="token">api token</param>
        internal static void DeleteContact(int id, string url, string token)
        {
            try
            {
                string apiURL = url + Constants.DeleteContact + "/" + id;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiURL));
                httpRequest.ContentType = "application/json";
                httpRequest.Method = "GET";
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
                string json = string.Empty;
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        json = (new StreamReader(stream)).ReadToEnd();                       
                    }
                }
            }
            catch (Exception)
            {
                throw;                
            }            
        }
    }
}