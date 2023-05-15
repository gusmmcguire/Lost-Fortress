using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Extend your player from this interface to allow for the handling of an empty animation tag, an error will be thrown otherwise
/// </summary>
public interface IPaperDoll
{
	void HandleEmptyAnimationTransitionTag();
}


public class PaperDollAnimator : MonoBehaviour
{

	[SerializeField] PaperDollAnimationDictionary _animations;

	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _behindSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _baseSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _outfitSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _cloakSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _faceSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _hairSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _hatSpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _toolASpriteRenderer;
	[SerializeField, FoldoutGroup("Sprite Renderers"), ChildGameObjectsOnly] SpriteRenderer _toolBSpriteRenderer;

	[SerializeField] UnityEvent _endOneTimeAnimations;

	PaperDollAnimationClip _curAnimation;

	int _lastDirection = 2;

	private void Start()
	{
		SetAnimation("Idle");
	}

	private void Update()
	{
		_curAnimation.Animate();

		if (_behindSpriteRenderer != null)
		{
			_behindSpriteRenderer.sprite = _curAnimation.CurBehindSprite.AnimationFrame;
		}
		if (_baseSpriteRenderer != null)
		{
			_baseSpriteRenderer.sprite = _curAnimation.CurBaseSprite.AnimationFrame;
		}
		if (_outfitSpriteRenderer != null)
		{
			_outfitSpriteRenderer.sprite = _curAnimation.CurOutfitSprite.AnimationFrame;
		}
		if (_cloakSpriteRenderer != null)
		{
			_cloakSpriteRenderer.sprite = _curAnimation.CurCloakSprite.AnimationFrame;
		}
		if (_faceSpriteRenderer != null)
		{
			_faceSpriteRenderer.sprite = _curAnimation.CurFaceSprite.AnimationFrame;
		}
		if (_hairSpriteRenderer != null)
		{
			_hairSpriteRenderer.sprite = _curAnimation.CurHairSprite.AnimationFrame;
		}
		if (_hatSpriteRenderer != null)
		{
			_hatSpriteRenderer.sprite = _curAnimation.CurHatSprite.AnimationFrame;
		}
		if (_toolASpriteRenderer != null)
		{
			_toolASpriteRenderer.sprite = _curAnimation.CurToolASprite.AnimationFrame;
			if (_curAnimation.CurToolASprite.IsBehind) _toolASpriteRenderer.sortingOrder = -1;
			else _toolASpriteRenderer.sortingOrder = 7;
		}
		if (_toolBSpriteRenderer != null)
		{
			_toolBSpriteRenderer.sprite = _curAnimation.CurToolBSprite.AnimationFrame;
			if (_curAnimation.CurToolBSprite.IsBehind) _toolBSpriteRenderer.sortingOrder = -2;
			else _toolBSpriteRenderer.sortingOrder = 8;
		}

		if (_curAnimation.IsDone)
		{
			SetAnimation(_curAnimation.TagToTransitionTo);
			_endOneTimeAnimations?.Invoke();
		}
	}

	/// <summary>
	/// Sets a new animation from the animation dictionary contained in the animator.
	/// </summary>
	/// <param name="tag">The Key in the animation dictionary to retrieve and set</param>
	public void SetAnimation(string tag)
	{
		if (_curAnimation != null && _curAnimation.IsOneTime && !_curAnimation.IsDone) return;

		if (_animations.TryGetAnimationByTag(tag, out var clip))
		{
			if (_curAnimation == clip) return;
			_curAnimation = clip;
			_curAnimation.Initialize();
		}
		else
		{
			if (string.IsNullOrEmpty(tag))
			{
				foreach (var paperDoll in GetComponents<IPaperDoll>())
				{
					paperDoll.HandleEmptyAnimationTransitionTag();
				}

				foreach (var paperDoll in GetComponentsInChildren<IPaperDoll>())
				{
					paperDoll.HandleEmptyAnimationTransitionTag();
				}
			}
			else
				Debug.LogError($"Key: {tag}, does not exist in the animations dictionary");
		}
	}

	/// <summary>
	/// Sets the direction of the animation based on the movement of the character.
	/// </summary>
	/// <param name="input">A Vector2 representation of the direction of movement of the character</param>
	public void SetDirection(Vector2 input)
	{
		_curAnimation.SetDirection(GetDirection(input));
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

		bool isLeft = 135 < angle && angle < 225;
		bool isDown = 225 < angle && angle < 315;
		bool isRight = 315 < angle || angle < 45;
		bool isUp = 45 < angle && angle < 135;

		direction = isLeft ? 3 :
			isDown ? 2 :
			isRight ? 1 :
			isUp ? 0 :
			-1;
		_lastDirection = direction;

		return direction;
	}
}
