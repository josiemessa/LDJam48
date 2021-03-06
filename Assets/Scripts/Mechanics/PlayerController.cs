using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Platformer.UI;
using TMPro;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject, IEquatable<PlayerController>
    {
        public int Id = 0;
        // public AudioClip jumpAudio;
        // public AudioClip respawnAudio;
        // public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;

        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        /// <summary>
        /// How much horizontal axis input is required to get the player to start moving,
        /// and conversely stop moving.
        /// </summary>
        public float moveThreshold = 0.1f;

        public JumpState jumpState = JumpState.Grounded;
        private bool m_stopJump;

        // TODO: make these private/internal or something as they auto-set themselves on startup
        [HideInInspector] public Collider2D collider2d;

        // [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public Health health;

        public bool ControlEnabled
        {
            get => _controlEnabled;
            set
            {
                _underControlIcon.SetActive(value);
                _controlEnabled = value;
            }
        }

        private GameObject _underControlIcon;
        private bool _controlEnabled = true;

        bool jump;
        Vector2 move;

        SpriteRenderer spriteRenderer;

        private Animator _animator;
        private readonly PlayerModel _model = GetModel<PlayerModel>();
        public TMP_Text healthText;
        public Image healthPanel;

        public Bounds Bounds => collider2d.bounds;

        public bool HasRecipe = false;
        public bool HasPotion = false;

        public bool HasCrystal
        {
            get => _hasCrystal;
            set
            {
                _crystalGiven = value;
                _hasCrystal = value;
            }
        }

        private bool _hasCrystal;

        private Panel _healthPanelName;

        private HUDModel _hudModel = GetModel<HUDModel>();
        private bool _crystalGiven = false;

        void Awake()
        {
            // set up player
            Id = _model.players.Count;
            _underControlIcon = transform.GetChild(0).gameObject;

            // set up health
            health = GetComponent<Health>();
            switch (Id)
            {
                case 0:
                    _healthPanelName = Panel.HealthDisplay1;
                    _hudModel.UIController.Display(Panel.HealthDisplay1);
                    healthText = _hudModel.UIController.healthText1;
                    healthPanel = _hudModel.UIController.healthPanel1.GetComponent<Image>();
                    break;
                case 1:
                    _healthPanelName = Panel.HealthDisplay2;
                    _hudModel.UIController.Display(Panel.HealthDisplay2);
                    healthText = _hudModel.UIController.healthText2;
                    healthPanel = _hudModel.UIController.healthPanel2.GetComponent<Image>();

                    health.CurrentHp = 1;
                    break;
                case 2:
                    _healthPanelName = Panel.HealthDisplay3;
                    _hudModel.UIController.Display(Panel.HealthDisplay3);
                    healthText = _hudModel.UIController.healthText3;
                    healthPanel = _hudModel.UIController.healthPanel3.GetComponent<Image>();

                    health.CurrentHp = 1;
                    break;
                default:
                    Debug.Log($"unknown player ID {Id}");
                    break;
            }

            healthText.text = $"{health.CurrentHp}";

            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            // audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            _model.Register(this);
        }

        protected override void Start()
        {
            health.playerController = this;
        }

        protected override void Update()
        {
            if (ControlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    m_stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }

                if (_crystalGiven)
                {
                    _hudModel.UIController.Display(Panel.MagicCrystal);
                    _crystalGiven = false;
                }
                if (Input.GetButtonDown("Consume") && HasPotion)
                {
                    health.CurrentHp++;
                    HasPotion = false;
                }
            }
            else
            {
                move.x = 0;
            }

            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    m_stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }

                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }

                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * _model.jumpModifier;
                jump = false;
            }
            else if (m_stopJump)
            {
                m_stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * _model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // animator.SetBool("grounded", IsGrounded);

            if (Mathf.Abs(move.x) < moveThreshold) move.x = 0;
            _animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void SelfDesctruct()
        {
            _hudModel.UIController.Hide(_healthPanelName);
            _model.DeRegister(this);
            Destroy(gameObject);
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        public bool Equals(PlayerController other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayerController) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(PlayerController left, PlayerController right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PlayerController left, PlayerController right)
        {
            return !Equals(left, right);
        }
    }
}