using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

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

        // internal Animator animator;
        readonly PlayerModel model = GetModel<PlayerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            _underControlIcon = transform.GetChild(0).gameObject;
            health = GetComponent<Health>();
            // audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            // animator = GetComponent<Animator>();
            model.Register(this);
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
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (m_stopJump)
            {
                m_stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // animator.SetBool("grounded", IsGrounded);
            // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
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