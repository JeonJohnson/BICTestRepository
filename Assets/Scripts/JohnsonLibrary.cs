//using System.Collections;
//using System.Collections.Generic;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Funcs
{
	public static string GetEnumName<T>(int index) where T : struct, IConvertible
	{//where 조건 struct, IConvertible => Enum으로 제한
		return Enum.GetName(typeof(T), index);
	}


	public static string TrimUnderBar(string str)
	{
		return str.Replace('_', ' ');
	}

	public static int B2I(bool boolean)
	{
		//false => 값 무 (0)
		//true => 값 유 
		return Convert.ToInt32(boolean);
	}

	public static bool I2B(int integer)
	{
		return Convert.ToBoolean(integer);
	}

	public static List<T> ListShuffle<T>(ref List<T> list)
	{
		System.Random rnd = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rnd.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}

		return list;
	}

	public static bool IntegerRandomCheck(int percent)
	{
		int rand = UnityEngine.Random.Range(1, 101);

		if (rand > percent)
		{
			return false;
		}

		return true;
	}


	public static int DontOverlapRand(int curNum, int min, int ExclusiveMax)
	{
		int iRand;

		while (true)
		{
			iRand = UnityEngine.Random.Range(min, ExclusiveMax);

			if (iRand != curNum)
			{
				break;
			}
		}

		return iRand;
	}

	public static Vector3 DegreeAngle2Dir(float degreeAngle)
	{
		//각도를 벡터로 바꿔주는 거
		
		//ex)회전되지 않은 오브젝트인 경우
			//rotation의 y값 euler값 넣으면 forward Dir나옴.

		//조금 더 자세한 내용은 벡터 내적, 외적 봐보셈

		float radAngle = degreeAngle * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(radAngle), 0f, Mathf.Cos(radAngle));
	}

	//public bool CheckInFovByGameObject(GameObject target, GameObject me,Structs.FovStruct fovStruct, float range)
	//{
	//	Collider[] hitObjs = Physics.OverlapSphere(me.transform.position, range);

	//	if (hitObjs.Length == 0)
	//	{
	//		return false;
	//	}

	//	foreach (Collider col in hitObjs)
	//	{
	//		if (col.gameObject != target)
	//		{
	//			continue;
	//		}

	//		Vector3 dir = (target.transform.position - me.transform.position).normalized;

	//		float angleToTarget = Mathf.Acos(Vector3.Dot(me.transform.forward, dir)) * Mathf.Rad2Deg;
	//		//내적해주고 나온 라디안 각도를 역코사인걸어주고 오일러각도로 변환.
	//		if (angleToTarget <= (fovStruct.fovAngle * 0.5f)
	//			&& !Physics.Raycast(me.transform.position, dir, range/*, 여기에 인바이로먼트 레이어*/))
	//		{
	//			return true;
	//		}
	//	}

	//	return false;
	//}


	public static Vector3 Random(Vector3 min, Vector3 max)
	{
		float x = UnityEngine.Random.Range(min.x, max.x);
		float y = UnityEngine.Random.Range(min.y, max.y);
		float z = UnityEngine.Random.Range(min.z, max.z);

		return new Vector3(x, y, z);
	}

	public static Vector3 Vec3_Random(float min, float max)
	{
		float x = UnityEngine.Random.Range(min, max);
		float y = UnityEngine.Random.Range(min, max);
		float z = UnityEngine.Random.Range(min, max);

		return new Vector3(x, y, z);
	}

	public static Structs.RayResult RayToWorld(Vector2 screenPos)
	{
		//이걸 그냥 충돌한 놈이 그라운드 일때만 리턴하게?
		//아니면 소환하는 곳에서 충돌된 놈이 그라운드가 아니면 그 새기 크기 판단해서 옆에 생성되게?

		Structs.RayResult rayResult = new Structs.RayResult();

		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit castHit;

		if (Physics.Raycast(ray, out castHit))
		{
			rayResult.hitPosition = castHit.point;
			rayResult.hitPosition.y = 0f;
			rayResult.hitObj = castHit.transform.gameObject;
			rayResult.isHit = true;
			rayResult.ray = ray;
			rayResult.rayHit = castHit;
		}
		else
		{
			rayResult.isHit = false;
		}

		return rayResult;
	}


	public static bool RayToWorld(ref Structs.RayResult result, Vector2 screenPos)
	{
		//이걸 그냥 충돌한 놈이 그라운드 일때만 리턴하게?
		//아니면 소환하는 곳에서 충돌된 놈이 그라운드가 아니면 그 새기 크기 판단해서 옆에 생성되게?

		//Structs.RayResult rayResult = new Structs.RayResult();

		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit castHit;

		if (Physics.Raycast(ray, out castHit))
		{
			result.hitPosition = castHit.point;
			result.hitPosition.y = 0f;
			result.hitObj = castHit.transform.gameObject;
			result.isHit = true;
			result.ray = ray;
			result.rayHit = castHit;

			return true;
		}
		else
		{
			return false;
		}

	}

	public static bool RayToWorld(ref Structs.RayResult result, Vector3 dir, Vector3 origin)
	{
		//Ray ray = Camera.main.ScreenPointToRay(screenPos);
		//Vector3 origin = Camera.main.ScreenToWorldPoint(screenPos);
		//Ray ray = new Ray(origin, dir);
		RaycastHit castHit;

		if (Physics.Raycast(origin, dir, out castHit))
		{
			result.hitPosition = castHit.point;
			result.hitPosition.y = 0f;
			result.hitObj = castHit.transform.gameObject;
			result.isHit = true;
			//result.ray = ray;
			result.rayHit = castHit;

			return true;
		}
		else
		{
			return false;
		}

	}

	public static void ChangeMesh(GameObject origin, Mesh mesh)
	{
		MeshFilter tempFilter = origin.GetComponent<MeshFilter>();

		if (tempFilter != null)
		{
			tempFilter.mesh = mesh;
		}
	}



	public static GameObject FindGameObjectInChildrenByName(GameObject Parent, string ObjName)
	{
		if (Parent == null)
		{
			return null;
		}

		//그냥 transform.Find 로 찾으면 한 단계 아래 자식들만 확인함.
		int childrenCount = Parent.transform.childCount;

		GameObject[] findObjs = new GameObject[childrenCount];

		if (Parent.name == ObjName)
		{
			return Parent;
		}

		if (childrenCount == 0)
		{
			return null;
		}
		else
		{
			for (int i = 0; i < childrenCount; ++i)
			{
				findObjs[i] = FindGameObjectInChildrenByName(Parent.transform.GetChild(i).gameObject, ObjName);

				if (findObjs[i] != null && findObjs[i].name == ObjName)
				{
					return findObjs[i];
				}
			}

			return null;
		}
	}

	public static GameObject FindGameObjectInChildrenByTag(GameObject Parent, string ObjTag)
	{
		if (Parent == null)
		{
			return null;
		}

		int childrenCount = Parent.transform.childCount;

		GameObject[] findObjs = new GameObject[childrenCount];

		if (Parent.CompareTag(ObjTag))
		{
			return Parent;
		}

		if (childrenCount == 0)
		{
			return null;
		}
		else
		{
			for (int i = 0; i < childrenCount; ++i)
			{
				findObjs[i] = FindGameObjectInChildrenByTag(Parent.transform.GetChild(i).gameObject, ObjTag);

				if (findObjs[i] != null && findObjs[i].CompareTag(ObjTag))
				{
					return findObjs[i];
				}
			}
			return null;
		}
	}
	public static T FindComponentInNearestParent<T>(Transform curTransform) where T : Component
	{
		if (curTransform == null)
		{
			return null;
		}

		T tempComponent = curTransform.GetComponent<T>();

		if (tempComponent == null)
		{
			if (curTransform.parent != null)
			{
				tempComponent = FindComponentInNearestParent<T>(curTransform.parent);
			}
			else
			{
				return null;
			}
		}

		return tempComponent;
	}


	public static void RagdollObjTransformSetting(Transform originObj, Transform ragdollObj)
	{
		for (int i = 0; i < originObj.childCount; ++i)
		{
			if (originObj.childCount != 0)
			{
				RagdollObjTransformSetting(originObj.GetChild(i), ragdollObj.GetChild(i));
			}

			ragdollObj.GetChild(i).localPosition = originObj.GetChild(i).localPosition;
			ragdollObj.GetChild(i).localRotation = originObj.GetChild(i).localRotation;

		}
	}



	public static bool IsAnimationAlmostFinish(Animator animCtrl, string animationName, float ratio = 0.95f)
	{
		if (animCtrl.GetCurrentAnimatorStateInfo(0).IsName(animationName))
		{//여기서 IsName은 애니메이션클립 이름이 아니라 애니메이터 안에 있는 노드이름임
			if (animCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime >= ratio)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsAnimationCompletelyFinish(Animator animCtrl, string animationName, float ratio = 1.0f)
	{
		if (animCtrl.GetCurrentAnimatorStateInfo(0).IsName(animationName))
		{//여기서 IsName은 애니메이션클립 이름이 아니라 애니메이터 안에 있는 노드이름임
			if (animCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime >= ratio)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsAnimationPlay(Animator animCtrl, string animationName, int animationLayer)
	{
		if (animCtrl.GetCurrentAnimatorStateInfo(animationLayer).IsName(animationName))
		{//여기서 IsName은 애니메이션클립 이름이 아니라 애니메이터 안에 있는 노드이름임
			return true;
		}
		return false;
	}

	public static T FindResourceFile<T>(string path) where T : UnityEngine.Object
	{
		T temp = Resources.Load<T>(path);

		if (temp == null)
		{
			Debug.Log(path + "\nhas not exist!");
		}

		return temp;
	}

	public static GameObject CheckGameObjectExist(string name)
	{
		GameObject temp = GameObject.Find(name);

		if (temp == null)
		{
			temp = new GameObject();
			temp.name = name;
		}

		return temp;
	}

	public static GameObject CheckGameObjectExist<T>(string name) where T : Component
	{
		GameObject temp = GameObject.Find(name);

		if (temp == null)
		{
			temp = new GameObject();
			temp.name = name;
		}

		T tempComponent = temp.GetComponent<T>();

		if (tempComponent == null)
		{
			temp.AddComponent<T>();
		}

		return temp;
	}

	public static T CheckComponentExist<T>(string gameObjectName) where T : Component
	{
		GameObject temp = GameObject.Find(gameObjectName);

		if (temp == null)
		{
			temp = new GameObject();
			temp.name = gameObjectName;
		}

		T tempComponent = temp.GetComponent<T>();

		if (tempComponent == null)
		{
			tempComponent = temp.AddComponent<T>();
		}

		return tempComponent;
	}


	public static string ReadTextFile(string filePath)
	{ //StreamingAssets/리소스내의 폴더 위치 적어주면 댐.
	  //유니티가 최종 실행파일을 빌드할때 Resources 내의 모든 파일들을 바이너리화 해서 뽑지 않음.
	  //사용되는것들(ex. 프리팹,모델,스크립트,Resources.load등으로 불러오는것만)만 뽑음
	  //StreamingAssets폴더내의 있는 파일들은 다 뽑아내기때문에
	  //유니티에서 지원하는?? 파일형식 제외하고는 저기 넣어야 안정적이겠즤

		string path = Application.streamingAssetsPath + "/";
		path += filePath + ".txt";


		FileInfo fileInfo = new FileInfo(path);
		string text;
		if (fileInfo.Exists)
		{
			StreamReader reader = new StreamReader(path);
			text = reader.ReadToEnd();
			reader.Close();
		}
		else
		{
			Debug.Log(filePath + "에는 파일이 없읍니다. 확인요망");
			text = "TextFile has no exist";
		}

		return text;
	}


	public static GameObject FindManagerBoxes(bool isDontDestroy)
	{
		GameObject boxObj = null;

		if (isDontDestroy)
		{
			boxObj = CheckGameObjectExist("ManagerBox");
			GameObject.DontDestroyOnLoad(boxObj);
		}
		else
		{
			boxObj = CheckGameObjectExist("ManagerBox_Destory");
		}

		return boxObj;
	}

}



public static class Defines
{
	public const int right = 1;
	public const int left = 0;

	public const int ally = 0;
	public const int enemy = 1;

	public const float winCX = 1600f;
	public const float winCY = 900f;

	public const int tileX = 64;
	public const int tileY = 64;

	public const float gravity = -9.8f;

	public const float PI = 3.14159265f;

	public static Vector3 TrashVector3 = new Vector3(float.MinValue, float.MinValue, float.MinValue);


	public static string managerPrfabFolderPath = "ManagerPrefabs/";


	public static Vector3[] test =
	{
		new Vector3(0f,0f,0f)
	};

	public static string[] enemyNameStr =
	{ 
		"Spirit",
		"Archer"
	};

	public static string[] ArcherAnimTriggerStr =
	{
		"tIdle",
		"tEquip",
		"tWalk",
		"tAttack",
		"tHit",
		"tLookAround",
		"tDeath"
	};

}

namespace Enums
{
	public enum eScenes
	{
		Intro,
		Title,
		Lobby,
		InGame,
		End
	}


}

namespace Structs
{
	[System.Serializable]
	public struct RayResult
	{
		public bool isHit;
		public Vector3 hitPosition;
		public GameObject hitObj;
		public Ray ray;
		public RaycastHit rayHit;
	}


	[System.Serializable]
	public struct EnemyStatus
	{

    }

	[System.Serializable]
	public struct PlayerStatus
	{

	}

	[System.Serializable]
	public struct WeaponStatus
	{

	}


	[System.Serializable]
	public struct DamagedStruct
	{

	}


    
}

