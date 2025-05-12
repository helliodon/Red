using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerPresenter: IDisposable
{
    private  IInputHandler inputHandler;
    private  IPlayerView playerView;
    private readonly float moveSpeed = 10f;
    private readonly float jumpForce = 10f;

    private IDisposable moveSubscription;
    private IDisposable jumpSubscription;
    private IDisposable collisionSubscription;
    private bool isGrounded;
    private bool doGroundCheck = true;

    [Inject]
    public void Construct(IPlayerView playerView, IInputHandler inputHandler)
    {
        this.inputHandler = inputHandler;
        this.playerView = playerView;
        Initialize();
    }

    private void Initialize()
    {
        SubscribeJump();
        SubscribeGroundCheck();
        SubscribeMove();
    }

    private void SubscribeMove()
    {
        // Continuous force application while moving
        moveSubscription = Observable.EveryUpdate()
            .Where(_ => inputHandler.MoveDirection.Value != 0) // Ensure only while a direction is active
            .Subscribe(_ =>
            {
                playerView.Rigidbody2D.AddForce(Vector3.right * inputHandler.MoveDirection.Value * moveSpeed);

                // Limit velocity
                if (playerView.Rigidbody2D.linearVelocity.magnitude > moveSpeed)
                {
                    playerView.Rigidbody2D.linearVelocity = playerView.Rigidbody2D.linearVelocity.normalized * moveSpeed;
                }
            });
    }

    private void SubscribeGroundCheck()
    {
        collisionSubscription = Observable.EveryUpdate()
            .Where(_ => playerView != null && playerView.CircleCollider2D != null && playerView.CircleCollider2D.IsTouchingLayers() && doGroundCheck) // check if ball is grounded
            .Subscribe(_ =>
            {
                if (!isGrounded)
                    isGrounded = true;
            });
    }

    private void SubscribeJump()
    {
        jumpSubscription = inputHandler.JumpPressed.Where(jump => jump && isGrounded).Subscribe(_ =>
        {
            isGrounded = false;
            doGroundCheck = false;
            Observable.TimerFrame(3) // Delay before checking ground state 
            .Subscribe(__ =>
            {
                doGroundCheck = true;
            });
            playerView.Rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        });
    }

    public void Dispose()
    {
        moveSubscription?.Dispose();
        jumpSubscription?.Dispose();
        collisionSubscription?.Dispose();
    }
}
