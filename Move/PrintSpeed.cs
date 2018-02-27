using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintSpeed : MonoBehaviour {
    private Vector3 position1;
    private Vector3 position2;
    private bool timeFlag=false;
    public float speedInterval=0.2f;
    private float time = 0;
    public float speed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time>speedInterval) {
            time = 0;
            if (timeFlag)
            {
                position1 = transform.position;
            }
            else
            {
                position2 = transform.position;
            }
            timeFlag = !timeFlag;
            speed = Vector3.Distance(position1, position2)/speedInterval;
        }
	}
}
