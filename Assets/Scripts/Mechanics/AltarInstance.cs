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
    private GameObject m_lastSeenPlayer;
    private PlayerModel m_model = Simulation.GetModel<PlayerModel>();

    private void Update()
    {
        if (!activatable || !Input.GetButtonDown("Fire1")) return;
        var ev = Schedule<ActivateAltar>();
        ev.Player = m_lastSeenPlayer;
        EndAltarActivation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject;
        var playerCtrller = player.GetComponent<PlayerController>();
        if (playerCtrller != null && m_model.ActivePlayer == playerCtrller) BeginAltarActivation(player);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) EndAltarActivation();
    }

    void BeginAltarActivation(GameObject player)
    {
        activatable = true;
        m_lastSeenPlayer = player;
        Schedule<PlayerAltarCollision>();
    }

    void EndAltarActivation()
    {
        activatable = false;
        m_lastSeenPlayer = null;
        Schedule<PlayerAltarCollisionEnded>();

    }
}
