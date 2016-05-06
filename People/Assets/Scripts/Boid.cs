using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour {
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float mass = 1.0f;

    public float forceMultiplier;
    public float maxSpeed = 5.0f;
    public float maxForce = 5.0f;

    public bool seekEnabled;
    public Vector3 seekTargetPosition;

    public bool arriveEnabled;
    public Vector3 arriveTargetPosition;
    public float slowingDistance = 15;

    public bool fleeEnabled;
    public float fleeRange = 15.0f;
    public Vector3 fleeTargetPosition;

    public bool pursueEnabled;
    public GameObject pursueTarget;
    Vector3 pursueTargetPos;

    public bool offsetPursueEnabled = false;
    public bool offsetPursueGaurd = false;
    public GameObject offsetPursueTarget = null;
    public Vector3 offset;
    public Vector3 offsetPursueTargetPos;

    [HideInInspector]
    public int current = 0;

    public bool pathFollowEnabled = false;
    public Path path = new Path();

    [Header("Scene Avoidance")]
    public bool sceneAvoidanceEnabled;
    public float sceneAvoidanceWeight;
    public float sceneAvoidanceForwardFeelerDepth = 30;
    public float sceneAvoidanceSideFeelerDepth = 15;

    Collider myCollider;

    public void TurnOffAll()
    {
        seekEnabled = arriveEnabled = fleeEnabled = pursueEnabled = offsetPursueEnabled = pathFollowEnabled = false;
    }
    
    public Vector3 FollowPath()
    {
        float epsilon = 5.0f;
        float dist = (transform.position - path.NextWaypoint()).magnitude;
        if (dist < epsilon)
        {
            path.AdvanceToNext();
        }
        if ((!path.Looped) && path.IsLast)
        {
            return Arrive(path.NextWaypoint());
        }
        else
        {
            return Seek(path.NextWaypoint());
        }
    }
    Vector3 SceneAvoidance()
    {
        Vector3 force = Vector3.zero;
        RaycastHit info;
        Vector3 feelerDirection;
        bool collided = false;
        List<FeelerInfo> feelers = new List<FeelerInfo>();

        float forwardFeelerDepth = sceneAvoidanceForwardFeelerDepth + ((velocity.magnitude / maxSpeed) * sceneAvoidanceForwardFeelerDepth);
        float sideFeelerDepth = sceneAvoidanceSideFeelerDepth + ((velocity.magnitude / maxSpeed) * sceneAvoidanceSideFeelerDepth);

        feelers.Add(new FeelerInfo(Vector3.forward, forwardFeelerDepth));

        feelerDirection = Vector3.forward;
        feelerDirection = Quaternion.AngleAxis(-45, Vector3.up) * feelerDirection; // Left feeler
        feelers.Add(new FeelerInfo(feelerDirection, sideFeelerDepth));

        feelerDirection = Vector3.forward;
        feelerDirection = Quaternion.AngleAxis(45, Vector3.up) * feelerDirection; // Right feeler
        feelers.Add(new FeelerInfo(feelerDirection, sideFeelerDepth));

        feelerDirection = Vector3.forward;
        feelerDirection = Quaternion.AngleAxis(45, Vector3.right) * feelerDirection; // Up feeler
        feelers.Add(new FeelerInfo(feelerDirection, sideFeelerDepth));

        feelerDirection = Vector3.forward;
        feelerDirection = Quaternion.AngleAxis(-45, Vector3.right) * feelerDirection; // Down feeler
        feelers.Add(new FeelerInfo(feelerDirection, sideFeelerDepth));

        for (int i = 0; i < feelers.Count; i++)
        {
            Vector3 feelerDir = transform.TransformDirection(feelers[i].localDirection);
            float feelerDepth = feelers[i].depth;
            collided = Physics.Raycast(transform.position, feelerDir, out info, feelerDepth);
            /*if (drawGizmos)
            {
                LineDrawer.DrawLine(transform.position, transform.position + feelerDir * feelerDepth, Color.cyan);
            }*/
            if (collided && info.collider != myCollider)
            {
                force += CalculateIncidentForce(info.point, info.normal);
              /*  if (drawGizmos)
                {
                    LineDrawer.DrawLine(transform.position, transform.position + feelerDir * feelerDepth, Color.red);
                    if (drawGizmos)
                    {
                        LineDrawer.DrawLine(info.point, info.point + force, Color.magenta);
                    }
                }*/
            }
        }



        return force;
    }
    struct FeelerInfo
    {
        public Vector3 localDirection;
        public float depth;
        public FeelerInfo(Vector3 localDirection, float depth)
        {
            this.localDirection = localDirection;
            this.depth = depth;
        }
    }
    Vector3 CalculateIncidentForce(Vector3 point, Vector3 normal)
    {
        Vector3 desiredVelocity;
        desiredVelocity = point - transform.position;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;
        //Utilities.checkNaN(desiredVelocity);

        Vector3 force = Vector3.Reflect(desiredVelocity - velocity, -normal);
        return force;
    }


    public Vector3 Pursue(GameObject target)
    {
        Vector3 toTarget = target.transform.position - transform.position;
        float lookAhead = toTarget.magnitude  / maxSpeed;
        pursueTargetPos = target.transform.position
           + (target.GetComponent<Boid>().velocity * lookAhead);
        
        return Seek(pursueTargetPos);
    }

    // Use this for initialization
    void Start ()
    {
        myCollider = GetComponentInChildren<Collider>();

        if (offsetPursueEnabled)
        {
            offset = transform.position - offsetPursueTarget.transform.position;
            offset = Quaternion.Inverse(
                   offsetPursueTarget.transform.rotation) * offset;
        }
    }

    public Vector3 OffsetPursue(GameObject leader, Vector3 offset)
    {
        Vector3 target = leader.transform.TransformPoint(offset);
        Vector3 toTarget = transform.position - target;
        float dist = toTarget.magnitude;
        float lookAhead = dist / maxSpeed;

        offsetPursueTargetPos = target + (lookAhead * leader.GetComponent<Boid>().velocity);
        return Arrive(offsetPursueTargetPos);
    }

    public Vector3 OffsetPursueGaurd(GameObject leader, Vector3 offset)
    {
        Vector3 target = leader.transform.TransformPoint(offset);
        Vector3 toTarget = transform.position - target;
        float dist = toTarget.magnitude;
        float lookAhead = dist / maxSpeed;

        offsetPursueTargetPos = target + (lookAhead * leader.GetComponent<Boid>().velocity);
        offsetPursueTargetPos.y = transform.position.y;
        return Arrive(offsetPursueTargetPos);
    }


    public Vector3 Flee(Vector3 targetPos, float range)
    {
        Vector3 desiredVelocity;
        desiredVelocity = transform.position - targetPos;
        if (desiredVelocity.magnitude > range)
        {
            return Vector3.zero;
        }
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;
        Debug.Log("Flee");
        return desiredVelocity - velocity;
    }

    public Vector3 Arrive(Vector3 targetPos)
    {
        Vector3 toTarget = targetPos - transform.position;

        float slowingDistance = 15.0f;
        float distance = toTarget.magnitude;
        if (distance < 0.5f)
        {
            velocity = Vector3.zero;
            return Vector3.zero;
        } 
        float ramped = maxSpeed * (distance / slowingDistance);

        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);

        return desired - velocity;
    }

    void OnDrawGizmos()
    {
        if (seekEnabled)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, seekTargetPosition);
        }
        if (arriveEnabled)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, arriveTargetPosition);
        }
        if (pursueEnabled)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, pursueTargetPos);
        }
        if (offsetPursueEnabled)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, offsetPursueTargetPos);
        }

        if (pathFollowEnabled)
        {
            path.DrawGizmos();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + force);
    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        toTarget.Normalize();
        Vector3 desired = toTarget * maxSpeed;
        return desired - velocity;
    }
    private bool accumulateForce(ref Vector3 runningTotal, Vector3 force)
    {
        float soFar = runningTotal.magnitude;

        float remaining = maxForce - soFar;
        if (remaining <= 0)
        {
            return false;
        }

        float toAdd = force.magnitude;


        if (toAdd < remaining)
        {
            runningTotal += force;
        }
        else
        {
            runningTotal += Vector3.Normalize(force) * remaining;
        }
        return true;
    }


    // Update is called once per frame
    void Update()
    {
        force = Vector3.zero;
        Vector3 steeringForce = Vector3.zero;

        if (seekEnabled)
        {
            force += Seek(seekTargetPosition);
           
        }
        if (arriveEnabled)
        {
            force += Arrive(arriveTargetPosition);
        }
        if (fleeEnabled)
        {
            force += Flee(fleeTargetPosition, fleeRange);
        }
        if (pursueEnabled)
        {
            force += Pursue(pursueTarget);
        }

        if (offsetPursueEnabled)
        {
            force += OffsetPursue(offsetPursueTarget, offset);
        }
        if (offsetPursueGaurd)
        {
            force += OffsetPursueGaurd(offsetPursueTarget, offset);
        }

        if (pathFollowEnabled)
        {
            force += FollowPath();
        }
        if (sceneAvoidanceEnabled)
        {
            force += SceneAvoidance() * sceneAvoidanceWeight;
            force *= forceMultiplier;
        }

        force = Vector3.ClampMagnitude(force, maxForce);
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity;
        }        
    }
}
