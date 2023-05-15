[ManageableVariable]
public class IntVariable : Variable<int> { }

[System.Serializable]
public class IntReference : VariableReference<int, IntVariable>{}
