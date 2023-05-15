using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerInputJoiner : MonoBehaviour
{
    PlayerInputManager _manager;

	private void Awake()
	{
		_manager = GetComponent<PlayerInputManager>();
	}
}
