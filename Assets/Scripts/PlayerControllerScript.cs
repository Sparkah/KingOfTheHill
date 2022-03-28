using System.Collections;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private WorldGeneratorScript worldGeneratorScript;
    private int jumpTimer = 20;
    private bool canJump = true;
    private DynamicJoystick dynamicJoystick;
    private float joystickHorizontal;
    private int jumpLeftRightLimit = 0;

    void Start()
    {
        worldGeneratorScript = FindObjectOfType<WorldGeneratorScript>();
        dynamicJoystick = FindObjectOfType<DynamicJoystick>();
    }

    void Update()
    {
        if (Input.touchCount>0 && jumpTimer == 20)
        {
            Touch touch = Input.GetTouch(0);
            if(joystickHorizontal >= 0.5f && touch.phase == TouchPhase.Ended && jumpLeftRightLimit<4)
            {
                ++jumpLeftRightLimit;
                    canJump = true;
                    StartCoroutine(Strife(1));
                    return;
            }
            if (joystickHorizontal <= -0.5f && touch.phase == TouchPhase.Ended && jumpLeftRightLimit > -4)
            {
                --jumpLeftRightLimit;
                    canJump = true;
                    StartCoroutine(Strife(-1));
                    return;
            }
            if(touch.phase == TouchPhase.Ended && joystickHorizontal>-0.5f && joystickHorizontal<0.5f)
            {
                canJump = true;
                StartCoroutine(JumpUp());
            }
            joystickHorizontal = dynamicJoystick.Horizontal;
        }
    }

    //Перемещение влево-вправо
    IEnumerator Strife(int dir)
    {
        if (jumpTimer == 0)
        {
            CanStrifeAgain();
            canJump = false;
        }

        //Тут можно менять настройки как прыгает круг вправо-влево
        if (jumpTimer > 10 && canJump == true)
        {
            --jumpTimer;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.position += new Vector3(0.125f * dir/2, 0.1f/2, 0);
            StartCoroutine(Strife(dir));
        }

        if (jumpTimer > 0 && jumpTimer <= 10 && canJump == true)
        {
            --jumpTimer;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.position += new Vector3(0.075f * dir/2, -0.1f/2, 0);
            StartCoroutine(Strife(dir));
        }
    }

    private void CanStrifeAgain()
    {
        jumpTimer = 20;
    }

    //Прыжек вверх
    IEnumerator JumpUp()
    {
        if (jumpTimer == 0)
        {
            CanJumpAgain();
            canJump = false;
        }

        //Тут можно менять настройки как прыгает круг вверх
        if (jumpTimer > 10 && canJump ==true)
        {
            --jumpTimer;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.position += new Vector3(0, 0.125f, 0.025f);
            StartCoroutine(JumpUp());
        }
        if (jumpTimer > 0 && jumpTimer <= 10 && canJump == true)
        {
            --jumpTimer;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.position += new Vector3(0, -0.025f, 0.075f);
            StartCoroutine(JumpUp());
        }
    }

    private void CanJumpAgain()
    {
        jumpTimer = 20;
        worldGeneratorScript.MoveBlocks();
    }
}