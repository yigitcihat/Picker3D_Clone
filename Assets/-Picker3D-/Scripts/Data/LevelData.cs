using System;
using System.Collections.Generic;
using _Picker3D_.Scripts.Controllers;
 using Sirenix.OdinInspector;
using UnityEngine;

namespace _Picker3D_.Scripts.Data
{
    [CreateAssetMenu(fileName = "Level",menuName = "Data/Level Data")]
    [ShowOdinSerializedPropertiesInInspector]
    public class LevelData : ScriptableObject
    {
        public int containerSuccessSize;
        
        [ListDrawerSettings(Expanded = true)]
        public List<PartCustomizations> part = new List<PartCustomizations>();

        
    //     [BoxGroup("Parts")]
    // [HorizontalGroup("Split", 290), EnumToggleButtons, HideLabel]
    // public PartCustomizations Part;

    // [TableMatrix(HorizontalTitle = "Column", VerticalTitle = "Row")]
    // public ObjectType[,] ObjectGroupDesign = new ObjectType[8, 50];

    // [HorizontalGroup("Split", 55, LabelWidth = 70)]
    // [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
    // public Texture Icon;
    //
    // [VerticalGroup("Split/Meta")]
    // public string Name;
    //
    // [VerticalGroup("Split/Meta")]
    // public string Surname;
    //
    // [VerticalGroup("Split/Meta"), Range(0, 100)]
    // public int Age;
    //
    // [HorizontalGroup("Split", 290), EnumToggleButtons, HideLabel]
    // public CharacterAlignment CharacterAlignment;
   
    
    // [TabGroup("Starting Stats"), HideLabel]
    // public CharacterStats Skills = new CharacterStats();
    //
    // [HideLabel]
    // [TabGroup("Starting Equipment")]
    // public List<CharacterEquipment> StartingEquipment;


    }
}