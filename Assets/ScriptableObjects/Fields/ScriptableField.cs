using UnityEngine;
using System;
[Serializable]
public class ScriptableField<T> : ScriptableObject
{
    public T Value { get { return value; } set { this.value = value; } }

    [SerializeField]
    private T value;

    protected void Awake()
    {
        value = default(T);
    }
}