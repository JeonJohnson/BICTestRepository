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
public struct BuildingState
{
    public BuildingName name_e;
    
    public float maxHp;
    public float curHp;

    public float dmg;
    public float armor;

    public Vector2 size;

    public float offenseRange;

    public float spawnTime;
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
    public BuildingState state;


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
