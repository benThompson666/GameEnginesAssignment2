using UnityEngine;
using System.Collections;

public class GaurdFollowState : State
{
    GameObject leader;
    public GaurdFollowState(FSM owner):base(owner)
    {

    }

    public GaurdFollowState(FSM owner,GameObject leader) : base(owner)
    {
        this.leader = leader;
    }

    public override string Description()
    {
        return "Follow Leader State";
    }

    public override void Enter()
    {
        
        Boid boid = owner.GetComponent<Boid>();
        boid.offsetPursueTarget = leader;
        boid.offset = boid.transform.position - leader.transform.position;
        boid.offset = Quaternion.Inverse(
               leader.transform.rotation) * boid.offset;
        boid.offsetPursueGaurd = true;
       
        /*
        boid.forceMultiplier = 200;
        boid.sceneAvoidanceWeight = 200;
        boid.sceneAvoidanceForwardFeelerDepth = 500f;
        boid.sceneAvoidanceSideFeelerDepth = 200f;

        boid.sceneAvoidanceEnabled = true;
        */
    }

    public override void Exit()
    {
        Boid boid = owner.GetComponent<Boid>();
        boid.offsetPursueGaurd=false;
    }


    public override void Update()
    {
        // Somebody elase eaten the food?
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