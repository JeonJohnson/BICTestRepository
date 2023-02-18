using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFOW : MonoBehaviour
{
	#region singleton
	private static NewFOW instance = null;

    public static NewFOW Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find(typeof(NewFOW).Name).GetComponent<NewFOW>();
            }

            return instance;
        }
    }
    #endregion

    public Material fogMat;
    public Texture2D fogTex;

    public const float Visiting = 0f;
    public const float Visited = 0.4f;
    public const float Ever = 0.8f;


    public List<NewUnit> unitList;

    public NewFogTile[] allFogTiles;
    public List<NewFogTile> visitFogTiles; //이전 프레임에서 밝혀졌던 타일들
    public Color[] tileAlpha; //실제 쉐이더로 보낼(텍스쳐에 쓸) 알파값
    //전체 타일 개수 만큼 있음.

    void InitFogPlane()
	{
        GameObject fogPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        fogPlane.name = "FogTiles";
        fogPlane.layer = LayerMask.NameToLayer("FogTile");

        fogPlane.transform.position = NewMapGen.Instance.centerPos;
        fogPlane.transform.localScale = new Vector3(NewMapGen.Instance.planeSize * 0.1f, 1f, NewMapGen.Instance.planeSize * 0.1f);
        fogPlane.transform.rotation = Quaternion.Euler(0f, 45, 180f);
        //현재 우리는 Object들의 좌상단이 0,0인데 반해
        //텍스쳐 좌표계에서는 좌하단이 0,0이라서
        //그거 맞춘다구 180도 돌려주는거
        
		fogTex = new Texture2D(NewMapGen.Instance.tileCount, NewMapGen.Instance.tileCount);

        MeshRenderer mr = fogPlane.GetComponent<MeshRenderer>();

        mr.material = Instantiate(fogMat);
        mr.material.SetTexture("_MainTex", fogTex);

        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = false;

        //unitList = new List<NewUnit>();
        visitFogTiles = new List<NewFogTile>();
        allFogTiles = new NewFogTile[(int)Mathf.Pow(NewMapGen.Instance.tileCount, 2f)];
        tileAlpha = new Color[(int)Mathf.Pow(NewMapGen.Instance.tileCount, 2f)];

		for (int i = 0; i < allFogTiles.Length; ++i)
        {
            allFogTiles[i] = new NewFogTile();
            allFogTiles[i].fogState = VisitState.Ever;
        }
	}

    public void VisitTile(Vector3 curPos, float sightRange)
    {
        //int index = (_index.y * NewMapGen.Instance.tileCount) + _index.x;

        // x^2 + y^2 <= range^2 

        int rangeInt = (int)sightRange;

        for (int i = -rangeInt; i <= rangeInt; i++)
        {
            for (int j = -rangeInt; j <= rangeInt; j++)
            {
                if (i * i + j * j <= sightRange * sightRange)
                {
                    Vector2 pos = new Vector2(curPos.x + i, curPos.z + j);
                    IntVector2 vec2Index = NewMapGen.Instance.GetTileIndex(pos);
                    int index = (vec2Index.y * NewMapGen.Instance.tileCount) + vec2Index.x;
                    allFogTiles[index].fogState = Visiting;
                    visitFogTiles.Add(allFogTiles[index]);
                }
            }
        }

        //tileVisitStates[(index.y * NewMapGen.Instance.tileCount) + index.x].a = 0f;

    }

    public void ResetFogAlpha()
    {
        //이전 프레임에서 밝혀졌던 타일들 반투명으로 바꾸기
        //(이 이후에 유닛들의 현재 위치 다시 받아옴으로 ㄱㅊ)
        foreach (NewFogTile tile in visitFogTiles)
        {
            tile.fogState = VisitState.Visited;
        }

        visitFogTiles.Clear();

        //for (int i = 0; i < tileVisitStates.Length; ++i)
        //{
        //    tileVisitStates[i].a = 0.8f;
        //}
    }

    public void ApplyFogAlpha()
    {
        //fogTile들 다 돌면서 얻은 알파값을 실제 텍스쳐에 넣는거

        for(int i = 0; i < allFogTiles.Length;++i)
        {
            Color temp = Color.black;
            temp.a = ((int)allFogTiles[i].fogState) * 0.4f;
            tileAlpha[i] = temp;
        }

        fogTex.SetPixels(tileAlpha);
        fogTex.Apply();

        //for (int i = 0; i < NewMapGen.Instance.tileCount; ++i)
        //{
        //    for (int k = 0; k < NewMapGen.Instance.tileCount; ++k)
        //    { 
        //    }
        //}
    }


	private void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }
	}

	void Start()
    {
        InitFogPlane();
    }

    void Update()
    {
        //ResetFogAlpha();
        //ApplyFogAlpha();
    }

	private void FixedUpdate()
	{
        ResetFogAlpha();
        foreach (NewUnit unit in unitList)
        {
            VisitTile(unit.transform.position, unit.state.viewRange);
        }
        ApplyFogAlpha();

    }
}
