using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse2D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask movementLayer;
    [SerializeField] Vector2 lowerBounds;
    [SerializeField] Vector2 upperBounds;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        print(Input.mousePosition + ";;;" + mouseWorldPosition);
             
        transform.position = mouseWorldPosition;

    }

}
