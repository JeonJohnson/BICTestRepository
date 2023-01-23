using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float curHp;
	public float maxHp;

	public float moveSpd;

	public Vector3 targetPos;

	public NavMeshAgent navAgent;

	public bool isDead = false;

	public  void Hit(float dmg)
	{
		curHp -= dmg;

		if (!isDead && curHp <=0f)
		{
			isDead = true;
			ObjectManager.Instance.DeathEnemy(this);
		}
	}

	public void Destoryed()
	{
	}

	protected  void Move()
	{


	}

	public void SetSpeed(float percent)
	{
		navAgent.speed = moveSpd * percent;
	}

	protected  void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.speed = moveSpd;
		curHp = maxHp;
	}

	protected  void Start()
	{
		navAgent.SetDestination(targetPos);
	}

	protected  void Update()
	{
	}



	//private void OnTriggerStay(Collider other)
	//{
	//	if (other.CompareTag("Trap"))
	//	{
	//		navAgent.speed = moveSpd * 0.5f;
	//		Debug.Log("Æ®·¦¹âÀ½");
	//	}
	//}

	//private void OnTriggerExit(Collider other)
	//{
	//	if (other.CompareTag("Trap"))
	//	{
	//		navAgent.speed = moveSpd;
	//		Debug.Log("Æ®·¦¿¡¼­ ³ª¿È");
	//	}

	//}
}
