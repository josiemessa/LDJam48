using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class ActivateAltar : Simulation.Event<ActivateAltar>
    {
        public GameObject Player;
        public Vector3 SpawnPoint;

        private PlayerModel _model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            _model.ActivePlayer.health.CurrentHp--;
            _model.ActivePlayer.ControlEnabled = false;
            _model.ActivePlayer.healthPanel.color = Color.white;

            var model = Simulation.GetModel<PlayerModel>();
            var copy = Object.Instantiate(Player, SpawnPoint, Quaternion.identity);
            var newController = copy.GetComponent<PlayerController>();
            model.ActivePlayer = newController;
            newController.health.CurrentHp = 1;

            var ev = Simulation.Schedule<PlayerSpawn>();
            ev.spawnedPlayer = newController;
            // TODO: is there any way of not having to get the component at this point?
        }
    }
}