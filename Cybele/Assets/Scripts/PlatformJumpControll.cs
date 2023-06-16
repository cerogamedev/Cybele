using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumpControll : MonoBehaviour
{
    public bool isJumper = true;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.gameObject.GetComponent<ElevatorJump>().isNegative == false && isJumper == true)
        {
            player.GetComponent<PlayerControl>().ThrowUp();
            isJumper = false;
        }

    }
}
