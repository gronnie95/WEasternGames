using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float width;
    public float height;
    private Camera cameraReference;
    private Earlobe leftEarlobe;
    private Earlobe rightEarlobe;

#region unity methods
    // Start is called before the first frame update
    void Start()
    {
        cameraReference = gameObject.GetComponent<Camera>();
        leftEarlobe = new Earlobe();
        rightEarlobe = new Earlobe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion

/// <summary>
/// This class does something.
/// </summary>
public void UpdateEarlobePositions() {
    Vector3 newLeftEarPosition = gameObject.transform.position + (-gameObject.transform.right * GetHeadRadius());
    Vector3 newRightEarPosition = gameObject.transform.position + (gameObject.transform.right * GetHeadRadius());

    leftEarlobe.SetPosition(newLeftEarPosition);
    rightEarlobe.SetPosition(newRightEarPosition);
}

#region getters and setters
    public void SetWidth(float width) {
        this.width = width;
    }

    public void SetHeight(float height) {
        this.width = height; 
    }

    public Earlobe GetLeftEarlobe() {
        return this.leftEarlobe;
    }

    public Earlobe GetRightEarlobe() {
        return this.rightEarlobe;
    }

    public void SetLeftEarlobe(Earlobe lobe) {
        this.leftEarlobe = lobe;
    }

    public void SetRightEarlobe(Earlobe lobe) {
        this.rightEarlobe = lobe;;
    }

    public float GetHeadRadius() {
        return this.width/2;
    }
#endregion
}
