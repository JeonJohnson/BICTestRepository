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

    public Color[] tileVisitStates;

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
        
        

        tileVisitStates = new Color[(int)Mathf.Pow(NewMapGen.Instance.tileCount, 2f)];

		for (int i = 0; i < tileVisitStates.Length; ++i)
		{
			Color temp = Color.black;
			temp.a = 1f;
			tileVisitStates[i] = temp;
		}
	}

    public void VisitTile(IntVector2 index)
    {
        tileVisitStates[(index.y * NewMapGen.Instance.tileCount) + index.x].a = 0f;
    }

    public void ResetFogAlpha()
    {
        //for (int i = 0; i < tileVisitStates.Length; ++i)
        //{
        //    tileVisitStates[i].a = 0.8f;
        //}
    }

    public void ApplyFogAlpha()
    {

        fogTex.SetPixels(tileVisitStates);
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
        ApplyFogAlpha();

    }
}
