using System.Collections;
using UnityEngine;

public class SquareMobController : MonoBehaviour
{
    public float speed = 1.0f;
    public float up = 1.0f;
    public float down = 1.0f;
    public float left = 1.0f;
    public float right = 1.0f;
    public float waitTime = 0.2f;

    private Vector3 startPosition;
    private Vector3 rightPosition;
    private Vector3 downPosition;
    private Vector3 leftPosition;
    private Vector3 upPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        rightPosition = startPosition + new Vector3(right, 0, 0);
        downPosition = rightPosition - new Vector3(0, down, 0);
        leftPosition = downPosition - new Vector3(left, 0, 0);
        upPosition = leftPosition + new Vector3(0, up, 0);

        StartCoroutine(MoveInSquare());
    }

    IEnumerator MoveInSquare()
    {
        while (true)
        {
            yield return MoveRight();

            yield return new WaitForSeconds(waitTime);

            yield return MoveDown();

            yield return new WaitForSeconds(waitTime);

            yield return MoveLeft();

            yield return new WaitForSeconds(waitTime);

            yield return MoveUp();

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveLeft()
    {
        spriteRenderer.flipX = true;
        transform.Rotate(0, 0, -90);
        yield return MoveToPosition(leftPosition);
    }

    IEnumerator MoveRight()
    {
        spriteRenderer.flipX = false;
        transform.Rotate(0, 0, -90);
        yield return MoveToPosition(rightPosition);
    }

    IEnumerator MoveUp()
    {
        spriteRenderer.flipX = false;
        transform.Rotate(0, 0, 90);
        yield return MoveToPosition(upPosition);
    }

    IEnumerator MoveDown()
    {
        spriteRenderer.flipX = true;
        transform.Rotate(0, 0, 90);
        yield return MoveToPosition(downPosition);
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while ((transform.position - targetPosition).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}