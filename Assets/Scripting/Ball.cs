using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        ScoreWall,
        BounceWall,
        Player
    }

    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
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
    /// <summary>
    /// Returns ball to origin, then sends it in a random direction
    /// </summary>
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[(int)CollisionTag.ScoreWall]))
        {
            ResetBall();
            GameManager.IncrementScore(other.GetComponent<ScoreWall>().scoringPlayer);
            audioSource.clip = clipScore;
            audioSource.Play();
        }
        else if (other.CompareTag(tags[(int)CollisionTag.BounceWall]))
        {
            direction.y = -direction.y;
            audioSource.clip = clipBounceWall;
            audioSource.Play();
        }
        else if (other.CompareTag(tags[(int)CollisionTag.Player]))
        {
            direction.x = -direction.x;
            direction.y = transform.position.y - other.transform.position.y;
            direction = direction.normalized;
            audioSource.clip = clipBouncePlayer;
            audioSource.Play();
        }
    }
}
