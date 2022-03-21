using System.Collections;
using UnityEngine;

/**
 *  Randomly move an object in a radius from origin
 */
public class RandomMove : MonoBehaviour
{
    private float movementDuration = 2.0f;
    public float waitBeforeMoving = 1.0f;
    private bool _hasArrived = false;
    [SerializeField] private Transform origin;

    private void Update()
    {
        if (!_hasArrived)
        {
            _hasArrived = true;
            float randX = Random.Range(-4.0f, 4.0f);
            float randZ = Random.Range(-4.0f, 4.0f);
            StartCoroutine(MoveToPoint(new Vector3(origin.position.x + randX, origin.position.y,
                origin.position.z + randZ)));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        _hasArrived = false;
    }
}