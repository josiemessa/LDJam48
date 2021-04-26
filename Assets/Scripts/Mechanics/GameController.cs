using System;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlayerModel model = Simulation.GetModel<PlayerModel>();

        // This is here so it can be edited in the editor
        public HUDModel hudModel = Simulation.GetModel<HUDModel>();

        public PressurePlateController plateController;

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        // private void Awake()
        // {
        //     var player = FindObjectOfType<PlayerController>();
        //     if (player == null)
        //     {
        //         throw new Exception("could not find player in scene");
        //     }
        //
        //     model.ActivePlayer = player;
        // }

        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Switch player");
                Simulation.Schedule<SwitchBody>();
            }
            if (Instance == this) Simulation.Tick();
        }
    }
}