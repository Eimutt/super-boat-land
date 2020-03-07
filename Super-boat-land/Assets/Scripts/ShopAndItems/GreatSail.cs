using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSail : Item
{
	
	
    // Start is called before the first frame update
    void Start()
    {
        description = "A great sail. Improves movement speed on water dramatically.";
		itemName = "Great Sail";
		cost = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)){
			activate();
		}
		if (isActive){
			Settings.BoatController.setSpeed(3.0f);
		} else {
			Settings.BoatController.setSpeed(2.0f);
		}
    }
}
