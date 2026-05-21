using UnityEngine;

public class MicInput : MonoBehaviour
{
    AudioClip micClip;
    string micName;

    public Transform CloudHole;
    public RectTransform micBar;
    public float loudness = 0f;
    public float smoothedLoudness = 0f;
    public float MicThreshold = 0.0001f;

    public float minsize = 1.5f;
    public float maxsize = 10f;
    public float growSpeed = 3f; 
    public float shrinkSpeed = 0.03f;
    public float noiseFloor = 0.02f;

    float visibility = 0f;

    float[] samples = new float[256];

    void Start()
    {
        // get first available microphone
        micName = Microphone.devices[0];

        // start recording
        micClip = Microphone.Start(
            micName,
            true,
            10,
            44100
        );
    }

    void Update()
    {
        int micPosition =
            Microphone.GetPosition(micName) - samples.Length;

        if (micPosition < 0)
            return;

        // get audio data
        micClip.GetData(samples, micPosition);

        // compute loudness
        float sum = 0f;

        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        loudness = sum / samples.Length;

        // increase amplitude for sensitivity increase
        loudness *= 500f;
        loudness = Mathf.Max(0, loudness - noiseFloor);

        // smooth signal heavily
        smoothedLoudness = Mathf.Lerp(
            smoothedLoudness,
            loudness,
            0.5f * Time.deltaTime
        );

        // visualize mic input
        float visual = Mathf.Clamp(smoothedLoudness * 2000f, 0f, 300f);
        micBar.sizeDelta = new Vector2(
            visual,
            micBar.sizeDelta.y
        );

        // amplify microphone signal again
        float amplifiedLoudness =
            Mathf.Clamp01(smoothedLoudness);

        // if loud enough -> grow visibility
        if (amplifiedLoudness > MicThreshold)
        {
            visibility += growSpeed * amplifiedLoudness * Time.deltaTime;
        }
        else
        {
            // otherwise slowly shrink
            visibility -= shrinkSpeed * Time.deltaTime;
        }

        // clamp visibility
        visibility = Mathf.Clamp01(visibility);

        // convert visibility to cloud size
        float newSize = Mathf.Lerp(
            minsize,
            maxsize,
            visibility
        );

        // subtle pulse
        newSize += Mathf.Sin(Time.time) * 0.1f;

        // smooth scaling
        Vector3 targetScale =
            new Vector3(newSize, newSize, 1);

        CloudHole.localScale = Vector3.Lerp(
            CloudHole.localScale,
            targetScale,
            3f * Time.deltaTime
        );

        Debug.Log(smoothedLoudness);


    }

        
}