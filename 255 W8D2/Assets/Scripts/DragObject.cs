using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 position = Input.mousePosition;

        position.z = Camera.main.WorldToScreenPoint(transform.position).z;

        position = Camera.main.ScreenToWorldPoint(position);
        
        return position;
    }
}
