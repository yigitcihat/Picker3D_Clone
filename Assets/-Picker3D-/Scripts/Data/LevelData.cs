using System.Collections.Generic;
using _Picker3D_.Scripts.Controllers;
 using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Picker3D_.Scripts.Data
{
    [CreateAssetMenu(fileName = "Level",menuName = "Data/Level Data")]
    [ShowOdinSerializedPropertiesInInspector]
    public class LevelData : ScriptableObject
    {
        [SerializeField] internal GameObject partPlatform;
        [SerializeField] internal Color groundColor, borderColor, pickerColor, containerColor;
        [SerializeField]
        [SerializeReference]
        [ListDrawerSettings(Expanded = true)]
        internal List<PartCustomizations> partGroup = new List<PartCustomizations>();
        
    }
    // public enum LevelType { Default, Tutorial }
    //
    // [System.Serializable]
    // public class Level
    // {
    //     public Level(LevelData _levelData)
    //     {
    //         levelData = _levelData;
    //     }
    //
    //     [BoxGroup("LevelType")]
    //     [ListDrawerSettings(AlwaysAddDefaultValue = true)]
    //     public List<LevelType> LevelTypes = new List<LevelType>();
    //
    //     [ValueDropdown("LevelNames")]
    //     public string LoadLevelID;
    //
    //
    //
    //     private LevelData levelData;
    //     
    //
    // }

}