using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BreakTheCycle.CyberTaxi
{
    public class InputsController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputs;

        private Vector2 _previousMovementInput;

        public UnityAction<float> OnHorizontalInput;
        public UnityAction<float> OnVerticalInput;

        #region UNITY_LIFECYCLE

        private void OnEnable()
        {
            _inputs.action.Enable();
        }

        private void OnDisable()
        {
            _inputs.action.Disable();
        }

        private void Update()
        {
            Vector2 movementInput = _inputs.action.ReadValue<Vector2>();
            //Debug.Log($"[InputsController] Update() -> Horizontal: {movementInput.x}, Vertical: {movementInput.y}");
            if (movementInput != _previousMovementInput)
            {
                OnHorizontalInput?.Invoke(movementInput.x);
                OnVerticalInput?.Invoke(movementInput.y);

                _previousMovementInput = movementInput;
            }
        }

        #endregion
    }
}