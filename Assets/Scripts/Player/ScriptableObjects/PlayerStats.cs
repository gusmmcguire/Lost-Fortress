using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : XemblemScriptableObject
{ 
	/// <summary>
	/// Damage = Base Damage
	///			x (1 + sum of Crit damage)
	///			x (1 + sum of Reg damage)
	/// </summary>
	/// <returns>Current damage with all modifications</returns>
	public float Damage { get { return _baseDamage * CalculateCrit() * CalculateBonusDamage(); } }

	[SerializeField] private FloatReference _baseHealth;
	[SerializeField] private FloatReference _baseDamage;
	[SerializeField] private FloatReference _baseCritChance;

	[SerializeField] List<Boon> _boons = new List<Boon>();
	Health _runTimeHealth;

	public void Intialize(GameObject playerObject)
	{
		_runTimeHealth = playerObject.GetComponent<Health>();

		if (_runTimeHealth == null) Debug.LogError("No Health on Player Object");
	}
	public void TakeBoon(Boon boon)
	{
		_boons.Add(boon);
		if(boon.Type == ModType.HEALTH)
		{
			_runTimeHealth.AddHealth(_baseHealth * boon.ChangeVal);
		}
	}
	public void PurgeBoon(Boon boon)
	{
		_boons.Remove(boon);
	}

	private float CalculateBonusDamage()
	{
		float total = 1;
		foreach (Boon boon in _boons.Where(x => x.Type == ModType.DAMAGE))
		{
			total += boon.ChangeVal;
		}
		return total;
	}
	private float CalculateCrit()
	{
		float crit = 1;
		if (ShouldCrit())
		{
			crit = 2;
			Debug.Log("CRIT!");
			foreach (Boon boon in _boons.Where(x => x.Type == ModType.CRIT_DAMAGE))
			{
				crit += boon.ChangeVal;
			}
		}
		return crit;
	}
	private bool ShouldCrit()
	{
		float chance = CalculateCritChance();
		float rand1 = Random.value;
		float rand2 = Random.value;

		return (rand1 + rand2) * 0.5f <= chance;
	}
	private float CalculateCritChance()
	{
		float chance = _baseCritChance;

		foreach (var boon in _boons.Where(x => x.Type == ModType.CRIT_CHANCE))
		{
			chance += boon.ChangeVal;
		}

		return chance;
	}
}
