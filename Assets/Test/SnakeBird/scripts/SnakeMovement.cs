using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Position
{
    public Position(Vector3 cur, Vector3 futureNextMove)
    {
        currentPos = cur;
        futurePosNextMove = futureNextMove;
    }
    public Vector3 currentPos;
    public Vector3 futurePosNextMove;
}

public class SnakeMovement : MonoBehaviour
{
    public List<GameObject> gmObjSnake = new List<GameObject>();
    public List<Position> posManager = new List<Position>();
    public Rigidbody2D rbHead;
    public LayerMask wallLayer; // Lớp kiểm tra va chạm với tường
    public LayerMask groundLayer; // Lớp kiểm tra va chạm với mặt đất

    private void Start()
    {
        if (gmObjSnake.Count > 0)
        {
            rbHead = gmObjSnake[0].GetComponent<Rigidbody2D>();
            InitPos();
        }
    }

    private void InitPos()
    {
        posManager.Clear(); // Đảm bảo không bị trùng lặp dữ liệu
        for (int i = 1; i < gmObjSnake.Count; i++)
        {
            posManager.Add(new Position(gmObjSnake[i].transform.position, gmObjSnake[i - 1].transform.position));
        }
    }

    private Vector3 dir;
    private bool isPlay;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && dir != Vector3.right)
        {
            dir = Vector3.left;
            TryMoveWithInput(-1, 0, Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.W) && dir!=Vector3.down)
        {
            dir = Vector3.up;
            TryMoveWithInput(0, 1,Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) && dir != Vector3.up)
        {
            dir = Vector3.down;
            TryMoveWithInput(0, -1,Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.D) && dir != Vector3.left)
        {
            dir = Vector3.right;
            TryMoveWithInput(1, 0,Vector3.right);
        }
        Debug.DrawRay(gmObjSnake[0].transform.position,dir * 1f, Color.green);
    }

    private void FixedUpdate()
    {
        if (isPlay)
        {
            CheckAllSegmentsGrounded();   
        }
    }

    private void TryMoveWithInput(int x, int y, Vector3 dir)
    {
        Vector3 newHeadPosition = gmObjSnake[0].transform.position + new Vector3(x, y, 0);

        if (!isPlay) isPlay = true;
        if (CanMoveTo(newHeadPosition, dir))
        {
            gmObjSnake[0].transform.position = newHeadPosition;
            UpdatePosition(newHeadPosition);
        }
    }

    private bool CanMoveTo(Vector3 targetPosition, Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gmObjSnake[0].transform.position, direction, 1f, wallLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
        return hit.collider == null;
    }

    private void UpdatePosition(Vector3 headNewPosition)
    {
        for (int i = 0; i < posManager.Count; i++)
        {
            gmObjSnake[i + 1].transform.position = posManager[i].futurePosNextMove;
            posManager[i].currentPos = posManager[i].futurePosNextMove;
            if (i == 0)
            {
                posManager[i].futurePosNextMove = gmObjSnake[0].transform.position;
            }
            else
            {
                posManager[i].futurePosNextMove = posManager[i - 1].currentPos;
            }    
        }
    }

    private void CheckAllSegmentsGrounded()
    {
        bool allOffGround = true;

        foreach (GameObject segment in gmObjSnake)
        {
            RaycastHit2D hit = Physics2D.Raycast(segment.transform.position, Vector3.down, 1f, wallLayer);
            Debug.DrawRay(segment.transform.position, Vector3.down*1f,Color.red);
            if (hit.collider != null)
            {
                allOffGround = false;
                break;
            }
        }
        if (!allOffGround)
        {
          return;
        }
        for (int i = 0; i < gmObjSnake.Count; i++)
        {
            gmObjSnake[i].transform.position += new Vector3(0,-1,0);
        }
        posManager.Clear();
        InitPos();
    }
}
