using UnityEngine;
using UnityEditor;
public abstract class SaveableEditor<T> : UnityEditor.Editor where T : UnityEngine.Object, ISaveable
{
    protected T Obj { get; private set; }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save"))
            Obj.Save();
        if (GUILayout.Button("Load"))
            Obj.Load();
        if (GUILayout.Button("Delete"))
            Obj.DeleteFile();
        if (GUILayout.Button("Reset Fields"))
            Obj.Reset();
    }
    protected virtual void OnEnable()
    {
        this.Obj = target as T;
    }
}