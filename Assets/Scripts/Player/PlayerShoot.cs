using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject bulletEmitter;
    public GameObject bullet;
    public Transform playerCamera;

    public float bulletSpeed;


    void Update()
    {

        //float x = Screen.width / 2f;
        //float y = Screen.height / 2f;

        //ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        if (Input.GetButtonDown("Fire1"))
        {
            //The Bullet instantiation happens here.
            GameObject tempBullet;
            tempBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            //Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody tempRigidBody;
            tempRigidBody = tempBullet.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            tempRigidBody.AddForce(transform.forward * bulletSpeed);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(tempBullet, 10.0f);
        }
    }
}
