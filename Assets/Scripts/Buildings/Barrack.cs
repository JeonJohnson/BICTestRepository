using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building, ISpawnBuilding
{

	float curTime;
	void ISpawnBuilding.Spawn()
	{
	}
	public override void Hit()
	{
	}

	protected override void Awake()
	{
	}

	protected override void Destoryed()
	{
	}

	protected override void Start()
	{
	}

	protected override void Update()
	{
		curTime += Time.deltaTime;

		if (curTime >= state.spawnTime)
		{
			ObjectManager.Instance.SpawnUnit(0,transform.position);
			curTime = 0f;
		}
	}

}
