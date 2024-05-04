using System.Collections;
using UnityEngine;

public class HorizontalMobController : MonoBehaviour
{
    public float speed = 1.0f;
    public float width = 1.0f;
    public float waitTime = 0.2f;

    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rightPosition = transform.position + new Vector3(width, 0, 0);
        leftPosition = transform.position;

        StartCoroutine(MoveLeftRight());
    }

    IEnumerator MoveLeftRight()
    {
        while (true)
        {
            yield return MoveRight();

            yield return new WaitForSeconds(waitTime);

            yield return MoveLeft();

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveLeft()
    {
        spriteRenderer.flipX = true;
        yield return MoveToPosition(leftPosition);
    }

    IEnumerator MoveRight()
    {
        spriteRenderer.flipX = false;
        yield return MoveToPosition(rightPosition);
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