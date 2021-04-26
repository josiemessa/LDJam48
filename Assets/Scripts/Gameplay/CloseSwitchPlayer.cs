using Platformer.Core;
using Platformer.Model;
using Platformer.UI;

namespace Platformer.Gameplay
{
    public class CloseSwitchPlayer : Simulation.Event<CloseSwitchPlayer>
    {
        public override void Execute()
        {
            Simulation.GetModel<HUDModel>().UIController.Hide(Panel.SwitchBody);
        }
    }
}