using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Put this on anything that can attack.
 */
public class Attack : MonoBehaviour
{
    public float AOEPower;
    public float attackPower; //For regular attacks.
    public GameObject ParticleAOE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void normalAttack()
    {

    }

    public void AOEAttack()
    {
        Instantiate(ParticleAOE, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
