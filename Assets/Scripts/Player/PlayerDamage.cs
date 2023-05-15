using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : Damage
{
	[SerializeField] PlayerStats stats;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(_checkTag) && collision.TryGetComponent<Health>(out var health))
		{
			var damage = stats.Damage;
			print("Damage Dealt:" + damage);
			DealDamage(health, damage, collision.transform.position);
		}
	}
}
