
using UnityEngine;
using UnityEngine.Events;

public class FollowTarget : MonoBehaviour
{
    public const float MinDistanceForLerp = 0.01f;

    public float LerpDuration { get { return isFollowingLaunch ? followingLerpDuration : normalLerpDuration; } }

    [SerializeField]
    [Range(0.001f, 5.0f)]
    private float followingLerpDuration = 1.0f;
    [SerializeField]
    [Range(0.001f, 5.0f)]
    private float normalLerpDuration = 0.4f;

    [SerializeField]
    private int playerId = Player.HumanPlayerId;
    [SerializeField]
    private Vector3Field targetToFollowLaunch;
    [SerializeField]
    private Vector3Field targetToFollowIdle;
    [SerializeField]
    private Transform targetToFace;

    private bool isFollowingLaunch;
    private float timer;
    private Vector3 startLocation;
    private Transform myTransform;

    public void FollowLaunch(Player player)
    {
        if (player && player.Id == playerId)
        {
            isFollowingLaunch = true;
            Activate();
        }
    }
    public void FollowLaunchLocation(Player player)
    {
        if (player && player.Id == playerId)
        {
            isFollowingLaunch = false;
            Activate();
        }
    }
    private void Activate()
    {
        if (this.enabled)
        {
            OnEnable();
        }
        this.enabled = true;
    }
    private void Awake()
    {
        myTransform = transform;
    }
    private bool IsDataValid()
    {
        return targetToFace && targetToFollowIdle && targetToFollowLaunch;
    }
    private Vector3 GetTargetLocation()
    {
        if (!IsDataValid())
        {
            Debug.LogErrorFormat("{0} of type {1} requires a valid reference to a target to face and to 2 vector3 fields to follow", this, this.GetType());
            return myTransform.position;
        }
        return isFollowingLaunch ? targetToFollowLaunch.Value : targetToFollowIdle.Value;
    }
    // Use this for initialization
    private void OnEnable()
    {
        if (!IsDataValid() || Vector3.Distance(myTransform.position, GetTargetLocation()) < MinDistanceForLerp)
        {
            this.enabled = false;
            return;
        }

        startLocation = myTransform.position;
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float completitionPercentage = timer / LerpDuration;
        Vector3 nextLocation = Vector3.Lerp(startLocation, GetTargetLocation(), completitionPercentage);
        Quaternion nextRotation = Quaternion.LookRotation((targetToFace.position - nextLocation).normalized, Vector3.up);

        myTransform.SetPositionAndRotation(nextLocation, nextRotation);

        if (completitionPercentage >= 1.0f)
        {
            this.enabled = false;
        }
    }
}
