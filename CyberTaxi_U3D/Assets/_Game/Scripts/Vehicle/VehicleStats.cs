using System;
using BreakTheCycle.Util.NotificableFields;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    [Serializable]
    public class VehicleStats
    {
        [SerializeField] private NInt _speed;
        [SerializeField] private NInt _handling;
        [SerializeField] private NInt _brake;
        [SerializeField] private NInt _endurance;

        public int Speed
        {
            get => _speed.Value;
            set => _speed.Value = value;
        }

        public int Handling
        {
            get => _handling.Value;
            set => _handling.Value = value;
        }

        public int Brake
        {
            get => _brake.Value;
            set => _brake.Value = value;
        }

        public int Endurance
        {
            get => _endurance.Value;
            set => _endurance.Value = value;
        }
    }
}