using _Picker3D_.Scripts.Managers;

namespace _Picker3D_.Scripts.UI
{
    public class InGamePanel : Picker3dPanel
    {
        private void OnEnable()
        {
            GameManager.Instance.OnGameStart.AddListener(ShowPanel);
        }
        
    }
}
