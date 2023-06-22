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
        private List<Part> partGroup = new List<Part>() { new Part(), new Part(), new Part() };

        public List<Part> PartGroup => partGroup;

        private float _leftPosLimit = -4;
        internal float ForwardStartPosLimit = 15;
        
        public void InstantiateObjects()
        {
            foreach (var t in PartGroup)
            {
                for (var i = 0; i < t.Transposed.GetLength(0); i++)
                {
                    _leftPosLimit = -4.5f;
                    for (var j = 0; j <  t.Transposed.GetLength(1); j++)
                    {
                        if ( t.Transposed[i,j].Equals(true))
                        {
                            var poolObject = PoolingSystem.Instance.InstantiateAPS(objectType.gameObject.name);
                            poolObject.transform.position = new Vector3(_leftPosLimit, 0.3f, ForwardStartPosLimit);
                   
                        }
                        _leftPosLimit += 0.6f;
                    }
                    ForwardStartPosLimit += 0.6f;
                }

                ForwardStartPosLimit += 30;
            }
        }
        
    }

}
