using UnityEngine;
using UnityEngine.Events;


//custom unity events for dynamic referencing in editor
namespace CustomUnityEvents{
    [System.Serializable] public class IntEvent: UnityEvent<int> {}
    [System.Serializable] public class FloatEvent: UnityEvent<float> {}
}

