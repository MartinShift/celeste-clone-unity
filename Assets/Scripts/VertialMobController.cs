using System.Collections;
using UnityEngine;

public class VerticalMobController : MonoBehaviour
{
    public float speed = 1.0f;
    public float height = 1.0f;
    public float waitTime = 0.2f;

    private Vector3 upPosition;
    private Vector3 downPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        upPosition = transform.position + new Vector3(0, height, 0);
        downPosition = transform.position;

        StartCoroutine(MoveUpDown());
    }

    IEnumerator MoveUpDown()
    {
        while (true)
        {
            yield return MoveUp();

            yield return new WaitForSeconds(waitTime);

            yield return MoveDown();

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveUp()
    {
        spriteRenderer.flipX = false;
        yield return MoveToPosition(upPosition);
    }

    IEnumerator MoveDown()
    {
        spriteRenderer.flipX = true;
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