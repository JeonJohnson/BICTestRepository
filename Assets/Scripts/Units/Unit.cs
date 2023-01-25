using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public enum UnitName
{ 
    RifleMan,
    Medic,
    MachinegunMan,
    Sniper,
    RocketMan,


    End
}

[System.Serializable]
public struct UnitState
{
    public UnitName name_e;

    public float maxHp;
    public float curHp;

    public float range;

    public float dmg;

    public float rapid;

    public float reloadTime;

    public float healAmount;
    public float healTime;

    public float moveSpd;
}



public abstract class Unit : MonoBehaviour
{
    public UnitState state;

    public float bulletSpd;
    public Transform barrel;
    public int maxRound;
    public int curRound;
    public float curTime;

    public Enemy target;
    protected float dist;
    protected Vector3 dir;

    public NavMeshAgent navAgent;


    public void SetTransform(Transform tr)
    {
        navAgent.enabled = false;
        transform.position = tr.position;
        transform.rotation = tr.rotation;
        navAgent.enabled = true;
    }

    public void SetPosition(Vector3 pos)
    {
        navAgent.enabled = false;
        transform.position = pos;
        navAgent.enabled = true;
    }
    protected virtual Bullet Fire()
    {
        if (target != null && dist <= state.range)
        {
            curTime += Time.deltaTime;

            if (curTime >= state.rapid)
            {
                GameObject bulletObj = Instantiate(ObjectManager.Instance.bulletPrefab);

                bulletObj.transform.position = barrel.position;
                bulletObj.transform.forward = dir;
                bulletObj.GetComponent<Bullet>().dmg = state.dmg;
                bulletObj.GetComponent<Bullet>().spd = bulletSpd;
                --curRound;
                curTime = 0f;

                return bulletObj.GetComponent<Bullet>();
            }

            return null;
        }
        return null;
    }

    public virtual void Reload()
    {
        curTime += Time.deltaTime;

        if (curTime >= state.reloadTime)
        {
            curRound = maxRound;
        }
    }
    public abstract void Hit();
    public abstract void Heal();
    protected abstract void Destoryed();

    protected virtual void Move()
    {

    }

    protected virtual void Search()
    { 
          
    }


    protected virtual void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = state.moveSpd;
	}
	protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.gameObject.transform.position, state.range);
	}

	//protected void OnDrawGizmos()
	//{
	//       Gizmos.color
	//           = Color.red;


	//}


}
