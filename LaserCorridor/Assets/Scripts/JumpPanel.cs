using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    [SerializeField] float amount = 1f;
    [SerializeField] bool increase = true;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        // Don't activate again after first use
        if (hasTriggered)
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