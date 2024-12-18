using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(SerializableType))]
public class SerializableTypeDrawer : PropertyDrawer, IReset
{
    private bool _isInitialized = false;
    private List<string> _filteredTypesNames;
    private List<string> _filteredTypesAssemblyQualifiedNames;
    private TypeFilterAttribute _typeFilterAttribute;

    private SerializedProperty _serializedProperty;

    private void Initialize()
    {
        if (_isInitialized) return;
        _typeFilterAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(TypeFilterAttribute)) as TypeFilterAttribute;
        _filteredTypesNames = _typeFilterAttribute.FilterType.GetConcreteDerivedTypes(_typeFilterAttribute.FilterType.Assembly, true).Select(t => t.Name).ToList();
        _filteredTypesAssemblyQualifiedNames = _typeFilterAttribute.FilterType.GetConcreteDerivedTypes(_typeFilterAttribute.FilterType.Assembly, true).Select(t => t.AssemblyQualifiedName).ToList();
        _isInitialized = true;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Initialize();
        _serializedProperty = property;
        try
        {
            var typeProperty = _serializedProperty.FindPropertyRelative("_assemblyQualifiedName");

            if (string.IsNullOrEmpty(typeProperty.stringValue))
            {
                typeProperty.stringValue = _filteredTypesAssemblyQualifiedNames.First();
                _serializedProperty.serializedObject.ApplyModifiedProperties();
            }

            int selectedIndex = EditorGUI.Popup(position, "Filter Type", _filteredTypesAssemblyQualifiedNames.IndexOf(typeProperty.stringValue), _filteredTypesNames.ToArray());
            typeProperty.stringValue = _filteredTypesAssemblyQualifiedNames[selectedIndex];
            _serializedProperty.serializedObject.ApplyModifiedProperties();
        }
#pragma warning disable
        catch(ArgumentOutOfRangeException e)
        {
            Reset();
            Initialize();
        }
#pragma warning enable
    }

    public void Reset()
    {
        _serializedProperty.FindPropertyRelative("_assemblyQualifiedName").stringValue = string.Empty;
        _serializedProperty.serializedObject.ApplyModifiedProperties();
        _isInitialized = false;
    }
}
