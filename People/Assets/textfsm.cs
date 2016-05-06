using UnityEngine;
using System.Collections;

public class textfsm : FSM {

    // Use this for initialization
    public float health = 0f;
    public float maxHealth = 100.0f;

    State state = null;

    // Use this for initialization
    void Start()
    {
        SwitchState(new IdleState(this));
    }



  
}
