using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PrimaryWeapon : MonoBehaviour {

    public string name;
    public PrimaryWeapon(string _name)
    {
        this.name = _name;
    }
    public virtual void Shoot(Vector3 spawnPos, Vector3 direction) {
    }

}

public class Laser : PrimaryWeapon
{

    public string name;

    public LineRenderer lineRenderer;

    public float laserLenght;
    public float laserWidth;
    public float laserPower;
    public float laserSpeed;
    public Color c1;
    public Color c2;

    public void CreateLaser(Vector3 dir)
    {
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(laserWidth, laserWidth);
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetPosition(0, Vector3.zero + Vector3.forward);
        lineRenderer.SetPosition(1, Vector3.zero + (Vector3.forward * laserLenght));
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }

         
    public Laser(string _name):base(_name)
    {
    }

    public override void Shoot(Vector3 spawnPos,Vector3 dir)
    {
        GameObject laser=Instantiate(new GameObject("Laser"), spawnPos,Quaternion.identity)as GameObject;
        laser.transform.forward = dir;

        lineRenderer=laser.AddComponent<LineRenderer>();
        CreateLaser(dir);
        MoveForward mf=laser.AddComponent<MoveForward>();
        mf.speed = laserSpeed;
    }
}

