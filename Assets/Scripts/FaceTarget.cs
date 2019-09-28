using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    private void Update()
    {
        if (target)
        {
            myTransform.LookAt(target);
        }
        else
        {
            Debug.LogErrorFormat("{0} of type {1} requires a valid reference to a target", this, this.GetType());
        }
    }
}
