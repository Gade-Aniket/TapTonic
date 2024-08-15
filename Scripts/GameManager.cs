using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject prefab;
    private float radius = 1.5f;
    private List<GameObject> prefabs = new List<GameObject>();    // List to keep track of instantiated prefabs.
    public float CollisionExitTimer = 0.05f;
    public bool IsGameOver = false;
    int temp = 0;
    float CircleRedius = 0.6f;

    private void Awake()    // Sets this script as the singleton instance.
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;    // Sets the time scale to 1.
        TargetPointer.instance.IsColliding = false;    // Initialize collision state.
        prefab.GetComponent<CircleCollider2D>().radius = 0.6f;    // Set the initial radius for the prefab's CircleCollider2D.

        /// Calculate a random position within a circle and instantiate the first prefab.
        float randomAngle = Random.Range(0f, 360f);    // Generate a random angle.
        float x = Mathf.Cos(randomAngle * Mathf.Deg2Rad) * radius;    // X position calculation.
        float y = Mathf.Sin(randomAngle * Mathf.Deg2Rad) * radius;    // Y position calculation.
        Vector3 position = new Vector3(x, y, 0) + transform.position;    // Final position.

        GameObject initialPrefab = Instantiate(prefab, position, Quaternion.identity);    // Instantiate the prefab.
        prefabs.Add(initialPrefab);    // Add the instantiated prefab to the list.
    }

    void Update()
    {
        if (!IsGameOver)    // Update UI with TapText if the game is not over.
        {
            UIManager.instance.TapText();
        }

        if (TargetPointer.instance.IsColliding && Input.GetMouseButtonDown(0))    // Check if a new prefab should be spawned based on collision and input.
        {
            /// Calculate a random position within a circle and instantiate a new prefab.
            float randomAngle = Random.Range(0f, 360f);
            float x = Mathf.Cos(randomAngle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(randomAngle * Mathf.Deg2Rad) * radius;
            Vector3 position = new Vector3(x, y, 0) + transform.position;

            GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity);
            prefabs.Add(newPrefab);
            GameOver();    // Check if the game should end.

            if (prefabs.Count > 1)    // Remove the second-to-last prefab if more than one prefab exists.
            {
                Destroy(prefabs[prefabs.Count - 2]);    // Destroy the second-to-last prefab.
                prefabs.RemoveAt(prefabs.Count - 2);    // Remove it from the list.
            }
        }
    }
    public void GameOver()
    {
        if (TargetPointer.instance.IsColliding)    // Check if colliding and update game logic if necessary.
        {
            if (temp < TargetPointer.instance.flag)    // If the current flag is greater than the temporary value, update temp and modify game settings.
            {
                /// Update temp with the new flag value.
                /// Increase collider radius.
                /// Reduce collision exit timer.
                int temp = TargetPointer.instance.flag;
                prefab.GetComponent<CircleCollider2D>().radius = CircleRedius + 0.04f;
                CollisionExitTimer -= 0.005f;
            }
        }

        if (!TargetPointer.instance.IsColliding)    // If not colliding, update the collision exit timer.
        {
            CollisionExitTimer -= Time.deltaTime;    // Decrease timer based on frame time.

            if (Input.GetMouseButtonDown(0) && CollisionExitTimer <= 0)     // Check if game over conditions are met.
            {
                /// Pause the game.
                Time.timeScale = 0;
                IsGameOver = true;    // Set the game over flag.

                /// Game over logic here.
            }
        }
    }
}