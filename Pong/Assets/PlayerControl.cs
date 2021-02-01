using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Button to move up
    public KeyCode upButton = KeyCode.W;

    // Button to move down
    public KeyCode downButton = KeyCode.S;

    // Movement speed
    public float speed = 10.0f;

    // Upper and lower boundary
    public float yBoundary = 9.0f;

    // Rigidbody 2d
    private Rigidbody2D rigidBody2D;

    // Score
    private int score;

    // Bom
    private bool bomStat = false;

    // Debug
    // Last contact point
    private ContactPoint2D lastContactPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get velocity
        Vector2 velocity = rigidBody2D.velocity;

        // Go up
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        // Go down
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        // No button clicked
        else
        {
            velocity.y = 0.0f;
        }
        rigidBody2D.velocity = velocity;

        // Get pos
        Vector3 position = transform.position;

        // Pos pass yBoundary
        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        // Pos pass -yBoundary
        else if(position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }
        transform.position = position;
    }

    // Raise score
    public void IncrementScore()
    {
        score++;
    }

    // Reset score to 0
    public void ResetScore()
    {
        score = 0;
    }

    // Get score value
    public int Score
    {
        get { return score; }
    }

    // Access last contact point
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    public void ChangeBomStat(bool bomBool)
    {
        bomStat = bomBool;
    }

    public bool BomStat
    {
        get { return bomStat; }
    }
}
