using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapShape
{
    Square,
    Rhombus
}

public class MapGen : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 firstPos;
    public int xCount;
    public int yCount;
    public MapShape shape;
    
    public Vec3Boundary mapBoundary; //카메라Clamp기준이 될 맵 각 방향 위치

    public GameObject[,] mapArr;

    float cubeSize;
    float cubeDiagonalSize;
    public GameObject tilePrefab;

    public Material PlaneMat;

    GameObject CreateTile(Vector2 pos, int row, int col)
    {
        GameObject obj = Instantiate(tilePrefab);
        obj.tag = "Terrain";
        obj.transform.position = new Vector3(pos.x, 0f, pos.y);

        TestCube script = obj.GetComponent<TestCube>();
        string xStr = row < 10 ? "0" + row.ToString() : row.ToString();
        string yStr = col < 10 ? "0" + col.ToString() : col.ToString();
        script.posTex.text = $"({xStr},{yStr})";

        return obj;
    }

    void RhombusMapGen()
    {
        cubeSize = tilePrefab.transform.localScale.x;
        cubeDiagonalSize = Mathf.Sqrt(cubeSize * 0.5f);

        mapArr = new GameObject[xCount, yCount];

        Vector2 curPos = Vector2.zero;

        for (int y = 0; y < yCount; ++y)
        {
            for (int x = 0; x < xCount; ++x)
            {
                curPos.x += cubeDiagonalSize;
                curPos.y -= cubeDiagonalSize;

                CreateTile(curPos, x, y);
            }

            curPos.x = firstPos.x - (cubeDiagonalSize * y);
            curPos.y = firstPos.y - (cubeDiagonalSize * y);
        }

    }

    void SquareMapGen()
    {
        cubeSize = tilePrefab.transform.localScale.x;

        float side = Mathf.Sqrt(cubeSize * 0.5f);
        //x = Defines.tileX;
        //y = Defines.tileY;
        mapArr = new GameObject[Defines.tileX, Defines.tileY];

        Vector2 pos = Vector2.zero;

        for (int col = 0; col < Defines.tileY; ++col)
        {
            for (int row = 0; row < Defines.tileX; ++row)
            {
                GameObject obj = Instantiate(tilePrefab);
                obj.tag = "Terrain";
                TestCube script = obj.GetComponent<TestCube>();

                string xStr = row < 10 ? "0" + row.ToString() : row.ToString();
                string yStr = col < 10 ? "0" + col.ToString() : col.ToString();
                script.posTex.text = $"({xStr},{yStr})";

                mapArr[row, col] = obj;
                mapArr[row, col].transform.position = new Vector3(pos.x, 0f, pos.y);

                //            int iRand = Random.Range(0, 3);
                //            Material copymat = Instantiate(PlaneMat);
                //            switch (iRand)
                //{
                //                case 0:
                //                    { copymat.color = Color.white; }
                //                    break;
                //                case 1:
                //                    { copymat.color = Color.blue; }
                //                    break;
                //                case 2:
                //                    { copymat.color = Color.green; }
                //                    break;
                //	default:
                //		break;
                //}
                //            GameObject temp = obj.transform.Find("Mesh").gameObject;
                //            MeshRenderer mr = temp.GetComponent<MeshRenderer>();
                //            var mts = mr.materials;
                //            mts[0] = copymat;
                //            mr.materials = mts;

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


    private void Awake()
	{
        switch (shape)
        {
            case MapShape.Square:
                {
                    SquareMapGen();
                }
                break;

            case MapShape.Rhombus:
                {
                    RhombusMapGen();
                }
                break;

            default:
                break;
        }

    }
    void Start()
    {



	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
