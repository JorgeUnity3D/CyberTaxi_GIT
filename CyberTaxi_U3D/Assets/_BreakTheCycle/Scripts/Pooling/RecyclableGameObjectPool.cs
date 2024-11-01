using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace BreakTheCycle
{
    public class RecyclableGameObjectPool
    {
        [SerializeField] private RecyclableGameObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private IObjectPool<RecyclableGameObject> _pool;

        public RecyclableGameObjectPool(RecyclableGameObject prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new ObjectPool<RecyclableGameObject>(Create, OnGetPooledObject, OnReleasePoolObject);
        }

        private RecyclableGameObject Create()
        {
            RecyclableGameObject newRecyclableBehaviour = Object.Instantiate(_prefab, _parent).GetComponent<RecyclableGameObject>();
            newRecyclableBehaviour.SetUp(_pool);
            Transform newTransform = newRecyclableBehaviour.transform;
            newTransform.position = Vector3.zero;
            newTransform.rotation = Quaternion.identity;
            return newRecyclableBehaviour;
        }

        private void OnGetPooledObject(RecyclableGameObject recyclableBehaviour)
        {
            recyclableBehaviour.gameObject.SetActive(true);
            recyclableBehaviour.InitRecyclable();
        }

        private void OnReleasePoolObject(RecyclableGameObject recyclableBehaviour)
        {
            recyclableBehaviour.ReleaseRecyclable();
            recyclableBehaviour.gameObject.SetActive(false);
            recyclableBehaviour.transform.SetParent(_parent);
        }

        public TComponent SpawnObject<TComponent>()
        {
            RecyclableGameObject recyclableBehaviour = _pool.Get();
            return recyclableBehaviour.GetComponent<TComponent>();
        }
    }
}
