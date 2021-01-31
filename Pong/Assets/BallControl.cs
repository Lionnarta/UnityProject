using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D
    private Rigidbody2D rigidBody2D;

    // Initial force
    public float xInitialForce;
    public float yInitialForce;

    // Debug
    // Trajectory
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 consSpeed = GetComponent<Rigidbody2D>().velocity.normalized;
        GetComponent<Rigidbody2D>().velocity = consSpeed * 10;
    }

    void ResetBall()
    {
        // Reset pot to 0,0
        transform.position = Vector2.zero;

        // Reset velocity to zero
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // Randomize y initial force
        float yRandomInitialForce = Random.Range(-yInitialForce,yInitialForce);

        // Randomize direction
        float randomDirection = Random.Range(0,2);

        if(randomDirection < 1.0f)
        {
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        // Return ball to initial pos
        ResetBall();
        // Give force to the ball after 2 second
        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}