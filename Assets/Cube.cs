using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	[SerializeField]
	float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        transform.position += Vector3.right * h * speed * Time.deltaTime;

        transform.position += Vector3.up * v * speed * Time.deltaTime;

    }
}
