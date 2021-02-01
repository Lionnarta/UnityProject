using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // player control score
    public PlayerControl player;

    // GameManager
    [SerializeField]
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        if(anotherCollider.name == "Ball")
        {
            // increase player score
            player.IncrementScore();

            if(player.Score < gameManager.maxScore)
            {
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
        if(anotherCollider.name == "FireBall")
        {
            if (player.Score < gameManager.maxScore)
            {
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
