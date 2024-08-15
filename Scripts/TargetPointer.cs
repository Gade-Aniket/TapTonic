using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public static TargetPointer instance;

    public float rotationSpeed = 150f;
    public int Tap = 0;
    public bool IsColliding = false;
    public int flag = 0;

    private void Awake()   // Sets this script as the singleton instance.
    {
        instance = this;
    }
    public void Start()    // Sets the time scale to 1 at the start of the game.
    {
        Time.timeScale = 1;
    }

    public void OnCollisionStay2D(Collision2D target)
    {
        IsColliding = true;    // Sets the IsColliding flag to true when a collision occurs.
        GameManager.instance.CollisionExitTimer = 0.05f;    // Sets a collision exit timer in the GameManager.
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsColliding = false;    // Sets the IsColliding flag to false when the collision ends.
    }

    void Update()
    {
        /// Checks for mouse clicks and increments Tap and flag counters.
        /// Increases rotation speed on each click.
        /// Calls GameOver method in GameManager (which seems unnecessary here).
        /// Rotates the pointer based on the Tap count.
        if (Input.GetMouseButtonDown(0))
        {
            flag++;
            Tap++;
            rotationSpeed = rotationSpeed * 1.05f;
        }

        GameManager.instance.GameOver();    // This line seems out of place, consider removing or explaining its purpose.

        if (Tap % 2 == 0)   // This condition  control the rotation direction (left and right) of target object.
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}