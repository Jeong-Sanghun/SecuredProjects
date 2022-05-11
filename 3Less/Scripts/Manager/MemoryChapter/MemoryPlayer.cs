using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MemoryPlayer : MonoBehaviour
{
    [SpineAnimation("walk")]
    public string moveAnimation;

    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    float moveSpeed = 3;

    [SerializeField]
    SkeletonAnimation skeletonAnimation;

    //종종 씬매니저에서 쓸 때가 있음
    [SerializeField]
    public GameObject spritePlayerObject;

    public float minPosX;
    public float maxPosX;

    float camYWorldPos;
    [SerializeField]
    Transform camTransform;

    [SerializeField]
    MemorySceneManagerParent sceneManager;
    Vector3 originScale;
    bool isStartLookingRight;


    public enum MoveType
    {
        Left,
        Right,
        RightMoveAuto,
        idle,
    }

    public MoveType moveType;

    //public StageManager1 stageManager1;


    //각 레벨매니저에서 봐줌
    public bool isPlayPossible;


    public enum AnimState
    {
        Idle,
        Walk,
    }

    public AnimState animState;


    // Start is called before the first frame update
    void Start()
    {
        camYWorldPos = -1;
        skeletonAnimation.AnimationName = moveAnimation;
        originScale = transform.localScale;
        if (originScale.x < 0)
        {
            isStartLookingRight = false;
        }
        else
        {
            isStartLookingRight = true;
        }
    }

    public void ButtonDownLeft()
    {
        moveType = MoveType.Left;
        if (isPlayPossible)
        {
            ToggleToSkeleton();
        }

    }

    public void ButtonUpLeft()
    {
        moveType = MoveType.idle;
        if (isPlayPossible)
        {
            ToggleToSprite();
        }


    }

    public void ButtonDownRight()
    {
        moveType = MoveType.Right;
        if (isPlayPossible)
        {
            ToggleToSkeleton();
        }

    }

    public void ButtonUpRight()
    {
        moveType = MoveType.idle;
        if (isPlayPossible)
        {
            ToggleToSprite();
        }
    }

    public void ToggleToSkeleton()
    {
        skeletonAnimation.gameObject.SetActive(true);
        spritePlayerObject.SetActive(false);
    }

    public void ToggleToSprite()
    {
        skeletonAnimation.gameObject.SetActive(false);
        spritePlayerObject.SetActive(true);
    }


    public void SetCamYWorldPos(float y)
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
                    if (isStartLookingRight)
                    {
                        transform.localScale = new Vector3(-originScale.x, originScale.y, originScale.z);
                    }
                    else
                    {
                        transform.localScale = originScale;
                    }
                    
                    transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                    break;

                case MoveType.Right:
                    skeletonAnimation.AnimationName = moveAnimation;
                    if (!isStartLookingRight)
                    {
                        transform.localScale = new Vector3(-originScale.x, originScale.y, originScale.z);
                    }
                    else
                    {
                        transform.localScale = originScale;
                    }
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


    private void OnTriggerEnter2D(Collider2D col)
    {
        sceneManager.TriggerEnter(col.gameObject.name);
    }
}
