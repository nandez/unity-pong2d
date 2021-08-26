using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    /// <summary>
    /// Indicates the speed used for pad movement.
    /// </summary>
    public float speed = 40;

    /// <summary>
    /// Indicates the name of the current axis.
    /// </summary>
    public string inputAxisName;




    /// <summary>
    /// Bind the key to be used to move current pad upwards.
    /// Note: Commented out to try Input approach instead...
    /// </summary>
    //public KeyCode upKey;

    /// <summary>
    /// Bind the key to be used to move current pad downwards.
    /// Note: Commented out to try Input approach instead...
    /// </summary>
    ///public KeyCode downKey;




    // Update is called once per frame
    void Update()
    {
        // Note: Commented out to try Input approach instead...
        /*if (Input.GetKey(upKey))
        {
            this.transform.Translate(0f, 0.1f, 0f);
        }
        else if (Input.GetKey(downKey))
        {
            this.transform.Translate(0f, -0.1f, 0f);
        }*/
    }

    private void FixedUpdate()
    {
        var verticalValue = Input.GetAxisRaw(inputAxisName);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, verticalValue) * speed;
    }
}
