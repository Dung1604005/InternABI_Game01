using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// Contains methods and properties related to event buses and event types in the Unity application.
/// </summary>
public static class EventBusUtil
{
    // This store all of type (structs/classes that implement IEvent)
    public static IReadOnlyList<Type> EventTypes {get; set;}

    // This store all of event Bus

    public static IReadOnlyList<Type> EventBusTypes {get;set;}

    #if UNITY_EDITOR
    public static PlayModeStateChange PlayModeState { get; set; }

    //The [InitializeOnLoadMethod] attribute causes this method to be called every time a script
    /// is loaded or when the game enters Play Mode in the Editor. This is useful to initialize

    [InitializeOnLoadMethod]

    //Clear old listener and add new Listener
    public static void InitializeEditor()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    // When player exit play mode 
    // => clear all  listener of every bus
    static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        PlayModeState = state;
        if(state == PlayModeStateChange.ExitingPlayMode)
        {
            ClearAllBuses();
        }

    }
    #endif 
    // Init eventBus before scene load
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Initialize()
    {
        EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
        EventBusTypes = InitializeAllBuses();
    }
    /// <summary>
    /// Init all event buses
    /// </summary>
    /// <returns></returns>
    static List<Type> InitializeAllBuses()
    {
        List<Type> eventBusTypes = new List<Type>();
        // Get the type of event bus

        var typeDef = typeof(EventBus<>);

        foreach(var eventType in EventTypes)
        {
            // make specific type with event type
            var busType = typeDef.MakeGenericType(eventType);

            eventBusTypes.Add(busType);

            Debug.Log($"Initialized EventBus<{eventType.Name}>");
        }
        return eventBusTypes;
    }
    /// <summary>
    /// Clears (removes all listeners from) all event buses in the application.
    /// </summary>

    static void ClearAllBuses()
    {
        Debug.Log("Clearing all buses");

        for(int i= 0; i < EventBusTypes.Count; i++)
        {
            // Get type of event bus
            var busType = EventBusTypes[i];

            // Get method cleare from that event bus by reflection and call Clear
            var clearMethod = busType.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );

            clearMethod?.Invoke(null, null);
        }
    }



}
