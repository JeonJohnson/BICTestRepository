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

    public Enemy target;

    public NavMeshAgent navAgent;


    public void SetPosition(Transform tr)
    {
        navAgent.enabled = false;
        transform.position = tr.position;
        transform.rotation = tr.rotation;
        navAgent.enabled = true;
    }
    protected abstract void Fire();
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
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }

}
