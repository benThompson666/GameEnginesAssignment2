using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public float rot;
    public float rotSpeed=.1f;
	// Use this for initialization
	void FixedUpdate () {
     

            //RenderSettings.skybox.SetFloat("_Exposure", Mathf.Sin(Time.time * Mathf.Deg2Rad * 100) + 2);
            // RenderSettings.skybox.SetFloat("_Rotation", rot);
            // RenderSettings.skybox.SetFloat("_Rotation", Time.time * (.01f));
            //RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1f);
    }



}
