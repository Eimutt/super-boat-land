using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishController : MonoBehaviour
{
    public float escapeSpeed;


    private float alertRadius;
    private Transform playerPos;
    public Animator animator;
    // Start is called before the first frame update
    private Vector2 fleePosition;
    private float angle;
    private Vector2 moveDirection;
    private bool escape;

    //These variables are for the fish when it is hooked.
    //When resetDirection is true, the fish will generate a new angle that it will move towards.
    private bool resetDirection;
    private float resetTime;
    private bool catched;

    void Start()
    {
        alertRadius = 0.5f;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        escape = false;
        resetDirection = true;
        resetTime = -0.1f;
        catched = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = 0.0f;
        d = Vector3.Distance(transform.position, playerPos.transform.position);
        if ((d < alertRadius || escape) && !animator.GetBool("isHooked"))
        {
            //ENEMY WILL CHASE YOU.
            animator.SetBool("flee", true);
            if (!escape)
            {
                //fleePosition = -1 * playerPos.position * 1000;
                moveDirection = new Vector2(transform.position.x - playerPos.position.x, transform.position.y - playerPos.position.y);
                float sign = (playerPos.position.y > transform.position.y) ? -1.0f : 1.0f;
                angle = ((Vector2.Angle(Vector2.right, moveDirection) * sign) + 360 ) * Mathf.Deg2Rad;
                Debug.Log(angle);
            }
            Vector2 patrolTowardsPoint = (new Vector2(transform.position.x, transform.position.y) + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
            transform.position = Vector2.MoveTowards(transform.position, patrolTowardsPoint, 3 * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(angle*Mathf.Rad2Deg+180, Vector3.forward);
            escape = true;
        }

        /*
         *  Capture the fish while it will try to escape.
         */
        if(animator.GetBool("isHooked"))
        {
            //Set 1 time variables here.
            if(!catched)
            {
                playerPos.GetComponent<BoatController>().speed = escapeSpeed;
            }
            if(resetTime > 0.0f)
            {
                //Vector2 forward = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle))*-3;
                transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
                //Nåt fel med denna.......... Ser ut att bli fel då skeppet går från pos till neg vice versa.
                transform.position = Vector2.MoveTowards(transform.position, fleePosition, escapeSpeed * Time.deltaTime);

                //The player boat is also pushed a little bit.
                playerPos.position = Vector2.MoveTowards(playerPos.position, transform.position, escapeSpeed * 0.5f * Time.deltaTime);

                resetTime -= 0.05f;
            }
            else
            {
                //if(!catched)
                    fleePosition = -1 * playerPos.position*100;
                //else
                    fleePosition = -1 * new Vector2(Mathf.Abs(playerPos.position.x), Mathf.Abs(playerPos.position.y)) * 100;
                //float rand = Random.Range(-Mathf.PI/10, Mathf.PI/10);
                Vector2 tempMoveDir = new Vector2(fleePosition.x - transform.position.x, fleePosition.y - transform.position.y);
                resetTime = Random.Range(2.0f, 10.0f);
                //ERROR: Atan2 returnerar 2 värden så ibland fick vi en negativ angle så fisken vände sig direkt mot spelaren..
                angle = Mathf.Atan2(tempMoveDir.y, tempMoveDir.x) * Mathf.Rad2Deg + Random.Range(-45.0f, 45.0f);
            }
            catched = true;

            ////You can basically controll the fish at the same time as you can control your own character.
            Vector2 joyStickDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            /*
             *  Vi vill att den riktningen vi håller in, adderas till fiskens hastighet.
             *  Om du håller in i samma riktning som fisken så blir dess hastighet 2 ggr mer.
             *  Om du håller in i motsatt riktning så blir dess hastighet -speed och dras därmed mot dig.
             * 
             *  Du kan utföra dot product. Normalisera vektorerna och kör dot så får du cos0 mellan vektorerna (eftersom a dot b = cos0).
             *  Om cos0 = 0 så är de ortogonala. Om cos0 = 1 så pekar du i samma riktning som fisken och då ska fiskens hastighet dubbleras.
             *  Du vill ha cos0 = -1. för att lösa detta problemet kan du köra (a dot b) * 2.
             */
            //if(joyStickDirection != Vector2.zero)
                //transform.position = Vector2.MoveTowards(transform.position, joyStickDirection*100, (playerPos.GetComponent<BoatController>().speed-escapeSpeed) * Time.deltaTime);
        }
    }
}
