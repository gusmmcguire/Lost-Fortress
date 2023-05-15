using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManageableEnemy]
public class SimpleFollowAI : Enemy
{
	GameObject attackArea;
	MeleePlayerController _playerController;

	public override void OnAwake(GameObject incomingObject, GameObject[] attackAreas)
	{
		attackArea = attackAreas[0];
		enemyObject = incomingObject;
		_animator = incomingObject.GetComponent<Animator>();
		_playerController = FindObjectOfType<MeleePlayerController>();
		_rb = incomingObject.GetComponent<Rigidbody2D>();
		_nextAttackTime = Time.time + _attackCooldown;
		_isDead = false;
	}

	public override void TriggerDamageAnimation()
	{
		_animator.SetTrigger("damage");
	}

	public override void OnUpdate()
	{
		if (_isDead || _playerController == null)
		{
			_animator.SetBool("IsMoving", false);
			return;
		}

		Vector3 playerPosition = _playerController.transform.position;
		Vector2 distance = playerPosition - enemyObject.transform.position;

		if (distance.magnitude < _distanceToAttack)
		{
			if (!_isAttacking && Time.time >= _nextAttackTime)
				TriggerAttackAnimation();
		}
		else if (distance.magnitude < _distanceToAggro)
		{
			Move(distance.normalized);
			return;
		}
		_animator.SetBool("IsMoving", false);
	}

	public void TriggerAttackAnimation()
	{
		_isAttacking = true;
		_animator.SetTrigger("attack");
	}

	public override void FinishAttack()
	{
		attackArea.SetActive(false);
		_isAttacking = false;
	}

	private void Move(Vector2 direction)
	{
		int count = _rb.Cast(direction, results, _speed * Time.deltaTime);
		if (count != 0) return;

		_rb.MovePosition(enemyObject.transform.position + (Vector3)(direction * _speed * Time.deltaTime));

		_animator.SetBool("IsMoving", true);
	}

	public override void OnDefeated()
	{
		_isDead = true;
		_animator.SetTrigger("defeated");
	}

	public void RemoveEnemy()
	{
		Destroy(enemyObject);
	}

	public override void TriggerAttackCollider()
	{
		_nextAttackTime = Time.time + _attackCooldown;
		attackArea.SetActive(true);
	}
}
