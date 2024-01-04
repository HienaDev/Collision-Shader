using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastButton : MonoBehaviour
{

    [SerializeField] private LayerMask ballMask;

    [SerializeField] private GameObject[] turrets;

    // Start is called before the first frame update
    void Start()
    {
        //ballMask = 1 << ballMask;
        //ballMask = ~ballMask;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.green);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, ballMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(hit.collider.gameObject.name);

            hit.collider.gameObject.GetComponent<ChangeButtonColor>()?.Selected();

            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (GameObject turret in turrets)
                {
                    turret.SetActive(!turret.activeSelf);
                }
            }
        }
    }
}
