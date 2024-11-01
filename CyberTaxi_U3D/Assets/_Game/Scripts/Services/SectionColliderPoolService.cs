using BreakTheCycle;
using BreakTheCycle.CyberTaxi;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class SectionColliderPoolService : MonoBehaviour
    {
        [SerializeField] private RecyclableGameObject _sectionColliderPrefab;
        [SerializeField] private RecyclableGameObjectPool _sectionColliderPool;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService<SectionColliderPoolService>(this);
            _sectionColliderPool = new RecyclableGameObjectPool(_sectionColliderPrefab, transform);
        }

        public RecyclableSectionCollider GetSectionCollider()
        {
            return _sectionColliderPool.SpawnObject<RecyclableSectionCollider>();
        }
    }
}
