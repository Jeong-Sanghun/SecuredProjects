using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FishState : MonoBehaviour
{
    [SpineAnimation("idle")]
    public string idleAnimation;

    [SpineAnimation("move")]
    public string moveAnimation;

    SkeletonAnimation skeletonAnimation;

    public GameObject[] targetObjectArray;

    public float speed;

    public enum MoveState
    {
        Idle,
        Talk,
    }

    public MoveState moveState;

    // Start is called before the first frame update
    void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationName = idleAnimation;
        skeletonAnimation.skeleton.ScaleX = 1f;
    }

    public void SetStartLookingRight(bool lookingRight)
    {
        if (lookingRight)
        {
            skeletonAnimation.skeleton.ScaleX = 1f;

        }
        else
        {
            skeletonAnimation.skeleton.ScaleX = -1f;
        }
    }

    //각 레벨매니저에서 불러옴.
    public void GotoNextTarget(int targetIndex,bool startLookingRight,bool endLookingRight)
    {
        if (targetIndex < targetObjectArray.Length)
        {
            StartCoroutine(FishMoveCoroutine(targetIndex, startLookingRight, endLookingRight));
        }
    }

    IEnumerator FishMoveCoroutine(int targetIndex, bool startLookingRight, bool endLookingRight)
    {
        skeletonAnimation.AnimationName = moveAnimation;
        if (startLookingRight)
        {
            skeletonAnimation.skeleton.ScaleX = 1f;

        }
        else
        {
            skeletonAnimation.skeleton.ScaleX = -1f;
        }

        while (Vector3.Distance(gameObject.transform.position, targetObjectArray[targetIndex].transform.position) > 1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetObjectArray[targetIndex].transform.position, Time.deltaTime * speed);
            yield return null;
        }
        skeletonAnimation.AnimationName = idleAnimation;
        if (endLookingRight)
        {
            skeletonAnimation.skeleton.ScaleX = 1f;

        }
        else
        {
            skeletonAnimation.skeleton.ScaleX = -1f;
        }

    }
}
