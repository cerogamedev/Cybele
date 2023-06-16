using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public float enemyHealth, EnemyMaxHealth, enemyAttack;
    public GameObject bloodEffect;
    public float teleportDelay = 2f; // Iþýnlanma gecikmesi süresi
    public bool isTeleporting = true;

    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float nextFireTime = 0f;
    public float shootingDistance = 5f; // Ateþ etmek için gereken minimum mesafe
    private void Awake()
    {
        enemyHealth = transform.position.y / 10;
        enemyAttack = transform.position.y / 8;
    }
    void Start()
    {
        EnemyMaxHealth = enemyHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth < 0)
            Destroy(this.gameObject);
        transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(enemyHealth/EnemyMaxHealth, 1);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= shootingDistance && Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate;
            if (isTeleporting)
            {
                StartCoroutine(TeleportAfterDelay());
            }
        }
 

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Health>().Damage(enemyAttack);
        }
    }
    public void TakeDamage(int Damage)
    {
        enemyHealth -= Damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }
    private void FireBullet()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * 10f;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
    }
    private IEnumerator TeleportAfterDelay()
    {
        isTeleporting = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, shootingDistance);

        GameObject[] platformObjects = colliders.Where(c => c.CompareTag("Platform") && c.transform.position.y > transform.position.y).Select(c => c.gameObject).ToArray();

        if (platformObjects.Length > 0)
        {
            GameObject closestPlatform = platformObjects.OrderBy(p => Vector2.Distance(transform.position, p.transform.position)).First();
            Vector2 teleportPosition = closestPlatform.transform.position + Vector3.up;
            transform.position = teleportPosition;
            closestPlatform.transform.tag = "UsedPlatform";
        }

        yield return new WaitForSeconds(teleportDelay);
        isTeleporting = true;
    }

}
