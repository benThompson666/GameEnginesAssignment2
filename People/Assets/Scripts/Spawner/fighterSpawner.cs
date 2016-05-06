using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fighterSpawner : MonoBehaviour {

    List<GameObject> fighters;
    public GameObject fighterPrefab;
    public int x, y, z;
    public float gapX, gapY, gapZ;
    

	// Use this for initialization
	void Start () {
        fighters = new List<GameObject>();
        BoxFormation(x, gapX, y, gapY, z, gapZ);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BoxFormation(x, gapX, y, gapY, z, gapZ);
        }
	
	}

    void BoxFormation(int _x,float gapX,int _y,float gapY, int _z,float gapZ)
    {
        foreach(GameObject g in fighters)
        {
            Destroy(g);
        }
        fighters.Clear();

        for(int x=0;x< _x;x++)
        {
            for(int y=0;y< _y;y++)
            {
                for(int z=0;z< _z;z++)
                {
                    GameObject g = Instantiate(fighterPrefab, transform.position + new Vector3(x * gapX, y * gapY, z * gapZ),transform.rotation) as GameObject;
                    fighters.Add(g);
                    g.AddComponent<GuiHealth>();
                }
            }
        }
    }
}
