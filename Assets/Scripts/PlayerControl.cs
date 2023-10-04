/*
   _____         _                        _____  _                    _              
  / ____|       | |                      / ____|| |                  | |             
 | |  __   __ _ | |  __ _ __  __ _   _  | (___  | |__    ___    ___  | |_  ___  _ __ 
 | | |_ | / _` || | / _` |\ \/ /| | | |  \___ \ | '_ \  / _ \  / _ \ | __|/ _ \| '__|
 | |__| || (_| || || (_| | >  < | |_| |  ____) || | | || (_) || (_) || |_|  __/| |   
  \_____| \__,_||_| \__,_|/_/\_\ \__, | |_____/ |_| |_| \___/  \___/  \__|\___||_|   
                                  __/ |                                              
                                 |___/                                
  
  *** Player Control and Fire Laser                                               */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    /*
    ###################################################################
    #                                                                 #
    #   Player Settings (Speed, Start Position, Bounds, Laser etc)    #
    #                                                                 #
    ###################################################################
    */

    // Player's horizontal and vertical speed
    [SerializeField] public float playerSpeedHorizontal = 5.0f;
    [SerializeField] public float playerSpeedVertical = 5.0f;
    // Player's Start position
    [SerializeField] private float playerStartPositionX = 0f;
    [SerializeField] private float playerStartPositionY = -2f;

    // Player's Bounds
    [SerializeField] private float playerBoundX = 11.4f;
    [SerializeField] private float playerBoundTop = 4.0f;
    [SerializeField] private float playerBoundBottom = -3.0f;

    // LaserPrefab selection
    [SerializeField] public GameObject LaserPrefab;
    // How many second should be pass between two laser fires.
    [SerializeField] public float laserFireRate = 0.4f;
    private float _laserNextFire = -1f;

    void Start()
    {
        // Set Player position to start position
        transform.position = new Vector3(playerStartPositionX, playerStartPositionY, 0);
    }


    void Update()
    {
        // Read Joysstick data and Move Player
        MovePlayer();

        // Keep Player in bounds and trasport horizontally
        BoundPlayer();

        // If Fire button triggered, instantiate laser with offset
        FireLaser();

    }

    void MovePlayer()
    {
        /*
        ###################################################################
        #                                                                 #
        #   Read Joytick Data and set Player position                     #
        #                                                                 #
        ###################################################################
        */
        // Read Input axis datas and multiply with playerSpeed
        float horizontalInput = Input.GetAxis("Horizontal") * playerSpeedHorizontal;
        float verticalInput = Input.GetAxis("Vertical") * playerSpeedVertical;

        /* Move player to new position in all directions */
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime);
    }

    void BoundPlayer()
    {
        /*
        ###################################################################
        #                                                                 #
        #   Keep Player in Bounds  & Transport Player Horizontally        #
        #                                                                 #
        ###################################################################
        */

        /* KEEP PLAYER IN VERTICAL LIMITS (playerBoundTop AND playerBoundBottom) */
        // if player position.y is greater than playerBoundTop, set the Y position to playerBoundTop
        if (transform.position.y >= playerBoundTop)
        {
            transform.position = new Vector3(transform.position.x, playerBoundTop, 0);
        }

        // if player position.y is less than playerBoundBottom, set Y position to playerBoundBottom
        else if (transform.position.y <= playerBoundBottom)
        {
            transform.position = new Vector3(transform.position.x, playerBoundBottom, 0);
        }

        /* TRANSPORT PLAYER IN HORIZONTAL AXIS */
        // if player position.x greater than playerBoundX (Player reached Right side), Transport player from Right to Left side
        if (transform.position.x > playerBoundX)
        {
            transform.position = new Vector3(-playerBoundX, transform.position.y, 0);
        }

        // if player position.x less than playerBoundX (Player reached Left side), Transport player from Left to Right side
        else if (transform.position.x < -playerBoundX)
        {
            transform.position = new Vector3(playerBoundX, transform.position.y, 0);
        }
    }


    void FireLaser()
    {
        /*
        ###################################################################
        #                                                                 #
        #   Fire Up a laser                                               #
        #                                                                 #
        ###################################################################

        With Cooldown System, player can fire laser "laserFireRate" seconds later.
        for more info : https://docs.unity3d.com/ScriptReference/Time-time.html

        */
        if (Input.GetButtonDown("Fire1") && Time.time > _laserNextFire)
        {
            // Cooldown System.
            // "_laserNextFire"  is internal variable for meausuring "laserFireRate"
            _laserNextFire = Time.time + laserFireRate;

            Debug.Log("Fire!");
            // Instantiate LaserPrefab with offset
            Vector3 laserOffset = new Vector3(0, 0.8f, 0);
            Instantiate(LaserPrefab, transform.position + laserOffset, Quaternion.Euler(0, 0, 0));
        }
    }


}
