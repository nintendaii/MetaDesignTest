using System;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace Module.App.Scripts
{
    public class ModelEditorController: ComponentControllerBase<ModelBase, ModelEditorView>, IBindComponentCreativeMode
    {
        private Material currentEditingMaterial;
        private void OnEnable()
        {
            View.colorPicker.onColorChange.AddListener(OnColorChange);
            View.sleevesPickerButton.onClick.AddListener(EditSleeves);
            View.bodyPickerButton.onClick.AddListener(EditBody);
            View.colorPicker.gameObject.SetActive(false);
            currentEditingMaterial = View.bodyMaterial;
        }

        public EditorData GetEditorData() => new()
        {
            SleevesColor = View.sleevesMaterial.color,
            BodyColor = View.bodyMaterial.color
        };

        public void SetUpMaterials(Color sleevesColor, Color bodyColor)
        {
            View.sleevesMaterial.color = sleevesColor;
            View.bodyMaterial.color = bodyColor;
        }

        private void EditSleeves()
        {
            View.colorPicker.gameObject.SetActive(true);
            currentEditingMaterial = View.sleevesMaterial;
        }

        private void EditBody()
        {
            View.colorPicker.gameObject.SetActive(true);
            currentEditingMaterial = View.bodyMaterial;
        }

        private void OnDisable()
        {
            View.colorPicker.onColorChange.RemoveListener(OnColorChange);
            View.sleevesPickerButton.onClick.RemoveListener(EditSleeves);
            View.bodyPickerButton.onClick.RemoveListener(EditBody);
        }

        private void OnColorChange(Color color) => currentEditingMaterial.color = color;
    }

    [Serializable]
    public class ModelEditorView : ViewBase
    {
        [Header("Buttons")] 
        public Button sleevesPickerButton;
        public Button bodyPickerButton;
        [Header("Editor values")]
        public FlexibleColorPicker colorPicker;
        public Material sleevesMaterial;
        public Material bodyMaterial;
    }

    public class EditorData
    {
        public Color SleevesColor;
        public Color BodyColor;
    }
}