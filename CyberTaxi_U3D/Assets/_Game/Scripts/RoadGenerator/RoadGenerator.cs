using BreakTheCycle.Core.ServiceLocator;
using NaughtyAttributes;
using System.Buffers;
using System.Collections.Generic;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class RoadGenerator : MonoBehaviour
    {
        [Header("Generators")]
        [SerializeField, ReadOnly] private ChunkGenerator _chunkGenerator;
        [SerializeField, ReadOnly] private SectionColliderGenerator _sectionColliderGenerator;
        [Header("Pools")]
        [SerializeField, ReadOnly] private ChunkPool _chunkPool;
        [SerializeField, ReadOnly] private SectionColliderPool _sectionColliderPool;

        private RecyclableChunkLogic _currentChunk;
        private RecyclableChunkLogic _previousChunk;
        private RecyclableSectionCollider[] _currentRecyclableSectionColliders;
        private RecyclableSectionCollider[] _previousRecyclableSectionColliders;

        #region UNITY_LIFECYCLE

        private void Awake()
        {
            Application.targetFrameRate = 60;

            ServiceLocator.Instance.RegisterService(this);

            _chunkGenerator = GetComponent<ChunkGenerator>();

            _chunkPool = GetComponent<ChunkPool>();
            _sectionColliderPool = GetComponent<SectionColliderPool>();

        }

        private void Start()
        {
            GenerateRoad();
        }

        private void OnDestroy()
        {
            if (_currentRecyclableSectionColliders != null)
            {
                ArrayPool<RecyclableSectionCollider>.Shared.Return(_currentRecyclableSectionColliders, true);
            }
            if (_previousRecyclableSectionColliders != null)
            {
                ArrayPool<RecyclableSectionCollider>.Shared.Return(_previousRecyclableSectionColliders, true);
            }
        }

        #endregion

        #region CONTROL

        public void GenerateRoad()
        {
            _currentChunk = _chunkGenerator.GenerateChunk(HandlePlayerCrossedChunk);            
            _sectionColliderGenerator = _currentChunk.GetComponent<SectionColliderGenerator>();
            if (_currentRecyclableSectionColliders == null)
            {
                int maxCollidersCount = _sectionColliderGenerator.CalculateMaxColliderCount();
                _currentRecyclableSectionColliders = ArrayPool<RecyclableSectionCollider>.Shared.Rent(maxCollidersCount);
                _previousRecyclableSectionColliders = ArrayPool<RecyclableSectionCollider>.Shared.Rent(maxCollidersCount);
            }
            _currentRecyclableSectionColliders = _sectionColliderGenerator.GenerateSectionColliders(_sectionColliderPool);
        }

        public void HandlePlayerCrossedChunk()
        {
            if (_previousChunk != null)
            {
                _sectionColliderPool.RecycleColliders(_previousRecyclableSectionColliders);
                _chunkPool.RecycleChunk(_previousChunk);
            }
            _previousRecyclableSectionColliders = _currentRecyclableSectionColliders;
            _previousChunk = _currentChunk;
            GenerateRoad();
        }

        #endregion

    }
}
