using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// Indicates the speed used for ball movement.
    /// </summary>
    public float speed = 40;

    /// <summary>
    /// Indicates the sound to be used when ball hits a wall.
    /// </summary>
    public AudioClip collideSoundClip;


    // Start is called before the first frame update
    void Start()
    {
        this.Initialize(true);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        var ballPosition = transform.position;

        // Here we check if we're colliding with any of the pads.
        if (col.gameObject.name == "LeftPaddle" || col.gameObject.name == "RightPaddle")
        {
            var padPosition = col.transform.position;
            var padHeight = col.collider.bounds.size.y;

            // Simple calculation to determine where the ball hits the pad, so we have a factor in the following range:
            // 1 --> Top of Pad
            // ...
            // 0 --> Middle of Pad
            // ...
            // -1 --> Bottom of pad
            var hitFactor = (ballPosition.y - padPosition.y) / padHeight;

            // Calculate de direction based on padName
            var dir = col.gameObject.name == "LeftPaddle" ? 1 : -1;

            // Sets the new velocity
            GetComponent<Rigidbody2D>().velocity = new Vector2(dir, hitFactor).normalized * speed;
        }
        else if (col.gameObject.CompareTag("GoalZone"))
        {
            // In this case, we collide with a goal wall, so we notify the score manager.
            GameManager.Instance.Score(this.gameObject, col);
        }

        // We play a sound when the ball collides with any object but the goal zones.
        // In those cases, the GameManager will be the one responsible for processing a score (even the sound).
        if (!col.gameObject.CompareTag("GoalZone"))
            SoundManager.Instance.Play(this.collideSoundClip);

    }

    /// <summary>
    /// Sets the ball position to 0,0 and gives an initial velocity to one side.
    /// </summary>
    public void Initialize(bool startMovement)
    {
        // Reset ball position..
        this.transform.position = Vector2.zero;

        if (startMovement)
        {
            // Generates a random number either 1 or 2.. if 2 then throws ball to right otherwise to left.
            var side = Random.Range(1, 3) % 2 == 0 ? 1 : -1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(side, 0) * speed;
        }
    }
}
