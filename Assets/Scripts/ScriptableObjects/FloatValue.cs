using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {

    public float initialValue;

    [HideInInspector]
    public float RuntimeValue; // so isnt changed in inspector

    // Serializing saves things like health in memory
    public void OnAfterDeserialize() { // after unloading everything
        RuntimeValue = initialValue; // cache values
    }
    public void OnBeforeSerialize() { // before loading anything so when starting up

    }
}
