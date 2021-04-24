using Platformer.Core;

namespace Platformer.Gameplay
{
    public class PressurePlateActive : Simulation.Event<PressurePlateActive>
    {
        public PressurePlateInstance Instance;
        public override void Execute()
        {
            Instance.Ticks++;
        }
    }
}