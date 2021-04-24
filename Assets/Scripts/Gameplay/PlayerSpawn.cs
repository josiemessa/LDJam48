using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

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
            // TODO: manage health of copies
            spawnedPlayer.health.Increment();
            // spawnedPlayer.Teleport(model.spawnPoint.transform.position);
            spawnedPlayer.jumpState = PlayerController.JumpState.Grounded;
            // player.animator.SetBool("dead", false);
            spawnedPlayer.ControlEnabled = false;

            var ev = Simulation.Schedule<EnablePlayerInput>(2f);
            ev.Player = spawnedPlayer;
        }
    }
}