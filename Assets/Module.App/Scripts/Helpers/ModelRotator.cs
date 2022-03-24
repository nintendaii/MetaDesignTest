using System;
using DG.Tweening;
using UnityEngine;

namespace Module.App.Scripts.Helpers
{
    public class ModelRotator: MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(new Vector3(0, 360, 0), 10f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}