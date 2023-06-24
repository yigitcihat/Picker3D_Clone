using _Picker3D_.Scripts.Managers;

namespace _Picker3D_.Scripts.UI
{
    public class FailPanel :  Picker3dPanel
    {
        private void OnEnable()
        {
          GameManager.Instance.OnGameFail.AddListener(ShowPanel);
        }


    }
}
