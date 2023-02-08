using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingName
{ 
    
   

}

public class BuildingPlacer : MonoBehaviour
{
    public GameObject gridPrefab;

    GameObject[,] grids = new GameObject[5, 5];

    public bool isPlacing = false;

    GameObject preOverTile = null;

    Vector2 curSize;

    Vector3 CenterPos(GameObject pos1, GameObject pos2)
    {
        Vector3 dir = (pos1.transform.position - pos2.transform.position).normalized;
        float dist = Vector3.Distance(pos1.transform.position, pos2.transform.position);
        return pos2.transform.position + (dir * (dist * 0.5f));
    }


    public void CreateGrids()
    {
        float diagonal = Mathf.Sqrt(0.5f);
        Vector3 pos = new Vector3(0f, 0.001f, 0f);
        for (int y = 0; y < 5; ++y)
        {
            pos.x = 0f - (diagonal * y);
            pos.z = 0f - (diagonal * y);
            
            for (int x = 0; x < 5; ++x)
            {
                GameObject obj = Instantiate(gridPrefab);
                obj.transform.SetParent(transform);
                obj.transform.position = pos;
                grids[x, y] = obj;

                obj.SetActive(false);

                pos.x += diagonal;
                pos.z -= diagonal;
            }
        }
    }

    public void SettingSquareGrid(int size)
    {
        //정사각형(가로세로 길이 같은 건물)

        curSize.x = size;
        curSize.y = size;

        for (int y = 0; y < size; ++y)
        {
            for (int x = 0; x < size; ++x)
            {
                grids[x, y].SetActive(true);
            }
        }
        isPlacing = true;
    }

    public void SettingRectangleGrid(int xSize, int ySize)
    {


    }

    public void CancelPlace()
    {
        isPlacing = false;

        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                grids[x, y].SetActive(false);
            }
        }
    }

    public void Placing()
    {
        if (isPlacing)
        {
            //마우스 위치에 레이 쏘기
            //충돌한 타일의 인덱스, 위치 파악하기
            //이전 타일과 다른경우 grid도 따라가기,
            //같은 경우 그대로

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelPlace();
                return;
            }

            if (Input.GetMouseButtonDown(0))
            { 
                GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);

                var scale = building.transform.localScale;
                scale.x = curSize.x;
                scale.y = curSize.x;
                scale.z = curSize.x;
                building.transform.localScale = scale;

                building.transform.rotation = Quaternion.Euler(new Vector3(0f, 45f, 0f));

                building.transform.position = CenterPos(grids[0,0],grids[(int)curSize.x-1,(int)curSize.y-1]);

                CancelPlace();
                return;
            }
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,100f, LayerMask.NameToLayer("Terrain")))
            {
                GameObject tileObj = hit.collider.gameObject;

                if (preOverTile == null)
                {
                    preOverTile = tileObj;
                }
                else
                {
                    if (preOverTile != tileObj)
                    {
                        transform.position = hit.collider.transform.position;
                    }
                }
            }



        }
    }

    private void Awake()
	{
        CreateGrids();

    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Placing();
    }
}
