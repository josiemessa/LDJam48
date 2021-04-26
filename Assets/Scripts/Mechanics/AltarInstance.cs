using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using static Platformer.Core.Simulation;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AltarInstance : MonoBehaviour
{
    public SpawnPoint spawnPoint;
    public bool activatable;
    private GameObject _lastSeenPlayer;
    private readonly PlayerModel _model = GetModel<PlayerModel>();

    private void Update()
    {
        if (!activatable || !Input.GetButtonDown("Fire1")) return;

        var ev = Schedule<ActivateAltar>();
        ev.Player = _lastSeenPlayer;
        ev.SpawnPoint = spawnPoint.transform.position;
        EndAltarActivation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject;
        var playerCtrller = player.GetComponent<PlayerController>();
        if (playerCtrller != null && _model.ActivePlayer == playerCtrller)
        {
            if (playerCtrller.health.CanActivateAltar)
            {
                BeginAltarActivation(player);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) EndAltarActivation();
    }

    void BeginAltarActivation(GameObject player)
    {
        activatable = true;
        _lastSeenPlayer = player;
        Schedule<PlayerAltarCollision>();
    }

    void EndAltarActivation()
    {
        activatable = false;
        _lastSeenPlayer = null;
        Schedule<PlayerAltarCollisionEnded>();
    }
}