using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonColor : MonoBehaviour
{

    private bool selected = false;

    private Material material;
    

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (selected)
        {
            material.color = Color.green;
        }
        else
            material.color = Color.red;

        selected = false;
    }

    public void Selected() => selected = true;
}
