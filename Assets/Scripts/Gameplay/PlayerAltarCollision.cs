using System.Net.Mail;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;
using Unity.Mathematics;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    public class PlayerAltarCollision : Simulation.Event<PlayerAltarCollision>
    {

        private HUDModel _hudModel = Simulation.GetModel<HUDModel>();

        public override void Execute()
        {
            _hudModel.UIController.Display(Panel.ActivationText);
        }
    }
}