using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] clips = new AudioClip[0];
    [SerializeField]
    private bool playOnEnable = false;
    [SerializeField]
    private bool loop;
    [SerializeField]
    private int firstIndex = -1;

    private int currentClipIndex;

    public void Play(int index = -1)
    {
        if (!source || clips.Length <= 0)
        {
            return;
        }
        currentClipIndex = index < 0 ? Utils.GetRandomInt(0, clips.Length, currentClipIndex) :index;
        source.Stop();
        source.time = 0.0f;
        source.clip = clips[currentClipIndex];
        source.Play();
    }
    public void Stop()
    {
        if (source)
        {
            source.Stop();
        }
    }
    protected void OnEnable()
    {
        if(!source)
        {
            this.enabled = false;
            return;
        }
        this.enabled = loop;
        if(source.playOnAwake || playOnEnable)
        {
            Play(firstIndex);
        }
    }
    protected void Awake()
    {
        currentClipIndex = -1;
        if (!source)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a valid reference to an audiosource", this, this.GetType());
        }
    }
    protected void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    protected virtual void OnValidate()
    {
        if (!source)
        {
            source = GetComponent<AudioSource>();
        }
        if (source)
        {
            source.loop = false;
        }
    }
    protected void Update()
    {
        if (!source || !loop)
        {
            this.enabled = false;
            return;
        }
        if (!source.isPlaying)
        {
            Play();
        }
    }
}
