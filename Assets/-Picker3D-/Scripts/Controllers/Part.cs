using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Picker3D_.Scripts.Controllers
{
    [System.Serializable]
    public class Part
    {
        // [FormerlySerializedAs("Position")]
        // [LabelText("Position")]
        // [HorizontalGroup("Split", 500), EnumToggleButtons, HideLabel]
        // public PositionType position;

        [ShowInInspector]
        [LabelText("ObjectGroupDesign")]
        [ListDrawerSettings(Expanded = true)]
        [TableMatrix(HorizontalTitle = "Column", VerticalTitle = "Row", ResizableColumns = false)]
        public bool[,] _objectGroup = new bool[8, 8];
    }
    public enum PositionType
    {
        None,
        Left,
        Center,
        Right
    }
}