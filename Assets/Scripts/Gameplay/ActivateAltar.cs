using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class ActivateAltar : Simulation.Event<ActivateAltar>
    {
        public GameObject Player;
        public PlayerController PlayerController;
        private PlayerModel _model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            PlayerController.health.Decrement();
            PlayerController.ControlEnabled = false;
            var spawnPoint = Simulation.GetModel<PlayerModel>().spawnPoint;
            var copy = Object.Instantiate(Player, spawnPoint.position, Quaternion.identity);

            var ev = Simulation.Schedule<PlayerSpawn>();
            // TODO: is there any way of not having to get the component at this point?
            ev.spawnedPlayer = copy.GetComponent<PlayerController>();
        }
    }
}