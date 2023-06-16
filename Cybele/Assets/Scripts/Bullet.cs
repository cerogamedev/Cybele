using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = transform.position.y / 10;
        Destroy(this.gameObject, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().Damage(damage);

            Destroy(this.gameObject);
        }
    }
}
