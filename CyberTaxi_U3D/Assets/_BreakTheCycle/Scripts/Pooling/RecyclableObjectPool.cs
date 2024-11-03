using UnityEngine;
using UnityEngine.Pool;

namespace BreakTheCycle.Core.Pooling
{
    public class RecyclableObjectPool
    {
        [SerializeField] private RecyclableObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private IObjectPool<RecyclableObject> _pool;

        public RecyclableObjectPool(RecyclableObject prefab, Transform parent, int defaultCapacity = 10, int maxSize = 10000)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new ObjectPool<RecyclableObject>(Create, OnGetPooledObject, OnReleasePoolObject, null, true, defaultCapacity, maxSize);
        }

        private RecyclableObject Create()
        {
            RecyclableObject newRecyclableObject = Object.Instantiate(_prefab, _parent).GetComponent<RecyclableObject>();
            newRecyclableObject.SetUp(_pool, _parent);
            Transform newTransform = newRecyclableObject.transform;
            newTransform.position = Vector3.zero;
            newTransform.rotation = Quaternion.identity;
            return newRecyclableObject;
        }

        private void OnGetPooledObject(RecyclableObject recyclableBehaviour)
        {
            recyclableBehaviour.gameObject.SetActive(true);
            recyclableBehaviour.InitRecyclable();
        }

        private void OnReleasePoolObject(RecyclableObject recyclableBehaviour)
        {
            recyclableBehaviour.ReleaseRecyclable();
            recyclableBehaviour.gameObject.SetActive(false);
            recyclableBehaviour.transform.SetParent(_parent);
        }

        public TComponent SpawnObject<TComponent>()
        {
            RecyclableObject recyclableObject = _pool.Get();
            return recyclableObject.GetComponent<TComponent>();
        }
    }
}