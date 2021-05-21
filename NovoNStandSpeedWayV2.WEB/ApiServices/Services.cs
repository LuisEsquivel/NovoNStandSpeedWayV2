

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NovoNStandSpeedWayV2.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;


namespace WEB.ApiServices
{


    public static class Services
    {

        public static string url = "";
        public static StringContent data = null;
        public static HttpClient client;
        public static Uri uri;
        public static HttpResponseMessage responseMessage;


        public static object SetStringContent<T>(T model, string Action = "")
        {
            url = $"{CoreResources.UrlBase}{ CoreResources.Prefix}/{typeof(T).Name.ToLower()}";
            var json = JsonConvert.SerializeObject(model);
            data = new StringContent(json, Encoding.UTF8, "application/json");
            client = new HttpClient();
            uri = new Uri(url);

            return true;
        }

        public static object SetResponse<T>()
        {
            object model = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var readTask = responseMessage.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<T>(readTask.ToString());
                Responses.StatusCode = (int)responseMessage.StatusCode;
            }

            return model;
        }



        public static void SetResponse()
        {

            if (responseMessage.IsSuccessStatusCode)
            {
                var readTask = responseMessage.Content.ReadAsStringAsync().Result;
                JObject jsonn = JObject.Parse(readTask.ToString());
            }

        }


        public static List<T> Get<T>()
        {

            List<T> objeto = null;

            var url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}";


            try
            {

                using (var client = new HttpClient())
                {

                    var uri = new Uri(Path.Combine(url));
         
                    responseMessage = client.GetAsync(uri).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string readTask = responseMessage.Content.ReadAsStringAsync().Result;
                        objeto = JsonConvert.DeserializeObject<List<T>>(readTask.ToString());
                    }
                    else
                    {
                        objeto = null;
                    }
                }


                return objeto;

            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return objeto;
            }

        }






        public static object Login<T>(T model)
        {

            object objeto = null;
            var url = CoreResources.UrlBase + CoreResources.Prefix + "/usuarios/login";

            try
            {

                using (var client = new HttpClient())
                {

                    var uri = new Uri(Path.Combine(url));
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync(uri, content).Result;

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string readTask = responseTask.Content.ReadAsStringAsync().Result;
                        objeto = JsonConvert.DeserializeObject<T>(readTask.ToString());

                    }
                    else
                    {

                        objeto = null;

                    }
                }


                return objeto;

            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return objeto;
            }

        }


        public static List<T> GetById<T>(string id)
        {

            try
            {
                url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}/{id}";
                uri = new Uri(url);
                client = new HttpClient();
                responseMessage = client.GetAsync(uri).Result;
                List<T> list = new List<T>();
                var o = (T)SetResponse<T>();
                list.Add(o);
                return list;

            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return null;
            }


        }

        public static List<T> GetCurrentRow<T>(string id)
        {

            try
            {

                url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}/getcurrentrow/{id}";
                uri = new Uri(url);
                client = new HttpClient();
                var json = JsonConvert.SerializeObject(id);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                responseMessage = client.PostAsync(uri, data).Result;
                var list = (List<T>)SetResponse<T>();
                return list;
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return null;
            }

        }



        public static List<T> Insert<T>(T model)
        {
            try
            {
                SetStringContent<T>(model);
                responseMessage = client.PostAsync(uri, data).Result;
                List<T> lst = new List<T>();
                lst.Add((T)SetResponse<T>());
                return lst;
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return null;
            }

        }


        public static List<T> Update<T>(T model)
        {

            try
            {
                SetStringContent<T>(model);
                responseMessage = client.PutAsync(uri, data).Result;
                List<T> lst = new List<T>();
                lst.Add((T)SetResponse<T>());
                return lst;
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return null;
            }

        }


        public static void Delete<T>(string id)
        {
            try
            {
                url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}{id}";
                uri = new Uri(url);
                client = new HttpClient();
                responseMessage = client.DeleteAsync(uri).Result;
                SetResponse();
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
            }

        }


    }

}




