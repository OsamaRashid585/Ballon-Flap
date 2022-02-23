using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    public bool _gameOver;
    private float _floatForce = 50;
    private float _gravityModifier = 1.5f;
    private Rigidbody _playerRb;
    private AudioSource _playerAudio;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem fireworksParticle;
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip explodeSound;
    [SerializeField] private AudioClip groundsound;
    [SerializeField] private GameObject _gameOverTxt;


    // Start is called before the first frame update
    void Start()
    {
        _gameOver = false;
        _gameOverTxt.SetActive(false);
        Physics.gravity *= _gravityModifier;
        _playerAudio = GetComponent<AudioSource>();
        _playerRb = GetComponent<Rigidbody>();
        _playerRb.isKinematic = false;
        // Apply a small upward force at the start of the game
        _playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        Flap();
        OffScreenEffect();
    }

    private void Flap()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !_gameOver)
        {
            _playerRb.AddForce(Vector3.up * _floatForce);
        }
    }
    private void OffScreenEffect()
    {
        if (transform.position.y > 14.5f)
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
        if (transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, 14.5f, transform.position.z);
            _playerAudio.PlayOneShot(groundsound, 1.0f);

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            _playerAudio.PlayOneShot(explodeSound, 1.0f);
            _gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            _playerRb.isKinematic = true;
            _gameOverTxt.SetActive(true);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            _playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
