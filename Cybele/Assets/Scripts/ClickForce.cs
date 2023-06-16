using UnityEngine;

public class ClickForce : MonoBehaviour
{
    public float clickForce = 5f;
    public int maxJumpCount = 3;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private Animator anim;
    [HideInInspector] public int jumpCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (clickPosition - (Vector2)transform.position).normalized;
            if (this.transform.position.x == clickPosition.x && this.transform.position.y == clickPosition.y)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.AddForce(direction * clickForce, ForceMode2D.Impulse);
            }
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            jumpCount = 0; // Yere deðince hakký sýfýrla
        }
    }
}
