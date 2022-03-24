using System;
using Module.Core.MVC;
using UnityEngine.Pool;
using Zenject;

namespace Module.App.Scripts
{
    public class PresetsFactoryController: ComponentControllerBase, IBindComponentCreativeMode
    {
        [Inject] private readonly UnitPresetsFactory unitPresetsFactory;

        private IObjectPool<UnitPreset> objectPool;

        private void Awake()
        {
            objectPool =
                new LinkedPool<UnitPreset>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyObject);
        }

        public UnitPreset CreatePreset(Preset presetData)
        {
            var unit = objectPool.Get();
            unit.SetUp(presetData);
            return unit;
        }

        public void ReleasePreset(UnitPreset unit)
        {
            objectPool.Release(unit);
        }

        private void OnDestroyObject(UnitPreset unit) => Destroy(unit);

        private void OnReturnToPool(UnitPreset unit) => unit.HideComponent();

        private void OnTakeFromPool(UnitPreset unit) => unit.ShowComponent();

        private UnitPreset CreatePooledItem() => unitPresetsFactory.Create();
    }
}