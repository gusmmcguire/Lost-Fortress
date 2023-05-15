using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonSelectable : MonoBehaviour
{
    Boon _boonToSelect;
    PlayerStats _stats;
 
    public void Initialize(Boon boon, PlayerStats stats)
    {
        _boonToSelect = boon;
        _stats = stats;
    }

    public void Select()
    {
        _stats.TakeBoon(_boonToSelect);
        Destroy(transform.parent.parent.gameObject);
    }
}
