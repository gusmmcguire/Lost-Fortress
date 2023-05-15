using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonPickup : MonoBehaviour
{
	[SerializeField] GameObject _selectionCanvasPrefab;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<BasePlayerController>(out var player))
		{
			Instantiate(_selectionCanvasPrefab).GetComponentInChildren<BoonSelectorMenu>().Initialize(player.Stats);
			Destroy(gameObject);
		}
	}
}
