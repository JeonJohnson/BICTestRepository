using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

	public Vector3 targetPos;

	public NavMeshAgent navAgent;


	public  void Hit()
	{
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
