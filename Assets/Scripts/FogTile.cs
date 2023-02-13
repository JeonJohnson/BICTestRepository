using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTile : MonoBehaviour
{
    public VisitState fogState;
	public Material mat;

	private void Awake()
	{
		
	}

	private void Start()
	{
		mat = GetComponentInChildren<Renderer>().material;
	}

	private void Update()
	{
		Color color = Color.black;
		color.a = (float)fogState * 0.5f;		
		mat.SetColor("_Color",color);
	}
}
