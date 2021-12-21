using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using UnityEngine.Events;

public class OnDestroyListener : ScriptableEventListener<GameObject, ScriptableOnDestroyEvent, UnityEvent<GameObject>>
{
}
