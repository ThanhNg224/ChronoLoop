using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the bullet collides with an object tagged "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);  // Destroy the bullet
        }
    }

}