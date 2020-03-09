using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
	
	private List<Enemy> enemyList;
	private List<Enemy> realEnemies;
    void Start()
    {
        enemyList = new List<Enemy>();
		
        var enemies = GameObject.FindObjectsOfType<Enemy>();
		var settings = GameObject.FindObjectsOfType<Settings>();
		//Instantiate(enemies[1], Vector3.zero, new Quaternion(0,0,0,0));
		realEnemies = new List<Enemy>();
		
		//Debug.Log(enemies);
        foreach (var enemy in enemies)
        {
			//Debug.Log(enemy);
			//enemy.Settings = settings[0];
            //enemy.EnemyManager = this;
			enemyList.Add(enemy);
			//Debug.Log(enemy);
			
			Vector2 randomVector = new Vector2(UnityEngine.Random.Range(-0.5f, 1.0f), UnityEngine.Random.Range(-0.5f, 1.0f));
			realEnemies.Add(Instantiate(enemy, enemy.transform.position, new Quaternion(0,0,0,0))); //this.gameObject.GetComponent<Enemy>()
            enemy.transform.Translate(new Vector2(999999, 999999));
        }

        //Debug.Log(settings[0]);
        //Debug.Log(enemyList);
        //Debug.Log(GetEnemies());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		realEnemies.RemoveAll(item => item.Equals(null));
		realEnemies.RemoveAll(item => item == null);
        foreach (Enemy enemy in realEnemies)
        {
            enemy.UpdateEnemy(Time.fixedDeltaTime);
        }
    }

    public List<Enemy> GetEnemies()
    {
		return realEnemies;

    }
	
}
