using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkLeftSprites;

    [SerializeField] List<Sprite> runDownSprites; //RUNNING ANIMATION
    [SerializeField] List<Sprite> runUpSprites;
    [SerializeField] List<Sprite> runRightSprites;
    [SerializeField] List<Sprite> runLeftSprites;

    [SerializeField] FacingDirection defaultDirection = FacingDirection.Down;

    //parameters
    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool isMoving { get; set; }

    public bool IsRunning { //RUNNING ANIMATION
        get;
        set;
    }

    //states
    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkRightAnim;
    SpriteAnimator walkLeftAnim;

    SpriteAnimator runDownAnim; //RUNNING ANIMATION
    SpriteAnimator runUpAnim;
    SpriteAnimator runRightAnim;
    SpriteAnimator runLeftAnim;

    SpriteAnimator currentAnim;
    bool wasPreviouslyMoving;

    //refrences
    SpriteRenderer spriteRenderer;

    private void Start() //initializing the sprites to sprite renderer
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDownAnim = new SpriteAnimator(walkDownSprites, spriteRenderer);
        walkUpAnim = new SpriteAnimator(walkUpSprites, spriteRenderer);
        walkRightAnim = new SpriteAnimator(walkRightSprites, spriteRenderer);
        walkLeftAnim = new SpriteAnimator(walkLeftSprites, spriteRenderer);

        runDownAnim = new SpriteAnimator(runDownSprites, spriteRenderer); //RUNNING ANIMATION
        runUpAnim = new SpriteAnimator(runUpSprites, spriteRenderer);
        runRightAnim = new SpriteAnimator(runRightSprites, spriteRenderer);
        runLeftAnim = new SpriteAnimator(runLeftSprites, spriteRenderer);

        SetFacingDirection(defaultDirection);

        currentAnim = walkDownAnim;
    }

    private void Update()
    {
        var prevAnim = currentAnim;
        
            if (IsRunning) {  //RUNNING ANIMATION
                if (MoveX == 1)
                    currentAnim = runRightAnim;
                else if (MoveX == -1)
                    currentAnim = runLeftAnim;
                else if (MoveY == 1)
                    currentAnim = runUpAnim;
                else if (MoveY == -1)
                    currentAnim = runDownAnim;
            } else
            {
                if (MoveX == 1)
                    currentAnim = walkRightAnim;
                else if (MoveX == -1)
                    currentAnim = walkLeftAnim;
                else if (MoveY == 1)
                    currentAnim = walkUpAnim;
                else if (MoveY == -1)
                    currentAnim = walkDownAnim;
            }


        if (currentAnim != prevAnim || isMoving != wasPreviouslyMoving)
        currentAnim.Start();

        if (isMoving)
        currentAnim.HandleUpdate();
        else
        spriteRenderer.sprite = currentAnim.Frames[0];

        wasPreviouslyMoving = isMoving;
    }

    public void SetFacingDirection(FacingDirection dir)
    {
        if (dir == FacingDirection.Right)
            MoveX = 1;
        else if (dir == FacingDirection.Left)
            MoveX = -1;
        else if (dir == FacingDirection.Down)
            MoveX = -1;
        else if (dir == FacingDirection.Up)
            MoveX = 1;
    }
    
    public FacingDirection DefaultDirection {
        get => defaultDirection;
    }

}

public enum FacingDirection{ Up, Down, Left, Right }
