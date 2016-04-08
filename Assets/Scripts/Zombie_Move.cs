using UnityEngine;
using System.Collections;

public class Zombie_Move : MonoBehaviour
{
    public float speed;
    // Use this for initialization
    void Start()
    {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);

    }
    GameObject[] gameObjects;
    void OnBecameInvisible()
    {
            PlayerHealth.Health -= 1;
            Destroy(gameObject);
    }
}
