﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public enum Type {Spike, Arrow, Platform, Bounce, Wall};
    public Type type;
    public Vector3 dir;
    private Vector3 initPos;
    public bool isActivated;

    public void initTrap()
    {
        initPos = gameObject.transform.position;
        isActivated = false;

        if (gameObject.name.Contains("spike"))
        {
            type = Type.Spike;
        }
        else if (gameObject.name.Contains("arrow"))
        {
            type = Type.Arrow;
        }
        else if (gameObject.name.Contains("platform"))
        {
            type = Type.Platform;
        }
        else if (gameObject.name.Contains("bounce"))
        {
            type = Type.Bounce;
        }
        else if (gameObject.name.Contains("wall"))
        {
            type = Type.Wall;
            gameObject.SetActive(false);
        }
    }

    public void activate()
    {
        //Debug.Log("activate");

        switch (type) {
            case Type.Spike:
                //Debug.Log("I am a spike");
                break;
            case Type.Arrow:
                //Debug.Log("I am a arrow");
                break;
            case Type.Platform:
                //Debug.Log("I am a platform");
                if (!isActivated)
                {
                    isActivated = true;
                    StartCoroutine(locationTransition(transform.position, transform.position + dir));
                }
                break;
            case Type.Wall:
                //Debug.Log("I am a wall");
                break;
            case Type.Bounce:
                gameObject.GetComponent<Animator>().SetTrigger("isSpring");
                break;
        }

        isActivated = true;
        gameObject.SetActive(true);

    }

    public void deactivate()
    {
        //transform.position = initPos;
        isActivated = false;
        //Debug.Log("Deactivate");

        switch (type) {
            case Type.Spike:
                //Debug.Log("Reset spike");
                gameObject.SetActive(false);
                break;
            case Type.Arrow:
                //Debug.Log("Reset arrow");
                gameObject.transform.position = initPos;
                gameObject.SetActive(false);
                break;
            case Type.Platform:
                //Debug.Log("Reset platform");
                gameObject.transform.position = initPos; //new Vector3(2.36f,3.63f,0); //stopgap measure
                gameObject.SetActive(true);
                break;
            case Type.Bounce:
                Debug.Log("Reset bounce");
                //gameObject.SetActive(false);
                break;
            case Type.Wall:
                Debug.Log("Reset wall");
                gameObject.SetActive(false);
                break;
        }

    }

    private void Update()
    {
        if (isActivated)
        {
            switch (type)
            {
                case Type.Spike:
                    // Move the object upward in world space 1 unit/second.
                    //transform.Translate(1 * Vector3.up * Time.deltaTime, Space.World);
                    break;
                case Type.Arrow:
                    transform.Translate(1 * dir * Time.deltaTime, Space.World);
                    break;
            }
        }
    }

    IEnumerator locationTransition(Vector3 startPosition, Vector3 endPosition)
    {
        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.5f;
        while (currentAnimationTime < totalAnimationTime)
        {
            if (!isActivated)
            {
                break;
            }

            currentAnimationTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, currentAnimationTime / totalAnimationTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
