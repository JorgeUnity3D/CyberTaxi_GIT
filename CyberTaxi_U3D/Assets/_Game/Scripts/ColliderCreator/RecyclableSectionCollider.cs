using System;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{

    public class RecyclableSectionCollider : RecyclableGameObject
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void SetUp(Transform parent, Vector3 position, Vector3 size)
        {
            transform.SetParent(parent);
            transform.position = position;
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.size = size;
        }        
    }
}
