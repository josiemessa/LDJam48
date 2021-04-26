using System;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class SwitchBody : Simulation.Event<SwitchBody>
    {
        private PlayerModel model = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            var playerList = model.players;
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i] != model.ActivePlayer) continue;
                var newPlayer = i + 1 < playerList.Count ? playerList[i + 1] :  playerList[0];
                var ev = Simulation.Schedule<SetActiveBody>();
                ev.Player = newPlayer;
            }
        }
    }
}