using UnityEngine;


public abstract class BaseOptionsShower : MonoBehaviour
{
    public bool IsUiDirty { get; private set; }

    [SerializeField]
    private float textUpdateIntervall = 0.25f;

    private float timer;

    public abstract void ResetValues();
    public void DirtyUi()
    {
        IsUiDirty = true;
    }
    protected virtual void OnEnable()
    {
        UpdateText();
        timer = 0.0f;
    }
    protected abstract void UpdateText();

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= textUpdateIntervall)
        {
            timer = 0.0f;
            if (IsUiDirty)
            {
                UpdateText();
                IsUiDirty = false;
            }
        }
    }
}
