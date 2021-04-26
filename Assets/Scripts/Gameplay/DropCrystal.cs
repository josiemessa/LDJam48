using Platformer.Core;
using Platformer.Model;

namespace Platformer.Gameplay
{
    public class DropCrystal : Simulation.Event<DropCrystal>
    {
        private PlayerModel _model = Simulation.GetModel<PlayerModel>();
        public override void Execute()
        {
            var player = _model.players.Find(player => player.HasRecipe && !player.HasPotion);
            player.HasCrystal = true;
        }
    }
}