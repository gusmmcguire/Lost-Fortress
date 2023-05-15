using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[ManageableVariable]
public class Vector2Variable : Variable<Vector2> { }

[System.Serializable]
public class Vector2Reference : VariableReference<Vector2, Vector2Variable> {}
