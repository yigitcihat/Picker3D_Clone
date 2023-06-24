using System.Collections.Generic;
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


        private float _leftPosLimit = -3.5f;
        

        public void InstantiateObjects()
        {
            for (var index = 0; index < partGroup.Count; index++)
            {
                var t = partGroup[index];
                for (var i = t.Transposed.GetLength(0) - 1; i >= 0; i--)
                {
                    _leftPosLimit = -3.5f;
                    for (var j = 0; j <t.Transposed.GetLength(1); j++)
                    {
                        if (t.Transposed[i, j].Equals(true))
                        {
                            var poolObject = PoolingSystem.Instance.InstantiateAPS(t.ObjectType.ToString());
                            poolObject.transform.position = new Vector3(_leftPosLimit, 0.25f,
                                GameManager.Instance.ForwardStartPosLimit);
                        }

                        _leftPosLimit += 0.5f;
                    }

                    GameManager.Instance.ForwardStartPosLimit += 0.5f;
                }

                GameManager.Instance.ForwardStartPosLimit += 35;
            }
        }
        
    }

}
