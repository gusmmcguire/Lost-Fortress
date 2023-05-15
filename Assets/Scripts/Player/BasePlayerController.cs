using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour, IPaperDoll
{
	[SerializeField] protected PlayerStats playerStats;
	[SerializeField] protected InputLayer inputLayer;
	[SerializeField, TabGroup("Movement Properties")] protected float moveSpeed = 700f;
	[SerializeField, TabGroup("Movement Properties")] protected float maxSpeed = 2.2f;
	[SerializeField, TabGroup("Movement Properties")] protected float idleFriction = 0.9f;

	protected Vector2 movementInput;
	protected Rigidbody2D rb;
	protected PaperDollAnimator animator;

	protected bool canMove = true;

	public PlayerStats Stats { get { return playerStats; } }

	public bool IsMoving
	{
		set
		{
			if (value) animator.SetAnimation("Walk");
			else animator.SetAnimation("Idle");
		}
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<PaperDollAnimator>();
		playerStats.Intialize(gameObject);
	}

	private void Update()
	{
		OnMovement(inputLayer.movementInput);
		if (inputLayer.Attack)
		{
			OnAttack();
		}

	}

	private void FixedUpdate()
	{
		if (canMove && movementInput != Vector2.zero)
		{
			rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
			IsMoving = true;
		}
		else
		{
			rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
			IsMoving = false;
		}
	}

	private void OnMovement(Vector2 movementValue)
	{
		movementInput = movementValue;
		animator.SetDirection(movementInput);
	}

	private void OnAttack()
	{
		inputLayer.Attack = false;
		animator.SetAnimation("Attack");
		StartAttack();
	}

	public abstract void StartAttack();

	public abstract void EndAttack();

	protected void LockMovement()
	{
		canMove = false;
	}

	protected void UnlockMovement()
	{
		canMove = true;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	public void HandleEmptyAnimationTransitionTag()
	{
		Destroy();
	}

	public void Die()
	{
		animator.SetAnimation("Death");
		canMove = false;
	}
}
