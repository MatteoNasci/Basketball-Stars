using UnityEngine.Events;
using UnityEngine;
using System;
[Serializable]
public class TimerOver : UnityEvent { }
public class TimedEvent : MonoBehaviour
{
    public float Duration { get { return duration; } }
    public float TimeElapsed { get { return timeElapsed; } }

    [SerializeField]
    private bool restartOnEnable = false;
    [SerializeField]
    private bool enableDisableSelf = true;
    [SerializeField]
    private TimerOver onTimerOver;
    [Range(0.0f, float.MaxValue)]
    [SerializeField]
    private float duration = 5.0f;


    private float timeElapsed;

    protected virtual void OnEnable()
    {
        if (restartOnEnable)
        {
            Restart();
        }
    }
    public void Restart()
    {
        timeElapsed = 0.0f;
        Resume();
    }
    void ChangeActive(bool enabled)
    {
        if (enableDisableSelf)
        {
            this.enabled = enabled;
        }
        else
        {
            gameObject.SetActive(enabled);
        }
    }
    public void Pause()
    {
        ChangeActive(false);
    }
    public void Resume()
    {
        ChangeActive(true);
    }
    public void Reset()
    {
        timeElapsed = 0.0f;
    }

    protected virtual void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > duration)
        {
            Pause();
            onTimerOver.Invoke();
        }
    }
}
