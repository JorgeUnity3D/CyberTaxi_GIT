using UnityEngine;
using UnityEngine.Pool;

namespace BreakTheCycle.Core.Pooling
{
    public abstract class RecyclableObject : MonoBehaviour
    {
        private IObjectPool<RecyclableObject> _objectPool;
        private Transform _parent;

        public Transform Parent
        {
            get => _parent;
        }

        internal void SetUp(IObjectPool<RecyclableObject> objectPool, Transform parent)
        {
            _objectPool = objectPool;
            _parent = parent;
        }

        internal virtual void InitRecyclable() { }
        internal virtual void ReleaseRecyclable() { }

        public void Recycle()
        {
            if (gameObject.activeSelf)
            {
                _objectPool.Release(this);
            }
        }
    }
}