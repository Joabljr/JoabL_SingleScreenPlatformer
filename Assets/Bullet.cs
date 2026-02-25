using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 10f;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    BlockHealth block = collision.GetComponent<BlockHealth>();

    if (block != null)
    {
        block.TakeDamage(1);   // deal 1 damage
        Destroy(gameObject);   // destroy the bullet
    }
    
EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

if (enemy != null)
{
    enemy.TakeDamage(1);
    Destroy(gameObject);
}


}



}
