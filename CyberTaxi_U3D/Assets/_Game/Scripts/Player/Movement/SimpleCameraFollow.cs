using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class SimpleCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        
        private Vector3 _offset;

        #region UNITY_LIFECYCLE

        private void Start()
        {
            _offset = _cameraTransform.position - transform.position;
        }

        private void LateUpdate()
        {
            _cameraTransform.position = transform.position + _offset;
        }

        #endregion
    }
}