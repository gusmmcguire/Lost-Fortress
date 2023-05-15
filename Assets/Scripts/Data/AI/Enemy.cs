using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class Enemy : XemblemScriptableObject
{
	[SerializeField] protected float _speed = 5;
	[SerializeField] protected float _distanceToAggro = 1;
	[SerializeField] protected float _distanceToAttack = 0.5f;
	[SerializeField] protected float _attackCooldown = 1f;

	protected Animator _animator;
	protected Rigidbody2D _rb;
	protected bool _isDead = false;
	protected bool _isAttacking;
	protected float _nextAttackTime;
	protected List<RaycastHit2D> results = new List<RaycastHit2D>();
	protected GameObject enemyObject;

	public abstract void OnAwake(GameObject incomingObject, GameObject[] attackAreas);
	public abstract void OnUpdate();
	public abstract void TriggerDamageAnimation();
	public abstract void TriggerAttackCollider();
	public abstract void OnDefeated();
	public abstract void FinishAttack();

}
