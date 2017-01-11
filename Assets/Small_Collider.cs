using UnityEngine;
using System.Collections;

public class Small_Collider : MonoBehaviour {

    private PlayerAnimation PlayAni;
    void Awake()
    {
        PlayAni = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAnimation>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.obs && Gamecontroller.GameSta == GameState.Playing && PlayAni.anistate == AnimationState.Silde)
        {
            Gamecontroller.GameSta = GameState.End;
        }
    }
}
