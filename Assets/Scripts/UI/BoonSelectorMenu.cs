using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BoonSelectorMenu : MonoBehaviour
{
	[SerializeField] GameObject _boonSelectionPrefab;
	[SerializeField] Boon[] _boons;

	// Start is called before the first frame update
	public void Initialize(PlayerStats stats)
	{
		int rand1, rand2, rand3;
		rand1 = Random.Range(0, _boons.Length);
		Instantiate(_boonSelectionPrefab, gameObject.transform).GetComponent<BoonSelectable>().Initialize(_boons[rand1], stats);
		if (_boons.Length > 1)
		{
			do { rand2 = Random.Range(0, _boons.Length); } while (rand1 == rand2);
			Instantiate(_boonSelectionPrefab, gameObject.transform).GetComponent<BoonSelectable>().Initialize(_boons[rand2], stats);
			if (_boons.Length > 2)
			{
				do { rand3 = Random.Range(0, _boons.Length); } while (rand1 == rand3 || rand2 == rand3);
				Instantiate(_boonSelectionPrefab, gameObject.transform).GetComponent<BoonSelectable>().Initialize(_boons[rand3], stats);
			}
		}
	}
}
