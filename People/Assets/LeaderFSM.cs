using UnityEngine;
using System.Collections;

public class LeaderFSM : FSM {

    public Camera sideView1Camera;
    public Camera skyCamera;
    public Camera overview;
    public Camera circleLeaderCam;
    public Camera fpsCam;
    public GameObject enemyPrefab;

    public GameObject star;
    bool battleBegun=false;


    // Use this for initialization
    public float health = 0f;
    public float maxHealth = 100.0f;

    State state = null;

    // Use this for initialization
    void Start()
    {
        if (sideView1Camera != null)
        {
            overview.enabled = false;
            StartCoroutine("leaderMovement");
        }
    }

    IEnumerator leaderMovement()
    {
        yield return new WaitForSeconds(4.5f);
        //overview.enabled = true;
        skyCamera.enabled= sideView1Camera.enabled = false;
        skyCamera.enabled = true;
        SwitchState(new ApproachState(this, transform.position + (transform.forward * 20)));
        yield return new WaitForSeconds(4.5f);
        sideView1Camera.enabled = false;
        skyCamera.enabled = true;
        yield return new WaitForSeconds(6.5f);
        overview.enabled = true;
        skyCamera.enabled = sideView1Camera.enabled = false;
   

    }


    IEnumerator BeginBattle()
    {
        overview.enabled = false;
        sideView1Camera.enabled = true;
        yield return new WaitForSeconds(2.5f);
        skyCamera.enabled = sideView1Camera.enabled = false;
        overview.enabled = true;
        SwitchState(new ApproachState(this, transform.position + (transform.forward * 30)));
        yield return new WaitForSeconds(4.5f);
        overview.enabled = skyCamera.enabled = sideView1Camera.enabled = false;
        sideView1Camera.enabled = true;
        yield return new WaitForSeconds(4.5f);
        overview.enabled = skyCamera.enabled = sideView1Camera.enabled = false;
        circleLeaderCam.enabled = true;
        
        yield return new WaitForSeconds(3.0f);
        circleLeaderCam.transform.position = (transform.position -circleLeaderCam.transform.position).normalized * -2f+circleLeaderCam.transform.position;
        
        /*CircleSpawner cs=gameObject.AddComponent<CircleSpawner>();
        cs.prefab = enemyPrefab;
        cs.numberOfPoints = 8;
        cs.dist = 6;
        */

        yield return new WaitForSeconds(3.0f);
        circleLeaderCam.transform.position = circleLeaderCam.transform.position+Vector3.up*10f;
        circleLeaderCam.transform.LookAt(transform.position);

        yield return new WaitForSeconds(8.0f);
        overview.enabled=circleLeaderCam.enabled= skyCamera.enabled = sideView1Camera.enabled = false;
        fpsCam.enabled = true;

        yield return new WaitForSeconds(15.0f);
        overview.enabled = circleLeaderCam.enabled = skyCamera.enabled = sideView1Camera.enabled = false;
        fpsCam.enabled = false;


    }




    // Update is called once per frame
    void Update()
    {
        if (sideView1Camera != null)
        {
            if (skyCamera.enabled)
                skyCamera.transform.Rotate(Vector3.right, Time.deltaTime * 2.0f);
            if (sideView1Camera.enabled)
                sideView1Camera.transform.Rotate(Vector3.up, Time.deltaTime * 1.0f);
            if (overview.enabled && !battleBegun)
            {
                if (Vector3.Distance(transform.position, overview.transform.position) > 10f)
                {
                    overview.transform.Translate((transform.position - overview.transform.position).normalized * Time.deltaTime * 10.0f, Space.World);
                }
                else
                {
                    battleBegun = true;
                    StartCoroutine("BeginBattle");
                }
            }

            if (circleLeaderCam.enabled)
            {
                circleLeaderCam.transform.parent.transform.Rotate(Vector3.up, 20f * Time.deltaTime);
            }
        }

        if (state != null)
        {
            state.Update();
            Debug.Log(state.Description());
        }
    }
}
