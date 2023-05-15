using UnityEngine;
using UnityEngine.Events;

[ManageableEvent]
public class Vector2EventObject : EventSO
{
    [HideInInspector] public UnityEvent<Vector2> OnEventRaised;

    public void Invoke(Vector2 obj)
	{
		if (OnEventRaised == null) return;
		OnEventRaised.Invoke(obj);
	}
}