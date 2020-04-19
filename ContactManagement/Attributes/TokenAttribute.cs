using ContactManagement.Core;
using ContactManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace ContactManagement.Attributes
{
    /// <summary>
    /// TOken Attribute
    /// </summary>
    public class TokenAttribute : ActionFilterAttribute
    {
        //Get and save the API token on Session variables
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string token = (string)filterContext.HttpContext.Session.Contents[SessionKeys.APIToken];
            if (string.IsNullOrEmpty(token))
            {
                string apiToken = string.Empty;
                apiToken = GetToken();
                if (!string.IsNullOrEmpty(apiToken))
                {
                    filterContext.HttpContext.Session.Add(SessionKeys.APIToken, apiToken);
                }
            }
        }

        private string GetToken()
        {
            string token = string.Empty;
            try
            {
                var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", "evolent" ),
                        new KeyValuePair<string, string> ( "Password", "001319a4-24d1-4212-86be-389245a3add9")
                    };
                var content = new FormUrlEncodedContent(pairs);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync("http://localhost:53278/token", content).Result;
                    var tokenResponse = response.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<APIToken>(tokenResponse).AccessToken;
                }
            }
            catch (AggregateException ex) // If the API is not available multiple errors are thrown, thus we are capturing Aggregate excp here.
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return token;
        }
    }
}