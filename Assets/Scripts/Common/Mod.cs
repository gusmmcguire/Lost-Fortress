using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModType
{
    DAMAGE,
    HEALTH,
    CRIT_CHANCE,
    CRIT_DAMAGE
}

[System.Serializable]
public class Mod
{
    [SerializeField] private ModType type;
    [SerializeField,LabelText("Percent Increase"), Range(0,100)] private float val;
    
    public float ModVal { get { return val * .01f; } }
    public ModType Type { get { return type; } }
}
