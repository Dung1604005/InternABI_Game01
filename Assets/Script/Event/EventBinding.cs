using System;
using UnityEngine;

public interface IEventBinding<T>
{
    // Event when have Args
    public Action<T> OnEvent{get; set;}

    // Event when have no Args
    public Action OnEventNoArgs {get; set;}
}
public class EventBinding<T>: IEventBinding<T> where T: IEvent
{
    Action<T> onEvent = _ =>{};

    Action onEventNoArgs = () => {};

    public Action<T> OnEvent
    {
        get => onEvent;
        set => onEvent = value;
    }

    public Action OnEventNoArgs
    {
        get => onEventNoArgs;
        set => onEventNoArgs= value;
    }

    public EventBinding(Action<T> onEvent) {this.onEvent = onEvent;}

    public EventBinding(Action onEventNoArgs) {this.onEventNoArgs = onEventNoArgs;}

    public void Add(Action<T> onEvent){this.onEvent += onEvent;}

    public void Remove(Action<T> onEvent){this.onEvent -= onEvent;}

    public void Add(Action onEventNoArgs){this.onEventNoArgs += onEventNoArgs;}

    public void Remove(Action onEventNoArgs){this.onEventNoArgs -= onEventNoArgs;}

    


}
