using System.Collections.Generic;
using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Picker3D_.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private float platformLength = 60;
        [SerializeField] private PlayerController playerController;
        private float _currentPlatformPos;

        [FormerlySerializedAs("LevelData")] [BoxGroup("Level Data")] [SerializeField]
        public List<LevelData> levels = new List<LevelData>();

        private LevelData _currentLevelData;
        private LevelData _nextLevelData;


        [HideInInspector] public UnityEvent onLevelStart = new UnityEvent();
        [HideInInspector] public UnityEvent onLevelFinish = new UnityEvent();

        public int Level
        {
            get
            {
                var level = PlayerPrefs.GetInt(PlayerPrefKeys.LastLevel, 0);
                if (level > levels.Count - 1)
                {
                    level = 0;
                }

                return level;
            }
            set => PlayerPrefs.SetInt(PlayerPrefKeys.LastLevel, value);
        }

        public void Start()
        {
            for (var i = 0; i <= Level + 1; i++)
            {
                LoadLevel(i);
            }
        }

        public void LoadLevel(int level)
        {
            Debug.Log(levels[level].name);
            for (var j = 0; j < levels[level].partGroupCustomization.partGroup.Count; j++)
            {
                var t = levels[level].partGroupCustomization.partGroup[j];
                var currentPos = new Vector3(0, 0, _currentPlatformPos);
                var part = Instantiate(levels[level].partPlatform.gameObject, currentPos, Quaternion.identity);
                part.transform.SetParent(transform, true);
                var partController = part.GetComponent<PartController>();
                partController.SetPartConfig(levels[level].borderColor, levels[level].groundColor,
                    levels[level].containerColor,
                    levels[level].containerGroundColor);
                _currentPlatformPos += platformLength;
                levels[level].partGroupCustomization.InstantiateObjects();
                partController.container.requireObjectCount =
                    t.ContainerSuccessSize;
                if (j + 1 == levels[level].partGroupCustomization.partGroup.Count)
                {
                    partController.finalLine.SetActive(true);
                }
            }


            _currentLevelData = levels[Level];
            _nextLevelData = levels[Level + 1];
            playerController.SetColor(levels[Level].pickerColor);
        }
    }
}