using _Picker3D_.Scripts.Managers;
using UnityEngine;

namespace _Picker3D_.Scripts.UI
{
    public class FailPanel :  Picker3dPanel
    {
        private void OnEnable()
        {
          GameManager.Instance.OnGameFail.AddListener(ShowPanel);
        }

        public void RestartLevel()
        {
            // var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //
            // SceneManager.LoadScene(activeSceneIndex);
            EventManager.OnLevelRestart.Invoke();
            LevelManager.Instance.RestartLevel();
            var transform1 = PlayerController.Instance.transform;
            var position = transform1.position;
            position -=
                (position - new Vector3(0, 0, +5));
            transform1.position = position;
            HidePanel();
        }

    }
}
