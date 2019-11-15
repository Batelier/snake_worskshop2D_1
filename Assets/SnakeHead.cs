using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour
{
    public static float gamespeed;
    Vector2 snakeDirection;
    private bool _ate;
    private bool _horizontalDirection;

    public GameObject tailPart;
    private List<GameObject> Tail = new List<GameObject>();

    public Text Score;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        _ate = false;
        _horizontalDirection = true;
        snakeDirection = Vector2.right;

        gamespeed = 0.1f;
        InvokeRepeating("Move" , gamespeed, gamespeed);
    }

    private void Move()
    {
        Vector2 lastPos = transform.position;
        transform.Translate(snakeDirection);


        if (Tail.Count > 0)
        {
            Tail[Tail.Count - 1].transform.position = lastPos;
            Tail.Insert(0, Tail[Tail.Count -1]);
            Tail.RemoveAt(Tail.Count - 1);
        }


        if (_ate)
        {
            GameObject newTailPart = Instantiate(tailPart, lastPos, Quaternion.identity);
            Tail.Add(newTailPart);
        }
        _ate = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!_horizontalDirection)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                snakeDirection = Vector2.right;
                _horizontalDirection = !_horizontalDirection;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                snakeDirection = Vector2.left;
                _horizontalDirection = !_horizontalDirection;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                snakeDirection = Vector2.up;
                _horizontalDirection = !_horizontalDirection;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                snakeDirection = Vector2.down;
                _horizontalDirection = !_horizontalDirection;
            }
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            GameOver();
        }
        if (collision.CompareTag("Food"))
        {
            _ate = true;
            Destroy(collision.gameObject);

            updateScore();
        }
    }

    void updateScore()
    {
        score++; // score = score + 1;
        Score.text = "Score : " + score;
    }

    void GameOver()
    {
        Destroy(this);
        foreach (GameObject tailPart in Tail)
        {
            Destroy(tailPart.gameObject);
        }
    }
}
