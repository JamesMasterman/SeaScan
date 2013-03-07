using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Diagnostics;
using System.Web;


namespace SeaScanUAV
{
    public class WebServiceConsumer
    {
        protected HttpWebRequest  request;
        protected HttpWebResponse response;

        public string BaseURL{get; set;}
        public string UserName { get; set; }
        public string Password { get; set; }

        protected static WebServiceConsumer instance = null;

        public static WebServiceConsumer GetInstance()
        {
            if (instance == null)
            {
                instance = new WebServiceConsumer();
            }

            return instance;
        }

        protected WebServiceConsumer()
        {
            this.BaseURL = Properties.Settings.Default.webservice;
            this.UserName = Properties.Settings.Default.username;
            this.Password = Properties.Settings.Default.password;
        }

        public List<Location> GetLocations(string id, bool inuse, bool targetDetected)
        {
            List<Location> locations = new List<Location>();
            Location[] aLocations = null;
            string url = BaseURL + "/locations/" + UserName + "/" + Password + "/" + id + "/" + inuse + "/" + targetDetected;
            DataContractJsonSerializer deserialiser = new DataContractJsonSerializer(typeof(Location[]));

            try
            {
                string responseText = GetWebServiceData(url);

                if (responseText != null)
                {
                    System.Console.Write(responseText);
                    MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseText));
                    aLocations = deserialiser.ReadObject(ms) as Location[];
                    ms.Close();
                }

                locations = aLocations.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
            return locations;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            User[] aUsers = null;
            string url = BaseURL + "/pilot/" + UserName + "/" + Password;
            DataContractJsonSerializer deserialiser = new DataContractJsonSerializer(typeof(User[]));

            try
            {
                string responseText = GetWebServiceData(url);

                if (responseText != null)
                {
                    System.Console.Write(responseText);
                    MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseText));
                    aUsers = deserialiser.ReadObject(ms) as User[];
                    ms.Close();
                }

                users = aUsers.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
            return users;
        }

        public List<Camera> GetCameras()
        {
            List<Camera> cameras = new List<Camera>();
            Camera[] aCameras = null;
            string url = BaseURL + "/cameras/" + UserName + "/" + Password;
            DataContractJsonSerializer deserialiser = new DataContractJsonSerializer(typeof(Camera[]));

            try
            {
                string responseText = GetWebServiceData(url);

                if (responseText != null)
                {
                    System.Console.Write(responseText);
                    MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseText));
                    aCameras = deserialiser.ReadObject(ms) as Camera[];
                    ms.Close();
                }

                cameras = aCameras.ToList();
            }
            catch (Exception e)
            {
                throw;
            }

            return cameras;
        }

        public List<Airframe> GetAirframes()
        {
            List<Airframe> planes = new List<Airframe>();
            Airframe[] aPlanes = null;
            string url = BaseURL + "/airframes/" + UserName + "/" + Password;
            DataContractJsonSerializer deserialiser = new DataContractJsonSerializer(typeof(Airframe[]));

            try
            {
                string responseText = GetWebServiceData(url);

                if (responseText != null)
                {
                    System.Console.Write(responseText);
                    MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseText));
                    aPlanes = deserialiser.ReadObject(ms) as Airframe[];
                    ms.Close();
                }

                planes = aPlanes.ToList();
            }
            catch (Exception e)
            {
                throw;
            }

            return planes;
        }

        public List<TargetType> GetTargetTypes()
        {
            List<TargetType> targets = new List<TargetType>();
            TargetType[] aTargets = null;
            string url = BaseURL + "/targettypes/" + UserName + "/" + Password;
            DataContractJsonSerializer deserialiser = new DataContractJsonSerializer(typeof(TargetType[]));

            try
            {
                string responseText = GetWebServiceData(url);

                if (responseText != null)
                {
                    System.Console.Write(responseText);
                    MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseText));
                    aTargets = deserialiser.ReadObject(ms) as TargetType[];
                    ms.Close();
                }

                targets = aTargets.ToList();
            }
            catch (Exception e)
            {
                throw;
            }

            return targets;
        }
        
        public HttpStatusCode PostMission(Mission mission, ref string responseInfo)
        {
            HttpStatusCode result = HttpStatusCode.OK;
          
            string url = BaseURL + "/mission/new";
            responseInfo = "";

            //get mission as json string ready for post.
            string jsonEncodedMission = JsonEncode(mission);

            //url encoding is required to make the post work.
            string postDataAsString = "username=" + HttpUtility.UrlEncode(UserName) + "&password=" + HttpUtility.UrlEncode(Password) + "&mission=" + HttpUtility.UrlEncode(jsonEncodedMission);
            byte[] postData = Encoding.UTF8.GetBytes(postDataAsString);
            
            result = PostWebServiceData(url, postData, ref responseInfo);

            if (result != null && result == HttpStatusCode.OK) //get the mission id
            {               
               
                Mission[] aMissions = null;           
                DataContractJsonSerializer deserialiser = new DataContractJsonSerializer(typeof(Mission[]));

                try
                {                    
                    if (responseInfo != null)
                    {
                        System.Console.Write(responseInfo);
                        MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseInfo));
                        aMissions = deserialiser.ReadObject(ms) as Mission[];
                        ms.Close();
                    }                 
                        
                }
                catch (Exception e)
                {
                    throw;
                }

                if (aMissions != null && aMissions.Length > 0)
                {
                    if (aMissions[0] != null)
                    {
                        mission.ID = aMissions[0].ID;
                    }
                }
            
            }

            return result;
        }

        protected string JsonEncode(object obj)
        {
            string jsonEncodedObject = "";
            try
            {
                //get mission as json string ready for post.
                DataContractJsonSerializer serialiser = new DataContractJsonSerializer(obj.GetType());

                MemoryStream jsonStream = new MemoryStream();
                serialiser.WriteObject(jsonStream, obj);

                jsonStream.Position = 0;
                using (StreamReader jsonStringConverter = new StreamReader(jsonStream))
                {
                    jsonEncodedObject = jsonStringConverter.ReadToEnd();
                    jsonStringConverter.Close();

                }
                //clean up
                jsonStream.Close();
                jsonStream.Dispose();

            }
            catch (Exception e)
            {
                jsonEncodedObject = "";
            }

            return jsonEncodedObject;
        }

        protected HttpStatusCode PostWebServiceData(string url, byte[] postData, ref string responseInfo)
        {
            HttpStatusCode result = HttpStatusCode.OK;
            try
            {
                response = null;
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(postData, 0, postData.Length);                  
                }                 

                response = request.GetResponse() as HttpWebResponse;
                if (request.HaveResponse == true && response == null)
                {
                    String msg = "response was not returned or is null";
                    result = HttpStatusCode.BadRequest;
                    throw new WebException(msg);
                }

                result = response.StatusCode;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    String msg = "response with status: " + response.StatusCode + " " + response.StatusDescription;
                    result = response.StatusCode;
                    throw new WebException(msg);
                }

                // get the response content
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                responseInfo = reader.ReadToEnd();

                Debug.WriteLine(responseInfo);
                reader.Close();
                reader.Dispose();               
                
            }
            catch (WebException e)
            {

                System.Console.Write(e.Message);
                if (e.Response != null)
                {
                    response = (HttpWebResponse)e.Response;
                }
                else
                {
                    System.Console.Write(e.Message);
                }


                result = HttpStatusCode.InternalServerError;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();                    
                }
            }

            return result;
        }

        protected string GetWebServiceData(string url)
        {            
            request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            string responseText = null;           
            
            try
            {
                response = request.GetResponse() as HttpWebResponse;

                if (request.HaveResponse == true && response == null)
                {
                    String msg = "response was not returned or is null";
                    throw new WebException(msg);
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    String msg = "response with status: " + response.StatusCode + " " + response.StatusDescription;
                    throw new WebException(msg);
                }

                // check response headers for the content type
                string contentType = response.GetResponseHeader("Content-Type");

                // get the response content
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                responseText = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                // handle failures
            }
            catch (WebException e)
            {

                System.Console.Write(response.StatusCode + " " + response.StatusDescription);
                if (e.Response != null)
                {
                    response = (HttpWebResponse)e.Response;
                }
                else
                {

                    System.Console.Write(e.Message);
                }

            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return responseText;

        }
    }
}
