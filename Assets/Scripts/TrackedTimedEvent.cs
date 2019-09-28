using UnityEngine.UI;
using UnityEngine;
public class TrackedTimedEvent : TimedEvent
{
    [SerializeField]
    private Text tracker;

    private int lastValue = int.MinValue;
    protected override void OnEnable()
    {
        base.OnEnable();

        UpdateText();

        if (!tracker)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a Text reference", this, this.GetType());
        }
    }
    void UpdateText()
    {
        int newValue = Mathf.RoundToInt(Duration - TimeElapsed);
        if (newValue != lastValue)
        {
            lastValue = newValue;
            if (tracker)
            {
                tracker.text = lastValue.ToString();
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        UpdateText();
    }
}
