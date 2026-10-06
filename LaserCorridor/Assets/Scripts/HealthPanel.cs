using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] int amount = 25;
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
            playerStats.heal(amount);
        }
        else
        {
            playerStats.takeDamage(amount);
        }

        Debug.Log(
            "Health panel activated. Current health: " +
            playerStats.GetCurrentHealth()
        );
    }
}