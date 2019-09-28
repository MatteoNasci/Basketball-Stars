
using UnityEngine;

public class MaterialBlinking : MonoBehaviour
{
    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material blinkingMaterial;

    [Range(0.01f, 1.0f)]
    [SerializeField]
    private float blinkingDuration = 0.2f;
    [Range(0.01f, 2.0f)]
    [SerializeField]
    private float blinkingIntervall = 0.5f;

    [SerializeField]
    private MeshRenderer blinker;

    private float timer;
    private bool isBlinking;
    private void OnValidate()
    {
        if (!blinker)
        {
            blinker = GetComponent<MeshRenderer>();
        }
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    private void Awake()
    {
        if (!blinker)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a meshrenderer reference", this, this.GetType());
        }
        else
        {
            blinker.material = defaultMaterial;
        }
    }

    private void OnDisable()
    {
        if (blinker)
        {
            blinker.material = defaultMaterial;
        }
        isBlinking = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!blinker)
        {
            this.enabled = false;
            return;
        }

        timer += Time.deltaTime;
        if (isBlinking)
        {
            if (timer > blinkingDuration)
            {
                isBlinking = false;
                timer = 0.0f;
                blinker.material = defaultMaterial;
            }
        }
        else if (timer > blinkingIntervall)
        {
            isBlinking = true;
            timer = 0.0f;
            blinker.material = blinkingMaterial;
        }
    }
}
