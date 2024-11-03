using BreakTheCycle.Core.Pooling;
using BreakTheCycle.Core.ServiceLocator;
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
            ServiceLocator.Instance.RegisterService<SectionColliderPool>(this);
            _sectionColliderPool = new RecyclableObjectPool(_sectionColliderPrefab, transform);
        }

        #endregion

        #region POOL

        public RecyclableSectionCollider GetSectionCollider()
        {
            return _sectionColliderPool.SpawnObject<RecyclableSectionCollider>();
        }

        public void ReleaseSectionColliders(Transform section)
        {
            //todo: release all the colliders of a chunk that is being destroyed            
        }

        #endregion

    }
}
