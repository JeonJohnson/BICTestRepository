using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class ObjectManager : Manager<ObjectManager>
{
    //public List<GameObject> prefabs;

    //public Dictionary<string, GameObject>

    public GameObject enemyPrefab;
    public int spawnEnemyCount;
    public float enemySpawnTime;
    [HideInInspector]
    public int curEnemyCount;
    public List<Enemy> enemyList;


    public List<GameObject> buildingPrefabs;
    List<Building> allyBuildings;
    List<Building> enemyBuildings;

    //public Enemy SearchCloseEnemy(Unit unit)
    //{
    //    //foreach (Enemy enemy in enemyList)
    //    //{ 
    //    //    enemy

    //    //}

    //    enemyList.Find(x => )
    //}



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
                            Instantiate(prefab,new Vector3(i,0.5f,k), Quaternion.identity);
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



    public IEnumerator EnemySpawnCoroutine()
    {
        int leftSpawnCount = spawnEnemyCount;

        while (leftSpawnCount > 0)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();



            yield return new WaitForSeconds(enemySpawnTime);
        }




            
    
    }


	private void Awake()
	{

        SettingBuildings();

    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
