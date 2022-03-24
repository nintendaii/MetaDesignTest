using System;
using Module.Core.MVC;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Module.App.Scripts
{
    public class UnitPreset: ComponentControllerBase<ModelBase, PresetView>
    {
        [Inject] private readonly ModelEditorController modelEditorController;
        [Inject] private readonly SaveLoadModelController saveLoadModelController;

        private Preset presetData;
        public void SetUp(Preset preset)
        {
            presetData = preset;
            View.title.text = preset.Title;
            View.sleevesImage.color = preset.SleevesColor;
            View.bodyImage.color = preset.BodyColor;
        }

        private void OnEnable()
        {
            View.button.onClick.AddListener(OnPresetPress);
        }

        private void OnDisable()
        {
            View.button.onClick.RemoveListener(OnPresetPress);
        }

        private void OnPresetPress()
        {
            saveLoadModelController.SetLoadScreenVisibility(false);
            modelEditorController.SetUpMaterials(presetData.SleevesColor, presetData.BodyColor);
        }
    }
    
    [Serializable]
    public class PresetView : ViewBase
    {
        public TMP_Text title;
        public Button button;
        public Image sleevesImage;
        public Image bodyImage;
    }
    
    public class UnitPresetsFactory: PlaceholderFactory<UnitPreset>{}
}