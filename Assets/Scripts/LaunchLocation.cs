
using UnityEngine;

public class LaunchLocation : MonoBehaviour
{
    public bool IsValid { get { return Location && cameraIdleLocation && cameraLaunchLocation; } }
    public Transform Location { get; private set; }
    public Transform CameraIdleLocation { get { return cameraIdleLocation; } }
    public Transform CameraLaunchLocation { get { return cameraLaunchLocation; } }
    public float GetSuggestedForceToScore { get { return suggestedForceToScore; } }

    [SerializeField]
    private float suggestedForceToScore;
    [SerializeField]
    private Transform cameraIdleLocation;
    [SerializeField]
    private Transform cameraLaunchLocation;
    // Use this for initialization
    void Awake ()
    {
        Location = transform;
	}
}
