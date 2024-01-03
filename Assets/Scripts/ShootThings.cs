using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootThings : MonoBehaviour
{

    [SerializeField] private GameObject shootable;
    [SerializeField] private float timeBetweenShots;
    private float justShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - justShot > timeBetweenShots)
        {
            GameObject temp = Instantiate(shootable, transform);
            temp.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 10f);
            justShot = Time.time;
        }
    }
}
