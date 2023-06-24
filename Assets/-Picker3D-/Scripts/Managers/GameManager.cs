using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Picker3D_.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {

        [HideInInspector]
        public UnityEvent OnGameStart = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnGameFail = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnStageSuccess = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnStageFail = new UnityEvent();

        [ReadOnly]
        [ShowInInspector]
        public bool IsStageCompleted { get; set; }

        [ShowInInspector] public bool isGameStarted;
        [HideInInspector]internal float ForwardStartPosLimit = 15;
        public void StartGame()
        {
            if (isGameStarted)
                return;

            isGameStarted = true;
            OnGameStart.Invoke();
            
        }

        public void EndGame()
        {
            if (!isGameStarted)
                return;
            isGameStarted = false;
            OnGameFail.Invoke();
        }

        /// <summary>
        /// Call it when the player wins or loses the game
        /// </summary>
        /// <param name="value"></param>
        [Button]
        public void CompeleteStage(bool value)
        {
            if (IsStageCompleted == true)
                return;

            if (value)
                OnStageSuccess.Invoke();
            else OnStageFail.Invoke();

            IsStageCompleted = true;
        }

        // private void OnApplicationPause(bool pause)
        // {
        //     if (pause)
        //     {                
        //         SaveLoadManager.SavePDP(playerData, SavedFileNameHolder.PlayerData);
        //     }
        // }
        //
        // private void OnApplicationQuit()
        // {            
        //     SaveLoadManager.SavePDP(playerData, SavedFileNameHolder.PlayerData);
    }
}        
    
