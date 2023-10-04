using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemyVerticalSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //move down always
        transform.Translate(Vector3.down * enemyVerticalSpeed * Time.deltaTime);

        //if reach to bottom bound (-7f), teleport to top bound (7)
        if (transform.position.y < -7f)
        {
            float _randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(_randomX, 7, 0);
        }
    }

    // If Something Collides Me (Enemy)
    private void OnTriggerEnter(Collider other)
    {

        // if other is player
        // Damage player
        // Destroy me
        if (other.tag == "Player")
        {
            Debug.Log("Damage Player");
            Debug.Log("Destroy Enemy that collides Player");
            Destroy(this.gameObject);

        }

        // if other is laser
        // destroy laser
        // destroy me
        if (other.tag == "Laser")
        {
            Debug.Log("A Laser collides Me (Enemy)");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }


    }

}
