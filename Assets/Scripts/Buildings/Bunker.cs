using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : Building
{

	public Enemy targetEnemy = null;

	public float dist;
	public Vector3 dir;
	public float curTime = 0f;


	public void Fire()
	{
		if (targetEnemy != null && dist <= state.offenseRange)
		{
			curTime += Time.deltaTime;

			if (curTime >= state.spawnTime)
			{
				for (int i = 0; i < 3; ++i)
				{
					GameObject bulletObj = Instantiate(ObjectManager.Instance.bulletPrefab);


					Vector3 tempPos = transform.position;
					tempPos.y = 1.5f;
					bulletObj.transform.position = tempPos;

					bulletObj.transform.forward = dir;
					bulletObj.GetComponent<Bullet>().dmg = state.dmg;
				}
				curTime = 0f;
			}
		}
	}

	public override void Hit()
	{
	}


	protected override void Destoryed()
	{
	}

	protected override void Awake()
	{
		base.Awake();
	}
	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		if (targetEnemy == null)
		{
			targetEnemy = ObjectManager.Instance.SearchCloseEnemy(gameObject, state.offenseRange);
		}
		
		if(targetEnemy != null)
		{//한번더 검사하게 else 안씀.

			dist = Vector3.Distance(transform.position, targetEnemy.transform.position);
			dir = (targetEnemy.transform.position - transform.position).normalized;

			if (dist > state.offenseRange | targetEnemy.isDead)
			{
				targetEnemy = null;
			}
			else
			{
				
			}
		}

		Fire();
	}

	private void OnDrawGizmosSelected()
	{
		
	}

}
