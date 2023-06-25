using System;
using System.Collections.Generic;
using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Picker3D_.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private float platformLength = 60;
        [SerializeField] private PlayerController playerController;
        private float _currentPlatformPos;

        [FormerlySerializedAs("LevelData")] [BoxGroup("Level Data")] [SerializeField]
        public List<LevelData> levels = new List<LevelData>();

        [HideInInspector] public UnityEvent onLevelStart = new UnityEvent();
        [HideInInspector] public UnityEvent onLevelFinish = new UnityEvent();

        public int Level
        {
            get
            {
                var level = PlayerPrefs.GetInt(PlayerPrefKeys.LastLevel, 0);

                return level;
            }
            set => PlayerPrefs.SetInt(PlayerPrefKeys.LastLevel, value);
        }


        public void Start()
        {
            for (var i = Level; i <= Level + 1; i++)
            {
                if (Level >= levels.Count)
                {
                    LoadRandomLevel();
                }
                else
                {
                    LoadLevel(Level);
                }
            }
        }

        public void LoadLevel(int level)
        {
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

                partController.container.requireObjectCount =
                    t.ContainerSuccessSize;
                if (j + 1 == levels[level].partGroupCustomization.partGroup.Count)
                {
                    partController.finalLine.SetActive(true);
                }
            }

            levels[level].partGroupCustomization.InstantiateObjects();

            playerController.SetColor(levels[Level].pickerColor);
        }

        public void LoadRandomLevel()
        {
            int randomLevel = Random.Range(0, levels.Count);
            for (var j = 0; j < levels[randomLevel].partGroupCustomization.partGroup.Count; j++)
            {
                var t = levels[randomLevel].partGroupCustomization.partGroup[j];
                var currentPos = new Vector3(0, 0, _currentPlatformPos);
                var part = Instantiate(levels[randomLevel].partPlatform.gameObject, currentPos,
                    Quaternion.identity);
                part.transform.SetParent(transform, true);
                var partController = part.GetComponent<PartController>();
                partController.SetPartConfig(levels[randomLevel].borderColor,
                    levels[randomLevel].groundColor,
                    levels[randomLevel].containerColor,
                    levels[randomLevel].containerGroundColor);
                _currentPlatformPos += platformLength;

                partController.container.requireObjectCount =
                    t.ContainerSuccessSize;
                if (j + 1 == levels[randomLevel].partGroupCustomization.partGroup.Count)
                {
                    partController.finalLine.SetActive(true);
                }
            }

            levels[Random.Range(0, levels.Count)].partGroupCustomization.InstantiateObjects();

            playerController.SetColor(levels[Random.Range(0, levels.Count)].pickerColor);
        }

        public void RestartLevel()
        {
            GameManager.Instance.ForwardStartPosLimit = 15;
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                Destroy(child.gameObject);
            }
            _currentPlatformPos = 0;
            for (var i = Level; i <= Level + 1; i++)
            {
                if (Level >= levels.Count)
                {
                    LoadRandomLevel();
                }
                else
                {
                    LoadLevel(Level);
                }
            }
            playerController.SetWaitingState(PlayerStates.PlayerState.Moving);
        }
    }
}