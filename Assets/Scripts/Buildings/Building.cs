using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingName
{ 
    Nexsus,
    Bunker,
    Barrack,
    Trap,
    Barricade,
    End
}

[System.Serializable]
public struct State
{
    public BuildingName name_e;
    
    public float maxHp;
    public float curHp;

    public float dmg;
    public float armor;

    public Vector2 size;
}

public interface IOffensiveBuilding
{
    public void Fire();
    
}

public interface ISpawnBuilding
{
    public void Spawn();
}

public abstract class Building : MonoBehaviour
{
    public State state;


    public abstract void Hit();
    protected abstract void Destoryed();

    
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