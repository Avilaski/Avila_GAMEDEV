using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            LaserPatternDamage pattern =
                GetComponentInParent<LaserPatternDamage>();

            if (pattern != null)
            {
                pattern.TryDamage(playerStats);
            }
        }
    }
}