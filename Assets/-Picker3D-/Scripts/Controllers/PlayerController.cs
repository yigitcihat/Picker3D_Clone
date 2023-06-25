using System;
using System.Collections;
using _Picker3D_.Scripts.Managers;
using _Picker3D_.Scripts.Others;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    public class PlayerController : Singleton<PlayerController>
    {
        #region Variables

        [SerializeField] private float speed;
        [SerializeField] private float forceLimit;
        [SerializeField] private float width;
        [SerializeField] private Renderer pickerRenderer;

        [HideInInspector] public bool canMove;

        private const float SpeedMultiply = 100;
        private const float ForceMultiply = 150;
        private float _initialSpeed;
        private Rigidbody _rigidBody;
        private Rigidbody Rigidbody => _rigidBody == null ? _rigidBody = GetComponent<Rigidbody>() : _rigidBody;

        private PlayerStates.PlayerState _myState;
        private float _firstDistanceFromFinish;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            _initialSpeed = speed;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.isGameStarted)
                MoveForward();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FinalLine"))
            {
                LevelManager.Instance.Level++;
                if (LevelManager.Instance.Level > LevelManager.Instance.levels.Count)
                {
                    LevelManager.Instance.LoadRandomLevel();
                }
                else
                {
                    LevelManager.Instance.LoadLevel(LevelManager.Instance.Level);
                }

                GameManager.Instance.OnStageSuccess.Invoke();
                other.gameObject.SetActive(false);
                SetWaitingState(PlayerStates.PlayerState.Waiting);
            }
            else if (other.CompareTag("ContainerTrigger"))
            {
                var container = other.GetComponentInParent<Container>();
                CollectDetector.Instance.PushAllObjects();
                Destroy(other.gameObject);
                SetWaitingState(PlayerStates.PlayerState.Waiting);
                StartCoroutine(CheckPlayerState());
            }
        }

        #endregion

        #region Other Methods

        // Public Methods

        public void SetColor(Color pickerColor)
        {
            pickerRenderer.material.color = pickerColor;
            pickerRenderer.material.SetColor("_HColor",Color.white);
        }

        public void Slide(float value)
        {
            var clampedValue = Mathf.Clamp(value, -forceLimit * ForceMultiply, forceLimit * ForceMultiply);

            if (((transform.position.x <= -width && clampedValue < 0) ||
                 (transform.position.x >= width && clampedValue > 0)) && canMove)
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


        private IEnumerator CheckPlayerState()
        {
            yield return new WaitForSeconds(4f);
            if (!canMove)
                GameManager.Instance.OnGameFail.Invoke();
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
                    canMove = true;

                    break;
                case PlayerStates.PlayerState.Waiting:
                    speed = 0;
                    canMove = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}