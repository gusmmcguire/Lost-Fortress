using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManageableInput]
public class InputLayer : XemblemScriptableObject
{
	[FoldoutGroup("Input Debug")]
	[ReadOnly]
    public Vector2 movementInput;
	[FoldoutGroup("Input Debug")]
	[ReadOnly]
    public bool Attack;
	[FoldoutGroup("Input Debug")]
	[ReadOnly]
	public int direction;
}
