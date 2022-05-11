using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayController : PlayerParent
{
    [SerializeField]
    SceneManagerParent sceneManager;



    //public StageManager1 stageManager1;

    public enum SceneType
    {
        Type1,
        Type2,
    }

    public SceneType sceneType;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        camYWorldPos = -1;
        switch (sceneType)
        {
            case SceneType.Type1:
                //skeletonAnimation.Skeleton.FlipX = false;
                skeletonAnimation.skeleton.ScaleX = 1f;
                break;

            case SceneType.Type2:
                //skeletonAnimation.Skeleton.FlipX = true;
                skeletonAnimation.skeleton.ScaleX = 1f;
                isPlayPossible = false;
                break;
        }
    }

    public override void SetAnim(AnimState _animState)
    {
        switch (_animState)
        {
            case AnimState.Idle:
                skeletonAnimation.AnimationName = idleAnimation;
                break;

            case AnimState.Walk:
                skeletonAnimation.AnimationName = moveAnimation;
                break;
        }
    }

    public override void ButtonDownLeft()
    {
        if (moveType != MoveType.RightMoveAuto)
        {
            moveType = MoveType.Left;
        }
    }

    public override void ButtonUpLeft()
    {
        if (moveType != MoveType.RightMoveAuto)
        {
            moveType = MoveType.idle;
        }

    }

    public override void ButtonDownRight()
    {
        if (moveType != MoveType.RightMoveAuto)
        {
            moveType = MoveType.Right;
        }

    }

    public override void ButtonUpRight()
    {
        if (moveType != MoveType.RightMoveAuto)
        {
            moveType = MoveType.idle;
        }

    }

    public override void SetCamYWorldPos(float y)
    {
        camYWorldPos = y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayPossible)
        {
            
            switch (moveType)
            {
                case MoveType.Left:
                    skeletonAnimation.AnimationName = moveAnimation;
                    skeletonAnimation.skeleton.ScaleX = -1f;
                    transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                    break;

                case MoveType.Right:
                    skeletonAnimation.AnimationName = moveAnimation;
                    skeletonAnimation.skeleton.ScaleX = 1f;
                    transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                    break;

                case MoveType.idle:
                    skeletonAnimation.AnimationName = idleAnimation;
                    break;

                case MoveType.RightMoveAuto:
                    skeletonAnimation.AnimationName = moveAnimation;
                    skeletonAnimation.skeleton.ScaleX = 1f;
                    transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                    break;
            }

            if (gameObject.transform.position.x < minPosX)
            {
                gameObject.transform.position =
                    new Vector3(minPosX, gameObject.transform.position.y,
                    gameObject.transform.position.z);
            }

            if (gameObject.transform.position.x > maxPosX)
            {
                gameObject.transform.position =
                    new Vector3(maxPosX, gameObject.transform.position.y,
                    gameObject.transform.position.z);
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    RaycastHit hit = new RaycastHit();
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //    if (Physics.Raycast(ray.origin, ray.direction, out hit))
            //    {
            //        if (hit.transform.tag == "play")
            //        {
            //            Debug.Log("zzzzzzzzzzzzzzzz");
            //            //stageManager1.StartFadeOut();

            //        }
            //    }
            //}
        }
    }


    protected override void OnTriggerEnter2D(Collider2D col)
    {
        sceneManager.TriggerEnter(col.gameObject.name);
    }
}
