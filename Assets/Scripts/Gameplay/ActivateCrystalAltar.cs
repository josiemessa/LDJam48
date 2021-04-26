using System.Collections.Generic;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class ActivateCrystalAltar : Simulation.Event<ActivateCrystalAltar>
    {
        public SpawnPoint SpawnPoint;
        private PlayerModel _model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            List<int> playersToRemove = new List<int>();
            for (int i = 0; i < _model.players.Count; i++)
            {
                if (_model.players[i] != _model.ActivePlayer) playersToRemove.Add(i);

            }

            foreach (var i in playersToRemove)
            {
                var player = _model.players[i];

                if (player.HasPotion) _model.ActivePlayer.HasPotion = true;

                _model.ActivePlayer.health.CurrentHp += player.health.CurrentHp;
                player.SelfDesctruct();
            }

            _model.ActivePlayer.Teleport(SpawnPoint.transform.position);
        }
    }
}