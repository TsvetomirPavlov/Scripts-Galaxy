using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public event EventHandler onThresholdReached;
    [SerializeField]
    private float _speed = 6f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.3f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    //is triple shot created
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isShieldsActive = false;

    //variable reference to the shield obj
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    public int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _RightEngine;
    [SerializeField]
    private GameObject _LeftEngine;
    [SerializeField]
    private AudioClip _laserSoundClip;

    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()

    {
        //take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is null!");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source is null!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

#if UNITY_ANDROID
         if ( (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire"))&& Time.time > _canFire)
        {
            FireLaser();
        }

#elif UNITY_IOS
        if ( (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire"))&& Time.time > _canFire)
        {
            FireLaser();
        }
#else
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
#endif
        
    }

    void CalculateMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");  //Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); // Input.GetAxis("Vertical");

        //  transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //  transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); 

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
         
        transform.Translate(direction * _speed * Time.deltaTime);



        // if player posistion on y is > 0

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //if player on x > 11 x pos = -11 
        //if player on x < -11 x pos = 11

        /* if (transform.position.x >= 8.5f)
         {
             transform.position = new Vector3(-8.5f, transform.position.y, 0);
         }
         else if (transform.position.x <= -8.5)
         {
             transform.position = new Vector3(8.5f, transform.position.y, 0);
         }
     }*/
        if (transform.position.x >= 13f)
        {
            transform.position = new Vector3(-13f, transform.position.y, 0);
        }
        else if (transform.position.x <= -13f)
        {
            transform.position = new Vector3(13f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        //space - game object
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.73f, 1.15f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.15f, 0), Quaternion.identity);
        }

        //play the laser audio clip
        _audioSource.Play();

        //instantiate 3 lasers (3shot prefab)
        // if space key press fire 1 laser
        //if triple shot active = true, fire 3 (prefab)
        //else fire 1 laser
    }

    public void Damage()
    {
        //if shields is active - deactivate shield
        //return
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;
        //if lives is 2- activate right engine
        //else if lives is 1 - activate left engine
        if (_lives == 2)
        {
            _RightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _LeftEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);
        //check if dead - destroy us
        if (_lives < 1)
        {
            //communicate to spawn manager - let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    //method public void TripleShotActive

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        //start the powerDown coroutine
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //IEnumerator TripleShotPowerDownRoutine
    //wait 5 sec
    //set triple shot to false
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }
    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;

        _uiManager.UpdateScore(_score);
        if(_score % 100 == 0 )
        {
            onThresholdReached?.Invoke(this, EventArgs.Empty);
        }
    }
    //method to add 10 to the score
    //communicate with UI to add score


}

