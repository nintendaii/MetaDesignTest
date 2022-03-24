using System;
using System.Collections.Generic;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Module.App.Scripts
{
    public class SaveLoadModelController: ComponentControllerBase<ModelBase, SaveLoadModelView>, IBindComponentCreativeMode
    {
        [Inject] private readonly ModelFileManager modelFileManager;
        [Inject] private readonly ModelEditorController modelEditorController;
        [Inject] private readonly PopUpController popUpController;
        [Inject] private readonly PresetsFactoryController presetsFactoryController;

        private List<UnitPreset> allPresets;

        private void OnEnable()
        {
            View.saveModelButton.onClick.AddListener(SaveModel);
            View.loadModelButton.onClick.AddListener(LoadModel);
            View.closeButton.onClick.AddListener(delegate { SetLoadScreenVisibility(false); });
        }

        private void OnDisable()
        {
            View.saveModelButton.onClick.RemoveListener(SaveModel);
            View.loadModelButton.onClick.RemoveListener(LoadModel);
            View.closeButton.onClick.RemoveAllListeners();
        }

        private void SaveModel()
        {
            popUpController.Init("Name your preset", data =>
            {
                if (!modelFileManager.CheckIfPresetExists(data.InputFieldData))
                {
                    var editorData = modelEditorController.GetEditorData();
                    var preset = new Preset
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = data.InputFieldData,
                        BodyColor = editorData.BodyColor,
                        SleevesColor = editorData.SleevesColor
                    };
                    modelFileManager.AddNewPreset(preset);
                    popUpController.HideComponent();
                    popUpController.Init("Successfully saved", _ =>
                    {
                        popUpController.HideComponent();
                    }, false, false);
                }
                else
                {
                    popUpController.SetAlert();
                }
            } );
        }

        private void LoadModel()
        {
            SetLoadScreenVisibility(true);
            var presetsList = modelFileManager.presetsContainer.Container;
            allPresets = new List<UnitPreset>();
            foreach (var presetModel in presetsList)
            {
                var unit = presetsFactoryController.CreatePreset(presetModel);
                allPresets.Add(unit);
            }
        }

        public void SetLoadScreenVisibility(bool isActive)
        {
            View.loadScreen.SetActive(isActive);
            if (!isActive)
            {
                foreach (var presetModel in allPresets)
                {
                }

                for (var i = allPresets.Count; i --> 0;)
                {
                    presetsFactoryController.ReleasePreset(allPresets[i]);
                }
            }
        }
    }

    [Serializable]
    public class SaveLoadModelView : ViewBase
    {
        public Button saveModelButton;
        public Button loadModelButton;
        public Button closeButton;
        public GameObject loadScreen;
    }
}