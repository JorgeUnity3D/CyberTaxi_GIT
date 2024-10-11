using System;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    [Serializable]
    public class VehicleData
    {
        [SerializeField] private GameObject vehicleModelPrefab;
        [SerializeField] private VehicleType _vehicleType;
        [SerializeField] private VehicleStats _vehicleStats;

        public GameObject VehicleModelPrefab
        {
            get => vehicleModelPrefab;
        }

        public VehicleType VehicleType
        {
            get => _vehicleType;
        }

        public VehicleStats VehicleStats
        {
            get => _vehicleStats;
        }
    }
}