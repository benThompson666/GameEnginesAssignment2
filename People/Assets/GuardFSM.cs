using UnityEngine;
using System.Collections;

public class GuardFSM : FSM
{

    bool dead = false;
   
    // Use this for initialization
    public GameObject leader;
    public float health = 0f;
    public float maxHealth = 100.0f;
    State state = null;
    public string enemytag = "army1";

    // Use this for initialization
    void Start()
    {
        SwitchState(new GaurdFollowState(this, leader));
        //StartCoroutine("ExitFormation");
    }

    IEnumerator ExitFormation()
    {
        yield return new WaitForSeconds(2.0f);
        //SwitchState(new GaurdFollowState(this,));
    }

   
    System.Collections.IEnumerator Consume()
    {
        while (health>0)
        {
            yield return new WaitForSeconds(1.0f);
        }
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).GetComponent<Renderer>().material.color = Color.black;
        }
        SwitchState(new DeadState(this));
    }


    // Update is called once per frame
    void Update()
    {
        if (state != null)
        {
            state.Update();
        }
 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemytag && !dead)
        {
            SwitchState(new AttackState(this, other.gameObject));
        }
        
    }
 
}

