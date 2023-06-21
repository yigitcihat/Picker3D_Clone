using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Picker3D_.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject gameplayScreen;
        [SerializeField] private Image progressBar;
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;

        #endregion
    }
}
