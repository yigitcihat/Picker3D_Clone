using UnityEngine;

namespace _Picker3D_.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Variables
        private Vector3 _startMousePosition;
        private const float DistanceThreshold = 10f;
        private const float Smoothness = 5000000f;

        #endregion

        #region MonoBehaviour Callbacks

        private void Update()
        {
            // if(GameManager.Instance.isGameStarted)
            ControllerInput();
        }

        #endregion

        #region Other Methods

       

        private void ControllerInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                var currentMousePosition = Input.mousePosition;
                var distance = currentMousePosition.x - _startMousePosition.x;

                if (!(Mathf.Abs(distance) > DistanceThreshold)) return;
                var calculatedValue = (distance / Screen.width) * Smoothness ;

                // Slide
                PlayerController.Instance.Slide(calculatedValue);

                _startMousePosition = currentMousePosition;
            }
        }

        #endregion
    }
}
