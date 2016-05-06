using UnityEngine;
using System.Collections;

public class GuardIdleState : State {


    public GuardIdleState(FSM owner):base(owner)
    {

    }

    public override string Description()
    {
        return "Gaurd Idle State";
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }


    public override void Update()
    {
       
        // if (food == null)
        /*{
            owner.SwitchState(new IdleState(owner));
        }
        else
        {
            Boid boid = owner.GetComponent<Boid>();
            boid.seekTargetPosition = food.transform.position;
            if (Vector3.Distance(owner.transform.position, food.transform.position) < 1.0f)
            {
                EatFood();
                owner.SwitchState(new IdleState(owner));
            }
        }*/
    }
}
