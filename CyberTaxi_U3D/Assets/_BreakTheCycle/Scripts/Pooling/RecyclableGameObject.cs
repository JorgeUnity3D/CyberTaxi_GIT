using UnityEngine;
using UnityEngine.Pool;

namespace BreakTheCycle
{

    public abstract class RecyclableGameObject : MonoBehaviour
    {
        private IObjectPool<RecyclableGameObject> _objectPool;

        internal void SetUp(IObjectPool<RecyclableGameObject> objectPool)
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
