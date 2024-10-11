using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    [CreateAssetMenu(fileName = "SC_Vehicle", menuName = "CyberTaxi/Vehicle Object")]
    public class VehicleObject : ScriptableObject
    {
        [SerializeField] private VehicleData _vehicleData;

        public VehicleData VehicleData
        {
            get => _vehicleData;
        }
    }
}