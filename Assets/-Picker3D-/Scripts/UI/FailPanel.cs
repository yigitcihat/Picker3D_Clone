using _Picker3D_.Scripts.Managers;
using UnityEngine.SceneManagement;

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
            var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(activeSceneIndex);
        }

    }
}
