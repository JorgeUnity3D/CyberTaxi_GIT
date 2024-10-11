using UnityEngine;
using UnityEngine.Events;

namespace BreakTheCycle.CyberTaxi
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Transform _visualsHolder;
        
        public UnityAction OnCarCrossedChunk;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GameConsts.PLAYER_TAG)
            {
                OnCarCrossedChunk?.Invoke();
            }
        }
        
        public void SetVisuals(GameObject chunkMesh)
        {
            Destroy(_visualsHolder.GetChild(0).gameObject);
            chunkMesh.transform.SetParent(_visualsHolder);
            chunkMesh.transform.localPosition = Vector3.zero;
        }
    }
}