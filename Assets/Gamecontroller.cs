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
    public GameObject EndUI;
    
    
    void Update()
    {
        if(GameSta==GameState.Menu)
            if (Input.GetMouseButtonDown(0))
            {
                GameSta = GameState.Playing;
                startUI.SetActive(false);
            }
        if (GameSta == GameState.End)
        {
            EndUI.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                GameSta = GameState.Menu;
                Application.LoadLevel(0);
            }
        }
    }

    // Use this for initialization
	void Start () {
	
	}
	
}
