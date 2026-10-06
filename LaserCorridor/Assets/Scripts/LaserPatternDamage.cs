using UnityEngine;

public class LaserPatternDamage : MonoBehaviour
{
    bool hasDamagedPlayer = false;

    public void TryDamage(PlayerStats playerStats)
    {
        if (hasDamagedPlayer)
            return;

        playerStats.takeDamage(25); 

        hasDamagedPlayer = true;
    }
}
