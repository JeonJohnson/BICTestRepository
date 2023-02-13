using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public enum VisitState
{ 
    Visiting, // a = 0 
    Visited, // a = 0.5
    Ever //a = 1 
}

public class FOW : MonoBehaviour
{//Fog of War

    public GameObject fogTilePrefab;


    public FogTile[,] fogTiles;

    public GameObject CreateTile(Vector2 pos, int row, int col)
    {
        GameObject obj = Instantiate(fogTilePrefab);
        //obj.tag = "Fog";
        obj.transform.position = new Vector3(pos.x, 10f, pos.y);
        obj.transform.SetParent(transform);

        //Material cloneMat = Instantiate(fogTilePrefab.GetComponentInChildren<MeshRenderer>().material);
        //obj.GetComponentInChildren<MeshRenderer>().material = Instantiate(fogTilePrefab.GetComponentInChildren<MeshRenderer>().material);
        obj.GetComponentInChildren<MeshRenderer>().material = Instantiate(fogTilePrefab.GetComponentInChildren<Renderer>().sharedMaterial);

        fogTiles[row,col] = obj.GetComponent<FogTile>();

        return obj;
    }

    private void Awake()
	{
        fogTiles = new FogTile[128, 128];


    }

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
