using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float distance;
    public float distanceMin;

    public enum MoveState
    {
        Idle,
        Move,
    }

    public MoveState moveState;

    // Update is called once per frame
    void Update()
    {

        switch(moveState)
        {
            case MoveState.Idle:
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) > distance)
                {
                    moveState = MoveState.Move;
                }
                break;

            case MoveState.Move:
                gameObject.transform.position = Vector3.Lerp(
         gameObject.transform.position, new Vector3(target.transform.position.x,
         gameObject.transform.position.y, gameObject.transform.position.z),
         Time.deltaTime * speed);

                if (Vector3.Distance(gameObject.transform.position, target.transform.position) < distanceMin)
                {
                    moveState = MoveState.Idle;
                }
                else
                {
                   
                }

                break;
        }

    }
}
