using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public enum Layers
    {
        Default,
        TransparentFX,
        IgnoreRaycast,
        Ground,
        Water,
        UI, 
        Walls
    }
    private const int MaxJumps = 2;

    private const int SpringJump = 15;

    private bool canDash = true;

    private bool isDashing = false;

    private float deathAnimationDuration = 1.5f;

    private float dashSpeed = 17.3f;

    private float dashDuration = 0.2f;

    private float dashCooldown = 1.2f;

    private bool canMove = true;

    private bool isPaused = false;

    public float Speed = 5;

    public float Jump = 6f;

    public float SecondJump = 6;

    public float JumpCount = 0;

    public Rigidbody2D Rigidbody;

    private Collider2D _collider;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    public TrailRenderer TrailRenderer;

    public GameObject PauseMenu;

    private bool _isGrounded;

    public LayerMask Ground;

    public LayerMask Walls;

    public LayerMask Death;

    void Start()
    {
        this.Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        this.TrailRenderer = gameObject.GetComponent<TrailRenderer>();
        this._collider = gameObject.GetComponent<Collider2D>();
        this._spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this._animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && this.canDash && this.canMove)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing && canMove)
        {
            UpdatePlayerPosition();
        }
    }
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        this._animator.SetBool("IsDashing", true);

        Vector2 dashDirection = this._spriteRenderer.flipX ? Vector2.left : Vector2.right;
        this.Rigidbody.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
        this.TrailRenderer.emitting = true;

        yield return new WaitForSeconds(this.dashDuration);

        this.Rigidbody.velocity = new Vector2(0, this.Rigidbody.velocity.y);
        this.TrailRenderer.emitting = false;
        isDashing = false;
        this._animator.SetBool("IsDashing", false);

        yield return new WaitForSeconds(this.dashCooldown);

        this.canDash = true;
    }

    private void UpdatePlayerPosition()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");

        if (moveInput > 0)
        {
            this.Rigidbody.velocity = new Vector2(Speed, this.Rigidbody.velocity.y);
            this._spriteRenderer.flipX = false;
            this._animator.SetBool("IsRunning", true);
        }
        else if (moveInput < 0)
        {
            this.Rigidbody.velocity = new Vector2(-Speed, this.Rigidbody.velocity.y);
            this._spriteRenderer.flipX = true;
            this._animator.SetBool("IsRunning", true);
        }
        else if (moveInput == 0 && _collider.IsTouchingLayers(Ground))
        {
            this._animator.SetBool("IsRunning", false);
        }
        if (jumpInput > 0 && _collider.IsTouchingLayers(Ground))
        {
            this.Rigidbody.velocity = new Vector2(this.Rigidbody.velocity.x, Jump);
            this._animator.SetBool("IsJumping", true);
            this.JumpCount++;
        }
        //else if (Input.GetKeyDown(KeyCode.Space) && this.JumpCount == 1)
        //{
        //    Debug.Log($"Second Jump of {JumpCount}");
        //    this.Rigidbody.velocity = new Vector2(this.Rigidbody.velocity.x, SecondJump);
        //    this.JumpCount++;
        //}
        else if (_collider.IsTouchingLayers(Walls) && !_collider.IsTouchingLayers(Ground))
        {
            if (jumpInput > 0)
            {
                this.Rigidbody.velocity = new Vector2(this.Rigidbody.velocity.x, Jump);
            }
            this._animator.SetBool("IsClimbing", true);
        }
        else if (this.Rigidbody.velocity.y < 0)
        {
            this._animator.SetBool("IsFalling", true);
        }
        else
        {
            this._animator.SetBool("IsFalling", false);
        }
        if (!_collider.IsTouchingLayers(Walls))
        {
            this._animator.SetBool("IsClimbing", false);
        }
        if (_collider.IsTouchingLayers(Ground) && jumpInput == 0)
        {
            this._animator.SetBool("IsJumping", false);
            this.JumpCount = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spring"))
        {
            this.Rigidbody.velocity = new Vector2(this.Rigidbody.velocity.x, SpringJump);
            this._animator.SetBool("IsJumping", true);
            Animator springAnimator = collision.gameObject.GetComponent<Animator>();
            springAnimator.Play("SpringAnimation");
        }
        if(Rigidbody.IsTouchingLayers(Death))
        {
            StartCoroutine(AnimateDeath());
        }
    }

    public void SwitchPause()
    {
        isPaused = !isPaused;
        PauseMenu.SetActive(isPaused);
        canMove = !isPaused;
    }

    private IEnumerator AnimateDeath()
    {
        _animator.Play("Death");
        canMove = false;

        yield return new WaitForSeconds(deathAnimationDuration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canMove = true;
    }
}
