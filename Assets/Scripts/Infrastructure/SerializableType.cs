using System;
using UnityEngine;

[Serializable]
public class SerializableType : ISerializationCallbackReceiver , IReset
{
    [SerializeField] 
    private string _assemblyQualifiedName = string.Empty;

    private Type _type;
    public Type Type { get { return _type; } }

    public void OnAfterDeserialize()
    {
        if (TryGetType(_assemblyQualifiedName, out var type)) _type = type;
    }

    public void OnBeforeSerialize()
    {
        _assemblyQualifiedName = _type?.AssemblyQualifiedName ?? _assemblyQualifiedName;
    }

    public void Reset()
    {
        _assemblyQualifiedName = string.Empty;
        _type = null;
    }

    private bool TryGetType(string typeString, out Type type)
    {
        type = Type.GetType(typeString);
        return type != null;
    }
}
