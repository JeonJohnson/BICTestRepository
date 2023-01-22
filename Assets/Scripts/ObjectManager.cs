using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class ObjectManager : Manager<ObjectManager>
{
    //public List<GameObject> prefabs;

    //public Dictionary<string, GameObject>


    public GameObject bulletPrefab;

    public Transform enemySpawnPos;
    public GameObject enemyPrefab;
    public int spawnEnemyCount;
    public float enemySpawnTime;
    [HideInInspector]
    public int curEnemyCount;
    public List<Enemy> enemyList;


    public List<GameObject> buildingPrefabs;
    public NavigationBaker navBaker;
    List<Building> allyBuildings;
    List<Building> enemyBuildings;

 


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
                            Instantiate(prefab,new Vector3(i,1f,k), Quaternion.identity);
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

    public Enemy SearchCloseEnemy(GameObject unit, float dist = 6)
	{
        //foreach (Enemy enemy in enemyList)
        //{ 
        //    enemy

        //}

        if (enemyList.Count == 0)
        {
            return null;
        }

       var TempList =  enemyList.OrderByDescending(x => Vector3.Distance(unit.transform.position, x.transform.position));

        foreach (Enemy result in TempList)
        {
            if (!result.isDead)
            {
                return result;
            }
        }

        return null;
       
	}



	public IEnumerator EnemySpawnCoroutine()
    {
        int leftSpawnCount = spawnEnemyCount;

        while (leftSpawnCount > 0)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();
            enemyList.Add(newEnemyScript);

            yield return new WaitForSeconds(enemySpawnTime);
        }
                    
    }

    public void DeathEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }


	private void Awake()
	{

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
        
    }
}
