using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IdleState:State
{
    public IdleState(FSM owner):base(owner)
    {
        
    }

    public IdleState(EnemyFighterFSM owner):base(owner)
    {

    }

    public override string Description()
    {
        return "Idle State";
    }

    public override void Enter()
    {
        Boid boid = owner.GetComponent<Boid>();
        boid.forceMultiplier = 200;
        boid.sceneAvoidanceWeight = 200;
        boid.sceneAvoidanceForwardFeelerDepth = 500f;
        boid.sceneAvoidanceSideFeelerDepth = 200f;
        
        boid.sceneAvoidanceEnabled = false;
        // boid.seekEnabled = true;
        boid.arriveEnabled = true;
        float range = 50.0f;
        boid.maxForce = 30f;
        boid.maxSpeed = 40f;

        boid.path.waypoints.Clear();
        Vector3 min = new Vector3(Random.Range(-range, range), Random.Range(-range, range), -range);

        boid.path.waypoints.Add(min);

        Vector3 max = new Vector3(Random.Range(-range, range), Random.Range(-range, range), range);
        Vector3 m2 = new Vector3(Random.Range(-range, range), Random.Range(-range, range), range);
        Vector3 m3 = new Vector3(Random.Range(-range, range), Random.Range(-range, range), range);

        boid.path.waypoints.Add(max);
        boid.path.waypoints.Add(m2);
        boid.path.waypoints.Add(m3);

        boid.TurnOffAll();
        boid.pathFollowEnabled = true;
        boid.path.Looped = true;

        boid.path.next = (int)Random.Range(0, 2);
    }

    public override void Exit()
    {
        Boid boid = owner.GetComponent<Boid>();
        boid.pathFollowEnabled = false;
    }

    public override void Update()
    {
        // Nothing for now
    }
}