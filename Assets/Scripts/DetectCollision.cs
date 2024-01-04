using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class DetectCollision : MonoBehaviour
{

    private Material material;

    private Vector3 defaultFocalPoint;

    [SerializeField] private int maxFocalPoints;

    private int index;

    [SerializeField] private bool destroyCollidedObjects;

    [SerializeField] private float frequency;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;


        defaultFocalPoint = new Vector3(1000f, 1000f, 1000f);

        for (int i = 0; i < maxFocalPoints; i++)
        {
            material.SetVector($"_FocalPoint{i}", defaultFocalPoint);
            material.SetFloat($"_Progression{i}", 0);
        }

        index = 0;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < maxFocalPoints; i++)
        {

            //Debug.Log(i);

            float aux = material.GetFloat($"_Progression{i}");

            if (aux > 0 && aux < 50)
            {
                aux += 1f * frequency * Time.deltaTime;
                material.SetFloat($"_Progression{i}", aux);
            }

            if (aux >= 50)
            {
                material.SetFloat($"_Progression{i}", 0);
                material.SetVector($"_FocalPoint{i}", defaultFocalPoint);
            }
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint  point in collision.contacts)
        {
            
            Debug.Log(transform.InverseTransformPoint(point.point));

            material.SetVector($"_FocalPoint{index % maxFocalPoints}", transform.InverseTransformPoint(point.point));

            material.SetFloat($"_Progression{index % maxFocalPoints}", 0.001f);

            

            index++;

            Debug.Log(index);

            //if (index == int.MaxValue) index = 0;

            if (destroyCollidedObjects) Destroy(collision.gameObject);
        }
    
    }


}
