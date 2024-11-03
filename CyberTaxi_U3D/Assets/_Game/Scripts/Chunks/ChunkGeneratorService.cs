using System.Collections.Generic;
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
            //_currentChunk = Instantiate(_chunkPrefab, new Vector3(0, 0, _accumulatedOffsetZ), Quaternion.identity).GetComponent<RecyclableChunk>();
            _currentChunk = _chunkPool.GetChunkLogic();

            //int randomIndex = Random.Range(0, _chunkMeshPrefabs.Count);
            RecyclableGameObject chunkMesh = _chunkPool.GetChunkMesh();
            //GameObject chunkMesh = Instantiate(_chunkMeshPrefabs[randomIndex], Vector3.zero, Quaternion.Euler(0, 90 * randomRotation, 0));

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
