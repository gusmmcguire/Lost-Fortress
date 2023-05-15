using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[InlineProperty]
[LabelWidth(75)]
public class VariableReference<TValue, TAsset> where TAsset : Variable<TValue>
{
	[HorizontalGroup("Reference", MaxWidth = 100)]
	[ValueDropdown("valueList")]
	[HideLabel]
	[SerializeField]
	protected bool useValue = true;

	[ShowIf("useValue")]
	[HorizontalGroup("Reference")]
	[HideLabel]
	[SerializeField]
	protected TValue _value;
	
	[HideIf("useValue")]
	[HideLabel]
	[HorizontalGroup("Reference")]
	[InlineEditor]
	[SerializeField]
	protected TAsset AssetReference;

	private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
	{
		{"Value", true },
		{"Reference", false },
	};

	public TValue Value
	{
		get
		{
			if(AssetReference == null || useValue)
			{
				return _value;
			}
			else
			{
				return AssetReference.Value;
			}
		}
	}

	public static implicit operator TValue(VariableReference<TValue, TAsset> valueRef)
	{
		return valueRef.Value;
	}
}