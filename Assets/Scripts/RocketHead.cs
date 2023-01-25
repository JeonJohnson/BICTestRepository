using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHead : MonoBehaviour
{

    public float dmg;

    public float spd;

    public float curTime = 0f;

    public float explosionRange;

    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = Color.red;
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        //transform.position += transform.forward * spd * Time.deltaTime;

        if (curTime >= 5f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ãæµ¹");
        if (other.CompareTag("Environment") | other.CompareTag("Enemy"))
        {
            var cols = Physics.OverlapSphere(transform.position, explosionRange, LayerMask.GetMask("Enemy"));

            foreach (Collider col in cols)
            {
                Enemy enemy = col.transform.root.gameObject.GetComponent<Enemy>();

                if (enemy)
                {
                    enemy.Hit(dmg);
                }
            }
        }

        Destroy(gameObject);
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = color;

        Gizmos.DrawWireSphere(transform.position, explosionRange);
	}
}
