using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //variables
    public Transform player;
    private float playerPoY;
    private float playerPoX;

    //camera's Limitations
    public float maxHigh = 2.5f;
    public float minHigh = -2.9f;

    public float maxDest = 100f;
    public float minDest = -15f;

    //every frame in the game
    private void Update()
    {
        //about all this.. (Wow Noam, You need to find a shorter code...)
        //this is the camera's movement.
        //every frame the camera checks the player's position and moving with him.
        //but there are times that the player jumps too high and if the camera will go with him it will get to end of the bg.
        // so.. what i've done here is specifieng all the things that can happen
        // and what the camera need to do to make the game stay cool. [dest means destination]

        //The most extreme cases
        if (player.position.y >= maxHigh && player.position.x <= minDest)
        {
            //max height & min dest 
            playerPoX = minDest;
            playerPoY = maxHigh;
        }else if(player.position.y <= minHigh && player.position.x <= minDest)
        {
            //min height & min dest 
            playerPoX = minDest;
            playerPoY = minHigh;
        }
        else if (player.position.x <= minDest && player.position.y >= maxHigh)
        {
            //max dest & max height
            playerPoX = maxDest;
            playerPoY = maxHigh;

        }
        else if (player.position.x >= maxDest && player.position.y <= minHigh)
        {
            //max dest & min height
            playerPoX = maxDest;
            playerPoY = minHigh;
        }
        else
        {
            //send the real player position because all good
            playerPoY = player.position.y;
            playerPoX = player.position.x;
        }

        //cases with getting out of the y limitation
        if (player.position.y >= maxHigh)
        {
            playerPoY = maxHigh;
        }
        else if (player.position.y <= minHigh)
        {
            playerPoY = minHigh;
        }

        //cases with getting out of the x limitation
        else if (player.position.x <= minDest)
        {

            playerPoX = maxDest;

        }
        else if (player.position.x >= maxDest)
        {
            playerPoX = maxDest;
        }
        else
        {
            //send the real player position because all good
            playerPoY = player.position.y;
            playerPoX = player.position.x;
        }

        //the most important code in all of it. the CAMERA MOVEMENT! 
        transform.position = new Vector3(playerPoX, playerPoY, transform.position.z);
        //ok it's 5:00 you need to sleep noam. code tomorrow!!!!!!
    }
}
