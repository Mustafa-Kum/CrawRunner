using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensity : MonoBehaviour
{
    [SerializeField] private Light2D intensity;
    public float minIntensity = 1.0f;
    public float maxIntensity = 5.0f;
    public float speed = 1.0f;

    private void Update()
    {
        // Intensity deðerini ping-pong iþlemi kullanarak sürekli olarak deðiþtirin.
        float pingPongValue = Mathf.PingPong(Time.time * speed, 1.0f);
        float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, pingPongValue);
        intensity.intensity = newIntensity;
    }
}
