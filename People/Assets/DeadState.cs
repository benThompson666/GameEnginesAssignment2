using UnityEngine;
using System.Collections;
using System;

public class DeadState : State {

    Vector3 force;

    public DeadState(FSM owner):base(owner)
    {
    }

    public DeadState(FSM owner,Vector3 force) : base(owner)
    {
        this.force = force;
    }

    public override string Description()
    {
        return "dead State";
    }

    public override void Enter()
    {
        Boid b = owner.GetComponent<Boid>();
        b.TurnOffAll();
        //Rigidbody rb=owner.gameObject.AddComponent<Rigidbody>();
        //rb.AddForce(force);
    }

    public override void Exit()
    {

    }

    public override void Update()
    {

        throw new NotImplementedException();
    }
}
