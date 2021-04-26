using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PressurePlateTaskComplete : Simulation.Event<PressurePlateTaskComplete>
    {
        public PressurePlateController _controller;
        public override void Execute()
        {
            Debug.Log("Task complete");
            _controller.Door.enabled = false;
            _controller.DoorColor.color = new Color(1f, 1f, 1f, 0.2f);
            var ev = Simulation.Schedule<CloseDoor>(10f);
            ev.Door = _controller.Door;
            ev.DoorColor = _controller.DoorColor;

        }
    }
}