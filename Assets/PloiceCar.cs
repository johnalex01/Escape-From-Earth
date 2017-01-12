using UnityEngine;
using System.Collections;

public class PloiceCar : MonoBehaviour {

    public AudioSource Tire;
    public bool HavePlayVoice = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (HavePlayVoice == false && Gamecontroller.GameSta==GameState.End  )
        {
            Tire.Play();
            HavePlayVoice = true;
        }
	}
}
