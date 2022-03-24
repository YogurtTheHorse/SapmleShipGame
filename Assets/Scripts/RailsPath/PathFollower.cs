using System;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Path path;
    public GameObject objectToMove;

    public float timeSpeed;
    public int currentSection = 0;
    public float tParam = 0;

    private void Update()
    {
        var zero = transform.position;
        var section = path.sections[currentSection];

        Vector3 p0 = section.startPoint + zero;
        Vector3 p1 = section.startPoint + section.startTangent + zero;
        Vector3 p2 = section.endPoint + section.endTangent + zero;
        Vector3 p3 = section.endPoint + zero;

        if (tParam < 1)
        {
            tParam += Time.deltaTime * timeSpeed;

            var objectPosition = Mathf.Pow(1 - tParam, 3) * p0
                                 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1
                                 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2
                                 + Mathf.Pow(tParam, 3) * p3;

            objectToMove.transform.position = objectPosition;
        }
        else
        {
            tParam = 0f;

            currentSection = (currentSection + 1) % path.sections.Length;
        }
    }
}