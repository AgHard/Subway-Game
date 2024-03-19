using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 50.0f; // The duration of a full day in seconds
    private float currentTime = 0.0f;
    private bool isDaytime = true; // Flag to determine if it's currently daytime

    public Light directionalLight;
    public Color daytimeLightColor;
    public Color nighttimeLightColor;

    public float maxSunAngle = 60.0f; // Maximum angle on the x-axis for the sun during the day
    public float initialSunAngle = -5.0f; // Initial angle on the x-axis for the sun at the start of the day

    private void Start()
    {
        // Set the initial sun angle at the start of the day
        directionalLight.transform.localRotation = Quaternion.Euler(initialSunAngle, 0.0f, 0.0f);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        // Check if it's time to switch from day to night or vice versa
        if (currentTime >= dayDuration)
        {
            isDaytime = !isDaytime; // Toggle between day and night
            currentTime = 0.0f; // Reset the timer
        }

        // Update lighting and sun angle based on the time of day
        if (isDaytime)
        {
            // Adjust the rotation of the sun to simulate its movement across the sky
            float rotationAngle = Mathf.Lerp(initialSunAngle, maxSunAngle, currentTime / dayDuration);
            directionalLight.transform.localRotation = Quaternion.Euler(rotationAngle, 0.0f, 0.0f);

            directionalLight.intensity = 1.0f;
            directionalLight.color = daytimeLightColor;
        }
        else
        {
            directionalLight.intensity = 0.2f; // Adjust this value for nighttime intensity
            directionalLight.color = nighttimeLightColor;
        }
    }
}
