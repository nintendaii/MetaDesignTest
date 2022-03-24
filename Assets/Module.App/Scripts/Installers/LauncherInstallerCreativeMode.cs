using BestHTTP;
using UnityEngine;

namespace Module.App.Scripts
{
    public class LauncherInstallerCreativeMode: Core.Launchers.LauncherInstaller
    {
        [SerializeField] private UnitPreset unitPresetPrefab;
        [SerializeField] private Transform placeToSpawn;
        
        protected override void InstallComponents()
        {
            RegisterComponents<IBindComponentCreativeMode>();
        }

        protected override void InstallFactory()
        {
            Container.BindFactory<UnitPreset, UnitPresetsFactory>().FromComponentInNewPrefab(unitPresetPrefab)
                .UnderTransform(placeToSpawn);
        }
    }
}