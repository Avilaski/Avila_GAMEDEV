using UnityEngine;

public class SpeedPanel : MonoBehaviour
{
    [SerializeField] float amount = 1f;
    [SerializeField] bool increase = true;

    private bool hasTriggered = false;

    FlashlightPanel flashlightPanel;

    void Awake()
    {
        flashlightPanel = GetComponent<FlashlightPanel>();
        if (flashlightPanel == null)
        {
            flashlightPanel = gameObject.AddComponent<FlashlightPanel>();
        }

    }

    void OnTriggerEnter(Collider other) { TryActivate(other); }
    void OnTriggerStay(Collider other) { TryActivate(other); }

    void TryActivate(Collider other)
    {
        // Don't activate again after first use
        if (hasTriggered || !flashlightPanel.IsIlluminated())
            return;

        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats == null)
            return;

        hasTriggered = true;

        if (increase)
        {
            playerStats.increaseSpeed(amount);
        }
        else
        {
            playerStats.decreaseSpeed(amount);
        }

        Debug.Log(
            "Speed panel activated. Current speed: " +
            playerStats.GetCurrentSpeed()
        );
    }
}
