using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

    State state = null;

    public void SwitchState(State state)
    {
        if (this.state != null)
        {
            this.state.Exit();
        }

        this.state = state;

        if (this.state != null)
        {
            this.state.Enter();
        }
    }
}
