using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float LaserVerticalSpeed = 5.0f;
    public float LaserVerticalLimit = 6.0f;

    // Update is called once per frame
    void Update()
    {
        // Move UP Laser
        transform.Translate(Vector3.up * LaserVerticalSpeed * Time.deltaTime);


        ///Destroy Laser when it reaches LaserVerticalLimit
        if (transform.position.y >= LaserVerticalLimit)
        {
            Debug.Log("Laser reached 'LaserVerticalLimit' and it should be destroyed.");
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Laser became invisible and it should be destroyed.");
        Destroy(this.gameObject);

        // Çalışmadı amına koduğum!!!
    }

}
