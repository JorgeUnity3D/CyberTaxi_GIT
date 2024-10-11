using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BreakTheCycle.CyberTaxi
{
    public class ChunkService : MonoBehaviour
    {
        [SerializeField] private GameObject _chunkPrefab;
        [SerializeField] private List<GameObject> _chunkMeshPrefabs;
        [SerializeField] private float _chunkOffsetZ;

        private Chunk _currentChunk;
        private Chunk _previousChunk;
        private float _accumulatedOffsetZ;
        
        private void Start()
        {
            _accumulatedOffsetZ = 0;
            SetUpChunk();
        }
        
        private void SetUpChunk()
        {
            Debug.Log($"[ChunkService] SetUpChunk() -> Setting up chunk at Z: {_accumulatedOffsetZ}");
            _currentChunk = Instantiate(_chunkPrefab, new Vector3(0, 0, _accumulatedOffsetZ), Quaternion.identity).GetComponent<Chunk>();
            int randomIndex = UnityEngine.Random.Range(0, _chunkMeshPrefabs.Count);
            int randomRotation = UnityEngine.Random.Range(0, 4);
            GameObject chunkMesh = Instantiate(_chunkMeshPrefabs[randomIndex], Vector3.zero, Quaternion.Euler(0, 90 * randomRotation, 0));
            _currentChunk.SetVisuals(chunkMesh);
            _currentChunk.OnCarCrossedChunk += HandleCarCrossedChunk;
        }

        private void HandleCarCrossedChunk()
        {
            Debug.Log($"[ChunkService] HandleCarCrossedChunk() -> ");
            _accumulatedOffsetZ += _chunkOffsetZ;
            if (_previousChunk != null)
            {
                _previousChunk.OnCarCrossedChunk -= HandleCarCrossedChunk;
                Destroy(_previousChunk.gameObject);
            }
            _previousChunk = _currentChunk;
            SetUpChunk();
        }
    }
}
