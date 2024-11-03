using BreakTheCycle.Core.Pooling;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class RecyclableSectionCollider : RecyclableObject
    {
        private BoxCollider _boxCollider;

        #region UNITY_LIFECYCLE

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        #endregion

        #region SETUP

        public void SetUp(Transform parent, Vector3 position, Vector3 size)
        {
            transform.SetParent(parent);
            transform.position = position;
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.size = size;
        }

        #endregion

    }
}
