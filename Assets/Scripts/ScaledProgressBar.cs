using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum Axis
{
    X,
    Y,
    Z
}
public class ScaledProgressBar : MonoBehaviour
{
    [SerializeField]
    private Transform bar;
    [SerializeField]
    private Axis scaleAx = Axis.X;
    [SerializeField]
    private float scaleTollerance = 0.01f;

    private Func<Transform, float> getFillRate = GetFillRateX;
    private Action<Transform, float> setFillRate = SetFillRateX;

    private void OnValidate()
    {
        if (!bar)
        {
            bar = transform;
        }
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    private void Awake()
    {
        //To update getfillrate and setfillrate with the correct saved scaleax
        UpdateDelegates();
        if (!bar)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a bar transform reference to work", this, this.GetType());
        }
    }

    public void SetFillRate(float FillAmount)
    {
        if (Mathf.Abs(FillAmount - getFillRate(bar)) > scaleTollerance)
        {
            setFillRate(bar, FillAmount);
        }
    }
    public float GetFillRate()
    {
        return getFillRate(bar);
    }
    private void UpdateDelegates()
    {
        switch (scaleAx)
        {
            case Axis.X:
                getFillRate = GetFillRateX;
                setFillRate = SetFillRateX;
                break;
            case Axis.Y:
                getFillRate = GetFillRateY;
                setFillRate = SetFillRateY;
                break;
            case Axis.Z:
                getFillRate = GetFillRateZ;
                setFillRate = SetFillRateZ;
                break;
            default:
                break;
        }
    }
    private static void SetFillRateX(Transform Bar, float FillAmount)
    {
        Vector3 Scale = Bar.localScale;
        Bar.localScale = new Vector3(FillAmount, Scale.y, Scale.z);
    }
    private static void SetFillRateY(Transform Bar, float FillAmount)
    {
        Vector3 Scale = Bar.localScale;
        Bar.localScale = new Vector3(Scale.x, FillAmount, Scale.z);
    }
    private static void SetFillRateZ(Transform Bar, float FillAmount)
    {
        Vector3 Scale = Bar.localScale;
        Bar.localScale = new Vector3(Scale.x, Scale.y, FillAmount);
    }
    private static float GetFillRateX(Transform Bar)
    {
        return Bar.localScale.x;
    }
    private static float GetFillRateY(Transform Bar)
    {
        return Bar.localScale.y;
    }
    private static float GetFillRateZ(Transform Bar)
    {
        return Bar.localScale.z;
    }
}
