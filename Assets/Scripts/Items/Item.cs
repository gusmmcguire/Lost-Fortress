using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Item : ScriptableObject
{
	[BoxGroup("Basic Info")]
	[HideLabel]
	[PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 100)]
	[HorizontalGroup("Basic Info/Horz Layer 1", width:.25f)]
	public Sprite ItemIcon;

	[BoxGroup("Basic Info")]
	[LabelWidth(80)]
	[LabelText("Display Name")]
	[HorizontalGroup("Basic Info/Horz Layer 1")]
	[VerticalGroup("Basic Info/Horz Layer 1/Vert Layer 1")]
	public string ItemName;

}
