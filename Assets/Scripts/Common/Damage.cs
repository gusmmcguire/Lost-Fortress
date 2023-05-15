using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	[SerializeField] protected string _checkTag;
	[SerializeField, ShowIf("@!IsPlayer()")] FloatReference _damage;
	[SerializeField] protected GameObject _damageVisPrefab;


	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(_checkTag) && collision.TryGetComponent<Health>(out var health))
		{
			DealDamage(health, _damage, collision.transform.position);
		}
	}
	
	protected void DealDamage(Health health, float damage, Vector3 collisionPoint)
	{
		health.DealDamage(damage);
		Instantiate(_damageVisPrefab, collisionPoint, Quaternion.identity).GetComponent<DamageVisual>().Initialize(damage, Color.white, IsPlayer() ? Color.yellow : Color.red);
	}

	private bool IsPlayer()
	{
		return typeof(PlayerDamage).IsAssignableFrom(GetType());
	}
}
