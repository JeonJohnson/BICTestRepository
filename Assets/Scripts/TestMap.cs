using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    // Start is called before the first frame update
    int x;
    int y;
    public GameObject[,] mapArr;

    int cubeSize = 1;
    public Transform box;
    public GameObject prefab;
   

    //public float minX;
    //public float maxX;
    //public float minZ;
    //public float maxZ;

	private void Awake()
	{
		
	}
	void Start()
    {
        x = Defines.tileX;
        y = Defines.tileY;
        //minX = 0;
        //minZ = 0;
        //maxX = minX + (x * cubeSize);
        //maxZ = minZ + (y * cubeSize);

        mapArr = new GameObject[x, y];

        for(int i = 0; i <x; ++i)
		{
            for (int k = 0; k < y; ++k)
            {
                mapArr[i, k] = Instantiate(prefab);
                mapArr[i, k].transform.position = new Vector3(i, 0f, k);
                mapArr[i, k].transform.SetParent(box);

            }
		}



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
