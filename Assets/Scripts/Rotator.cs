using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotationSpeed { get { return rotationSpeed; } }
    public Axis RotationAx
    {
        get { return rotationAx; }
        set
        {
            if (value != rotationAx)
            {
                rotationAx = value;
                UpdateAx();
            }
        }
    }

    [SerializeField]
    private Axis rotationAx = Axis.Y;
    [SerializeField]
    private float rotationSpeed = 90.0f;

    private Transform myTransform;

    private System.Action<Transform, float> rotate;

    void UpdateAx()
    {
        switch (rotationAx)
        {
            case Axis.X:
                rotate = RotateX;
                break;
            case Axis.Y:
                rotate = RotateY;
                break;
            case Axis.Z:
                rotate = RotateZ;
                break;
            default:
                break;
        }
    }
    void Awake()
    {
        myTransform = transform;
        UpdateAx();
    }
    public void SetValues(float rotationSpeed, Axis rotationAx)
    {
        this.rotationSpeed = rotationSpeed;
        this.RotationAx = rotationAx;
    }

    // Update is called once per frame
    void Update ()
    {
        rotate(myTransform, RotationSpeed * Time.deltaTime);
	}
    private static void RotateX(Transform Transform, float Rotation)
    {
        Transform.Rotate(Rotation, 0.0f, 0.0f);
    }
    private static void RotateY(Transform Transform, float Rotation)
    {
        Transform.Rotate(0.0f, Rotation, 0.0f);
    }
    private static void RotateZ(Transform Transform, float Rotation)
    {
        Transform.Rotate(0.0f, 0.0f, Rotation);
    }
}
