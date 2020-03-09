using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    // Start is called before the first frame update
	private List<Crew> crewList;
	public float SeparationRadius = 0.3f;
	public List<Crew> realCrew;
	private LandMovementHandler Captain;
	private Settings settings;
    void Start()
    {
		Captain = GameObject.FindObjectsOfType<LandMovementHandler>()[0];
		//var captain = GetComponent<LandMovementHandler>();
		settings = GameObject.FindObjectsOfType<Settings>()[0];
        crewList = new List<Crew>();
        var crews = GameObject.FindObjectsOfType<Crew>();
		realCrew = new List<Crew>();
		
		//Debug.Log(crewList);
        foreach (var crew in crews)
        {
			
			crewList.Add(crew);
			//Vector2 randomVector = new Vector2(UnityEngine.Random.Range(-0.5f, 1.0f), UnityEngine.Random.Range(-0.5f, 1.0f));
			realCrew.Add(Instantiate(crew, crew.transform.position, new Quaternion(0,0,0,0)));
            crew.transform.position = new Vector2(99999, 99999);
		}
		foreach(Crew crew in realCrew){
			
			crew.Captain = Captain;
			crew.Settings = settings;
            crew.CrewManager = this;
			
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		realCrew.RemoveAll(item => item == null);
        foreach (Crew crew in realCrew)
        {
            crew.UpdateCrew(Time.fixedDeltaTime);
        }
		if (Input.GetKeyDown(KeyCode.J)&& !realCrew[0].Equals(null)){
			Destroy(realCrew[0].gameObject);
			realCrew.RemoveAll(item => item == null);
		}
		if (Input.GetKeyDown(KeyCode.L) && realCrew.Count < 5){
			Vector2 randomVector = new Vector2(UnityEngine.Random.Range(-0.5f, 1.0f), UnityEngine.Random.Range(-0.5f, 1.0f));
			realCrew.Add(Instantiate(crewList[0], randomVector, new Quaternion(0,0,0,0)));
			foreach(Crew crew in realCrew){
			
			crew.Captain = Captain;
			crew.Settings = settings;
            crew.CrewManager = this;
			}
		}
            
    }
	public List<Crew> getCrew(){
		return realCrew;
	}

}
