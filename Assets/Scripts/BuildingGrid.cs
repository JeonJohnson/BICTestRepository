using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public GameObject gridPrefab;

    GameObject[,] girds = new GameObject[5, 5];

    public void CreateGrids()
    {
        float diagonal = Mathf.Sqrt(0.5f);

        Vector3 pos = Vector3.zero;

        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                GameObject obj = Instantiate(gridPrefab);
                obj.transform.SetParent(transform);
                obj.transform.position = pos;
                girds[x, y] = obj;

                obj.SetActive(false);

                pos.x += diagonal;
                pos.y -= diagonal;
            }

            pos.x = 0f - (diagonal * y);
            pos.y = 0f - (diagonal * y);
        }
    
    }

    public void SettingSquareGrid(int size)
    { 
        
    
    }

    public void SettingRectangleGrid(int xSize, int ySize)
    { 
    

    }

	private void Awake()
	{
		
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
