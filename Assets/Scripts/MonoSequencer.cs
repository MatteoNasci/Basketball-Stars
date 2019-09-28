using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSequencer<T> : MonoBehaviour where T : MonoBehaviour
{
    public int CurrentIndex { get { return currentIndex; } set { ActivateIndex(value); } }
    public bool IsCurrentIndexValid { get { return IsIndexValid(CurrentIndex); } }
    public int ElementsCount { get { return toSequence.Length; } }

    [SerializeField]
    private bool destroyElementsOnDestroy = true;
    [SerializeField]
    protected T[] toSequence = new T[0];

    private int currentIndex;
    // Use this for initialization
    protected virtual void OnEnable()
    {
        ActivateIndex(0);
    }
    protected virtual void OnDisable()
    {
        DeactivateCurrent();
    }
    public bool IsIndexValid(int IndexToValidate)
    {
        return (IndexToValidate >= 0 && IndexToValidate < ElementsCount);
    }
    public T GetCurrentElement()
    {
        return GetIndexedElement(currentIndex);
    }
    public T GetIndexedElement(int Index)
    {
        return IsIndexValid(Index) ? toSequence[Index] : null;
    }
    public void DeactivateCurrent()
    {
        if (ElementsCount <= 0 || !IsCurrentIndexValid)
        {
            return;
        }

        T CurrentElement = toSequence[currentIndex];
        if (CurrentElement)
        {
            CurrentElement.gameObject.SetActive(false);
        }
    }
    public void ActivateNext()
    {
        ActivateIndex(currentIndex + 1);
    }
    public void ActivatePrevious()
    {
        ActivateIndex(currentIndex - 1);
    }
    public virtual void ActivateIndex(int IndexToActivate)
    {
        if (ElementsCount <= 0)
        {
            return;
        }

        if (IndexToActivate < 0)
        {
            IndexToActivate = ElementsCount - 1;
        }

        T obj = toSequence[currentIndex];
        if (obj)
        {
            obj.gameObject.SetActive(false);
        }

        currentIndex = IndexToActivate % ElementsCount;

        obj = toSequence[currentIndex];
        if (obj)
        {
            obj.gameObject.SetActive(true);
        }
    }
    protected void OnDestroy()
    {
        if (!destroyElementsOnDestroy)
        {
            return;
        }
        for (int i = 0; i < ElementsCount; i++)
        {
            T obj = toSequence[i];
            if (obj)
            {
                GameObject.Destroy(obj.gameObject);
            }
        }
    }
}