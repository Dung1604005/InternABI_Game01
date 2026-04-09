using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class EventBus<T> where T: IEvent
{
    static readonly HashSet<EventBinding<T>> bindings = new HashSet<EventBinding<T>>();

    public static void Register(EventBinding<T> eventBinding) { bindings.Add(eventBinding);}

    public static void DeRegister(EventBinding<T> eventBinding) { bindings.Remove(eventBinding);}


    public static void Raise(T @event)
    {
        var snapShot = new HashSet<EventBinding<T>>(bindings);
        foreach(var binding in snapShot)
        {
            if (bindings.Contains(binding))
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();

            }
        }

    }

    static void Clear()
    {
        Debug.Log($"Clearing {typeof(T).Name} bindings");
        bindings.Clear();
    }
}
