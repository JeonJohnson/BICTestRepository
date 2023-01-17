using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGrid : MonoBehaviour
{

    public Material groundMat;

    public int rowCount; //15
    public int colCount; //60

    public void CreateGrid()
    {
        Vector3 firstPos = new Vector3(0f, 0.5f, 0f);

        for (int row = 0; row < rowCount; ++row)
        {
            firstPos.x = 0f;
            
            for (int col = 0; col < colCount; ++col)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = firstPos;
                cube.transform.parent = transform;

                cube.GetComponent<MeshRenderer>().material = groundMat;


                if (col != colCount - 1)
                { firstPos.x += 1f; }
            }

            firstPos.z += 1f;
        }

    }


	private void Awake()
	{
        CreateGrid();

	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
