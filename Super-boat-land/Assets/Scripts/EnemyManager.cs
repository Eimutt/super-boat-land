using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
	
	private List<Enemy> enemyList;
    void Start()
    {
        enemyList = new List<Enemy>();
        var enemies = GameObject.FindObjectsOfType<Enemy>();
		var settings = GameObject.FindObjectsOfType<Settings>();
		
		Debug.Log(enemies);
        foreach (var enemy in enemies)
        {
			Debug.Log(enemy);
			enemy.Settings = settings[0];
            enemy.EnemyManager = this;
			enemyList.Add(enemy);
		}
		Debug.Log(enemyList);
		Debug.Log(GetEnemies());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.UpdateEnemy(Time.fixedDeltaTime);
        }
    }

    public IEnumerable<Enemy> GetEnemies()
    {
		return enemyList;

    }
	
}
