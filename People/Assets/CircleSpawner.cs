using UnityEngine;
using System.Collections;

public class CircleSpawner : MonoBehaviour {

    public GameObject prefab;
    public Vector3 center;
    public float dist;
    public int numberOfPoints;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * 360 / numberOfPoints;
            Quaternion q = Quaternion.AngleAxis(angle, transform.up);
            GameObject g=Instantiate(prefab, transform.position, q)as GameObject;
            g.transform.Translate(Vector3.forward * dist);
            g.transform.Rotate(Vector3.up, 180f);

        }
    }
}
