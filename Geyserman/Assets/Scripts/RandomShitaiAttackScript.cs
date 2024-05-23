using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShitaiAttackScript : MonoBehaviour
{
    public GameObject[] referencePoints;
    public GameObject[] anchorPoints;
    public GameObject player;
    public ParticleSystem leftFlyFire;
    public ParticleSystem rightFlyFire;
    public bool hasArrived = false;

    private Animator animator;
    private int numAttack;
    private int index;
    private float angle;
    private float rotate;
    private float distanceToPlayer;
    private float up;
    [SerializeField]private float speedToMove;
    private float distanceMoved;
    private float initialVerticalPosition;
    private float interpolateAmount;
    private Quaternion prevRot;

    private Vector3 posx;
    private bool proof = true;
    private bool hasWalking = false;
    private bool hasPass = false;
    private bool turnRight = false;
    private bool turnLeft = false;
    private bool isNegative = false;
    private float time;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        initialVerticalPosition = gameObject.transform.localPosition.y;
        numAttack = 0;
    }


    void FixedUpdate()
    {
        if (GetComponentInChildren<ShitaiActivationScript>().isActive)
        {
            /*if (!proof) 
            {
                FlyAndAttack();
                MoveShitaiFly();
            }
            

            if(proof) 
            {
                angle = 0;
                up = 0;
                index = 0;
                IdleShitai();
                proof = false;
            }*/

            if (numAttack == 0) 
            {
                IdleShitai();
            }
            else if (numAttack == 1)
            {
                MagmaBeamAttack();
            }
            else if (numAttack == 2)
            {
                MagmaRainAttack();
            }
            else if (numAttack == 3) 
            {
                FlyAndAttack();
            }
        }
    }

    void IdleShitai()
    {
        distanceToPlayer = (player.transform.localPosition - gameObject.transform.localPosition).x - 23.46185f;

        if (!hasWalking) 
        {
            StartCoroutine(DelayWalkShitai());
        }

        if(hasWalking) 
        {
            RotateTowardsPlayer();
        }

        if (hasWalking && (distanceToPlayer >= 16 || distanceToPlayer <= -16))
        {
            StartCoroutine(WalkingShitai());
        }
        else if (distanceToPlayer < 16 || distanceToPlayer > -16) 
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Upshot", false);
            animator.SetBool("Sideshot", false);
            animator.SetBool("Downshot", false);
        }
        
    }

    IEnumerator DelayWalkShitai()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);

        yield return new WaitForSeconds(2f);

        hasWalking = true;
    }

    IEnumerator WalkingShitai()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);

        float movementFrame = 4 * Time.deltaTime;

        posx = new Vector3(player.transform.position.x, 0, 0);

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posx, movementFrame);

        yield return new WaitForSeconds(5f);

        if (numAttack == 0)
        {
            //numAttack = Random.Range(1, 4);

            numAttack = 1;
        }
        hasWalking = false;
    }

    void RotateTowardsPlayer() 
    {
        if (gameObject.transform.position.x < player.transform.position.x)
        {
            if (gameObject.transform.eulerAngles.y == 270)
            {
                turnRight = true;
            }

            if (time <= 180 && turnRight)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 270 - time, 0);
                time += Time.deltaTime * 100;

            }
            else if (time > 180 && gameObject.transform.eulerAngles.y == 90)
            {
                time = 0;
                turnRight = false;
            }
        }

        if (gameObject.transform.position.x > player.transform.position.x)
        {
            if (gameObject.transform.eulerAngles.y == 90)
            {
                turnLeft = true;
            }

            if (time <= 180 && turnLeft)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 90 + time, 0);
                time += Time.deltaTime * 100;
            }
            else if (time > 180 && gameObject.transform.eulerAngles.y == 270)
            {
                time = 0;
                turnLeft = false;
            }
        }
    }

    void MagmaRainAttack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Upshot", true);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);
    }

    void MagmaBeamAttack()
    {
        StartCoroutine(AttackRainShitai());
    }

    IEnumerator AttackRainShitai()
    {

        if (hasPass) 
        {
            AttackTowardsPlayer(90, 90, 270, 90);

            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Upshot", false);
            animator.SetBool("Sideshot", true);
            animator.SetBool("Downshot", false);
        }

        if (gameObject.transform.eulerAngles.y == 0) 
        {
            hasPass = false;
        }

        yield return new WaitForSeconds(4f);

        if (!hasPass) 
        {
            AttackTowardsPlayer(180, -90, 360, -90);
        }

        yield return new WaitForSeconds(4f);

        if (numAttack == 1) 
        {
            numAttack = 0;
            hasPass = true;
        }
    }

    void AttackTowardsPlayer(int actualRightAngle, int rightAmountAngle, int actualLeftAngle, int leftAmountAngle)
    {
        if (gameObject.transform.position.x < player.transform.position.x)
        {
            if (gameObject.transform.eulerAngles.y == actualRightAngle)
            {
                turnRight = true;
            }

            if (rightAmountAngle < 0)
            {
                rightAmountAngle = Mathf.Abs(rightAmountAngle);
                isNegative = true;
            }

            if (time <= rightAmountAngle && turnRight)
            {
                if (!isNegative)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, actualRightAngle + time, 0);
                }
                else if (isNegative) 
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, actualRightAngle - time, 0);
                }
                
                time += Time.deltaTime * 250;
            }
            else if (time > rightAmountAngle && gameObject.transform.eulerAngles.y == (actualRightAngle + rightAmountAngle))
            {
                time = 0;
                turnRight = false;
                isNegative = false;
            }
        }

        if (gameObject.transform.position.x > player.transform.position.x)
        {
            if (gameObject.transform.eulerAngles.y == actualLeftAngle)
            {
                turnLeft = true;
            }

            if (time <= leftAmountAngle && turnLeft)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, actualLeftAngle + time, 0);
                time += Time.deltaTime * 250;
            } 
            else if (time > leftAmountAngle && gameObject.transform.eulerAngles.y == (actualLeftAngle + leftAmountAngle))
            {
                time = 0;
                turnLeft = false;
            }
        }
    }


    void FlyAndAttack()
    {

        float lastVerticalPosition = gameObject.transform.localPosition.y;

        if (!hasArrived && angle <= 90)
        {
            angle += Time.deltaTime * 250.0f;
            up += Time.deltaTime * 2f;

            if (((gameObject.transform.localPosition.y + up) - lastVerticalPosition) <= 1)
            {
                gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + up);
            }

            gameObject.transform.rotation = Quaternion.Euler(0, -angle + 270, 0);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Upshot", false);
            animator.SetBool("Sideshot", false);
            animator.SetBool("Downshot", true);
        }

        if (angle == 90 && ((gameObject.transform.localPosition.y + up) - lastVerticalPosition) >= 1 && gameObject.transform.position != referencePoints[referencePoints.Length - 1].transform.position)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            hasArrived = true;
            angle = 0;
            //leftFlyFire.Play();
            //rightFlyFire.Play();
        }

        if (hasArrived)
        {
            interpolateAmount += Time.deltaTime;
            interpolateAmount = Mathf.Clamp01(interpolateAmount);

            if (gameObject.transform.position != referencePoints[index + 1].transform.position)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, QuadraticLerp(referencePoints[index].transform.position, anchorPoints[index].transform.position, referencePoints[index + 1].transform.position, interpolateAmount), speedToMove);
            }

            if (gameObject.transform.position == referencePoints[index + 1].transform.position)
            {
                interpolateAmount = 0;

                if (gameObject.transform.position == referencePoints[referencePoints.Length - 1].transform.position)
                {
                    angle += Time.deltaTime * 250.0f;

                    if (angle <= 90)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 180 - angle, 0);
                    }
                    else if (angle == 90)
                    {
                        angle = 0;
                        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                        hasArrived = false;
                    }

                    animator.SetBool("Walk", false);
                    animator.SetBool("Idle", true);
                    animator.SetBool("Upshot", false);
                    animator.SetBool("Sideshot", false);
                    animator.SetBool("Downshot", false);

                }
                else if (index < referencePoints.Length)
                {
                    index += 1;
                }
            }
        }
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, interpolateAmount);
    }
}
