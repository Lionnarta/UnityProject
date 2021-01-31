using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidbody;

    public GameObject ballAtCollision;

    // Initiate collision state
    bool drawBallAtCollision = false;
    // Initiate center point of collision
    Vector2 offsetHitPoint = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius, ballRigidbody.velocity.normalized);
        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            // Collision occur
            if (circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;

                Vector2 hitNormal = circleCastHit2D.normal;

                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    // In vector
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    // Out vector
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    // Check dot product for collision line
                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0f && outDot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f);

                        drawBallAtCollision = true;
                    }
                }

                break;
            }
        }
        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }else
        {
            ballAtCollision.SetActive(false);
        }
    }
}
