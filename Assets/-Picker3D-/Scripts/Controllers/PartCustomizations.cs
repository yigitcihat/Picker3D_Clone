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
        
        [SerializeField]
        [ListDrawerSettings(Expanded = true)]
        internal List<Part> partGroup = new List<Part>() { new Part(), new Part(), new Part() };


        private float _leftPosLimit = -4;
        internal float ForwardStartPosLimit = 15;

        public void InstantiateObjects()
        {
            foreach (var t in partGroup)
            {
                for (var i = 0; i < t.Transposed.GetLength(0); i++)
                {
                    _leftPosLimit = -4.5f;
                    for (var j = t.Transposed.GetLength(1)-2; j >= 0; j--)
                    {
                        if ( t.Transposed[i,j].Equals(true))
                        {
                            var poolObject = PoolingSystem.Instance.InstantiateAPS(t.ObjectType.gameObject.name);
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
