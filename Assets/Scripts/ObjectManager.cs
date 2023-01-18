using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class ObjectManager : Manager<ObjectManager>
{
    public List<GameObject> prefabs;

    //public Dictionary<string, GameObject>

    public GameObject enemyPrefab;
    public int spawnEnemyCount;
    public float enemySpawnTime;
    [HideInInspector]
    public int curEnemyCount;
    public List<Enemy> enemyList;

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
