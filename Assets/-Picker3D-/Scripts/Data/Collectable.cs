using System;
using _Picker3D_.Scripts.Managers;
using UnityEngine;

namespace _Picker3D_.Scripts.Data
{
    public class Collectable : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.layer = LayerMask.NameToLayer("Collecteable");
            EventManager.OnLevelRestart.AddListener(()=>PoolingSystem.Instance.DestroyAPS(gameObject));
        }
    }
}
