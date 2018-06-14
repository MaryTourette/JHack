/**
 * 
 * This class store user details including the JASON Web Token
 * 
 **/

namespace filebrowser
{
    //Class of users with details and jwt token
    class User
    {
        public string Username { get; set; }
        public string eMail { get; set; }
        public string access_token { get; set; }
    }
}
