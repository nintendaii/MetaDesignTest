using System;
using Module.App.Scripts.Server;
using Module.App.Scripts.Server.Schemas;
using Module.Core.MVC;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Module.App.Scripts.Helpers;

namespace Module.App.Scripts
{
    public class AuthController: ComponentControllerBase<ModelBase, AuthView>,IBindComponent
    {
        private void OnEnable()
        {
            View.checkEmailButton.onClick.AddListener(CheckEmail);
            View.checkUsernameButton.onClick.AddListener(CheckUsername);
        }

        private void OnDisable()
        {
            View.checkEmailButton.onClick.RemoveListener(CheckEmail);
            View.checkUsernameButton.onClick.RemoveListener(CheckUsername);
        }

        private void CheckEmail()
        {
            var input = View.emailInputField.text;
            if (RegexValidators.ValidateEmail(input))
            {
                ServerAPI.GetEmail(input, GetEmailCallback);
            }
            else
            {
                View.emailStatusText.text = "Email is invalid";
                View.emailStatusText.color = Color.red;
            }
        }
        
        private void CheckUsername()
        {
            var input = View.usernameInputField.text;
            ServerAPI.GetUsername(input, GetUsernameCallback);
        }

        //COMMENT: Technically, methods GetEmailCallback and GetUsernameCCallback could be made as a single method, because
        //their structure is the same. We can just path the reference to the corresponding TMP Texts depending on the 
        //input type (username or email). But I decided to keep these methods separately because the logic for these callbacks
        //could be different in future 
        private void GetEmailCallback(string data, bool isSuccess)
        {
            try
            {
                if (isSuccess)
                {
                    var responseSchema = JsonConvert.DeserializeObject<AuthResponseSchema>(data);
                    if (responseSchema.available)
                    {
                        View.emailStatusText.text = "Available";
                        View.emailStatusText.color = Color.green;
                    }
                    else
                    {
                        View.emailStatusText.text = "Unavailable";
                        View.emailStatusText.color = Color.red;
                    }
                }
                else
                {
                    var errorResponseSchema = JsonConvert.DeserializeObject<ErrorResponseSchema>(data);
                    View.emailStatusText.text = errorResponseSchema.message;
                    View.emailStatusText.color = Color.red;
                }
                
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                View.emailStatusText.text = e.ToString();
                throw;
            }
        }
        
        private void GetUsernameCallback(string data, bool isSuccess)
        {
            try
            {
                if (isSuccess)
                {
                    var responseSchema = JsonConvert.DeserializeObject<AuthResponseSchema>(data);
                    if (responseSchema.available)
                    {
                        View.usernameStatusText.text = "Available";
                        View.usernameStatusText.color = Color.green;
                    }
                    else
                    {
                        View.usernameStatusText.text = "Unavailable";
                        View.usernameStatusText.color = Color.red;
                    }
                }
                else
                {
                    var errorResponseSchema = JsonConvert.DeserializeObject<ErrorResponseSchema>(data);
                    View.usernameStatusText.text = errorResponseSchema.message;
                    View.usernameStatusText.color = Color.red;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                View.emailStatusText.text = e.ToString();
                throw;
            }
        }
    }
    
    [Serializable]
    public class AuthView : ViewBase
    {
        public TMP_InputField emailInputField;
        public Button checkEmailButton;
        public TMP_Text emailStatusText;
        public TMP_InputField usernameInputField;
        public Button checkUsernameButton;
        public TMP_Text usernameStatusText;
    }
}