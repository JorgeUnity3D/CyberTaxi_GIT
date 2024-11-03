using System.Collections.Generic;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class ChunkPool : MonoBehaviour
    {
        [Header("Chunk Logic")]
        [SerializeField] private RecyclableObject _chunkLogicPrefab;
        [SerializeField] private RecyclableObjectPool _chunkLogicPool;
        [Header("Chunk Meshes")]
        [SerializeField] private List<RecyclableGameObject> _chunkMeshPrefabs;
        [SerializeField] private List<RecyclableObjectPool> _chunkMeshPools;

        #region UNITY_LIFECYCLE

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService<ChunkPool>(this);
            _chunkLogicPool = new RecyclableObjectPool(_chunkLogicPrefab, transform, 2, 4);
            _chunkMeshPools = new List<RecyclableObjectPool>();
            foreach (RecyclableObject chunkMeshPrefab in _chunkMeshPrefabs)
            {
                _chunkMeshPools.Add(new RecyclableObjectPool(chunkMeshPrefab, transform, 2, 10));
            }
        }

        #endregion

        #region POOL

        public RecyclableChunkLogic GetChunkLogic()
        {
            return _chunkLogicPool.SpawnObject<RecyclableChunkLogic>();
        }    
        
        public RecyclableGameObject GetChunkMesh()
        {
            int randomIndex = Random.Range(0, _chunkMeshPools.Count);
            return _chunkMeshPools[randomIndex].SpawnObject<RecyclableGameObject>();
        }

        public void RecycleChunk(RecyclableChunkLogic previousChunk)
        {
            previousChunk.Recycle();
        }

        #endregion
    }
}
