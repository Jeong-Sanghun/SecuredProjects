using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FishState1 : MonoBehaviour
{
    [SpineAnimation("idle")]
    public string idleAnimation;

    [SpineAnimation("move")]
    public string moveAnimation;

    SkeletonAnimation skeletonAnimation;

    public GameObject goTarget1;
    public GameObject goTarget2;
    public GameObject goTarget3;

    public float speed;

    public enum MoveState
    {
        Idle,
        Target1,
        Target2,
        Target3,
    }

    public MoveState moveState;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationName = idleAnimation;
        skeletonAnimation.skeleton.ScaleX = 1f;
    }

    public void GotoTarget1()
    {
        //skeletonAnimation.skeleton.ScaleX = -1f;
        skeletonAnimation.skeleton.ScaleX = 1f;
        moveState = MoveState.Target1;
        speed = 2.5f;
    }

    public void GotoTarget2()
    {
        skeletonAnimation.skeleton.ScaleX = -1f;
        moveState = MoveState.Target2;
        speed = 2.5f;
    }

    public void GotoTarget3()
    {
        skeletonAnimation.skeleton.ScaleX = -1f;
        moveState = MoveState.Target3;
      //  speed = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        switch(moveState)
        {
            case MoveState.Target1:
                //gameObject.transform.position = Vector3.Lerp(
                gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position, goTarget1.transform.position,
                   Time.deltaTime * speed);

                    if (Vector3.Distance(gameObject.transform.position, goTarget1.transform.position) < 1f)
                    {
                        skeletonAnimation.skeleton.ScaleX = 1f;
                        moveState = MoveState.Idle;
                    }
                break;

            case MoveState.Target2:
                gameObject.transform.position = Vector3.MoveTowards(
                 gameObject.transform.position, goTarget2.transform.position,
                 Time.deltaTime * speed);

                if (Vector3.Distance(gameObject.transform.position, goTarget2.transform.position) < 1f)
                {
                    skeletonAnimation.skeleton.ScaleX = 1f;
                    moveState = MoveState.Idle;
                }
                break;

            case MoveState.Target3:
                gameObject.transform.position = Vector3.MoveTowards(
                 gameObject.transform.position, goTarget3.transform.position,
                 Time.deltaTime * speed);

                if (Vector3.Distance(gameObject.transform.position, goTarget3.transform.position) < 1f)
                {
                    //skeletonAnimation.skeleton.ScaleX = 1f;
                    moveState = MoveState.Idle;
                }
                break;

            case MoveState.Idle:

                break;
        }
    }
}

