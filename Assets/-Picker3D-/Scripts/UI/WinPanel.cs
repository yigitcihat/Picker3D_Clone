using System;
using _Picker3D_.Scripts.Managers;

namespace _Picker3D_.Scripts.UI
{
    public class WinPanel :  Picker3dPanel
    {
        private void OnEnable()
        {
            GameManager.Instance.OnStageSuccess.AddListener(ShowPanel);
        }

    }
}
