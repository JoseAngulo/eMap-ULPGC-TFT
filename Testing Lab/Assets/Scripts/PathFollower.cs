using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;
    public bool animateCharacter = true;

    private void Start()
    {
        if (gameObject.GetComponent<Animator>())
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", animateCharacter);
        }
        
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
   
}
