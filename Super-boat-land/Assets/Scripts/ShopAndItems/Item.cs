using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public int cost;
	public string itemName;
	public string description;
	public bool isOwned = false;
	public bool isActive = false;
	
	public int getCost(){
		return cost;
	}
	
	public string getName(){
		return name;
	}
	
	public string getDescription(){
		return description;
	}
	
	public bool getIsOwned(){
		return isOwned;
	}
	
	public bool getIsActive(){
		return isActive;
	}
	public void buy(){
		Inventory.Ducats -= cost;
		isOwned = true;
	}
	
	public void activate(){
		isActive = !isActive;
	}
  

}
