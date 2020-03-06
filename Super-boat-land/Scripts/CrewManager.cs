using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    // Start is called before the first frame update
	private List<Crew> crewList;
    void Start()
    {

		var settings = GameObject.FindObjectsOfType<Settings>();
        crewList = new List<Crew>();
        var crews = GameObject.FindObjectsOfType<Crew>();
        foreach (var crew in crews)
        {
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

}
