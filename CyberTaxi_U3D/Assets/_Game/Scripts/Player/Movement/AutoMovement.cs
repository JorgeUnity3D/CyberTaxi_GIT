using System.Collections.Generic;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    public class AutoMovement : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _vehicleMeshPrefabs;
        [SerializeField] private List<TrailRenderer> _trailRenderers;
        [SerializeField] private List<Material> _trailRendererMaterials;
        [SerializeField] private Transform _visualsHolder;
        [SerializeField] private float _speed;

        private void Start()
        {
            SetUp();
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void SetUp()
        {
            //Vehicle
            Destroy(_visualsHolder.GetChild(0).gameObject);
            int randomVehicleIndex = UnityEngine.Random.Range(0, _vehicleMeshPrefabs.Count);
            GameObject randomVehicle = Instantiate(_vehicleMeshPrefabs[randomVehicleIndex], _visualsHolder);
            randomVehicle.transform.localPosition = Vector3.zero;
            //Speed
            float randomSpeed = UnityEngine.Random.Range(5f, 10f);
            _speed = randomSpeed;

            //TrailRenderer Material
            int randomMaterialIndex = UnityEngine.Random.Range(0, _trailRendererMaterials.Count);
            Material randomMaterial = _trailRendererMaterials[randomMaterialIndex];
            foreach (TrailRenderer trailRenderer in _trailRenderers)
            {
                trailRenderer.material = randomMaterial;
            }
        }
    }
}