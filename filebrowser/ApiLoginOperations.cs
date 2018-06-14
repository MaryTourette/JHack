/**
*
* This class handles all WebAPI connections to the login, regestration, get user details, store user details including JASON Web Tokens 
* 
* ApiLoginOperations() is the constructor of the class and save the base URL to the server
         * @param string baseUrl
*
* AuthenticateUser() authenticate the user how try to login with the WebAPI. The methode connect to the sserver and checks the user informations here.
* if the login name and password correct the client recives a message and a JASON Web Token.
         * @param string username
         * @param string password
         * @return user packed in a JSON DeserializeObject
         * @return null if user was not found
* 
* GetUserDetails() Get user details from the WebAPI including the JASON WebToken
         * @param  User Model
         * @param  username
         * @param  password
         * @return user if user exists
         * @return null if user not exists
* 
*RegisterUser() register a user over the WebAPI with th credentials: username, password and email 
         * @param  string username
         * @param  string password
         * @param  string eMail
         * @return user if regestration was successful
         * @return null if regestration wans't successful
* 
**/

using System;
using System.Net;

//external libary 
using Newtonsoft.Json;

namespace filebrowser
{
    class ApiLoginOperations
    {
        //Base url
        private string baseUrl;

        public ApiLoginOperations()
        {
            //ToDo: insert base URL of server
            this.baseUrl = "http://localhost:5000/api";
        }

        //Authenticate user with Web Api Endpoint
        public User AuthenticateUser(string username, string password)
        {
            //ToDo: insert path to endpoint
            string endpoint = this.baseUrl + "/users/login";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);

                //ToDo Logging
            }
            catch (Exception)
            {
                //ToDo Logging
                return null;
            }
        }

        //Get user details from WebAPI including jwt
        public User GetUserDetails(User user)
        {
            string endpoint = this.baseUrl + "/users/" + user.Id;
            string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = access_token;
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                //ToDo Logging
                return user;
            }
            catch (Exception)
            {
                //ToDo Logging
                return null;
            }
        }

        //Register user over WebAPI
        public User RegisterUser(string username, string password, string eMail)
        {
            string endpoint = this.baseUrl + "/users";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password,
                eMail = eMail,
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
                //ToDo Logging
            }
            catch (Exception)
            {
                //ToDo Logging
                return null;
            }
        }
    }
}
