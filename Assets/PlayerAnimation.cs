using UnityEngine;
using System.Collections;

public enum AnimationState
{
    Idle,
    Run,
    TurnL,
    TurnR,
    Silde,
    Jump,
    Death
}
public class PlayerAnimation : MonoBehaviour {


    private PlayerMove plaMove;
    private bool HavePlayDeath = false;

    private Animation animation;
    public AnimationState anistate=AnimationState.Idle;

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
            if (plaMove.isJump == true)
                anistate = AnimationState.Jump;
        }
        else if (Gamecontroller.GameSta == GameState.End)
        {
            anistate = AnimationState.Death;
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
            case AnimationState.Jump:
               PlayAnimation("jump");
                break;
            case AnimationState.Death:
                PlayDeath();
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

    void PlayDeath()
    {
        if(HavePlayDeath==false&&!animation.IsPlaying("death")){
            animation.Play("death");
            HavePlayDeath=true;
        }

    }
}
