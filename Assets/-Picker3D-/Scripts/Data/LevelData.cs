using System.Collections.Generic;
using System.Threading;
using _Picker3D_.Scripts.Controllers;
 using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Picker3D_.Scripts.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Data/Level Data")]
    [ShowOdinSerializedPropertiesInInspector]
    public class LevelData : ScriptableObject
    {
        [SerializeField] internal GameObject partPlatform;
        [SerializeField] internal Color groundColor, borderColor, pickerColor, containerColor;
        [SerializeField]
        [SerializeReference]
        [ListDrawerSettings(Expanded = true)]
        internal List<PartCustomizations> partGroup = new List<PartCustomizations>() { new PartCustomizations(), new PartCustomizations(), new PartCustomizations() };
        
        public List<PartCustomizations> PartGroup => partGroup;

        private void OnEnable()
        {
            foreach (var partCustomizations in partGroup)
            {
                partCustomizations.ForwardStartPosLimit = 15;
                foreach (var part in partCustomizations.PartGroup)
                {
                    part.levelData = this;
                    part.OnLoad();
                }
            }
        }
        
    }
    
}