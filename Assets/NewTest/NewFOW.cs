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
                var obj = GameObject.Find("NewFOW");
                if (obj != null)
                { instance = obj.GetComponent<NewFOW>(); }
            }

            return instance;
        }
    }
    #endregion

    public bool fogBlurEffect;
    public Texture2D fogTex; //딱 기본적인 컬러값만 가지고 있을 친구
    public Material fogMat;
    
    public RenderTexture blurTex; //블러효과까지 먹인 최종값
    public Material blurMat;


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


        MeshRenderer mr = fogPlane.GetComponent<MeshRenderer>();
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = false;

        #region Texture/Material
        int mapLength = NewMapGen.Instance.tileCount;

        fogTex = new Texture2D(mapLength, mapLength, TextureFormat.ARGB32, false);
        fogMat = new Material(Shader.Find("Custom/FogShader"));

        blurTex = RenderTexture.GetTemporary(mapLength, mapLength);
        blurMat = new Material(Shader.Find("Custom/BlurShader"));
		#endregion
		
        mr.material = fogMat;
		//mr.material.SetTexture("_MainTex", fogTex);




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
        //이게 되는 이유가
        //원점을 0으로 뒀을때 
        //특정한 점(x,y) 까지의 거리가 sqrt(x^2+y^2) 임.
        //그게 거리(range)보다 작아야 하는걸
        //좀 풀이한 식
        //즉, 최대 거리와 같거나 작은 점을 고르는 것.
        int rangeInt = (int)sightRange;

        List<IntVector2> tempList = new List<IntVector2>();

        for (int y = -rangeInt; y <= rangeInt; y++)
        {
            for (int x = -rangeInt; x <= rangeInt; x++)
            {
                if (x * x + y * y <= sightRange * sightRange)
                {
                    Vector2 pos = new Vector2(curPos.x + x, curPos.z + y);
                    IntVector2 vec2Index = NewMapGen.Instance.GetTileIndex(pos);
                    tempList.Add(vec2Index);
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

        Graphics.Blit(fogTex, blurTex, blurMat);

        Texture tex = fogBlurEffect ? blurTex : fogTex;
        fogMat.SetTexture("_MainTex",tex);
    }

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
        Graphics.Blit(source, destination, blurMat);    
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


	private void OnDisable()
	{
        RenderTexture.ReleaseTemporary(blurTex);
	}
}
