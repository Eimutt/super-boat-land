using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField]
	public static int m_money = 100;
	public static int Ducats
    {
        get { return m_money; }
        set { m_money = value; }
    }
	public List<Item> heldItems;
	public Settings Settings { get; set;}
	
    // Start is called before the first frame update
    void Start()
    {
        Settings = GameObject.FindObjectsOfType<Settings>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
