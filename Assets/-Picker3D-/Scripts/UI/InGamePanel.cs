using System.Collections;
using System.Collections.Generic;
using _Picker3D_.Scripts.Managers;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace _Picker3D_.Scripts.UI
{
    public class InGamePanel : Picker3dPanel
    {
        [SerializeField]private TextMeshProUGUI currentLevelText;
        [SerializeField]private TextMeshProUGUI nextLevelText;
        [ListDrawerSettings(DraggableItems = false)][ShowInInspector]public List<Image> progressImages = new List<Image>();
        private int currentPart;
        private void OnEnable()
        {
            GameManager.Instance.OnGameStart.AddListener(ShowPanel);
            GameManager.Instance.OnStageSuccess.AddListener(()=> StartCoroutine(SetLevelBar()));
            EventManager.OnPartSuccess.AddListener(SetProgressBar);
        }

        private void Start()
        {
            currentPart = 0;
           StartCoroutine(SetLevelBar());
        }

        private IEnumerator SetLevelBar()
        {
            yield return new WaitForSeconds(0.3f);
            currentLevelText.text =(LevelManager.Instance.Level+1).ToString();
            nextLevelText.text =(LevelManager.Instance.Level+2).ToString();
            ClearProgressBar();
        }

        private void SetProgressBar()
        {
            
            if (currentPart==3)
            {
                currentPart = 0;
                ClearProgressBar();
            }
            else
            {
                progressImages[currentPart].DOColor(Color.green, 0.3f);
            }
            currentPart++;

        }

        private void ClearProgressBar()
        {
            currentPart = 0;
            foreach (var progress in progressImages)
            {
                progress.DOColor(Color.white, 0.3f);
              
            }
        }
    }
}
