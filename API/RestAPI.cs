using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AdobeSignatureV6.Constant;

namespace AdobeSignatureV6
{
    public class RestAPI
    {
        private string APIURL = GlobalVariables.APIURL;
        private string BASEAPIURL = GlobalVariables.BASEAPIURL;
        private string AccessToken = GlobalVariables.AccessToken;

        public RestAPI(string apiURL, string accessToken)
        {
            APIURL = apiURL.TrimEnd('/') + "/";
            AccessToken = accessToken;
        }


        public async Task<string> GetRestJson(string endpoint, string contentType = "application/json")
        {
            HttpResponseMessage response = await GetResponseMessage(endpoint, contentType);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(await GetError(response));
            }
        }


        public async Task<byte[]> GetRestBytes(string endpoint, string contentType = "*/*")
        {
            HttpResponseMessage response = await GetResponseMessage(endpoint, contentType);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                throw new Exception(await GetError(response));
            }
        }

        public async Task<string> PostRest(string endpoint, HttpContent data, string contentType = "application/json")
        {
            HttpResponseMessage response;

            response = await PostResponseMessage(endpoint, data, contentType);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(await GetError(response));
            }
        }

        public async Task<string> PutRest(string endpoint, HttpContent data, string contentType = "application/json")
        {
            HttpResponseMessage response;

            response = await PutResponseMessage(endpoint, data, contentType);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(await GetError(response));
            }
        }


        #region Private Methods

        private async Task<string> GetError(HttpResponseMessage response)
        {
            var errorString = await response.Content.ReadAsStringAsync();
            var errorCode = DeserializeJSon<AdobeSignatureV6.ErrorCode>(errorString);

            return errorCode.code + errorCode.error + System.Environment.NewLine + errorCode.message + errorCode.error_description;
        }


        private async Task<HttpResponseMessage> GetResponseMessage(string endpoint, string contentType)
        {
            endpoint = endpoint.TrimStart('/');
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APIURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            //client.DefaultRequestHeaders.Add("Access-Token", AccessToken);
            //client.DefaultRequestHeaders.Add("Toekn-Type", "Bearer");

            return await client.GetAsync(endpoint);
        }


        private async Task<HttpResponseMessage> PostResponseMessage(string endpoint, HttpContent contents, string contentType)
        {
            endpoint = endpoint.TrimStart('/');
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APIURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",AccessToken);
            //client.DefaultRequestHeaders.Add("Access-Token", AccessToken);
            //client.DefaultRequestHeaders.Add("Toekn-Type", "Bearer");

            return await client.PostAsync(endpoint, contents);
            
        }

        private async Task<HttpResponseMessage> PutResponseMessage(string endpoint, HttpContent contents, string contentType)
        {
            endpoint = endpoint.TrimStart('/');
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APIURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            //client.DefaultRequestHeaders.Add("Access-Token", AccessToken);
            //client.DefaultRequestHeaders.Add("Toekn-Type", "Bearer");

            return await client.PutAsync(endpoint, contents);

        }

        #endregion Private Methods


        #region JSON Methods

        internal string SerializeJSon<T>(T t)
        {
            string jsonString = string.Empty;

            DataContractJsonSerializerSettings a = new DataContractJsonSerializerSettings();


            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings()
            {

                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ss-f:ff"),
                UseSimpleDictionaryFormat = true,

            };

            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T), settings);
                ds.WriteObject(stream, t);
                byte[] data = stream.ToArray();
                jsonString = Encoding.UTF8.GetString(data, 0, data.Length);
            }

            return jsonString;
        }

        internal T DeserializeJSon<T>(string jsonString)
        {
            T obj;
            //dynamic dT = typeof(T);

            //if (dT.Name.EndsWith("List"))
            //    dT = dT.DeclaredProperties[0].PropertyType.GenericTypeArguments[0];

            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings()
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ss-f:ff"),
                UseSimpleDictionaryFormat = true
            };

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), settings);
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                obj = (T)ser.ReadObject(stream);
            }

            return obj;
        }

        #endregion JSON Methods




    }

}
