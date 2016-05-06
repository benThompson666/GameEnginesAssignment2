using UnityEngine;
using System.Collections;

public class PeopleRandomMovement : State
{

    public PeopleRandomMovement(FSM owner) : base(owner)
    {

    }

    public PeopleRandomMovement(EnemyFighterFSM owner) : base(owner)
    {

    }

    public override string Description()
    {
        return "Random Movement State";
    }

    public override void Enter()
    {
        Boid boid = owner.GetComponent<Boid>();
        boid.forceMultiplier = 200;
        boid.sceneAvoidanceWeight = 200;
        boid.sceneAvoidanceForwardFeelerDepth = 500f;
        boid.sceneAvoidanceSideFeelerDepth = 200f;

        //boid.sceneAvoidanceEnabled = true;
        // boid.seekEnabled = true;
        boid.arriveEnabled = true;
        float range = 10.0f;
        boid.maxForce = 5f;
        boid.maxSpeed = 1f;

        boid.path.waypoints.Clear();
        Vector3 min = new Vector3(Random.Range(-range, range),boid.transform.position.y, -range);

        boid.path.waypoints.Add(min);

        Vector3 max = new Vector3(Random.Range(-range, range), boid.transform.position.y, range);
        Vector3 m2 = new Vector3(Random.Range(-range, range), boid.transform.position.y, range);
        Vector3 m3 = new Vector3(Random.Range(-range, range), boid.transform.position.y, range);

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

