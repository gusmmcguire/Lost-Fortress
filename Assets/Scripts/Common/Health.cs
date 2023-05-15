using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Health : MonoBehaviour
{
	[SerializeField] FloatReference _maxHealth;
	[SerializeField, ReadOnly, FoldoutGroup("Debug")] float _curHealth;

	[HideInInspector] public float _runtimeMaxHealth;

	private void Awake()
	{
		_curHealth = _maxHealth;
		_runtimeMaxHealth = _maxHealth;
	}

	public void DealDamage(float damage)
	{
		_curHealth -= damage;
		if(TryGetComponent<AI_Agent>(out var ai))
		{
			ai.TriggerDamageAnimation(_curHealth <= 0);
		}
		else if (_curHealth <= 0 && TryGetComponent<BasePlayerController>(out var player))
		{
			player.Die();
		}
	}

	public void AddHealth(float health)
	{
		_curHealth += health;
		_runtimeMaxHealth += health;
	}
}
