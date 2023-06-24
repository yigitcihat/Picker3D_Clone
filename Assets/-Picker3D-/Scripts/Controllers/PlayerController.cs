using System;
using System.Collections;
using _Picker3D_.Scripts.Managers;
using UnityEngine;

namespace _Picker3D_.Scripts.Controllers
{
    public class PlayerController : Singleton<PlayerController>
    {
        #region Variables
        
        [SerializeField] private float speed;
        [SerializeField] private float forceLimit;
        [SerializeField] private float width;
        [SerializeField] private Renderer pickerRenderer;
        
        [HideInInspector] public bool canMove;
        
        private const float SpeedMultiply =100;
        private const float ForceMultiply =100;
        private float _initialSpeed;
        private Rigidbody _rigidBody;
        private Rigidbody Rigidbody => _rigidBody == null ? _rigidBody = GetComponent<Rigidbody>() : _rigidBody;

        private PlayerStates.PlayerState _myState;
        // private float _initialSpeed;
        private float _firstDistanceFromFinish;
        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            _initialSpeed = speed;
        }

        private void FixedUpdate()
        {
            if(GameManager.Instance.isGameStarted)
                MoveForward();
        }

        #endregion

        #region Other Methods

        // Public Methods

        public void SetColor(Color pickerColor)
        {
            pickerRenderer.material.color = pickerColor;
        }
        public void Slide(float value)
        {
            var clampedValue = Mathf.Clamp(value, -forceLimit * ForceMultiply, forceLimit * ForceMultiply);

            if ((transform.position.x <= -width && clampedValue < 0) || (transform.position.x >= width && clampedValue > 0))
            {
                clampedValue = 0f; 
            }

            var forceDirection = transform.right * clampedValue;
            Rigidbody.AddForce(forceDirection * Rigidbody.mass / 2);
        }
        #endregion
        #region Private Methods
        public void SetWaitingState(PlayerStates.PlayerState state)
        {
            _myState = state;
            UpdatePlayerState();
        }
        private void OnTriggerEnter(Collider other)
        {
            var containerTrigger = other.GetComponent<ContainerTrigger>();
            if (containerTrigger == null) return;
            SetWaitingState(PlayerStates.PlayerState.Waiting);
            var container = other.GetComponentInParent<Container>();
            canMove = CollectDetector.Instance.CheckHaveEnoughObjects(container.GetRequireObjectCount());
            StartCoroutine(CheckPlayerState());
            Destroy(other.gameObject);
        }
        
        private IEnumerator CheckPlayerState()
        {
            yield return new WaitForSeconds(4f);
            // if (!canMove) 
            // // EventManager.OnGameOver.Invoke();
        }

        private void MoveForward()
        {
            Rigidbody.velocity = transform.forward * (speed * SpeedMultiply * Time.fixedDeltaTime);
        }

        private void UpdatePlayerState()
        {
            switch (_myState)
            {
                case PlayerStates.PlayerState.Moving:
                    speed = _initialSpeed;
                    CollectDetector.Instance.ClearList();

                    break;
                case PlayerStates.PlayerState.Waiting:
                    speed = 0;
                    CollectDetector.Instance.PushAllObjects();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}

