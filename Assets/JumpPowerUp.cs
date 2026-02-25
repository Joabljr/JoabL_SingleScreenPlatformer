using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public int bonusJumps = 1;

    public void ApplyEffect()
    {
PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            player.extraJumps += bonusJumps;
        }
    }
}
