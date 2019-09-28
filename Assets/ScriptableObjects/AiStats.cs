using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[CreateAssetMenu(menuName = "Ai Stats")]
public class AiStats : SaveableScriptableObject
{
    public const string AiFilename = "Ai";

    public float Precision
    {
        get { return precision; }
        set { precision = Mathf.Clamp01(value); }
    }
    public float LaunchDelay
    {
        get { return launchDelay; }
        set { launchDelay = value; }
    }

    public override string Filename { get { return AiFilename; } }

    public override string FolderName { get { return Utils.ConfigFolderName; } }

    [SerializeField]
    [Range(0.75f, 1.0f)]
    private float precision = 0.975f;
    [SerializeField]
    [Range(0.0f, float.MaxValue)]
    private float launchDelay = 0.5f;

    public void UpdateValues(float Precision, float LaunchDelay)
    {
        this.precision = Precision;
        this.launchDelay = LaunchDelay;
        Save();
    }

    public override void Reset()
    {
        precision = 0.975f;
        launchDelay = 0.5f;
    }
}
