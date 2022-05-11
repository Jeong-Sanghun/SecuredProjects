using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerParent : MonoBehaviour
{
    [SpineAnimation("stand")]
    public string idleAnimation;

    [SpineAnimation("walk")]
    public string moveAnimation;

    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    protected float moveSpeed = 3;

    [SerializeField]
    protected SkeletonAnimation skeletonAnimation;

    public float minPosX;
    public float maxPosX;

    protected float camYWorldPos;
    [SerializeField]
    protected Transform camTransform;

    Vector3 originScale;


    public enum MoveType
    {
        Left,
        Right,
        RightMoveAuto,
        idle,
    }

    public MoveType moveType;

    //public StageManager1 stageManager1;


    //∞¢ ∑π∫ß∏≈¥œ¿˙ø°º≠ ∫¡¡‹
    public bool isPlayPossible;


    public enum AnimState
    {
        Idle,
        Walk,
    }

    public AnimState animState;

    public virtual void SetAnim(AnimState _animState)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        camYWorldPos = -1;
        skeletonAnimation.AnimationName = moveAnimation;
        originScale = transform.localScale;
    }

    public virtual void ButtonDownLeft()
    {

    }

    public virtual void ButtonUpLeft()
    {



    }

    public virtual void ButtonDownRight()
    {
        moveType = MoveType.Right;
        if (isPlayPossible)
        {
            ToggleToSkeleton();
        }

    }

    public virtual void ButtonUpRight()
    {
        moveType = MoveType.idle;
        if (isPlayPossible)
        {
            ToggleToSprite();
        }
    }

    public virtual void ToggleToSkeleton()
    {

    }

    public virtual void ToggleToSprite()
    {

    }


    public virtual void SetCamYWorldPos(float y)
    {
        camYWorldPos = y;
    }

    // Update is called once per frame
   


    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
