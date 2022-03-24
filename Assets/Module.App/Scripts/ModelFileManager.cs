using System;
using System.Collections.Generic;
using System.IO;
using Module.Core.MVC;
using UnityEngine;

namespace Module.App.Scripts
{
    public class ModelFileManager: ComponentControllerBase, IBindComponentCreativeMode
    {
        private string cachePath;
        private const string presetsJsonFileName = "/Presets.json";
        public PresetsContainer presetsContainer;

        private void Start()
        {
            cachePath = Application.persistentDataPath;
            if (File.Exists(cachePath + presetsJsonFileName))
            {
                ReadPresetsData();
                Debug.LogError("Read");
            }
            else
            {
                Debug.LogError("Fill");
                FillPresetsByDefault();
            }
        }
        
        private void FillPresetsByDefault()
        {
            var list = new List<Preset>();

            presetsContainer = new PresetsContainer
            {
                Container = list
            };
            var presets = JsonUtility.ToJson(presetsContainer, true);
            File.WriteAllText(cachePath + presetsJsonFileName, presets);
        }
        
        private void SavePresets()
        {
            var presets = JsonUtility.ToJson(presetsContainer, true);
            File.WriteAllText(cachePath + presetsJsonFileName, presets);
        }

        public void AddNewPreset(Preset preset)
        {
            presetsContainer.Container.Add(preset);
            SavePresets();
        }

        public bool CheckIfPresetExists(string title) =>
            presetsContainer.Container.Exists((preset => preset.Title == title));

        private void DeleteFile() => File.Delete(cachePath + presetsJsonFileName);

        private void ReadPresetsData()
        {
            var fileData = File.ReadAllText(cachePath + presetsJsonFileName);
            Debug.LogError(fileData);
            presetsContainer = JsonUtility.FromJson<PresetsContainer>(fileData);
        }
    }

    [Serializable]
    public class Preset
    {
        public string Id;
        public string Title;
        public Color BodyColor;
        public Color SleevesColor;
    }

    [Serializable]

    public class PresetsContainer
    {
        public List<Preset> Container;
    }
}