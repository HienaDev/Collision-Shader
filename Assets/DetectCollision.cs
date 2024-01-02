using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class DetectCollision : MonoBehaviour
{

    private Material material;
    private float progression;

    private Vector3 defaultFocalPoint;

    [SerializeField] private int maxFocalPoints;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        progression = 0;

        defaultFocalPoint = new Vector3(0f, 0f, 0f);

        material.SetVector("_FocalPoint0", defaultFocalPoint);
        material.SetVector("_FocalPoint1", defaultFocalPoint);

        index = 0;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < maxFocalPoints; i++)
        {
            float aux = material.GetFloat($"_Progression{i}");


            if (aux > 0 && aux < 1)
            {
                aux += 1f * Time.deltaTime;
                material.SetFloat($"_Progression{i}", aux);
            }

            

            if (aux >= 1)
            {
 
                material.SetVector($"_FocalPoint{i}", defaultFocalPoint);
            }
        }
            




        //if (progression > 0 && progression < 1)
        //{
        //    progression += 1f * Time.deltaTime;
        //    material.SetFloat("_Progression0", progression);
        //}

        //if (progression >= 1)
        //{ 
        //    progression = 0f;
        //    material.SetVector("_FocalPoint0", defaultFocalPoint);
        //}


    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint  point in collision.contacts)
        {
            
            Debug.Log(transform.InverseTransformPoint(point.point));

            material.SetVector($"_FocalPoint{index % maxFocalPoints}", transform.InverseTransformPoint(point.point));

            material.SetFloat($"_Progression{index % maxFocalPoints}", 0.001f);

            index++;

            Destroy(collision.gameObject);
        }
    
    }


}
