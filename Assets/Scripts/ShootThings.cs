using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootThings : MonoBehaviour
{

    [SerializeField] private GameObject shootable;
    [SerializeField] private float timeBetweenShots;
    private float justShot;

    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - justShot > timeBetweenShots)
        {
            GameObject temp = Instantiate(shootable, transform.position, transform.rotation, transform);
            temp.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            justShot = Time.time;
        }
    }
}
