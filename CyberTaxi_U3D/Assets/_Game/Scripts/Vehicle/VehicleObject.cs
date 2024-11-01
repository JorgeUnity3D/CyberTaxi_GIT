using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    [CreateAssetMenu(fileName = "SC_VehicleDataObject", menuName = "CyberTaxi/Vehicle DataObject")]
    public class VehicleObject : ScriptableObject
    {
        [SerializeField] private VehicleData _vehicleData;

        public VehicleData VehicleData
        {
            get => _vehicleData;
        }
    }
}