using UnityEngine;

public class EnemyUFO_Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Vector2 currentDirection;
    public float changeDirectionTime = 2f;
    private float directionTimer;
    public Transform firePoint;

    [Header("Dodging")]
    public float dodgeStrength = 0.2f;
    public float dodgeInterval = 3f;
    private float dodgeTimer;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float shootInterval = 2f;
    private float shootTimer;
    public float bulletSpeed = 10f;

    [Header("Player")]
    public Transform player;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        currentDirection = Random.insideUnitCircle.normalized;
        directionTimer = changeDirectionTime;
        dodgeTimer = dodgeInterval;
        shootTimer = shootInterval;
    }

    void Update()
    {
        Move();
        ChangeDirection();
        Dodge();
        Shoot();
        ClampInsideMovementBox();   // NEW — replaces ClampToTopHalf()
    }

    void Move()
    {
        transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
    }

    void ChangeDirection()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f)
        {
            currentDirection = Random.insideUnitCircle.normalized;
            directionTimer = changeDirectionTime;
        }
    }

    void Dodge()
    {
        dodgeTimer -= Time.deltaTime;
        if (dodgeTimer <= 0f)
        {
            currentDirection = (currentDirection + Random.insideUnitCircle * dodgeStrength).normalized;
            dodgeTimer = dodgeInterval;
        }
    }

    void Shoot()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = shootInterval;
        }
    }

    void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        float spawnDistance = 2.5f;
        Vector3 spawnPos = transform.position + (Vector3)(dir * spawnDistance);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        eb.Initialize(dir, bulletSpeed);

        Debug.Log("DIR = " + dir);
    }

    // ⭐ NEW — clean movement rectangle clamp
    void ClampInsideMovementBox()
    {
        Vector3 camPos = cam.transform.position;
        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;

        float minX = camPos.x - (w * 0.45f);
        float maxX = camPos.x + (w * 0.45f);

        float minY = camPos.y - (h * 0.1f);   // slightly above bottom
        float maxY = camPos.y + (h * 0.35f);  // stays in upper area

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
