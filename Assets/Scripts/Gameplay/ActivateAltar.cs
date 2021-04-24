using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class ActivateAltar : Simulation.Event<ActivateAltar>
    {
        public GameObject Player;

        public override void Execute()
        {
            var spawnPoint = Simulation.GetModel<PlayerModel>().spawnPoint;
            var copy = Object.Instantiate(Player, spawnPoint.position, Quaternion.identity);

            var ev = Simulation.Schedule<PlayerSpawn>();
            ev.spawnedPlayer = copy.GetComponent<PlayerController>();
        }
    }
}