using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    // Start is called before the first frame update
    public int x;
    public int y;
    public GameObject[,] mapArr;

    int cubeSize = 1;
    
    public GameObject prefab;
   


	private void Awake()
	{
		
	}
	void Start()
    {
        float side = Mathf.Sqrt(0.5f);
        //x = Defines.tileX;
        //y = Defines.tileY;
        mapArr = new GameObject[Defines.tileX, Defines.tileY];

        Vector2 pos = Vector2.zero;

        for (int col = 0; col < Defines.tileY; ++col)
		{
            for (int row = 0; row < Defines.tileX; ++row)
            {
                GameObject obj = Instantiate(prefab);
                TestCube script = obj.GetComponent<TestCube>();

                string xStr = row < 10 ? "0" + row.ToString() : row.ToString();
                string yStr = col < 10 ? "0" + col.ToString() : col.ToString();
                script.posTex.text = $"({xStr},{yStr})";

                mapArr[row, col] = obj;
                mapArr[row, col].transform.position = new Vector3(pos.x, 0f, pos.y);

                obj.transform.SetParent(transform);
                //mapArr[, k] = Instantiate(prefab);
                //mapArr[i, k].transform.position = new Vector3(i, 0f, k);
                //mapArr[i, k].transform.SetParent(transform);
                pos.x += side * 2f;
            }

            if (col == 0 | col % 2 == 0)
            { //0이거나 2의 배수이면 0.5씩 더 
                pos.x = side;
            }
            else
            {
                pos.x = 0f;
            }

            pos.y += side;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
