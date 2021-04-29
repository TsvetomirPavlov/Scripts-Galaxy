using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    
    
    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on Z axis
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime, Space.Self); 
    }

    //check for laser collision (trigger)-instatiate explosion at the position of the asteroid (us)
    //destroy explosion after 3s 

    private void OnTriggerEnter2D(Collider2D other)
    {
 
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f);

        }
    }
}
