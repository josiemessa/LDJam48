using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;

namespace Platformer.Gameplay
{
    public class CraftPotion : Simulation.Event<CraftPotion>
    {
        public PlayerController Player;
        private HUDModel _hudModel = Simulation.GetModel<HUDModel>();
        public override void Execute()
        {
            _hudModel.UIController.Display(Panel.HealthPotion);
            Player.HasPotion = true;

            Simulation.Schedule<DropCrystal>();

        }
    }
}