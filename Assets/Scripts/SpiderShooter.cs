using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShooter : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {

        yield return new WaitForSeconds(Random.Range(1, 4));

        Instantiate(bullet, transform.position, Quaternion.identity);
        StartCoroutine(Attack());

    }


}
