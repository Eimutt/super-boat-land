using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	[SerializeField]
    private float m_AttackRange = 3;
	public float AttackRange
    {
        get { return m_AttackRange; }
        set { m_AttackRange = value; }
    }
	[SerializeField]
    private float m_HurtRange = 0.2f;
	public float HurtRange
    {
        get { return m_HurtRange; }
        set { m_HurtRange = value; }
    }
	public Inventory Inventory { get; set;}
	//public EnemyManager EnemyManager { get; set; }
	public EnemyManager EnemyManager;
	public CrewManager CrewManager;
	public LandMovementHandler Captain;
	//public static BoatController BoatController { get; set; }
	public static BoatController BoatController;
	public List<GameObject> allLandObjects;
	
	
    // Start is called before the first frame update
    void Start()
    {
		//Inventory = GameObject.FindObjectsOfType<Inventory>()[0];
		//BoatController = GameObject.FindObjectsOfType<BoatController>()[0];
		EnemyManager = GameObject.FindObjectsOfType<EnemyManager>()[0];
		//EnemyManager = GetComponent<EnemyManager>();
		//Debug.Log(BoatController);
		//Debug.Log(EnemyManager);
		CrewManager = GameObject.FindObjectsOfType<CrewManager>()[0];
		Inventory = GetComponent<Inventory>();
		
		//var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		//var crew = GameObject.FindGameObjectsWithTag("Crew");
		List<Crew> crew = CrewManager.getCrew();
		List<Enemy> enemies = EnemyManager.GetEnemies();
		allLandObjects.Add(Captain.gameObject);
		foreach(Enemy go in enemies){
			allLandObjects.Add(go.gameObject);
		}
		foreach(Crew go in crew){
			allLandObjects.Add(go.gameObject);
		}
		/*
		foreach( GameObject go in allLandObjects){
			Debug.Log(go);
		}
		*/
		
    }

    // Update is called once per frame
    void Update()
    {
		allLandObjects.RemoveAll(item => item == null);
		foreach(Enemy go in EnemyManager.GetEnemies()){
			if(!allLandObjects.Contains(go.gameObject))
				allLandObjects.Add(go.gameObject);
		}
		foreach(Crew go in CrewManager.getCrew()){
			if(!allLandObjects.Contains(go.gameObject))
				allLandObjects.Add(go.gameObject);
		}
		//Debug.Log(BoatController);
        //Debug.Log(EnemyManager);
    }
}
