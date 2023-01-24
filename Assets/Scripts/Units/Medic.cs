using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class Medic : Unit
{
	public float healRange;
	public float curHealTime;
	
	
	public override void Heal()
	{
		curHealTime += Time.deltaTime;

		if (curHealTime >= state.healTime)
		{
			var cols = Physics.OverlapSphere(transform.position, healRange, LayerMask.GetMask("Unit"));


			if (cols.Length == 0)
			{
				return;
			}
			else
			{
				var list = cols.ToList();
				list.RemoveAll (x => x.transform.root.gameObject == this.gameObject);

				foreach (Collider col in list)
				{
					GameObject unitObj = col.transform.root.gameObject;

					Unit unit = unitObj.GetComponent<Unit>();

					//unit.state.curHp += unit.state.maxHp * 0.1f;

					unit.state.curHp = Mathf.Clamp(unit.state.curHp + unit.state.maxHp * 0.1f, 0, unit.state.maxHp);


					curHealTime = 0f;
				}
			}
		}
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
				bulletObj.GetComponent<Bullet>().spd = 3.3f;
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


		Heal();

	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawWireSphere(transform.position, healRange);

	}

}

