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
        Cursor.visible = true;
    }

    private void Update()
    {
        //RaycastHit hit;
        //Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
        //Vector3 mouseWorldPosition = hit.point;
        Vector2 inp = Input.mousePosition;
        inp.x /= Screen.width;
        inp.y /= Screen.height;
        Vector3 mouseWorldPosition = new Vector3(
            Mathf.Lerp(lowerBounds.x, upperBounds.x, inp.x),
            Mathf.Lerp(lowerBounds.y, upperBounds.y, inp.y),
            0);

        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

}
