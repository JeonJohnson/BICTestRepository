using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rally : MonoBehaviour
{

    public List<GameObject> objs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Funcs.RayResult result = Funcs.RayToWorld(Input.mousePosition, LayerMask.GetMask("Environment"));

        if (result.isHit)
        {
            transform.position = result.hitPosition;
        }

        foreach (GameObject obj in objs)
        {
            obj.SetActive(result.isHit);
        }


        if (Input.GetMouseButtonDown(0) )
        {
            if (result.isHit)
            {
                
            }
            else
            { 
            
            }

            gameObject.SetActive(false);
        }


        //if (result.isHit)
        //{
            

        //}
        //else 
        //{
        //    foreach (GameObject obj in objs)
        //    {
        //        obj.SetActive(false);
        //    }
        //}

    }
}
