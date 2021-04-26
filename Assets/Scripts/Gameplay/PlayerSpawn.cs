using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        public PlayerController spawnedPlayer;

        public override void Execute()
        {
            spawnedPlayer.collider2d.enabled = true;
            // if (spawnedPlayer.audioSource && spawnedPlayer.respawnAudio)
            //     spawnedPlayer.audioSource.PlayOneShot(spawnedPlayer.respawnAudio);
            spawnedPlayer.health.CurrentHp = 1;
            // spawnedPlayer.Teleport(model.spawnPoint.transform.position);
            spawnedPlayer.jumpState = PlayerController.JumpState.Grounded;
            // player.animator.SetBool("dead", false);
            spawnedPlayer.ControlEnabled = false;
            // Debug.Log($"Setting health of new body color to white");
            // spawnedPlayer.healthPanel.color = Color.white;

            Simulation.GetModel<HUDModel>().UIController.Display(Panel.AncientAltar);
        }
    }
}