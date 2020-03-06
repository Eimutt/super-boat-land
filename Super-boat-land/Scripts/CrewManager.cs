using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    // Start is called before the first frame update
	private List<Crew> crewList;
    void Start()
    {
		var captain = GameObject.FindObjectsOfType<LandMovementHandler>();
		//var captain = GetComponent<LandMovementHandler>();
		var settings = GameObject.FindObjectsOfType<Settings>();
        crewList = new List<Crew>();
        var crews = GameObject.FindObjectsOfType<Crew>();
		Debug.Log(crewList);
        foreach (var crew in crews)
        {
			crew.Captain = captain[0];
			crew.Settings = settings[0];
            crew.CrewManager = this;
			crewList.Add(crew);
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Crew crew in crewList)
        {
            crew.UpdateCrew(Time.fixedDeltaTime);
        }
    }
	public List<Crew> getCrew(){
		return crewList;
	}

}
