using BreakTheCycle.Core.Pooling;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class SectionColliderPool : MonoBehaviour
    {
        [SerializeField] private RecyclableObject _sectionColliderPrefab;
        [SerializeField] private RecyclableObjectPool _sectionColliderPool;
        
        #region UNITY_LIFECYCLE

        private void Awake()
        {
            _sectionColliderPool = new RecyclableObjectPool(_sectionColliderPrefab, transform, 200, 800);
        }

        #endregion

        #region POOLING

        public RecyclableSectionCollider GetSectionCollider()
        {
            return _sectionColliderPool.SpawnObject<RecyclableSectionCollider>();
        }

        internal void RecycleColliders(List<RecyclableSectionCollider> recyclableSectionColliders)
        {
            for (int i = 0; i < recyclableSectionColliders.Count; i++)
            {
                RecyclableSectionCollider recyclableSectionCollider = recyclableSectionColliders[i];
                recyclableSectionCollider.Recycle();
            }
        }

        #endregion

    }
}