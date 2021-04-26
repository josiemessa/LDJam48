using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;
using static Platformer.Core.Simulation;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CrystalAltarInstance : MonoBehaviour
{
    public SpawnPoint SpawnPoint;
    public bool activatable;
    private PlayerController _lastSeenPlayer;
    private PlayerModel _model = GetModel<PlayerModel>();
    private HUDModel _hudModel = GetModel<HUDModel>();

    private void Update()
    {
        if (!activatable || !Input.GetButtonDown("Fire1") || !_lastSeenPlayer.HasCrystal) return;
        var ev = Schedule<ActivateCrystalAltar>();
        ev.SpawnPoint = SpawnPoint;
        _lastSeenPlayer.HasCrystal = false;
        EndInteraction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var playerCtrller = other.gameObject.GetComponent<PlayerController>();
        if (playerCtrller != null && _model.ActivePlayer == playerCtrller && playerCtrller.HasCrystal)
        {
            activatable = true;
            _lastSeenPlayer = playerCtrller;
            _hudModel.UIController.interactionHintText.text = "Drop crystal on altar";
            _hudModel.UIController.Display(Panel.InteractionHint);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var playerCtrller = other.gameObject.GetComponent<PlayerController>();
        if (playerCtrller != null && _model.ActivePlayer == playerCtrller && playerCtrller.HasCrystal)
        {
            activatable = true;
            _lastSeenPlayer = playerCtrller;
            _hudModel.UIController.interactionHintText.text = "Drop crystal on altar";
            _hudModel.UIController.Display(Panel.InteractionHint);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            EndInteraction();
        }
    }

    private void EndInteraction()
    {
        _lastSeenPlayer = null;
        activatable = false;
        _hudModel.UIController.ResetInteractionText();
        _hudModel.UIController.Hide(Panel.InteractionHint);
    }
}