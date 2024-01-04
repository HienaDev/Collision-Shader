using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject shootable;
    [SerializeField] private GameObject saveBullets;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private GameObject shield;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject temp = Instantiate(shootable, transform.position, transform.rotation, saveBullets.transform);
            temp.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            shield.SetActive(!shield.activeSelf);
        }
    }
}
