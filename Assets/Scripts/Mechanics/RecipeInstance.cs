using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.UI;
using UnityEngine;

public class RecipeInstance : MonoBehaviour
{
    private HUDModel _hudModel = Simulation.GetModel<HUDModel>();
    private PlayerModel _model = Simulation.GetModel<PlayerModel>();
    private bool _active = false;

    private void Update()
    {
        if (!_active || !Input.GetButtonDown("Fire1")) return;

        _hudModel.UIController.Display(Panel.Recipe);
        _model.players.ForEach(player => player.HasRecipe = true);

        _active = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null || _model.ActivePlayer != player) return;

        _hudModel.UIController.Display(Panel.InteractionHint);
        _active = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //only execute OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null || _model.ActivePlayer != player) return;
        _hudModel.UIController.Hide(Panel.InteractionHint);
        _active = false;
    }
}