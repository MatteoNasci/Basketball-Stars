using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDistance : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float maxDistance = 6.5f;
    [SerializeField]
    private float minDistance = 6.0f;

    private Transform myTransform;
    private void Awake()
    {
        myTransform = transform;
    }

	private void LateUpdate ()
    {
        if(maxDistance < minDistance)
        {
            float temp = maxDistance;
            maxDistance = minDistance;
            minDistance = temp;
        }

        if (target)
        {
            Vector3 targetLocation = target.position;
            Vector3 myLocation = myTransform.position;

            Vector3 sub = myLocation - targetLocation;
            float sqrMagnitude = sub.sqrMagnitude;

            if (sqrMagnitude > (maxDistance * maxDistance))
            {
                Vector3 maxDistanceFromTarget = sub.normalized * maxDistance;
                myTransform.position = targetLocation + maxDistanceFromTarget;
            }
            else if(sqrMagnitude < (minDistance * minDistance))
            {
                Vector3 minDistanceFromTarget = sub.normalized * minDistance;
                myTransform.position = targetLocation + minDistanceFromTarget;
            }
        }
        else
        {
            Debug.LogErrorFormat("{0} of type {1} requires a valid reference to a target", this, this.GetType());
        }
    }
}
