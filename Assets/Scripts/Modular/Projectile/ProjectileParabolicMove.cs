using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParabolicMove : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public float height;
    public float speed;


    private void Start()
    {
        StartCoroutine(CoroutineMovement(a.position, b.position, height, speed));
    }

    IEnumerator CoroutineMovement(Vector3 start, Vector3 end, float height, float speed)
    {
        var centerPivot = (start + end) / 2;
        centerPivot -= new Vector3(0, height);

        var startRelative = start - centerPivot;
        var endRelative = end - centerPivot;

        var distance = Vector3.Distance(startRelative, endRelative);
        var journeyTime = distance / speed;

        float startTime = Time.time;

        while (Time.time < startTime + journeyTime)
        {
            float progress = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(startRelative, endRelative, progress) + centerPivot;
            yield return null;
        }

        transform.position = end;
        Debug.Log("Booom");
    }

}
