using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageableScriptableObjectAttribute : Attribute {}

public class ManageableEventAttribute : ManageableScriptableObjectAttribute { }
public class ManageableVariableAttribute : ManageableScriptableObjectAttribute { }
public class ManageableInputAttribute : ManageableScriptableObjectAttribute { }
public class ManageableAnimationLayerAttribute : ManageableScriptableObjectAttribute { }
public class ManageableAnimationSetAttribute : ManageableScriptableObjectAttribute { }
public class ManageableAnimationDictionaryAttribute : ManageableScriptableObjectAttribute { }
public class ManageableEnemyAttribute : ManageableScriptableObjectAttribute { }

public class SelectableBoonAttribute : Attribute {}