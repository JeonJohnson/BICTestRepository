using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    protected abstract void Fire();
    public abstract void Hit();
    public abstract void Heal();
    protected abstract void Destoryed();

    protected abstract void Move();


    protected virtual void Awake()
    {
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }

}
