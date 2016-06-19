using UnityEngine;
using System.Collections;

public class Zombie_Move : MonoBehaviour
{
    public float speed;
    public static float speedY = 0;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, speedY);

    }

    // Update is called once per frame
    void Update()
    {
        if (speedY == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, speedY) * Affint.ZombieSpeedFactor;
        }

        if (GetComponent<Rigidbody2D>().position.x < -7.6f)
        {
            Debug.Log("Nope");
            PlayerHealth.Health -= 1;
            Destroy(gameObject);
        }
    }


}
