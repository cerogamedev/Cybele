using UnityEngine;

public class ElevatorJump : MonoBehaviour
{
    public float negativeDuration; 
    public float revertDelay = 2f; 
    private bool isNegative = false; 
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float negativeTimer = 0f; 
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; 
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isNegative)
        {
            this.GetComponent<PlayerMovementWithDash>().enabled = false;
            isNegative = true; 
            negativeTimer = 0f; 
            spriteRenderer.color = Color.red; 
            rb.gravityScale = -1f; 
            Invoke("RevertToOriginal", negativeDuration); 
        }

        if (isNegative)
        {
            negativeTimer += Time.deltaTime; 

            if (negativeTimer >= negativeDuration + revertDelay)
            {
                isNegative = false; 
                spriteRenderer.color = originalColor; 
                rb.gravityScale = 1f; 
            }
        }
    }

    private void RevertToOriginal()
    {
        spriteRenderer.color = originalColor; 
        rb.gravityScale = 1f; 
        this.GetComponent<PlayerMovementWithDash>().enabled = true;

    }
}
