using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleMan : Unit
{
	public Transform barrel;
	float dist;
	Vector3 dir;

	public int maxRound;
	public int curRound;
	public float curTime;
	public override void Heal()
	{

	}

	public override void Hit()
	{

	}


	protected override void Destoryed()
	{

	}

	protected override void Fire()
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
				bulletObj.GetComponent<Bullet>().spd = 1.2f;
				--curRound;
				curTime = 0f;
			}
		}
	}

	public void Reload()
	{
		curTime += Time.deltaTime;

		if (curTime >= state.reloadTime)
		{
			curRound = maxRound;
		}
	}

	protected override void Move()
	{
		base.Move();

	}
	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();

		curRound = maxRound;
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
