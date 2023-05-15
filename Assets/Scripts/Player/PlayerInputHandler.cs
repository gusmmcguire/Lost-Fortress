using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField,InlineEditor] InputLayer _inputLayer;

	PlayerControls _playerControls;

	int _lastDirection = -1;

	//private void Awake()
	//{
	//	_playerControls = new PlayerControls();
	//}

	//private void OnEnable()
	//{
	//	_playerControls.Enable();
		
	//	_playerControls.Gameplay.Movement.started += OnMovement;
	//	_playerControls.Gameplay.Movement.performed += OnMovement;
	//	_playerControls.Gameplay.Movement.canceled += OnMovement;
	//	_playerControls.Gameplay.Attack.started += OnAttack;
	//}

	//private void OnDisable()
	//{
	//	_playerControls.Disable();
	//}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (context.phase != InputActionPhase.Started) return;
		_inputLayer.Attack = context.ReadValueAsButton();
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		Vector2 input = context.ReadValue<Vector2>();
		_inputLayer.movementInput = input;
		_inputLayer.direction = (GetDirection(input));
	}

	private int GetDirection(Vector2 input)
	{
		int direction = -1;
		if (input == Vector2.zero)
		{
			return _lastDirection;
		}

		float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
		if (angle < 0) angle = angle + 360;

		bool isLeft =  135 < angle && angle < 225;
		bool isDown =  225 < angle && angle < 315;
		bool isRight = 315 < angle || angle < 45;
		bool isUp =    45 < angle && angle < 135;

		direction = isLeft ? 3 :
			isDown ? 2 :
			isRight ? 1 :
			isUp ? 0 :
			-1;
		_lastDirection = direction;

		return direction;
	}
}
