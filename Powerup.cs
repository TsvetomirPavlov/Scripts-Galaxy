using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // leave the screen - destroy
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player -damage the player
        // destroy Us
        if (other.tag == "Player")
        {
            //communicate with the player script //communicate with the player script
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default value!");
                        break;
                }
            }

            Destroy(this.gameObject);
        }

       
    }
    //onTriggerCollision
    //only be collectible by the player (use Tag)
    //on collected - destroy
}
