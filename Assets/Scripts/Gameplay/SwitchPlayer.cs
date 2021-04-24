using System;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class SwitchPlayer : Simulation.Event<SwitchPlayer>
    {
        private PlayerModel model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            var playerList = model.players;
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i] != model.ActivePlayer)
                {
                    Debug.Log($"Player {i} was not active player");
                    continue;
                }
                Debug.Log($"Player {i} is active player");
                model.ActivePlayer.ControlEnabled = false;
                var newPlayer = i + 1 < playerList.Count ? playerList[i + 1] :  playerList[0];
                var ev = Simulation.Schedule<EnablePlayerInput>();
                ev.Player = newPlayer;
            }
        }
    }
}