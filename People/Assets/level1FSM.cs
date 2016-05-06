using UnityEngine;
using System.Collections;

public class level1FSM : FSM {

        // Use this for initialization
        public float health = 0f;
        public float maxHealth = 100.0f;

        State state = null;

        // Use this for initialization
        void Start()
        {
            StartCoroutine("ExitFormation");
        }

        IEnumerator ExitFormation()
        {
            yield return new WaitForSeconds(2.0f);
            SwitchState(new PeopleRandomMovement(this));
        }

        /*
        System.Collections.IEnumerator Consume()
        {
            while (hunger < maxHunger)
            {
                hunger++;
                // Change to black the more hungry we are
                Color spawnColor = GetComponent<FishParts>().spawnColor;
                GetComponent<Boid>().maxSpeed = ((maxHunger - hunger) / maxHunger) * 5.0f;
                for (int j = 0; j < transform.childCount; j++)
                {                
                    transform.GetChild(j).GetComponent<Renderer>().material.color = Color.Lerp(spawnColor, Color.black, hunger / maxHunger);
                }
                yield return new WaitForSeconds(1.0f);
            }
            SwitchState(new DeadState(this));
        }*/


        // Update is called once per frame
        void Update()
        {
            if (state != null)
            {
                state.Update();
                Debug.Log(state.Description());
            }
        }
        /*
        void OnTriggerEnter(Collider other)
        {

        }*/
    }

