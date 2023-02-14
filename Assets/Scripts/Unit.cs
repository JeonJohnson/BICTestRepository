using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitState
{
    public string name;
    public float curHp;
    public float fullHp;

    public float moveSpd;

    public float viewRange;

}

public class Unit : MonoBehaviour
{
	public UnitState state;

	public Vector3 destPos;
	public Vector3 dir;
	public float dist;


	public List<FogTile> visitedTiles = new List<FogTile>();

	private void FindAroundTile()
	{
		var cols = Physics.OverlapSphere(transform.position, state.viewRange,LayerMask.GetMask("Terrain"));

		for (int i = 0; i < cols.Length; ++i)
		{
			Vector2 pos = cols[i].GetComponentInParent<TestCube>().pos;

			FOW.Instance.fogTiles[(int)pos.x, (int)pos.y].fogState = VisitState.Visiting;
			FOW.Instance.visitTies.Add(FOW.Instance.fogTiles[(int)pos.x, (int)pos.y]);
			FOW.Instance.fogTiles[(int)pos.x, (int)pos.y].SetColor();
			
		}
			
	}


	private void Move()
	{
		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Terrain")))
			{
				//GameObject tileObj = hit.collider.gameObject;

				destPos = hit.collider.gameObject.transform.position;
			}
		}

		dir = (destPos - transform.position).normalized;
		dist = Vector3.Distance(destPos, transform.position);
		if (dist > 0.25f)
		{ transform.position += dir * Time.deltaTime * state.moveSpd; }
	}

	// Start is called before the first frame update
	void Start()
	{
		destPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		Move();
		
	}

	private void LateUpdate()
	{
		FindAroundTile();
	}

	private void OnDrawGizmos()
	{
		Color temp = Color.green;
		temp.a = 0.4f;
		Gizmos.color = temp;

		Gizmos.DrawSphere(transform.position, state.viewRange);
	}
}

