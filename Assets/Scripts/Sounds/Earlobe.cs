using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earlobe
{
    private Vector3 position;
    public Earlobe() {
        position = new Vector3(0,0,0);
    }

    public Vector3 GetPosition() {
        return position;
    }

    public void SetPosition(Vector3 newPosition) {
        position = newPosition;
    }
}
