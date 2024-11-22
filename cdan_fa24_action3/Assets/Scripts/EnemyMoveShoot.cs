using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveShoot : MonoBehaviour
{
    public float speed = 2f;
    public float stoppingDistance = 4f; // when enemy stops moving towards player
    public float retreatDistance = 3f; // when enemy moves away from approaching player
    private float timeBtwShots;
    public float startTimeBtwShots = 2;
    public GameObject projectile;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 PlayerVect;

    public int EnemyLives = 30;
    private Renderer rend;

    public float attackRange = 10;
    public bool isAttacking = false;
    private float scaleX;

    public bool isWebbed = false;
    private Color startColor = new Color(2.5f, 2.5f, 2.5f, 1f);

    // Sprites for up, down, left, and right directions
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        scaleX = gameObject.transform.localScale.x;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerVect = player.transform.position;

        timeBtwShots = startTimeBtwShots;

        rend = GetComponentInChildren<Renderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Set a default sprite
        spriteRenderer.sprite = spriteDown; // Default to "down" sprite
    }

    void Update()
    {
        float DistToPlayer = Vector3.Distance(transform.position, player.position);
        if ((player != null) && (DistToPlayer <= attackRange) && (!isWebbed))
        {
            // Determine direction to player
            Vector2 direction = (player.position - transform.position).normalized;

            // Change sprite based on direction
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Horizontal movement
                if (direction.x > 0)
                {
                    spriteRenderer.sprite = spriteRight; // Face right
                }
                else
                {
                    spriteRenderer.sprite = spriteLeft; // Face left
                }
            }
            else
            {
                // Vertical movement
                if (direction.y > 0)
                {
                    spriteRenderer.sprite = spriteUp; // Face up
                }
                else
                {
                    spriteRenderer.sprite = spriteDown; // Face down
                }
            }

            // Timer for shooting projectiles
            if (timeBtwShots <= 0)
            {
                isAttacking = true;
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
                isAttacking = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Web")
        {
            isWebbed = true;
            Debug.Log("I am webbed!");
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Web")
        {
            isWebbed = false;
            Debug.Log("I am free from web");
            gameObject.GetComponentInChildren<SpriteRenderer>().color = startColor;
        }
    }

    IEnumerator HitEnemy()
    {
        rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
        if (EnemyLives < 1)
        {
            Destroy(gameObject);
        }
        else yield return new WaitForSeconds(0.5f);
        rend.material.color = Color.white;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
