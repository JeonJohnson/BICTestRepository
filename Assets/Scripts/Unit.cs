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

}

public class Unit : MonoBehaviour
{
    public UnitState state;

    public Vector3 destPos;
    public Vector3 dir;
    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        destPos = transform.position;
    }

    // Update is called once per frame
    void Update()
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
        if (dist > 0.1f)
        { transform.position += dir * Time.deltaTime * state.moveSpd; }

	}
}
