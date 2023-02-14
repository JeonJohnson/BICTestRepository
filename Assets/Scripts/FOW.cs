using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public enum VisitState
{ 
    Visiting, //0 : a = 0 
    Visited, //1 :  a = 0.4
    Ever //2 : a = 0.8 
}

public class FOW : MonoBehaviour
{//Fog of War

    private static FOW instance = null;

    public static FOW Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.Find(typeof(FOW).Name).GetComponent<FOW>();
            }

            return instance;
        }
    }

    [SerializeField]
    GameObject fogTilePrefab;


    public FogTile[,] fogTiles;

    public List<FogTile> visitTies = new List<FogTile>();

    public GameObject CreateTile(Vector2 pos, int row, int col)
    {
        GameObject obj = Instantiate(fogTilePrefab);
        //obj.tag = "Fog";
        obj.transform.position = new Vector3(pos.x,0f, pos.y);
        obj.transform.SetParent(transform);

        //Material cloneMat = Instantiate(fogTilePrefab.GetComponentInChildren<MeshRenderer>().material);
        //obj.GetComponentInChildren<MeshRenderer>().material = Instantiate(fogTilePrefab.GetComponentInChildren<MeshRenderer>().material);
        obj.GetComponentInChildren<MeshRenderer>().material = Instantiate(fogTilePrefab.GetComponentInChildren<Renderer>().sharedMaterial);

        FogTile script = obj.GetComponent<FogTile>();
        script.pos = new Vector2(row, col);
        fogTiles[row,col] = script;

        return obj;
    }

    private void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }

        //fogTiles = new FogTile[128, 128];


    }

	void Start()
    {


    }

    void Update()
    {
        for (int i = 0; i < visitTies.Count; ++i)
        {
            visitTies[i].fogState = VisitState.Visited;
            visitTies[i].SetColor();
        }
        visitTies.Clear();
    }
}
