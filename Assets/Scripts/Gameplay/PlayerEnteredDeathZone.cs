using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player enters a trigger with a DeathZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredDeathZone"></typeparam>
    public class PlayerEnteredDeathZone : Simulation.Event<PlayerEnteredDeathZone>
    {
        public DeathZone deathzone;

        PlayerModel model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            Simulation.Schedule<PlayerDeath>(0);
        }
    }
}