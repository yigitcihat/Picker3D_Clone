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
        
        private const float SpeedMultiply =100;
        private const float ForceMultiply =100;
        
        private Rigidbody _rigidBody;
        private Rigidbody Rigidbody => _rigidBody == null ? _rigidBody = GetComponent<Rigidbody>() : _rigidBody;

        private PlayerStates.PlayerState _myState;
        // private float _initialSpeed;
        private float _firstDistanceFromFinish;

        #endregion

        #region MonoBehaviour Callbacks

        // private void Start()
        // {
        //     // _initialSpeed = speed;
        // }

        private void FixedUpdate()
        {
            if(GameManager.Instance.isGameStarted)
                MoveForward();
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (!other.CompareTag($"Storage_Trigger")) return;
        //     SetWaitingState(PlayerStates.PlayerState.Waiting);
        //     StartCoroutine(CheckPlayerState());
        //     Destroy(other.gameObject);
        // }

        #endregion

        #region Other Methods

        // Public Methods

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
        
        // Private Methods
        // private void SetWaitingState(PlayerStates.PlayerState state)
        // {
        //     _myState = state;
        //     UpdatePlayerState();
        // }

        
        // private IEnumerator CheckPlayerState()
        // {
        //     yield return new WaitForSeconds(4f);
        //     if (!canPass)
        //         EventManager.OnGameOver.Invoke();
        // }

        private void MoveForward()
        {
            Rigidbody.velocity = transform.forward * (speed * SpeedMultiply * Time.fixedDeltaTime);
        }

        // private void UpdatePlayerState()
        // {
        //     switch (_myState)
        //     {
        //         case PlayerStates.PlayerState.Moving:
        //             playerSpeed = _initialSpeed;
        //             // ObjectDetector.Instance.ClearList();
        //             break;
        //         case PlayerStates.PlayerState.Waiting:
        //             playerSpeed = 0;
        //             // ObjectDetector.Instance.PushAllObjects();
        //             break;
        //     }
        // }

        #endregion
    }
}

