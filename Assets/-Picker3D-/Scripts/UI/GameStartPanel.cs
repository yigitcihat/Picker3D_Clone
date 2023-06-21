using _Picker3D_.Scripts.Managers;

namespace _Picker3D_.Scripts.UI
{
    public class GameStartPanel : Picker3dPanel
    {
        private void OnEnable()
        {
            ShowPanel();
            // LevelManager.Instance.onLevelStart.AddListener(HidePanel);
        }

        private void OnDisable()
        {

            // LevelManager.Instance.onLevelStart.RemoveListener(HidePanel);
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
            HidePanel();
        }
    }
}
