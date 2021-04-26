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
                if (activePlates == _pressurePlateInstances.Length)
                {
                    var ev = Simulation.Schedule<PressurePlateTaskComplete>();
                    ev._controller = this;

                }
            }
        }

        public int tickThreshold = 12;
        public int tickInterval = 5;
        public Collider2D Door;
        public SpriteRenderer DoorColor;

        private PressurePlateInstance[] _pressurePlateInstances;
        private void Awake()
        {
            _pressurePlateInstances = GetComponentsInChildren<PressurePlateInstance>();
            foreach (var pressurePlateInstance in _pressurePlateInstances)
            {
                pressurePlateInstance.PlateController = this;
            }
        }
    }
}