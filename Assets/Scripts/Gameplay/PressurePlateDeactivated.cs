using Platformer.Core;

namespace Platformer.Gameplay
{
    public class PressurePlateDeactivated : Simulation.Event<PressurePlateDeactivated>
    {
        public PressurePlateInstance Instance;
        public override void Execute()
        {
            Instance.Ticks = 0;
            Instance.PlateController.ActivePlates--;
        }
    }
}