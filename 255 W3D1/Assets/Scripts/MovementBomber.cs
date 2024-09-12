using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBomber : MonoBehaviour
{

    //[SerializeField] Rigidbody thisRb;
    [SerializeField] GameObject bomb;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bomb, transform);
        }

        var posNew = transform.position;
        posNew.x = transform.position.x + speed * Time.deltaTime;
        transform.SetPositionAndRotation(posNew, transform.rotation);
    }
}
