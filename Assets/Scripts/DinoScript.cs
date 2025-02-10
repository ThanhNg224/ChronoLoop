using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoScript : MonoBehaviour
{
    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float speedMove = 1f;



    //public PlayerMovement playerMovement;

    public GameObject deathEffect;


    private Rigidbody2D body;



    private bool facingLeft = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();


    }

    private void Run()
    {
        if (facingLeft)
        {
            if (transform.position.x > left)
            {
                // Đảm bảo hướng di chuyển đúng
                if (transform.localScale.x != 5f)
                {
                    transform.localScale = new Vector2(5f, 5f);
                }

                // Di chuyển về bên trái
                body.velocity = new Vector2(-speedMove, 0f);
            }
            else
            {
                // Khi chạm giới hạn bên trái, đổi hướng
                body.velocity = Vector2.zero;
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < right)
            {
                // Đảm bảo hướng di chuyển đúng
                if (transform.localScale.x != -5f)
                {
                    transform.localScale = new Vector2(-5f, 5f);
                }

                // Di chuyển về bên phải
                body.velocity = new Vector2(speedMove, 0f);
            }
            else
            {
                // Khi chạm giới hạn bên phải, đổi hướng
                body.velocity = Vector2.zero;
                facingLeft = true;
            }
        }
    }



    void FixedUpdate()
    {

        Run();
        //Deadth();

    }
    private void DeadthEffect()
    {

        if (deathEffect != null)
        {

            deathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);

            Destroy(deathEffect, .5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (playerMovement.falling == true)
            //{
            //    DeadthEffect();
            //    Destroy(gameObject);
            //}
        }
    }
}
