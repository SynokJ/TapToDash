using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    private float MOVE_SPEED = 5.0f;
    private const float MOVE_JUMP = 3.0f;
    private const float MOVE_ROTATE_ANGLE = 45f;
    private Queue<MoveState> cmds;
    private bool isLast = false;

    public enum MoveState
    {
        run,
        right,
        left,
        up
    };

    public Player()
    {
        cmds = new Queue<MoveState>();
    }

    public Player(float speed)
    {
        MOVE_SPEED = speed;
        cmds = new Queue<MoveState>();
    }

    public void RefreshCurrentCommands(string[] cmds_data)
    {

        cmds.Clear();

        foreach (string cmd in cmds_data)
            switch (cmd)
            {
                case "right":
                    cmds.Enqueue(Player.MoveState.right);
                    break;
                case "left":
                    cmds.Enqueue(Player.MoveState.left);
                    break;
                case "up":
                    cmds.Enqueue(Player.MoveState.up);
                    break;
                default:
                    Debug.Log("incorrect command: " + cmd);
                    isLast = true;
                    break;
            }
    }

    public void CheckCurrentCommands()
    {
        string res = "";

        foreach (MoveState ms in cmds)
        {
            switch (ms)
            {
                case MoveState.right:
                    res += "right,";
                    break;
                case MoveState.left:
                    res += "left,";
                    break;
                case MoveState.up:
                    res += "up,";
                    break;
                case MoveState.run:
                    res += "run,";
                    break;
            }
        }

        Debug.Log(res.ToString());
    }

    public float GetSpeed()
    {
        return MOVE_SPEED;
    }

    public float GetJumpScale()
    {
        return MOVE_JUMP;
    }

    public float GetRotationAngle()
    {
        return MOVE_ROTATE_ANGLE;
    }

    public Queue<MoveState> GetCommandsContainer()
    {
        return cmds;
    }

    public MoveState GetCurCommand()
    {
        return cmds.Dequeue();
    }

    public bool IsEnded()
    {
        return isLast;
    }
}
