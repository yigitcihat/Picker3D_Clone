using System.Collections.Generic;
using _Picker3D_.Scripts.Controllers;
using _Picker3D_.Scripts.Data;
using _Picker3D_.Scripts.Managers;
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
        

        public void InstantiateObjects()
        {
            foreach (var t in partGroup)
            {
                for (var i = t.Transposed.GetLength(0)-1; i >=0 ; i--)
                {
                    _leftPosLimit = -4.5f;
                    for (var j = t.Transposed.GetLength(1)-2; j >= 0; j--)
                    {
                        if ( t.Transposed[i,j].Equals(true))
                        {
                            var poolObject = PoolingSystem.Instance.InstantiateAPS(t.ObjectType.ToString());
                            poolObject.transform.position = new Vector3(_leftPosLimit, 0.3f,  GameManager.Instance.ForwardStartPosLimit);
                   
                        }
                        _leftPosLimit += 0.6f;
                    }
                    GameManager.Instance.ForwardStartPosLimit += 0.6f;
                }

                GameManager.Instance.ForwardStartPosLimit += 30;
            }
        }
        
    }

}
