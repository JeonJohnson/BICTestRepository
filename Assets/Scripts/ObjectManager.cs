using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;



public class ObjectManager : Manager<ObjectManager>
{
    //public List<GameObject> prefabs;

    //public Dictionary<string, GameObject>


    public GameObject bulletPrefab;

    public Transform enemySpawnPos;
    public GameObject enemyPrefab;
    public int spawnEnemyCount;
    public int leftEnemyCount;
    public float enemySpawnTime;
    public Text leftEnemyTxt;
    public List<Enemy> enemyList;  
                                        
   


    public List<GameObject> buildingPrefabs;
    public NavigationBaker navBaker;
    public Transform barricadeBox;

    List<Building> allyBuildings;
    List<Building> enemyBuildings;


    
    public Text resourceTxt;
    public float resource;

    public Text populationTxt;
    public int maxPop;
    [HideInInspector]
    public int curPop;

    public Transform unitSpawnPos;
    public List<GameObject> unitPrefabs;
    public List<Unit>[] units;
    public List<Unit> aliveUnits;

    public void SettingBuildings()
    { 
        string mapFileString = Funcs.ExcelFileReader("mapFile");

        //var col = new List<string>();

        var array = new char[60, 15];

		int index = 0;
		for (int i = 0; i < 60; ++i)
		{
			for (int k = 0; k < 15; ++k)
			{
                if (mapFileString[index] != '\n' && mapFileString[index] != ' ')
                {
                    int charToInt = mapFileString[index] - '0';

                    GameObject prefab = buildingPrefabs[charToInt];
                    Building script = prefab.GetComponent<Building>();
                    Vector2 size = script.state.size;

                    if (size.x == size.y)
                    {
                        if (size.x == 1)
                        { 
                            GameObject barricade = Instantiate(prefab,new Vector3(i,1f,k), Quaternion.identity);
                            barricade.transform.parent = barricadeBox;
                        }
                    }
                    else
                    { 
                    
                    }
                    

                }


                //array[i, k] = mapFileString[index];
				++index;
			}
		}
		//int x = 0;
		//int y = 0;
		//for (int i = 0; i < mapFileString.Length; ++i)
		//{
		//    array[x, y] = mapFileString[i];
		//}

		//Funcs.LineToList(mapFileString, ref col);

		//


	}

    //private void PoolingEnemy()
    //{
    //    for (int i = 0; i < spawnEnemyCount; ++i)
    //    {
    //        GameObject newEnemy = Instantiate(enemyPrefab);
    //        Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();
    //        newEnemy.SetActive(false);
    //        leftEnemyList.Add(newEnemyScript);
    //    }
    //}

    public IEnumerator EnemySpawnCoroutine()
    {
        int leftSpawnCount = spawnEnemyCount;

        while (leftSpawnCount > 0)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();
            enemyList.Add(newEnemyScript);
            --leftSpawnCount;
            Debug.Log(leftSpawnCount + "마리 남음");

            yield return new WaitForSeconds(enemySpawnTime);
        }

    }

    public Enemy SearchCloseEnemy(GameObject unit, float dist)
	{
        if (enemyList.Count == 0)
        {
            return null;
        }

       var TempList =  enemyList.OrderBy(x => Vector3.Distance(unit.transform.position, x.transform.position)).ToList();


        TempList.RemoveAll(delegate (Enemy enemy)
        {
            return Vector3.Distance(unit.transform.position, enemy.transform.position) > dist;
        });


        foreach (Enemy result in TempList)
        {
            if (!result.isDead)
            {
                return result;
            }
        }

        return null;
	}

    public Enemy SearchCloseEnemy(GameObject unit)
    {
        if (enemyList.Count == 0)
        {
            return null;
        }

        var TempList = enemyList.OrderBy(x => Vector3.Distance(unit.transform.position, x.transform.position)).ToList();

        foreach (Enemy result in TempList)
        {
            if (!result.isDead)
            {
                return result;
            }
        }

        return null;
    }




    public void DeathEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        --leftEnemyCount;
    }


    private void AddResource()
    {
        resource += Time.deltaTime;

        resourceTxt.text = "<size=50>Resource</size>\n" + $"<size=100>{((int)resource)}</size>";
    }

    private void UpdatePopulaitionUI()
    {
        populationTxt.text = "<size=40>Population</size>\n" + $"<size=85>{curPop}/{maxPop}</size>";
    }

    public void SpawnUnit(int index)
    {
        int count = 1;

		switch (index)
		{
            case 0:
                {
                    count = 4;
                }
                break;
			default:
				break;
		}

        for (int i = 0; i < count; ++i)
        {
            if (curPop >= maxPop)
            { return; }
        
            GameObject newUnit = Instantiate(unitPrefabs[index]);
            newUnit.GetComponent<Unit>().SetTransform(unitSpawnPos);

            ++curPop;
        }

        //UpdatePopulaitionUI();

    }



    public void SpawnUnit(int index, Vector3 pos)
    {
        int count = 1;

        switch (index)
        {
            case 0:
                {
                    count = 4;
                }
                break;
            default:
                break;
        }

        for (int i = 0; i < count; ++i)
        {
            if (curPop >= maxPop)
            { return; }

            GameObject newUnit = Instantiate(unitPrefabs[index]);
            newUnit.GetComponent<Unit>().SetPosition(pos);


            ++curPop;
        }

        //UpdatePopulaitionUI();

    }

    public void SpawnUnitHotKey()
    {
        //@1=> 49
        for (int i = 49; i < 54; ++i)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                SpawnUnit(i - 49);
            }
        }
    }

	private void Awake()
	{

        enemyList = new List<Enemy>();

        leftEnemyCount = spawnEnemyCount;

        SettingBuildings();
        navBaker.BakeNavMesh();
    }

	// Start is called before the first frame update
	void Start()
    {
        
        StartCoroutine(EnemySpawnCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        leftEnemyTxt.text = leftEnemyCount.ToString();
        AddResource();
        SpawnUnitHotKey();

        UpdatePopulaitionUI();
    }
}
