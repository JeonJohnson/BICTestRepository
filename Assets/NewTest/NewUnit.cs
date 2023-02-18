using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUnit : MonoBehaviour
{

    public UnitState state;

    public Vector3 destPos;
    public Vector3 dir;
    public float dist;

    private void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Tile")))
            {
                //GameObject tileObj = hit.collider.gameObject;

                destPos = hit.point;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void OnDrawGizmos()
    {
        Color temp = Color.green;
        temp.a = 0.4f;
        Gizmos.color = temp;

        Gizmos.DrawSphere(transform.position, state.viewRange);
    }
}
