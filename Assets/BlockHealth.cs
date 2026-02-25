using UnityEngine;

public class BlockHealth : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(int amount)
{
    health -= amount;

    if (health <= 0)
    {
        JumpPowerUp power = GetComponent<JumpPowerUp>();
        if (power != null)
        {
            power.ApplyEffect();
        }

        Destroy(gameObject);
    }
}

}
