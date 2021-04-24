using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PressurePlateTaskComplete : Simulation.Event<PressurePlateTaskComplete>
    {
        public override void Execute()
        {
            Debug.Log("Task complete");
            // TODO: open door
        }
    }
}