using Platformer.Core;
using Platformer.Model;
using Platformer.UI;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PlayerAltarCollisionEnded : Simulation.Event<PlayerAltarCollisionEnded>
    {
        private HUDModel _hudModel = Simulation.GetModel<HUDModel>();
        public override void Execute()
        {
            _hudModel.UIController.Hide(Panel.InteractionHint);
        }
    }
}