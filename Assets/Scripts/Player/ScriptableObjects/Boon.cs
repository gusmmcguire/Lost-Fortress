using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Boon : XemblemScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Mod mod;

    public string Name { get { return _name; } }
    public ModType Type { get { return mod.Type; } }
    public float ChangeVal { get { return mod.ModVal; } }
}
