using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] Transform respawnPoint;
    [SerializeField] LaserSpawner laserSpawner;
    [SerializeField] TMP_Text healthText;


    private int currentHealth;
    private float currentSpeed;
    private float currentJumpHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = 4f;
        currentJumpHeight = 1.5f;
        UpdateHealthUI();

    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    public float GetCurrentJumpHeight()
    {
        return currentJumpHeight;
    }

    public void heal(int amount)
    {
        currentHealth += amount;
        // Ensure health doesn't exceed maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();

    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Health: " + currentHealth);

        // Ensure health doesn't drop below zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Respawn();
        }
        UpdateHealthUI();
    }


    public void increaseSpeed(float amount)
    {
        currentSpeed += amount;
        // Ensure speed doesn't exceed maximum
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    public void decreaseSpeed(float amount)
    {
        currentSpeed -= amount;
        // Ensure speed doesn't drop below zero
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }
    public void increaseJumpHeight(float amount)
    {
        currentJumpHeight += amount;
        // Ensure jump height doesn't exceed maximum
        if (currentJumpHeight > jumpHeight)
        {
            currentJumpHeight = jumpHeight;
        }
    }
    public void decreaseJumpHeight(float amount)
    {
        currentJumpHeight -= amount;
        // Ensure jump height doesn't drop below zero
        if (currentJumpHeight < 0)
        {
            currentJumpHeight = 0;
        }
    }

    void Respawn()
    {
        CharacterController controller = GetComponent<CharacterController>();
        MovementController movement = GetComponent<MovementController>();

        controller.enabled = false;
        transform.position = respawnPoint.position;

        if (laserSpawner != null)
        {
            laserSpawner.resetLasers();
        }

        controller.enabled = true;
        movement.ResetVelocity();

        currentHealth = maxHealth;
        currentSpeed = 4f;
        currentJumpHeight = 1.5f;
            UpdateHealthUI();


    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth;
    }
}
