using _Picker3D_.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Picker3D_.Scripts.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Data/Level Data")]
    [ShowOdinSerializedPropertiesInInspector]
    public class LevelData : ScriptableObject
    {
        [SerializeField] internal GameObject partPlatform;
        [SerializeField] internal Color groundColor, borderColor, pickerColor, containerColor, containerGroundColor;

        [SerializeField] [SerializeReference] [ListDrawerSettings(Expanded = true)]
        internal PartCustomizations partGroupCustomization;

        public PartCustomizations PartGroupCustomization => partGroupCustomization;

        private void OnEnable()
        {
            GameManager.Instance.ForwardStartPosLimit = 15;
            foreach (var part in partGroupCustomization.partGroup)
            {
                part.levelData = this;
                part.OnLoad();
            }
        }
    }
}

