using BreakTheCycle.Core.Pooling;
using BreakTheCycle.Core.ServiceLocator;
using UnityEngine;
using UnityEngine.Events;

namespace BreakTheCycle.CyberTaxi
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] private float _chunkOffsetZ;

        private float _accumulatedOffsetZ;
        private ChunkPool _chunkPool;
        private RecyclableChunkLogic _currentChunk;
        private RecyclableChunkLogic _previousChunk;

        #region UNITY_LIFECYCLE

        private void Awake()
        {
            _chunkPool = GetComponent<ChunkPool>();
        }

        private void Start()
        {
            _accumulatedOffsetZ = 0;
        }

        #endregion

        #region CONTROL

        public RecyclableChunkLogic GenerateChunk(UnityAction HandlePlayerCrossedChunk)
        {
            //Debug.Log($"[ChunkService] SetUpChunk() -> Setting up chunk at Z: {_accumulatedOffsetZ}");
            RecyclableChunkLogic chunkLogic = _chunkPool.GetChunkLogic();            
            RecyclableGameObject chunkMesh = _chunkPool.GetChunkMesh();

            chunkLogic.name = $"RecyclableChunkLogic_{chunkLogic.gameObject.GetInstanceID()}";
            string preffix = chunkMesh.name.Contains("01") ? 1.ToString("D2") : 2.ToString("D2");
            chunkMesh.name = $"{preffix}_RecyclableChunkMesh_{chunkLogic.gameObject.GetInstanceID()}";

            Vector3 chunkPosition = new Vector3(0, 0, _accumulatedOffsetZ);
            Quaternion chunkRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);

            chunkLogic.SetUp(chunkPosition, chunkRotation, chunkMesh, HandlePlayerCrossedChunk);
            _accumulatedOffsetZ += _chunkOffsetZ;
            return chunkLogic;
        }

        #endregion

    }
}
