using UnityEngine;
using System.Collections;
using System;

public class AttackState : State {

    GameObject target;
    public AttackState(FSM owner):base(owner)
    {
    }
    public AttackState(FSM owner,GameObject target) : base(owner)
    {
        this.target = target;
    }

    public override string Description()
    {
        throw new NotImplementedException();
    }

    public override void Enter()
    {
        Boid b=owner.GetComponent<Boid>();
        b.TurnOffAll();
        b.seekTargetPosition = target.transform.position;
        b.seekEnabled=true;

    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Vector3.Distance(owner.transform.position, target.transform.position) < 5f)
        {
            target.GetComponent<GuardFSM>().health-=50;
        }
        if(target.GetComponent<GuardFSM>().health<=0)
            owner.SwitchState(new IdleState(owner));
    }
}
