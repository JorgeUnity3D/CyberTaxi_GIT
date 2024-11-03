using BreakTheCycle.Core.Pooling;
using BreakTheCycle.Core.ServiceLocator;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class ChunkGeneratorService : MonoBehaviour
    {
        private ChunkPool _chunkPool;

        //[SerializeField] private GameObject _chunkPrefab;
        //[SerializeField] private List<GameObject> _chunkMeshPrefabs;
        [SerializeField] private float _chunkOffsetZ;

        private RecyclableChunkLogic _currentChunk;
        private RecyclableChunkLogic _previousChunk;
        private float _accumulatedOffsetZ;

        #region UNITY_LIFECYCLE

        private void Start()
        {
            _accumulatedOffsetZ = 0;
            _chunkPool = ServiceLocator.Instance.GetService<ChunkPool>();
            SetUpChunk();
        }

        #endregion

        #region CONTROL

        private void SetUpChunk()
        {
            Debug.Log($"[ChunkService] SetUpChunk() -> Setting up chunk at Z: {_accumulatedOffsetZ}");
            _currentChunk = _chunkPool.GetChunkLogic();

            RecyclableGameObject chunkMesh = _chunkPool.GetChunkMesh();

            Vector3 chunkPosition = new Vector3(0, 0, _accumulatedOffsetZ);
            Quaternion chunkRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
            _currentChunk.SetUp(chunkPosition, chunkRotation, chunkMesh.transform, HandleCarCrossedChunk);
        }

        private void HandleCarCrossedChunk()
        {
            Debug.Log($"[ChunkService] HandleCarCrossedChunk() -> ");
            _accumulatedOffsetZ += _chunkOffsetZ;
            if (_previousChunk != null)
            {
                //Destroy(_previousChunk.gameObject);
                _chunkPool.RecycleChunk(_previousChunk);
            }
            _previousChunk = _currentChunk;
            SetUpChunk();
        }

        #endregion

    }
}
