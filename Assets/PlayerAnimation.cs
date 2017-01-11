using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    private enum AnimationState
    {
        Idle,
        Run,
        TurnL,
        TurnR,
        Silde
    }

    private PlayerMove plaMove;

    private Animation animation;
    private AnimationState anistate=AnimationState.Idle;

    void Awake()
    {
        animation = transform.Find("Prisoner").GetComponent<Animation>();
        plaMove = this.GetComponent<PlayerMove>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Gamecontroller.GameSta == GameState.Menu)
        {
            anistate = AnimationState.Idle;
        }
        else if (Gamecontroller.GameSta == GameState.Playing)
        {
            anistate = AnimationState.Run;
            if (plaMove.TargetLineIndex > plaMove.CurLineIndex)
                anistate = AnimationState.TurnR;
            if (plaMove.TargetLineIndex < plaMove.CurLineIndex)
                anistate = AnimationState.TurnL;
            if (plaMove.isSlideing == true)
                anistate = AnimationState.Silde;
        }
	}
    void LateUpdate()
    {
        switch (anistate)
        {
            case AnimationState.Idle:
                PlayIdle();
                break;
            case AnimationState.Run:
                PlayAnimation("run");
                break;
            case AnimationState.TurnL:
                animation["left"].speed = 2;
                PlayAnimation("left");
                break;
            case AnimationState.TurnR:
                animation["right"].speed = 2;
                PlayAnimation("right");
                break;
            case AnimationState.Silde:
                PlayAnimation("slide");
                    break;
        }

    }

    void PlayIdle()
    {
        if (animation.IsPlaying("Idle_1") == false && animation.IsPlaying("Idle_2") == false)
        {
            animation.Play("Idle_1");
            animation.PlayQueued("Idle_2");
        }
    }

    void PlayAnimation(string aniName)
    {
        if (animation.IsPlaying(aniName) == false)
        {
            animation.Play(aniName);
        }
    }
}
