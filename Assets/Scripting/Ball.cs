using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
    [SerializeField] private string otherTag;
    private Vector2 direction;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clipBouncePlayer;
    [SerializeField] private AudioClip clipBounceWall;
    [SerializeField] private AudioClip clipScore;
    void Start()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[0]))
        {
            ResetBall();
        }
        else if (other.CompareTag(otherTag))
        {
            direction.y = -direction.y;
        }
        else if (other.CompareTag("Player"))
        {
            direction.x = -direction.x;
            direction.y = transform.position.y - other.transform.position.y;
            direction = direction.normalized;
        }
    }
}
