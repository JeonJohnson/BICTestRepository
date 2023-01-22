using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float dmg;

    public float spd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * spd * Time.deltaTime;
    }

	public void OnTriggerEnter(Collider other)
	{
        Debug.Log("충돌");
        if (other.CompareTag("Enemy"))
        {
            Enemy script = other.GetComponent<Enemy>();

            if (script&& !script.isDead)
            {
                Debug.Log("적이랑 충돌");
                other.GetComponent<Enemy>().Hit(dmg);
                Destroy(gameObject);
            }
        }
	}
}
