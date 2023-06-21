using System.Collections.Generic;
using _Picker3D_.Scripts.Controllers;
using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    
    [System.Serializable]
    public class PartCustomizations
    {
        public int containerSuccessSize;
        [SerializeField] private Collectable objectType;
        [SerializeField]
        [ListDrawerSettings(Expanded = true)]
        public List<Part> partGroup = new List<Part>();
        
    }

   
    
}
