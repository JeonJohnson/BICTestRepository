using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunMan : Unit
{
	public override void Heal()
	{
	}

	public override void Hit()
	{
	}


	protected override void Destoryed()
	{
	}

	protected override Bullet Fire()
	{
		Bullet bulletScript = base.Fire();

		if (bulletScript)
		{
			bulletScript.isPenetrate = false;
		}

		return bulletScript;
	}


	public override void Reload()
	{
		base.Reload();
		//curTime += Time.deltaTime;

		//if (curTime >= state.reloadTime)
		//{
		//	curRound = maxRound;
		//}
	}


	protected override void Move()
	{
		base.Move();
	}

	protected override void Search()
	{
		base.Search();
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


		target = ObjectManager.Instance.SearchCloseEnemy(gameObject);
		if (target == null)
		{
			navAgent.SetDestination(ObjectManager.Instance.enemySpawnPos.position);
			//target = ObjectManager.Instance.SearchCloseEnemy(gameObject);
		}

		if (target != null)
		{//한번더 검사하게 else 안씀.

			dist = Vector3.Distance(transform.position, target.transform.position);
			dir = (target.transform.position - transform.position).normalized;

			if (target.isDead)
			{
				target = null;
			}
			else
			{
				if (dist > state.range)
				{
					navAgent.isStopped = false;
					navAgent.SetDestination(target.transform.position);
				}
				else
				{
					navAgent.isStopped = true;

					if (curRound == 0)
					{
						Reload();
					}
					else
					{
						Fire();
					}
				}

			}
		}



	}
}
