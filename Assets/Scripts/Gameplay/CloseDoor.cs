using Platformer.Core;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class CloseDoor : Simulation.Event<CloseDoor>
    {
        public Collider2D Door;
        public SpriteRenderer DoorColor;
        public override void Execute()
        {
            Door.enabled = true;
            DoorColor.color = new Color(1f, 1f, 1f, 1f);

        }
    }
}