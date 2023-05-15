using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[ManageableEvent]
public class VoidEventObject : EventSO
{
	[HideInInspector] public UnityEvent OnEventRaised;

    public void Invoke()
	{
		if (OnEventRaised == null) return;
		OnEventRaised.Invoke();
	}
}
