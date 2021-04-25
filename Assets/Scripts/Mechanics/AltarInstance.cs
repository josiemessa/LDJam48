using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using static Platformer.Core.Simulation;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AltarInstance : MonoBehaviour
{
    public bool activatable;
    private GameObject _lastSeenPlayer;
    private PlayerController _lastSeenPlayerController;
    private PlayerModel m_model = GetModel<PlayerModel>();

    private void Update()
    {
        if (!activatable || !Input.GetButtonDown("Fire1")) return;
        var ev = Schedule<ActivateAltar>();
        ev.Player = _lastSeenPlayer;
        ev.PlayerController = _lastSeenPlayerController;
        EndAltarActivation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject;
        var playerCtrller = player.GetComponent<PlayerController>();
        if (playerCtrller != null && m_model.ActivePlayer == playerCtrller && playerCtrller.health.CanActivateAltar) BeginAltarActivation(player, playerCtrller);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) EndAltarActivation();
    }

    void BeginAltarActivation(GameObject player, PlayerController playerController)
    {
        activatable = true;
        _lastSeenPlayer = player;
        _lastSeenPlayerController = playerController;
        Schedule<PlayerAltarCollision>();
    }

    void EndAltarActivation()
    {
        activatable = false;
        _lastSeenPlayer = null;
        _lastSeenPlayerController = null;
        Schedule<PlayerAltarCollisionEnded>();

    }
}
