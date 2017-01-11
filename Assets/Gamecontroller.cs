using UnityEngine;
using System.Collections;
public enum GameState {
    Menu,
    Playing,
    End
}
public class Gamecontroller : MonoBehaviour {

    public static GameState GameSta = GameState.Menu;


    public GameObject startUI;

    void Update()
    {
        if(GameSta==GameState.Menu)
            if (Input.GetMouseButtonDown(0))
            {
                GameSta = GameState.Playing;
                startUI.SetActive(false);
            }
    }

    // Use this for initialization
	void Start () {
	
	}
	
}
