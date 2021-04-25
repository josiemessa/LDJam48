using System;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PressurePlateController : MonoBehaviour

    {
        [SerializeField]
        private int activePlates = 0;

        public int ActivePlates
        {
            get => activePlates;
            set
            {
                activePlates = value < 0 ? 0 : value;
                if (activePlates == _pressurePlateInstances.Length) Simulation.Schedule<PressurePlateTaskComplete>();
            }
        }

        public int tickThreshold = 12;
        public int tickInterval = 10;

        private PressurePlateInstance[] _pressurePlateInstances;
        private void Awake()
        {
            _pressurePlateInstances = FindObjectsOfType<PressurePlateInstance>();
            foreach (var pressurePlateInstance in _pressurePlateInstances)
            {
                pressurePlateInstance.PlateController = this;
            }
        }
    }
}