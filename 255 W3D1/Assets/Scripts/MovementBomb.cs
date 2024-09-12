using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", 5f);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
