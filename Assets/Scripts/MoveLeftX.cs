using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    private float _speed = 7;
    private Player _player;
    private float _leftBound = -10;
    [SerializeField] private float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // If game is not over, move to the left
        if (!_player._gameOver)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < _leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }

    }
}
