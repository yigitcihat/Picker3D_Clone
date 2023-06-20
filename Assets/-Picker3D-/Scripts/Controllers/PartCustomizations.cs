using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Picker3D_.Scripts.Controllers
{
    
    [System.Serializable]
    public class PartCustomizations
    {
        [SerializeField] private Collectable objectType;

        [FormerlySerializedAs("Position")]
        [LabelText("Position")]
        [HorizontalGroup("Split", 500), EnumToggleButtons, HideLabel]
        public PositionType position;

      
        [ShowInInspector]
        [LabelText("ObjectGroupDesign")]
        [TableMatrix(HorizontalTitle = "Column", VerticalTitle = "Row", ResizableColumns = false)]
        private bool[,] _objectGroup= new bool[5, 5];

        
    }

    public enum PositionType
    {
        None,
        Left,
        Center,
        Right
    } 
    public enum ObjectType
    { 
        None,
        Square,
        Ball,
        Pyramit,
        Torus
        
    }
    
}
