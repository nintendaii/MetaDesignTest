using System;
using Module.App.Scripts.Server;
using Module.App.Scripts.Server.Schemas;
using Module.Core.MVC;
using Newtonsoft.Json;
using UnityEngine;

namespace Module.App.Scripts
{
    public class AuthController: ComponentControllerBase<ModelBase, AuthView>,IBindComponent
    {
        private void Awake()
        {
            //On GetEmail button click
            ServerAPI.GetEmail("nikolaygonza@gmail.com", GetEmailCallback);
            //On GetUsername button click
            ServerAPI.GetUsername("nikolaygonza", GetUsernameCallback);
        }

        private void GetEmailCallback(string data, bool isSuccess)
        {
            try
            {
                var responseSchema = JsonConvert.DeserializeObject<AuthResponseSchema>(data);
                //Update email view
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        
        private void GetUsernameCallback(string data, bool isSuccess)
        {
            try
            {
                var responseSchema = JsonConvert.DeserializeObject<AuthResponseSchema>(data);
                //Update username view
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
    
    public class AuthView : ViewBase
    {
    }
}