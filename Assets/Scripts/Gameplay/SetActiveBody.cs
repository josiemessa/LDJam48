using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class SetActiveBody : Simulation.Event<SetActiveBody>
    {
        private readonly PlayerModel _model = Simulation.GetModel<PlayerModel>();
        public PlayerController Player;
        private readonly Color _color = new Color(.39f,.67f,.25f);
        // private readonly Color _color = Color.green;

        public override void Execute()
        {
            // disable old player
            if (_model.ActivePlayer.Id != Player.Id)
            {
                _model.ActivePlayer.ControlEnabled = false;
                _model.ActivePlayer.healthPanel.color = Color.white;
            }

            _model.ActivePlayer = Player;
            _model.ActivePlayer.ControlEnabled = true;
            _model.ActivePlayer.healthPanel.color = _color;

            var playerPos = Player.transform;
            _model.virtualCamera.m_Follow = playerPos;
            _model.virtualCamera.m_LookAt = playerPos;
        }
    }
}