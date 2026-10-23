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
    private StarterAssets.FirstPersonController firstPersonController;

    void Awake()
    {
        firstPersonController = GetComponent<StarterAssets.FirstPersonController>();
    }

    void LateUpdate()
    {
        ApplyMovementStats();
    }

    void ApplyMovementStats()
    {
        if (firstPersonController == null)
        {
            return;
        }

        firstPersonController.MoveSpeed = currentSpeed;
        firstPersonController.SprintSpeed = currentSpeed * 1.5f;
        firstPersonController.JumpHeight = currentJumpHeight;
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = 4f;
        currentJumpHeight = 1.5f;
        ApplyMovementStats();
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
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    public void decreaseSpeed(float amount)
    {
        currentSpeed -= amount;
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }

    public void increaseJumpHeight(float amount)
    {
        currentJumpHeight += amount;
        if (currentJumpHeight > jumpHeight)
        {
            currentJumpHeight = jumpHeight;
        }
    }

    public void decreaseJumpHeight(float amount)
    {
        currentJumpHeight -= amount;
        if (currentJumpHeight < 0)
        {
            currentJumpHeight = 0;
        }
    }

    public void Respawn()
    {
        if (respawnPoint == null)
        {
            Debug.LogError("Assign a Respawn Point on PlayerStats.", this);
            return;
        }

        CharacterController controller = GetComponent<CharacterController>();
        MovementController movement = GetComponent<MovementController>();

        bool controllerWasEnabled = controller != null && controller.enabled;
        if (controller != null)
        {
            controller.enabled = false;
        }
        transform.position = respawnPoint.position;

        if (laserSpawner != null)
        {
            laserSpawner.resetLasers();
        }

        if (controller != null)
        {
            controller.enabled = controllerWasEnabled;
        }

        if (movement != null)
        {
            movement.ResetVelocity();
        }

        if (firstPersonController != null)
        {
            firstPersonController.ResetMotion();
        }

        currentHealth = maxHealth;
        currentSpeed = 4f;
        currentJumpHeight = 1.5f;
        ApplyMovementStats();
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }
}
