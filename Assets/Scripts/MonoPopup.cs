using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoPopup<T> : MonoBehaviour where T : Component
{
    public delegate void ActivateTarget(T target);
    public delegate void DeactivateTarget(T target);

    public float Duration { get; set; }

    protected T Target { get { return target; } }

    protected ActivateTarget Activate { get; set; }
    protected DeactivateTarget Deactivate { get; set; }

    [SerializeField]
    private T target;

    [SerializeField]
    private float defaultDuration = 0.5f;

    private float timeElapsed;

    protected void Awake()
    {
        Duration = defaultDuration;
        Activate = GetActivationAction();
        Deactivate = GetDeactivationAction();
    }

    public void StartPopup()
    {
        this.enabled = true;
        timeElapsed = 0.0f;
        if (Activate != null)
        {
            Activate(target);
        }
    }

    private void OnValidate()
    {
        if (!target)
        {
            target = GetComponent<T>();
        }
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > Duration)
        {
            if (Deactivate != null)
            {
                Deactivate(target);
            }
            this.enabled = false;
        }
    }

    protected abstract ActivateTarget GetActivationAction();
    protected abstract DeactivateTarget GetDeactivationAction();
}
