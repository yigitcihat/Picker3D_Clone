using System;
using UnityEngine;

namespace _Picker3D_.Scripts.Data
{
    public class Collectable : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.layer = LayerMask.NameToLayer("Collecteable");
        }
    }
}
