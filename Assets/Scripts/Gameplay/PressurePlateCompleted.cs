using Platformer.Core;

namespace Platformer.Gameplay
{
    public class PressurePlateCompleted : Simulation.Event<PressurePlateCompleted>
    {
        public PressurePlateInstance Instance;
        public override void Execute()
        {
            Instance.PlateController.ActivePlates++;
        }
    }
}