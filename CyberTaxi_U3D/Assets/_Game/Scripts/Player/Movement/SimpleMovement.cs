using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class SimpleMovement : MonoBehaviour
    {
        [SerializeField] private InputsController _inputsController;

        [SerializeField] private float _speed;
        [SerializeField] private float _turnSpeed = 100f;
        [SerializeField] private float _autoStraightenSpeed = 2f;
        [SerializeField] private float _handling = 30f;

        private Quaternion _initialRotation;
        private float _horizontalInput;
        private float _verticalInput;

        #region UNITY_LIFECYCLE

        private void Start()
        {
            _initialRotation = transform.rotation;
            _inputsController.OnHorizontalInput += HandleHorizontalInput;
            _inputsController.OnVerticalInput += HandleVerticalInput;
        }

        private void OnDisable()
        {
            _inputsController.OnHorizontalInput -= HandleHorizontalInput;
            _inputsController.OnVerticalInput -= HandleVerticalInput;
        }
        
        private void Update()
        {
            transform.Translate(Vector3.forward * _verticalInput * _speed * Time.deltaTime);

            
            if (_horizontalInput != 0)
            {
                float angle = Quaternion.Angle(_initialRotation, transform.rotation);
                if ((angle < _handling))
                {
                    transform.Rotate(Vector3.up, _horizontalInput * _turnSpeed * Time.deltaTime);
                }
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _initialRotation, _autoStraightenSpeed * _turnSpeed * Time.deltaTime);
            }
        }

        #endregion

        #region CONTROL

        private void HandleHorizontalInput(float horizotalInput)
        {
            _horizontalInput = horizotalInput;
        }

        private void HandleVerticalInput(float verticalInput)
        {
            _verticalInput = verticalInput;
        }

        #endregion
    }
}