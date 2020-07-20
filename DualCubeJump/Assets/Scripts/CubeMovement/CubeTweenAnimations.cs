using UnityEngine;
using DG.Tweening;

public class CubeTweenAnimations
{
    const float MOVE_TIME = 0.2f;
    const float ROTATION_DEGREES = 180f;
    const float JUMP_HEIGHT = 1.5f;
    const float FLOOR_HEIGHT = 0.5f;

    Transform playerTransform;

    Sequence moveLeftSequence;
    Sequence moveRightSequence;
    Tween a, b, c, d, e, f;

    public CubeTweenAnimations(Transform playerTransform)
    {
        this.playerTransform = playerTransform;

        moveLeftSequence = DOTween.Sequence();
        moveRightSequence = DOTween.Sequence();
        moveLeftSequence.SetAutoKill(false);
        moveRightSequence.SetAutoKill(false);
        createMovementSequences();
    }

    public void DoTweensMovement(bool right, bool isGrounded)
    {
        if (right)
        {
            if(isGrounded)
                moveRightSequence.Restart();
            else
            {
                playerTransform.DORotate(new Vector3(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y - ROTATION_DEGREES,
                                         playerTransform.rotation.eulerAngles.z), MOVE_TIME, RotateMode.Fast).Play();
            }
        }
        else
        {
            if(isGrounded)
                moveLeftSequence.Restart();
            else
            {
                playerTransform.DORotate(new Vector3(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y + ROTATION_DEGREES,
                                         playerTransform.rotation.eulerAngles.z), MOVE_TIME, RotateMode.Fast).Play();
            }
        }
    }

    void createMovementSequences()
    {
        
        a = playerTransform.DORotate(new Vector3(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y + ROTATION_DEGREES,
                                     playerTransform.rotation.eulerAngles.z), MOVE_TIME, RotateMode.WorldAxisAdd);
        b = playerTransform.DORotate(new Vector3(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y - ROTATION_DEGREES,
                                     playerTransform.rotation.eulerAngles.z), MOVE_TIME, RotateMode.WorldAxisAdd);

        c = playerTransform.DOMoveY(JUMP_HEIGHT, MOVE_TIME/2f);
        d = playerTransform.DOMoveY(FLOOR_HEIGHT, MOVE_TIME/2f);

        e = playerTransform.DOMoveY(JUMP_HEIGHT, MOVE_TIME/2f);
        f = playerTransform.DOMoveY(FLOOR_HEIGHT, MOVE_TIME/2f);

        
        moveLeftSequence.Insert(0, a);
        moveLeftSequence.Join(c);
        moveLeftSequence.Append(d);

        
        moveRightSequence.Insert(0, b);
        moveRightSequence.Join(e);
        moveRightSequence.Append(f);
    }

}
