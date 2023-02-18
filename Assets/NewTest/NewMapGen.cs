using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileLayoutType
{ 
    Sqaure,
    Rhombus
}

public struct IntVector2
{
    public IntVector2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int x;
    public int y;
}

public class NewMapGen : MonoBehaviour
{
	#region singleton
	private static NewMapGen instance = null;

    public static NewMapGen Instance
    {
        get 
        {
            return instance;
        }
    }
	#endregion


	[Header("UserSetting Varis")]
    public Material planeMat;
    public TileLayoutType layoutType;
    public float tileSize;
    public int tileCount;
    //public int yCount;
    public Vector2 centerPos;

    [Header("Compute Varis")]
    public float tileDiagSize;
    public float planeSize;
    public float planeDiagSize;
    [HideInInspector]
    public Vector2[,] tilePos;

    //public Vec3Boundary mapBoundary;
    public Vec2Boundary camBoundary;


    public void ComputeDefaultVaris()
    {
        tilePos = new Vector2[tileCount, tileCount];
        
        tileDiagSize = tileSize / Mathf.Sqrt(2f) * 2f; 
        tileDiagSize = Mathf.Sqrt(Mathf.Pow(tileSize, 2f) + Mathf.Pow(tileSize, 2f));//테스트

        planeSize = tileSize * tileCount;
        //planeSize.y = (tileSize * yCount) * 0.1f;

        planeDiagSize = planeSize * 10f / Mathf.Sqrt(2f) * 2f;
        planeDiagSize = Mathf.Sqrt(Mathf.Pow(planeSize, 2f) + Mathf.Pow(planeSize, 2f));
    }

    public void InitMap()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = centerPos;
        if (layoutType == TileLayoutType.Rhombus)
        { plane.transform.rotation = Quaternion.Euler(new Vector3(0, 45f, 0f)); }
        plane.transform.localScale = new Vector3(planeSize * 0.1f, 1f, planeSize * 0.1f);
        plane.layer = LayerMask.NameToLayer("Tile");

        Material mat = Instantiate(planeMat);
        mat.mainTextureScale = new Vector2(tileCount, tileCount);
        plane.GetComponent<Renderer>().material = mat;

        for (int y = 0; y < tileCount; ++y)
        {
            for (int x = 0; x < tileCount; ++x)
            {
                tilePos[x, y] = GetTileCenterPos(x, y);
            }
        }

        camBoundary.LT = MapGen.CenterPos(tilePos[0, 0], tilePos[0, tileCount - 1]);
        camBoundary.RT = MapGen.CenterPos(tilePos[0, 0], tilePos[tileCount -1 ,0]);
        camBoundary.RB = MapGen.CenterPos(tilePos[tileCount - 1, 0], tilePos[tileCount -1 , tileCount - 1]);
        camBoundary.LB = MapGen.CenterPos(tilePos[tileCount - 1, tileCount - 1], tilePos[0,tileCount-1]);

        camBoundary.Center = MapGen.CenterPos(camBoundary.LT, camBoundary.RB);
    }


    public IntVector2 GetTileIndex(Transform tr)
    {

        return new IntVector2();
    }

    public IntVector2 GetTileIndex(Vector2 pos)
    {

        Vector2 firstPos = new Vector2();
        firstPos.x = centerPos.x;
        firstPos.y = centerPos.y + (planeDiagSize * 0.5f) - (tileDiagSize * 0.5f);


        //float tempX = ((pos.x - firstPos.x) - (pos.y - firstPos.y)) / (tileDiagSize * 0.5f);
        //float tempY = ((pos.x - firstPos.y) - (pos.y - firstPos.y)) / (tileDiagSize * 0.5f);

        float tempX = ((pos.x - firstPos.x -pos.y+firstPos.y) / (tileDiagSize * 0.5f)) * 0.5f;
        float tempY = ((-pos.y + firstPos.y -pos.x +firstPos.x) / (tileDiagSize * 0.5f)) * 0.5f;
        //Debug.Log("IsoVec : " + new Vector2(tempX, tempY));

        int x = Mathf.RoundToInt(tempX);
        int y = Mathf.RoundToInt(tempY);

        return new IntVector2(x,y);
    }

    public Vector2 GetTileCenterPos(int xIndex, int yIndex)
    {
        //원점에서부터 얼마나 떨어져 있는지를 찾는 방법.

        Vector2 firstPos = new Vector2();
        float xPos = 0f;
        float yPos = 0f;

        if (layoutType == TileLayoutType.Sqaure)
        {
            firstPos.x = centerPos.x - (planeSize * 0.5f) + (tileSize * 0.5f);
            firstPos.y = centerPos.y + (planeSize * 0.5f) + (tileSize * 0.5f);

            xPos = (xIndex * tileSize);
            yPos = (yIndex * tileSize);
        }
        else if (layoutType == TileLayoutType.Rhombus)
        {
            firstPos.x = centerPos.x;
            firstPos.y = centerPos.y + (planeDiagSize * 0.5f) - (tileDiagSize * 0.5f);

            //마름모꼴 타일맵 => 아이소 매트릭 형식
            //이 과정을 일반 world 좌표계 => 아이소 메트릭 좌표계 전환 임.
            //이게 뭐라 설명하기는 애매한게
            //진짜 그려서 확인해보면, 진짜 이렇게 됨.
            //굳이 설명을 하자면,
            //최상단 부분이 index[0,0]이고
            //오른쪽으로 (Index.x++) 갈수록 x값은 증가, y 값은 감소.
            //아래쪽으로 (Index.y--) 갈수록 x값은 감소, y값도 감소

            xPos = (xIndex - yIndex) * (tileDiagSize * 0.5f);
            //=> //xPos = firstPos.x + (xIndex * tileDiagSize) - (yIndex * tileDiagSize);

            yPos = (xIndex + yIndex) * (tileDiagSize * 0.5f);
            //=> //yPos = firstPos.y - (xIndex * tileDiagSize) - (yIndex * tileDiagSize);
            
        }

        //float xPos = layoutType == TileLayoutType.Sqaure ? 
        //    (xIndex * tileSize) + (tileSize * 0.5f) - (planeSize.x * 0.5f) :
        //    (xIndex * diaSize) + diaSize - (planeSize.x * 0.5f);
        
        //float yPos = layoutType == TileLayoutType.Sqaure ?
        //    (yIndex * tileSize) + (tileSize * 0.5f) - (planeSize.y * 0.5f) :
        //    (yIndex * diaSize) + diaSize - (planeSize.y * 0.5f);

        return new Vector2(firstPos.x +  xPos, firstPos.y - yPos);
    }

    public void ComputeViewRange()
    { 
    
    }

    public void CheckMouseRay()
    { 
    
    
    }

	private void Awake()
	{
		#region singleton
		if (instance == null)
        {
            instance = this;
        }
		#endregion

		ComputeDefaultVaris();
        InitMap();
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
			{
				//Debug.Log("Hit Pos : " + new Vector2(hit.point.x, hit.point.z));

				IntVector2 tileIndex = GetTileIndex(new Vector3(hit.point.x, hit.point.z));
                Debug.Log("Tile Index : " + new Vector2( tileIndex.x, tileIndex.y));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(camBoundary.LB.x, 0f, camBoundary.LB.y), 1f);

        Gizmos.color = Color.yellow; //RB
        Gizmos.DrawSphere(new Vector3(camBoundary.RB.x, 0f, camBoundary.RB.y), 1f);

        Gizmos.color = Color.green; //RT
        Gizmos.DrawSphere(new Vector3(camBoundary.RT.x, 0f, camBoundary.RT.y), 1f);

        Gizmos.color = Color.blue; //LT
        Gizmos.DrawSphere(new Vector3(camBoundary.LT.x, 0f, camBoundary.LT.y), 1f);

    }
}
