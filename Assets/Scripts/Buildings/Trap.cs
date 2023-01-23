using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Building
{
	public override void Hit()
	{
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Destoryed()
	{
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().SetSpeed(0.5f);
			Debug.Log("Æ®·¦¹âÀ½");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().SetSpeed(1);
			Debug.Log("Æ®·¦¿¡¼­ ³ª¿È");
		}

	}
}
