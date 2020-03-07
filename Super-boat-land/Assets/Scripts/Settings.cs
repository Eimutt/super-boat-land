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
	public Inventory Inventory { get; set;}
	//public EnemyManager EnemyManager { get; set; }
	public EnemyManager EnemyManager;
	public CrewManager CrewManager;
	//public static BoatController BoatController { get; set; }
	public static BoatController BoatController;
	
    // Start is called before the first frame update
    void Start()
    {
		//Inventory = GameObject.FindObjectsOfType<Inventory>()[0];
		BoatController = GameObject.FindObjectsOfType<BoatController>()[0];
		EnemyManager = GameObject.FindObjectsOfType<EnemyManager>()[0];
		//EnemyManager = GetComponent<EnemyManager>();
		//Debug.Log(BoatController);
		//Debug.Log(EnemyManager);
		CrewManager = GameObject.FindObjectsOfType<CrewManager>()[0];
		Inventory = GetComponent<Inventory>();
		
		
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(BoatController);
        //Debug.Log(EnemyManager);
    }
}
