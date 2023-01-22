using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float curHp;
	public float maxHp;


	public Vector3 targetPos;

	public NavMeshAgent navAgent;

	public bool isDead;

	public  void Hit(float dmg)
	{
		curHp -= dmg;

		if (curHp <= 0f)
		{
			ObjectManager.Instance.DeathEnemy(this);
		}
	}

	public void Destoryed()
	{
	}

	protected  void Move()
	{


	}
	protected  void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
	}

	protected  void Start()
	{
		navAgent.SetDestination(targetPos);
	}

	protected  void Update()
	{
	}
}
