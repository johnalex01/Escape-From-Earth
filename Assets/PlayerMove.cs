﻿using UnityEngine;
using System.Collections;

public enum MouseMoveDir
{
    NONE, UP, DOWN, LEFT, RIGHT
}

public class PlayerMove : MonoBehaviour {



    private Vector3 LastMouseDown=Vector3.zero;

    private MouseMoveDir dir = MouseMoveDir.NONE;
    public float moveSpeed = 100;
    public int CurLineIndex=1;
    public int TargetLineIndex = 1;
    public float SlideTime = 1.0f;

    private float SlideTimer = 0;
    public bool isSlideing = false;
    private envGenerate envGenerator;
    private float moveHorizantal=0;
    private float moveHorizantalSpeed = 6;
    private float[] xOffset = new float[3] {-14,0,14};
    void Awake()
    {
        envGenerator = Camera.main.GetComponent<envGenerate>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Gamecontroller.GameSta == GameState.Playing)
        {
            Vector3 targetPos = envGenerator.forest1.getNextTarget();
            targetPos = new Vector3(targetPos.x + xOffset[TargetLineIndex], targetPos.y, targetPos.z);
            Vector3 moveDir = targetPos - transform.position;
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
            MoveControl();
        }
        
	}

    private void MoveControl()
    {
        dir = GetTouchDir();
        print(dir);

        if (CurLineIndex != TargetLineIndex)
        {
            float moveLen = Mathf.Lerp(0, moveHorizantal, moveHorizantalSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + moveLen, transform.position.y, transform.position.z);

                moveHorizantal -= moveLen;

            if (Mathf.Abs( moveHorizantal )< 0.5f)
            {
                transform.position = new Vector3(transform.position.x + moveHorizantal, transform.position.y, transform.position.z);
                CurLineIndex = TargetLineIndex;
                moveHorizantal = 0;
            }
        }

        if (isSlideing == true)
        {
            SlideTimer += Time.deltaTime;
            if (SlideTime <= SlideTimer)
            {
                isSlideing = false;
                SlideTimer = 0;
            }
        }
    }

    MouseMoveDir GetKeyDir()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (TargetLineIndex < 2)
            {
                TargetLineIndex++;
                moveHorizantal = 14;
            }
            return MouseMoveDir.RIGHT;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (TargetLineIndex > 0)
            {
                TargetLineIndex--;
                moveHorizantal = -14;
            }
            return MouseMoveDir.LEFT;
        }

        return MouseMoveDir.NONE;
    }

    MouseMoveDir GetTouchDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LastMouseDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 MouseUp = Input.mousePosition;
            Vector3 TouchOffset = MouseUp - LastMouseDown;
            if (Mathf.Abs(TouchOffset.x) > 50 || Mathf.Abs(TouchOffset.y) > 50)
            {
                if (Mathf.Abs(TouchOffset.x) > Mathf.Abs(TouchOffset.y) && TouchOffset.x > 0)
                {
                        if (TargetLineIndex < 2)
                        {
                            TargetLineIndex++;
                            moveHorizantal = 14;
                        }
                        return MouseMoveDir.RIGHT;
                }else if(Mathf.Abs(TouchOffset.x) > Mathf.Abs(TouchOffset.y) && TouchOffset.x < 0)
                    {
                        if (TargetLineIndex > 0)
                        {
                            TargetLineIndex--;
                            moveHorizantal = -14;
                        }
                        return MouseMoveDir.LEFT;
                    }
                else if (Mathf.Abs(TouchOffset.x) < Mathf.Abs(TouchOffset.y) && TouchOffset.y > 0)
                {
                        return MouseMoveDir.UP;
                }
                else{
                    isSlideing = true;
                    SlideTimer = 0;
                     return MouseMoveDir.DOWN;
                }
               
            }

        }

        return MouseMoveDir.NONE;
    }

}
