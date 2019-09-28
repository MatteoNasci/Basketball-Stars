using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed = Vector3.right;
    [SerializeField]
    private bool localSpace = true;

    private Transform myTransform;
    private void Awake()
    {
        myTransform = transform;
    }

    private void Update()
    {
        Vector3 movement = speed * Time.deltaTime;

        myTransform.Translate(movement, localSpace ? Space.Self : Space.World);
    }
}
