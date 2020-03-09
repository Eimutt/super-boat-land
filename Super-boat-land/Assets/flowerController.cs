using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerController : MonoBehaviour
{
    BoxCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Player")
        {
            SceneManager.LoadScene("GrassLandFinal");
        }
    }
}
