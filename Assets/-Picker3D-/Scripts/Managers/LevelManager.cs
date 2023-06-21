using System.Collections.Generic;
using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Picker3D_.Scripts.Managers
{
    public class LevelManager  : Singleton<LevelManager>
    {
        [SerializeField] private float platformLength = 60;
        private float _currentPlatformPos;
        
        [FormerlySerializedAs("LevelData")]
        [BoxGroup("Level Data")]
        [SerializeField]
        public List<LevelData> levels = new List<LevelData>();


        [HideInInspector]
        public UnityEvent onLevelStart = new UnityEvent();
        [HideInInspector]
        public UnityEvent onLevelFinish = new UnityEvent();
        public int LevelIndex
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
        
        public override void Awake()
        {
            base.Awake();
            for (var i = 0; i <= LevelIndex+1; i++)
            {
                var currentPos = new Vector3(0, 0, _currentPlatformPos);
                Instantiate(levels[i].partPlatform.gameObject,
                    currentPos, Quaternion.identity).transform.parent = transform;
                _currentPlatformPos += platformLength;
            }
        }

        private void SetPartObjects(LevelData levelData, int levelIndex )
        {
            for (var i = 0; i < levelData.partGroup.Count; i++)
            {
                
            }
        }
       

       

    }
}

