using UnityEngine;
using System.Collections;
using System;

public class ApproachState : State {

    public Vector3 arrive;

    public ApproachState(FSM owner) : base(owner)
    {
        
    }
    public ApproachState(LeaderFSM owner,Vector3 arrive) : base(owner)
    {
        this.arrive = arrive;
    }

    public override string Description()
    {
        throw new NotImplementedException();
    }

    public override void Enter()
    {
        Boid boid = owner.GetComponent<Boid>();
        boid.arriveTargetPosition = arrive;
        boid.maxForce = 20f;
        boid.maxSpeed = 5f;

        boid.arriveEnabled=true;

    }

    public override void Exit()
    {
        Boid boid = owner.GetComponent<Boid>();
        boid.arriveEnabled = false;
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

}
