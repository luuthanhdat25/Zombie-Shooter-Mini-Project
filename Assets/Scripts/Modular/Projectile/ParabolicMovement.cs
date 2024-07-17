using AbstractClass;
using System.Collections;
using UnityEngine;

public class ParabolicMovement : AbsMovement
{
    [SerializeField]
    private float height;

    public override void Move(Vector3 moveDirectionOrDestination, float speed)
    {
        StartCoroutine(CoroutineMovement(transform.parent.position, moveDirectionOrDestination, height, speed));
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        return;
    }

    private IEnumerator CoroutineMovement(Vector3 start, Vector3 end, float height, float speed)
    {
        var centerPivot = (start + end) / 2;
        centerPivot -= new Vector3(0, height);

        var startRelative = start - centerPivot;
        var endRelative = end - centerPivot;

        var distance = Vector3.Distance(startRelative, endRelative);
        var journeyTime = distance / speed;

        float startTime = Time.time;

        Vector3 previousPosition = start;

        while (Time.time < startTime + journeyTime)
        {
            float progress = (Time.time - startTime) / journeyTime;
            transform.parent.position = Vector3.Slerp(startRelative, endRelative, progress) + centerPivot;

            Vector3 directionNormalize = (transform.parent.position - previousPosition).normalized;
            transform.parent.rotation = Quaternion.LookRotation(directionNormalize);

            previousPosition = transform.parent.position;
            yield return null;
        }

        transform.parent.position = end;
    }
}
