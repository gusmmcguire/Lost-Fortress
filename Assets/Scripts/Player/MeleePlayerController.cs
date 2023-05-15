using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleePlayerController : BasePlayerController
{
	[SerializeField,TabGroup("Attack Properties"), ChildGameObjectsOnly] GameObject swordAttackUp;
	[SerializeField, TabGroup("Attack Properties"), ChildGameObjectsOnly] GameObject swordAttackRight;
	[SerializeField,TabGroup("Attack Properties"), ChildGameObjectsOnly] GameObject swordAttackDown;
	[SerializeField, TabGroup("Attack Properties"), ChildGameObjectsOnly] GameObject swordAttackLeft;	

	public override void StartAttack()
	{
		LockMovement();
		if (inputLayer.direction == 0)
			swordAttackUp.SetActive(true);
		else if(inputLayer.direction == 1)
			swordAttackRight.SetActive(true);
		else if(inputLayer.direction == 2)
			swordAttackDown.SetActive(true);
		else if (inputLayer.direction == 3)
			swordAttackLeft.SetActive(true);
	}

	public override void EndAttack()
	{
		UnlockMovement();
		swordAttackLeft.SetActive(false);
		swordAttackRight.SetActive(false);
		swordAttackDown.SetActive(false);
		swordAttackUp.SetActive(false);
	}

	
}
