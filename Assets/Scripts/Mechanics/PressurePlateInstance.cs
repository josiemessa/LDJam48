using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;

public class PressurePlateInstance : MonoBehaviour
{
    public PressurePlateController PlateController;
    private int _ticks;
    private int _elapsedTickInterval = 0;
    private SpriteRenderer _spriteRenderer;

    public Color colour = new Color(57, 195, 181);

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int Ticks
    {
        get => _ticks;
        set
        {
            _ticks = value < 0 ? 0 : value;
            _elapsedTickInterval = 0;
            if (_ticks != PlateController.tickThreshold) return;
            var ev = Simulation.Schedule<PressurePlateCompleted>();
            ev.Instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) OnPressurePlate(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) OnPressurePlate(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() == null) return;
        _spriteRenderer.color = Color.white;

        var ev = Simulation.Schedule<PressurePlateDeactivated>();
        ev.Instance = this;
    }

    private void OnPressurePlate(bool force)
    {
        _spriteRenderer.color = colour;

        if (!force)
        {
            _elapsedTickInterval++;
            if (_elapsedTickInterval < PlateController.tickInterval)
            {
                return;
            }
        }
        var ev = Simulation.Schedule<PressurePlateActive>();
        ev.Instance = this;
    }
}
