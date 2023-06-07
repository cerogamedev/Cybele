using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public string _tag = "Platform"; 

    private Animator anim;
    private Rigidbody2D rb;
    private float minX, maxX;
    private Vector2 direction;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        float uzaklikKameraVeObjeArasi = transform.position.z - Camera.main.transform.position.z;
        Vector3 solSinirNoktasi = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklikKameraVeObjeArasi));
        Vector3 sagSinirNoktasi = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklikKameraVeObjeArasi));

        minX = solSinirNoktasi.x;
        maxX = sagSinirNoktasi.x;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tag) && this.gameObject.GetComponent<ElevatorJump>().isNegative == false)
        {
            ThrowUp();
            collision.gameObject.tag = "UsedPlatform";
        }
    }

    void ThrowUp()
    {

        //rb.AddForce(direction , ForceMode2D.Impulse);
        rb.velocity = direction;
    }
    private void Update()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX +2, maxX -2);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        direction = new Vector2(transform.position.x, 25);

    }
}
