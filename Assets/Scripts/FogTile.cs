using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTile : MonoBehaviour
{
    public VisitState fogState;
	public Material mat;

	public Vector2 pos;
	private void Awake()
	{
		
	}

	private void Start()
	{
		mat = GetComponentInChildren<Renderer>().material;
		
		fogState = VisitState.Ever;
		SetColor();
	}

	private void Update()
	{

	}

	private void LateUpdate()
	{

	}

	public void SetColor()
	{
		Color color = Color.black;

		//		color.a = (float)fogState * 0.4f;

		switch (fogState)
		{
			case VisitState.Visiting:
				{
					color.a = 0f;
				}
				break;
			case VisitState.Visited:
				{
					color.a = 0.5f;
				}
				break;
			case VisitState.Ever:
				{
					color.a = 1f;
				}
				break;
			default:
				break;
		}
		mat.SetColor("_Color", color);
	}
}
