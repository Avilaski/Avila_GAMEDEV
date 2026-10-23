using UnityEngine;

public class JumpPanel : MonoBehaviour
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
            playerStats.increaseJumpHeight(amount);
        }
        else
        {
            playerStats.decreaseJumpHeight(amount);
        }

        Debug.Log(
            "Jump panel activated. Current jump height: " +
            playerStats.GetCurrentJumpHeight()
        );
    }
}
