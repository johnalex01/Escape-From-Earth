using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float RotaSpeed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up * RotaSpeed * Time.deltaTime);
	}
}
