using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;

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
            spawnedPlayer.health.maxHP = 1;
            // spawnedPlayer.Teleport(model.spawnPoint.transform.position);
            spawnedPlayer.jumpState = PlayerController.JumpState.Grounded;
            // player.animator.SetBool("dead", false);
            spawnedPlayer.ControlEnabled = false;

            var ev = Simulation.Schedule<EnablePlayerInput>(1f);
            ev.Player = spawnedPlayer;
            Simulation.GetModel<HUDModel>().UIController.Display(Panel.SwitchBody);
            Simulation.Schedule<CloseSwitchPlayer>(5f);
        }
    }
}