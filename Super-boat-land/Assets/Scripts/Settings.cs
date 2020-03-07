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

    public EnemyManager EnemyManager { get; set; }
    public CrewManager CrewManager { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager = GameObject.FindObjectsOfType<EnemyManager>()[0];
        //EnemyManager = GameObject.Find("EnemyManager");
        CrewManager = GameObject.FindObjectsOfType<CrewManager>()[0];
        Debug.Log(CrewManager);
        //CrewManager = GameObject.Find("CrewManager");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
