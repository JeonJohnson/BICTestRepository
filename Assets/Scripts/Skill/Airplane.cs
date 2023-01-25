using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{

    public GameObject missilePrefab;

    public float spd;


    public List<Transform> missileTrs;

    public float spawnTime;
    public float curTime;


    public void DropMissile()
    {
        curTime += Time.deltaTime;

        if (curTime >= spawnTime)
        {
            curTime = 0f;

            int iRand = Random.Range(0, 4);

            GameObject missile = Instantiate(missilePrefab);

            Transform tr = missileTrs[iRand];
            missile.transform.position = tr.position;
        
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Time.deltaTime * Vector3.right * spd;


        if (transform.position.x >= 80f)
        {
            Destroy(gameObject);
        }

        DropMissile();
    }
}
