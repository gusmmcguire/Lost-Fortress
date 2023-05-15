using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Agent : MonoBehaviour
{
    [SerializeField, InlineEditor] Enemy _AIScriptableObject;
	[SerializeField] GameObject[] _attackAreas;

	private void Start()
	{
		_AIScriptableObject.OnAwake(gameObject, _attackAreas);
	}

	private void Update()
	{
		_AIScriptableObject.OnUpdate();
	}

	public void TriggerAttackCollider()
	{
		_AIScriptableObject.TriggerAttackCollider();
	}

	public void FinishAttack()
	{
		_AIScriptableObject.FinishAttack();
	}

	public void TriggerDamageAnimation(bool isDeath) {
		if(isDeath) _AIScriptableObject.OnDefeated();
		_AIScriptableObject.TriggerDamageAnimation(); 
	}

	public void OnDeath()
	{
		Destroy(gameObject);
	}
}
