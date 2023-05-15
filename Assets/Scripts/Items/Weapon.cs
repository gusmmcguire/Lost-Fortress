using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]

public class Weapon : Item
{
	[BoxGroup("Weapon Info")]
	[LabelWidth(100)]
	public float Damage;
	
	[BoxGroup("Weapon Info")]
	[LabelWidth(100)]
	public float AttackCooldown;
}
