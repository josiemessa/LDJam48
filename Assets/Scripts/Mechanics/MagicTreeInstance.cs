using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;
using UnityEngine;

public class MagicTreeInstance : MonoBehaviour
{
    private HUDModel _hudModel = Simulation.GetModel<HUDModel>();
    private PlayerModel _model = Simulation.GetModel<PlayerModel>();
    private bool _treeActive = false;
    private PlayerController _lastSeenPlayer;

    private void Update()
    {
        if (!_treeActive || !Input.GetButtonDown("Fire1")) return;
        if (_lastSeenPlayer.HasRecipe)
        {
            var ev = Simulation.Schedule<CraftPotion>();
            ev.Player = _lastSeenPlayer;
        }
        else
        {
            _hudModel.UIController.Display(Panel.MagicTree);
        }

        EndInteraction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null || _model.ActivePlayer != player) return;

        var ui = _hudModel.UIController;
        if (player.HasRecipe) ui.interactionHintText.text = "Click to Craft Potion";
        ui.Display(Panel.InteractionHint);
        _treeActive = true;
        _lastSeenPlayer = player;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null || _model.ActivePlayer != player) return;

        EndInteraction();
    }

    private void EndInteraction()
    {
        var ui = _hudModel.UIController;
        ui.ResetInteractionText();
        ui.Hide(Panel.InteractionHint);
        _treeActive = false;
        _lastSeenPlayer = null;
    }
}