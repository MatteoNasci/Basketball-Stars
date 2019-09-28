

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[System.Serializable]
public class LaunchEvent : UnityEvent<float> { }

public class GameInput : MonoBehaviour
{
    public const int LeftMouseButtonKey = 0;
    private delegate bool InputUpdate(GameInput inputHandler);

    public bool IsInputTimerOver { get { return launchTimer > launchData.MaxLaunchDuration; } }
    public float GetCurrentForce { get { return Mathf.Min(accumulatedForce, launchData.MaxForceAmount); } }
    public float GetCurrentForceNormalized { get { return (Mathf.Approximately(launchData.MaxForceAmount, 0.0f) ? 0.0f : (GetCurrentForce - launchData.MinForceAmount) / (launchData.MaxForceAmount - launchData.MinForceAmount)); } }

    [SerializeField]
    private ScaledProgressBar launchBar;
    [SerializeField]
    private PlayerData launchData;

    [SerializeField]
    private LaunchEvent onLaunched;

    private bool isLaunching;

    private InputUpdate isLaunchReady;

    private Vector3 lastInputPosition;
    private float accumulatedForce;
    private float launchTimer;

    public void Enable(Player player)
    {
        if (player && player.IsHumanPlayer)
        {
            this.enabled = true;
        }
    }
    // Use this for initialization
    void Awake()
    {
        if (!launchData)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a valid reference to a playerdata object", this, this.GetType());
        }

#if UNITY_IOS || UNITY_ANDROID
        isLaunchReady = IsLaunchReadyMobile;
#else 
        isLaunchReady = IsLaunchReadyStandalone;
#endif
        ResetInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (!launchData || Mathf.Approximately(launchData.MaxForceAmount, 0.0f))
        {
            return;
        }

        if (isLaunching)
        {
            launchTimer += Time.deltaTime;
            if (launchBar)
            {
                launchBar.SetFillRate(GetCurrentForceNormalized);
            }
        }

        if (isLaunchReady(this))
        {
            this.enabled = false;
            onLaunched.Invoke(GetCurrentForce);
            ResetInput();
        }
    }
    private void ResetInput()
    {
#if UNITY_STANDALONE
        lastInputPosition = Input.mousePosition;
#endif
        launchTimer = 0.0f;
        accumulatedForce = launchData.MinForceAmount;
        isLaunching = false;
    }

    private static bool IsLaunchReadyMobile(GameInput inputHandler)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                inputHandler.ResetInput();
                inputHandler.lastInputPosition = touch.position;
                inputHandler.isLaunching = true;
            }
            else if (inputHandler.IsInputTimerOver || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                return inputHandler.isLaunching;
            }
            else
            {
                Vector3 NewInputPosition = touch.position;
                float Delta = (NewInputPosition.y - inputHandler.lastInputPosition.y) * inputHandler.launchData.ForceMultiplier;

                inputHandler.accumulatedForce += Mathf.Max(Delta, 0.0f);

                inputHandler.lastInputPosition = NewInputPosition;
            }
        }

        return false;
    }
    private static bool IsLaunchReadyStandalone(GameInput inputHandler)
    {
        if (Input.GetMouseButtonDown(LeftMouseButtonKey))
        {
            inputHandler.ResetInput();
            inputHandler.isLaunching = true;
        }
        else if (inputHandler.IsInputTimerOver || Input.GetMouseButtonUp(LeftMouseButtonKey))
        {
            return inputHandler.isLaunching;
        }
        else
        {
            Vector3 NewInputPosition = Input.mousePosition;
            float Delta = (NewInputPosition.y - inputHandler.lastInputPosition.y) * inputHandler.launchData.ForceMultiplier;

            inputHandler.accumulatedForce += Mathf.Max(Delta, 0.0f);

            inputHandler.lastInputPosition = NewInputPosition;
        }

        return false;
    }
}
