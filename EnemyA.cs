using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private int _lives = 3;
    private Player _player;

    //handle to animator component
    private Animator _anim;
    [SerializeField]
    private AudioSource _audioSource;
    private bool _added = false;
    [SerializeField]
    private GameObject _RightHit;
    [SerializeField]
    private GameObject _LeftHit;

    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        //null check player
        if (_player == null)
        {
            Debug.LogError("Player is null!");
        }
        //assign component to anim
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //move down 4ms
        //if bottom of screen, respawn at top (with a new random x position)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            float randomX = Random.Range(-7f, 7f);
            transform.position = new Vector3(randomX, 8.5f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player -damage the player
        // destroy Us
        if (other.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            //trigger anim
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1.8f);

        }

        //if other is laser destroy - laser , destroy - Us 
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            //add 10 to score
            if (_player != null && _added == false)
            {
                _player.AddScore(60);
                _added = true;
            }
            --_lives;
            if (_lives == 2)
            {
                _RightHit.SetActive(true);
            }
            else if (_lives == 1)
            {
                _LeftHit.SetActive(true);
            }
            if (_lives < 1)
            {
                _anim.SetTrigger("onEnemyDeath");
                _speed = 0;
                _audioSource.Play();
                Destroy(this.gameObject, 1.8f);

            }
            //trigger anim

        }
    }
}
