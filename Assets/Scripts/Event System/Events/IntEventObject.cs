using UnityEngine;
using UnityEngine.Events;

[ManageableEvent]
public class IntEventObject : EventSO
{
    [HideInInspector] public UnityEvent<int> OnEventRaised;

    public void Invoke(int obj)
	{
		if (OnEventRaised == null) return;
		OnEventRaised.Invoke(obj);
	}
}