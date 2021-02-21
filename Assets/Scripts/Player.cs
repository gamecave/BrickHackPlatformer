using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string idNumber;

    int score = 0;

    public float maxSpeed = 3.0f;
    public float jumpHeight = 50.0f;
    public float gravity = 1.8f;
    public bool facingRight = true;
    public bool isGrounded = true;

    public float moveDirection = 0;

    Rigidbody2D rb;
    CapsuleCollider2D collision;
    public Text scoreText;

    float timeForScore = 1.0f;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<CapsuleCollider2D>();

        rb.freezeRotation = true;
        rb.gravityScale = gravity;

        facingRight = transform.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Change face
        if(moveDirection != 0)
        {
            if(moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        // Score Over Time
        timer += Time.deltaTime;

        if(timer >= timeForScore)
        {
            score += 10;
            timer = 0.0f;
            UpdateScore();
        }
        
    }

    public void PlayerMovement(string hori, string vert, string action)
    {
        switch (hori)
        {
            case "1":
                moveDirection = 1;
                break;
            case "-1":
                moveDirection = -1;
                break;
            default:
                moveDirection = 0;
                break;
        }

        rb.velocity = new Vector2((moveDirection * maxSpeed * Time.deltaTime), rb.velocity.y);

        if(vert == "1" || action == "1")
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Bounds bounds = collision.bounds;
        float collisionRadius = collision.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = bounds.min + new Vector3(bounds.size.x * 0.5f, collisionRadius * 0.9f, 0);
        // grounded?
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, collisionRadius);
        // any overlapping? yes, then isGrounded
        isGrounded = false;
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != collision)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            score += 50;
            UpdateScore();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            print("Player hit by projectile");
        }
    }
}
