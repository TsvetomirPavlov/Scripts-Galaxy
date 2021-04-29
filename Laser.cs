using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable 8 

    [SerializeField]
    private float _speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        transform.Translate(Vector3.up  * _speed * Time.deltaTime);

        //if laser position > 8 on y - destroy
        if (transform.position.y > 8f)
        {
            //check if object has parent - destroy parent too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
