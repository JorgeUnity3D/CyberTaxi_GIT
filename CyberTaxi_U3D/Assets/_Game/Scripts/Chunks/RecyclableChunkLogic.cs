using BreakTheCycle.Core.Pooling;
using UnityEngine;
using UnityEngine.Events;

namespace BreakTheCycle.CyberTaxi
{
    public class RecyclableChunkLogic : RecyclableObject
    {
        [SerializeField] private Transform _visualsHolder;

        private UnityAction OnCarCrossedChunk;

        #region ON_TRIGGER

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GameConsts.PLAYER_TAG)
            {
                OnCarCrossedChunk?.Invoke();
            }
        }

        #endregion

        #region SETUP

        public void SetUp(Vector3 chunkPosition, Quaternion chunkRotation, Transform chunkMesh, UnityAction OnCarCrossedChunkCallback)
        {
            transform.position = chunkPosition;
            Destroy(_visualsHolder.GetChild(0).gameObject);
            chunkMesh.SetParent(_visualsHolder);
            chunkMesh.localPosition = Vector3.zero;
            OnCarCrossedChunk += OnCarCrossedChunkCallback;
        }

        #endregion

        #region POOL

        internal override void ReleaseRecyclable()
        {
            OnCarCrossedChunk = null;
        }

        #endregion

    }
}