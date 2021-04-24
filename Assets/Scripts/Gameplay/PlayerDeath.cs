using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlayerModel model = Simulation.GetModel<PlayerModel>();

        // TODO: check for other player copies and control
        public override void Execute()
        {
            var player = model.ActivePlayer;
            if (player.health.IsAlive)
            {
                player.health.Die();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                // player.collider.enabled = false;
                player.ControlEnabled = false;

                // if (player.audioSource && player.ouchAudio)
                //     player.audioSource.PlayOneShot(player.ouchAudio);
                // player.animator.SetTrigger("hurt");
                // player.animator.SetBool("dead", true);
                Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}