using BreakTheCycle.Core.Pooling;
using UnityEngine;
using UnityEngine.Events;

namespace BreakTheCycle.CyberTaxi
{
    public class RecyclableChunkLogic : RecyclableObject
    {
        [SerializeField] private Transform _visualsHolder;
        private RecyclableGameObject _chunkMesh;
        public RecyclableGameObject RecyclableVisuals
        {
            get => _chunkMesh;
        }

        private UnityAction OnCarCrossedChunk;

        #region UNITY_LIFECYCLE

        private void Awake()
        {
            Destroy(_visualsHolder.GetChild(0).gameObject);
        }

        #endregion

        #region ON_TRIGGER

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GameConsts.PLAYER_TAG)
            {
                OnCarCrossedChunk?.Invoke();
                OnCarCrossedChunk = null;
            }
        }

        #endregion

        #region SETUP

        public void SetUp(Vector3 chunkPosition, Quaternion chunkRotation, RecyclableGameObject chunkMesh, UnityAction OnCarCrossedChunkCallback)
        {            
            transform.position = chunkPosition;
            _chunkMesh = chunkMesh;
            _chunkMesh.transform.SetParent(_visualsHolder);
            _chunkMesh.transform.localPosition = Vector3.zero;
            _chunkMesh.transform.localRotation = chunkRotation;
            OnCarCrossedChunk += OnCarCrossedChunkCallback;
        }

        #endregion

    }
}