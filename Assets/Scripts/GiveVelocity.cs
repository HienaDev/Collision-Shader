using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveVelocity : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
