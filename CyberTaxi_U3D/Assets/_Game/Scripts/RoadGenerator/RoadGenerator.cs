using BreakTheCycle.Core.ServiceLocator;
using NaughtyAttributes;
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
        private List<RecyclableSectionCollider> _recyclableSectionColliders;

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

        #endregion

        #region CONTROL

        public void GenerateRoad()
        {
            _currentChunk = _chunkGenerator.GenerateChunk(HandlePlayerCrossedChunk);
            _sectionColliderGenerator = _currentChunk.GetComponent<SectionColliderGenerator>();
            _recyclableSectionColliders = _sectionColliderGenerator.GenerateSectionColliders(_sectionColliderPool);
        }

        public void HandlePlayerCrossedChunk()
        {
            if (_previousChunk != null)
            {
                _sectionColliderPool.RecycleColliders(_recyclableSectionColliders);
                _recyclableSectionColliders.Clear();
                _chunkPool.RecycleChunk(_previousChunk);
            }
            _previousChunk = _currentChunk;
            GenerateRoad();
        }

        #endregion

    }
}
