
using UnityEngine;
using System;

[Serializable]
public class LocationFields
{
    public bool IsValid { get { return SuggestedForce; } }
    public bool IsAllValid { get { return SuggestedForce && IdleLocation && LaunchLocation; } }
    public FloatField SuggestedForce;
    public Vector3Field IdleLocation;
    public Vector3Field LaunchLocation;

    public void SetValues(LaunchLocation ToCopy)
    {
        if (ToCopy)
        {
            if (SuggestedForce)
            {
                SuggestedForce.Value = ToCopy.GetSuggestedForceToScore;
            }
            if (IdleLocation)
            {
                IdleLocation.Value = ToCopy.CameraIdleLocation.position;
            }
            if (LaunchLocation)
            {
                LaunchLocation.Value = ToCopy.CameraLaunchLocation.position;
            }
        }
    }
}


public class Player : MonoBehaviour
{
    public const int HumanPlayerId = 0;
    public const int AiPlayerId = 1;

    public bool IsHumanPlayer { get { return (Id == HumanPlayerId); } }

    public IntField Score { get { return score; } }
    public BoolField GetIsFireballActive { get { return isFireballActive; } }
    public LocationFields GetLocationFields { get { return locationFields; } }
    public bool IsFireballActive { get { return Utils.IsTrue(isFireballActive); } }
    protected float CurrentSuggestedForce { get { return locationFields.IsValid ? locationFields.SuggestedForce.Value : -1.0f; } }

    public bool IsScoreDirty { get; set; }
    public int CurrentLaunchBonus { get; set; }
    public int Id { get { return id; } protected set { id = value; } }
    public bool IsBallLaunching { get; private set; }
    protected Transform MyTransform { get; private set; }

    [SerializeField]
    private IntField score;
    [SerializeField]
    private LaunchLocations locations;
    [SerializeField]
    private PlayerData data;
    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private BoolField isFireballActive;
    [SerializeField]
    private LocationFields locationFields = new LocationFields();
    [SerializeField]
    private int id;

    private int currentLaunchLocationIndex;
    private Transform currentRestartLocation;


    public void OnScoreTrigger()
    {
        PrepareShot(true);
    }
    public void OnGameAreaExited()
    {
        PrepareShot(false);
    }
    public void OnLaunchDirtied(int launchBonus)
    {
        this.CurrentLaunchBonus = launchBonus;
        this.IsScoreDirty = (launchBonus == 0);
    }
    bool AreFieldsInitialized()
    {
        return isFireballActive && body && data && locations && score;
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    protected virtual void OnValidate()
    {
        id = HumanPlayerId;
        if (!body)
        {
            body = GetComponent<Rigidbody>();
        }
    }
    protected virtual void Awake()
    {
        MyTransform = transform;
        currentLaunchLocationIndex = -1;

        if (score)
        {
            score.Value = 0;
        }

        if (!AreFieldsInitialized())
        {
            Debug.LogErrorFormat("{0} of type {1} has missing references!", this, this.GetType());
        }

        if (body)
        {
            body.sleepThreshold = 0.0001f;
            body.useGravity = false;
        }
    }
    protected virtual void Start()
    {
        if (data)
        {
            data.AddPlayer(this);

            GameObject prefab = data.GetBallPrefab();
            if (prefab)
            {
                GameObject.Instantiate<GameObject>(prefab, MyTransform).transform.localPosition = Vector3.zero;
            }
        }
    }
    public virtual void PrepareShot(bool changeLaunchLocation)
    {
        IsBallLaunching = false;

        if (changeLaunchLocation || !currentRestartLocation)
        {
            LaunchLocation location = locations.GetRandomLocation(out currentLaunchLocationIndex, currentLaunchLocationIndex);
            currentRestartLocation = location.Location;
            locationFields.SetValues(location);
        }

        if (body)
        {
            body.MovePosition(currentRestartLocation.position);
            body.MoveRotation(currentRestartLocation.rotation);

            body.useGravity = false;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }
    public void LaunchBall(float Strenght)
    {
        IsScoreDirty = false;
        CurrentLaunchBonus = 0;
        float FinalStrenght = 0.0f;
        IsBallLaunching = true;

        if (data)
        {
            FinalStrenght = Mathf.Clamp(Strenght, data.MinForceAmount, data.MaxForceAmount);
        }

        if (body)
        {
            body.useGravity = true;
            body.AddForce(FinalStrenght * MyTransform.forward);
            body.AddTorque(FinalStrenght * UnityEngine.Random.insideUnitSphere.normalized);
        }
    }
    private void OnDestroy()
    {
        if (data)
        {
            data.RemovePlayer(this);
        }
    }
}
