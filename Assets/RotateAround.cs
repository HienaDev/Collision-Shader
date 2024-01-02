using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    [SerializeField] private float rotatingStrenght;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0f, 1f, 0f), rotatingStrenght * Time.deltaTime);
    }
}
