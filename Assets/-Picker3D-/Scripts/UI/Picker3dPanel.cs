using System.Collections.Generic;
using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Picker3D_.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Picker3dPanel : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private CanvasGroup CanvasGroup => (_canvasGroup == null) ? _canvasGroup = GetComponent<CanvasGroup>() : _canvasGroup;

        [ValueDropdown("PanelList")]
        public string panelID;

        public UnityEvent onPanelShown = new UnityEvent();
        public UnityEvent onPanelHide = new UnityEvent();



        private List<string> PanelList => PanalList.PanelIDs;

        private void Awake()
        {
            PanalList.Panels[panelID] = this;
        }


        [ButtonGroup("PanelVisibility")]
        public void ShowPanel()
        {
            if (CanvasGroup.alpha > 0)
                return;
            SetPanel(1, true,true);
        }

        [ButtonGroup("PanelVisibility")]
        public void HidePanel()
        {
            if (CanvasGroup.alpha == 0)
                return;


            SetPanel(0, false, false);
        }

        public void SetPanel(float alpha, bool interactable, bool blocksRaycast)
        {
            CanvasGroup.alpha = alpha;
            CanvasGroup.interactable = interactable;
            CanvasGroup.blocksRaycasts = blocksRaycast;
        }

        [ButtonGroup("PanelVisibility")]
        public void TogglePanel()
        {
            if (CanvasGroup.alpha == 0)
                ShowPanel();
            else HidePanel();
        }
    }
}
