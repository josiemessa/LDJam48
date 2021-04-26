using System;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using TMPro;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => CurrentHp > 0;

        /// <summary>
        /// Indicates whether the player has enough HP to be able to activate the altar
        /// </summary>
        public bool CanActivateAltar => CurrentHp > 1;

        public PlayerController playerController;

        public int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = value;
                playerController.healthText.text = $"{_currentHp}";
                if (_currentHp == 0)
                {
                    var ev = Schedule<HealthIsZero>();
                    ev.health = this;
                }
            }
        }
        private int _currentHp = 2;

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            CurrentHp = 0;
        }
    }
}