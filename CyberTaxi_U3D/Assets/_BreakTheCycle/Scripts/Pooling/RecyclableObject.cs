using UnityEngine;
using UnityEngine.Pool;

namespace BreakTheCycle.Core.Pooling
{
    public abstract class RecyclableObject : MonoBehaviour
    {
        private IObjectPool<RecyclableObject> _objectPool;

        internal void SetUp(IObjectPool<RecyclableObject> objectPool)
        {
            _objectPool = objectPool;
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