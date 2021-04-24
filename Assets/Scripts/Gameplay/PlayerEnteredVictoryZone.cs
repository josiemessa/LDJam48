using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{

    /// <summary>
    /// This event is triggered when the player character enters a trigger with a VictoryZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
    public class PlayerEnteredVictoryZone : Simulation.Event<PlayerEnteredVictoryZone>
    {
        public VictoryZone victoryZone;

        PlayerModel model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            // model.player.animator.SetTrigger("victory");
            model.ActivePlayer.ControlEnabled = false;
        }
    }
}