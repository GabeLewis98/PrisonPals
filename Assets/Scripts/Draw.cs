using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject tattooGun;
    public float lineDelta = 0.05f;
    public GameObject linePrefab;
    public float zHeight = 1f;
    public EdgeCollider2D stencilCollider;
    public Vector3 offset;
    public GameManager gameScript;

    LineRenderer currentLineRenderer;
    int lastIndex = 0;

    Vector3 lastPos;
    List<Vector3> tattooPoints = new List<Vector3>();
    List<LineRenderer> tattooLines = new List<LineRenderer>();

    private void Update()
    {
        Tat();
    }

    void Tat()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
            lastPos.z = zHeight;
            GameObject newLine = Instantiate(linePrefab);
            currentLineRenderer = newLine.GetComponent<LineRenderer>();
            addPoint(lastPos);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
            mousePos.z = zHeight;
            if (Vector3.Distance(mousePos, lastPos) >= lineDelta)
            {
                lastPos = mousePos;
                addPoint(lastPos);
            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lastIndex = tattooPoints.Count;
            tattooLines.Add(currentLineRenderer);
            currentLineRenderer = null;
            StartCoroutine(gameScript.EndTatto());
        }
    }

    void addPoint(Vector3 newPoint)
    {
        tattooPoints.Add(newPoint);
        currentLineRenderer.positionCount = tattooPoints.Count - lastIndex;
        Vector3[] newLinePoints = tattooPoints.GetRange(lastIndex, currentLineRenderer.positionCount).ToArray();
        currentLineRenderer.SetPositions(newLinePoints);
    }

    float checkScore(Vector3 currentPoint)
    {
        if (stencilCollider == null)
            return 0;

        Vector3 closestPoint = stencilCollider.ClosestPoint(currentPoint);
        float dist = Vector3.Distance(currentPoint, closestPoint);
        return dist + zHeight;     
    }

    public void setUpTattoo(EdgeCollider2D collider)
    {
        stencilCollider = collider;
        for (int i = 0; i < tattooLines.Count; i++)
        {
            Destroy(tattooLines[i].gameObject);
        }
        tattooLines = new List<LineRenderer>();
        tattooPoints = new List<Vector3>();
        lastIndex = 0;       
    }

    public float getFinalScore()
    {
        float total = 0;
        for (int i = 0; i < tattooPoints.Count; i++)
        {
            total += checkScore(tattooPoints[i]);
        }
        return total;
    }
}
