using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        private readonly PlayerModel _model = Simulation.GetModel<PlayerModel>();
        public PlayerController Player;

        public override void Execute()
        {
            var playerPos = Player.transform;
            _model.virtualCamera.m_Follow = playerPos;
            _model.virtualCamera.m_LookAt = playerPos;
            _model.ActivePlayer.ControlEnabled = false;
            _model.ActivePlayer = Player;
        }
    }
}