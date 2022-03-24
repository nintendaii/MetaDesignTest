using System;
using Module.Core.MVC;
using TMPro;
using UnityEngine.UI;

namespace Module.App.Scripts
{
    public class PopUpController: ComponentControllerBase<ModelBase, PopUpView>, IBindComponentCreativeMode
    {
        private void Awake()
        {
            HideComponent();
        }

        private void OnEnable()
        {
            View.cancelButton.onClick.AddListener(HideComponent);
        }

        private void OnDisable()
        {
            View.cancelButton.onClick.RemoveListener(HideComponent);
            View.submitButton.onClick.RemoveAllListeners();
        }

        public void Init(string contentText, Action<PopUpSubmitData> onSubmitPress, bool isCancelButton = true, bool isInputField = true)
        {
            ShowComponent();
            View.contentText.text = contentText;
            View.cancelButton.gameObject.SetActive(isCancelButton);
            View.inputField.gameObject.SetActive(isInputField);
            View.submitButton.onClick.AddListener(delegate
            {
                onSubmitPress?.Invoke(new PopUpSubmitData
                {
                    IsInputField = isInputField,
                    InputFieldData = View.inputField.text
                });
            });
        }

        public void SetAlert() =>
            View.inputField.text = "Preset already exists. Try another name";
    }

    [Serializable]
    public class PopUpView : ViewBase
    {
        public TMP_Text contentText;
        public TMP_InputField inputField;
        public Button submitButton;
        public Button cancelButton;
    }

    public class PopUpSubmitData
    {
        public bool IsInputField;
        public string InputFieldData;
    }
}