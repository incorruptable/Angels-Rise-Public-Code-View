using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyChainMovement : MonoBehaviour
{
    
    [SerializeField] private float followDistance = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private int childBase = 0;

    [SerializeField] private Transform[] cornerPoints;
    [SerializeField] private bool clockwise = true;

    private float longestLength, length, buffer;

    private int currentCornerIndex = -1;

    private Transform parentSegment;
    private bool isDetached = true;
    private bool hasTail = false;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            if (GetComponent<SpriteRenderer>().bounds.size.x > GetComponent<SpriteRenderer>().bounds.size.y)
                length = GetComponent<SpriteRenderer>().bounds.size.x;
            else length = GetComponent<SpriteRenderer>().bounds.size.y;
            buffer = (length / 2) + 1;

        }

        if (transform.parent != null)
        {
            parentSegment = transform.parent;
            isDetached = false;
        }

        if (transform.childCount > childBase)
            hasTail = true;

        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FollowParent()
    {
        //Follow parents and be rotated towards the next link in the chain. Every x seconds, rotate towards the player. Once facing player, shoot and rotate back. 
        /*Create a turn queue to simulate snake like behavior. In Update(), a check will be run where "If !ParentObject, FollowParent() gets called."
         * ParentObject passes along the turn queue down the line of objects, where if NewTurn isn't already part of TurnQueue, it gets added to the end of the turn queue.
         * If current position = nextTurn, remove that turn from the turn queue, then move to the next turn in the queue.
         */

    }

    private void FourCornersMovement()
    {
        //TODO: Move between the four corners as the new head. Movement is clockwise or counter clockwise.
    }

    private void DetachedMovement()
    {
        //TODO: Bounce between the four corners at random for a random number of times up to 10. Fire and then repeat.
    }

    private int ClosestCorner()
    {
        //This function checks the closest corner on the map.

        //Gives the largest possible distance on initialization for "closest distance" as a float.
        float closestDistance = float.MaxValue;
        int closestIndex = 0;

        //This is the bail out if the enemy can't detect any corners for some reason. The -1 return tells me something is very much wrong.
        if (cornerPoints == null || cornerPoints.Length == 0)
        {
            return -1;
        }

        for (int i = 0; i < cornerPoints.Length; i++)
        {
            //Obviously, loops through all the corners until it finds a the smallest distance to a corner.
            float distance = Vector3.Distance(transform.position, cornerPoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        //Returns it as an index. This'll be used to send the enemy towards it.
        return closestIndex;
    }

    struct Turn
    {
        Vector2 pos;
        Vector2 dir;
    }
}
