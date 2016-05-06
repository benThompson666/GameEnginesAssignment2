using UnityEngine;
using System.Collections;
using System;

public class CircleLeaderState : State {

    GameObject leader;

    public CircleLeaderState(FSM owner):base(owner)
    {

    }

    public CircleLeaderState(FSM owner,GameObject leader) : base(owner)
    {
        this.leader = leader;
    }

    public override string Description()
    {
        throw new NotImplementedException();
    }

    public override void Enter()
    {
        Boid boid = owner.GetComponent<Boid>();
        float range = 10;
/*
        Vector3 max = new Vector3(leader.transform.position-boid.transform.position, Random.Range(-range, range), range);
        Vector3 min = new Vector3(Random.Range(-range, range), Random.Range(-range, range), range);
        Vector3 m3 = new Vector3(Random.Range(-range, range), Random.Range(-range, range), range);
        */
        //boid.seekEnabled = true;
        //boid.arriveEnabled = true;
        boid.maxForce = 30f;
        boid.maxSpeed = 40f;

    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}
