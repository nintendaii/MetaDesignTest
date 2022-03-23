using System;
using BestHTTP;

namespace Module.App.Scripts.Server
{
    public static class ServerAPI
    {
        private const string BaseUrl = "https://api.fifth.app/";
        private const string UserEndpoint = "users/";
        private const string Email="email/";
        private const string Username = "username/";

        public static void GetUsername(string username, Action<string, bool> callback)
        {
            var request = new HTTPRequest(new Uri($"{BaseUrl}{UserEndpoint}{Username}?username={username}"), HTTPMethods.Get,
                (req, res) =>
                {
                    callback?.Invoke(res.DataAsText, res.IsSuccess);
                });
            request.Send();
        }
        
        public static void GetEmail(string email, Action<string, bool> callback)
        {
            var request = new HTTPRequest(new Uri($"{BaseUrl}{UserEndpoint}{Email}?email={email}"), HTTPMethods.Get,
                (req, res) =>
                {
                    callback?.Invoke(res.DataAsText, res.IsSuccess);
                });
            request.Send();
        }
        
    }
}