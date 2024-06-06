using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShitaiAttackScript : MonoBehaviour
{
    public GameObject[] referencePoints1;
    public GameObject[] anchorPoints1;

    public GameObject[] referencePoints2;
    public GameObject[] anchorPoints2;

    public GameObject referenceChoicePath;

    public GameObject player;

    public ParticleSystem leftFlyFire;
    public ParticleSystem rightFlyFire;
    public ParticleSystem beamFire;
    public ParticleSystem headFire;
    public ParticleSystem leftRainFire;
    public ParticleSystem rightRainFire;

    public bool hasArrived = false;

    [HideInInspector]

    public int numAttack;

    private Animator animator;
    private int numPath = 0;
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
    private float timer;
    private float timerAux;

    private Vector3 posx;
    //private bool proof = true;
    private bool hasWalking = false;
    private bool hasPass = true;
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

            //print(time);

            if (numAttack == 0)
            {
                IdleShitai();
            }
            else if (numAttack == 1)
            {
                MagmaBeamAttack();
                angle = 0;
                hasArrived = false;
                up = 0;
                interpolateAmount = 0;
            }
            else if (numAttack == 2)
            {
                MagmaRainAttack();
                angle = 0;
                hasArrived = false;
                up = 0;
                interpolateAmount = 0;
            }
            else if (numAttack == 3)
            {
                FlyAndAttack();
            }
            else if (numAttack == 5) 
            {
                DeathDrown();
            }
        }
    }

    //Ataque 0 (Idle):
    void IdleShitai()
    {
        distanceToPlayer = (player.transform.localPosition - gameObject.transform.localPosition).x - 23.46185f;

        if (gameObject.transform.position.x > player.transform.position.x)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (gameObject.transform.position.x > player.transform.position.x)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

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
            StartCoroutine(NotWalkingShitai());
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

        yield return new WaitForSeconds(3f);

        if (numAttack == 0)
        {
            numAttack = Random.Range(1, 4);

            //numAttack = 1;
        }

        hasWalking = false;
    }

    IEnumerator NotWalkingShitai()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);

        yield return new WaitForSeconds(3f);

        if (numAttack == 0)
        {
            numAttack = Random.Range(1, 4);

            //numAttack = 1;
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

    //Ataque 1 (Rayo de lava):
    void MagmaBeamAttack()
    {
        StartCoroutine(AttackRainShitai());
    }

    IEnumerator AttackRainShitai()
    {
        if (hasPass && timer < 3 && timer >= 0)
        {
            AttackTowardsPlayer(90, 90, 270, 90);

            timer += Time.deltaTime;

            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Upshot", false);
            animator.SetBool("Sideshot", true);
            animator.SetBool("Downshot", false);

            beamFire.Play();
            headFire.Play();
        }

        if (timer > 3)
        {
            hasPass = false;
            timer = -1;
        }

        yield return new WaitForSeconds(3f);

        if (timer == -1 && !hasPass)
        {
            AttackTowardsPlayer(180, -90, 0, -90);

            beamFire.Stop();
            headFire.Stop();

            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Upshot", false);
            animator.SetBool("Sideshot", false);
            animator.SetBool("Downshot", false);
        }

        yield return new WaitForSeconds(2f);

        if (numAttack == 1 && !hasPass)
        {
            numAttack = 0;
            hasPass = true;
            timer = 0;
        }
    }

    void AttackTowardsPlayer(int actualRightAngle, int rightAmountAngle, int actualLeftAngle, int leftAmountAngle)
    {
        if (gameObject.transform.position.x < player.transform.position.x)
        {
            int rightAngle = rightAmountAngle;

            if (gameObject.transform.eulerAngles.y == actualRightAngle || (actualRightAngle > gameObject.transform.eulerAngles.y - 3 && actualRightAngle < gameObject.transform.eulerAngles.y + 3))
            {
                turnRight = true;
            }

            if (rightAmountAngle < 0)
            {
                rightAngle = Mathf.Abs(rightAmountAngle);
                isNegative = true;
            }

            if (turnRight && timerAux < 3 && timerAux >= 0)
            {
                timerAux += Time.deltaTime;
            }

            if (time <= rightAngle && turnRight)
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
            else if (time > rightAngle /*&& gameObject.transform.eulerAngles.y == (actualRightAngle + rightAmountAngle)*/ && timerAux > 3)
            {
                time = 0;
                timerAux = 0;
                turnRight = false;
                if (isNegative)
                {
                    isNegative = false;
                }
               
            }
        }
        else if (gameObject.transform.position.x > player.transform.position.x)
        {
            int leftAngle = leftAmountAngle;

            if (gameObject.transform.eulerAngles.y == actualLeftAngle || (actualLeftAngle > gameObject.transform.eulerAngles.y - 3 && actualLeftAngle < gameObject.transform.eulerAngles.y + 3))
            {
                turnLeft = true;
            }

            if (leftAmountAngle < 0)
            {
                leftAngle = Mathf.Abs(leftAmountAngle);
                isNegative = true;
            }

            if (turnLeft && timerAux < 3 && timerAux >= 0)
            {
                timerAux += Time.deltaTime;
            }

            if (time <= leftAngle && turnLeft)
            {
                if (!isNegative)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, actualLeftAngle + time, 0);
                }
                else if (isNegative)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, actualLeftAngle - time, 0);
                }

                time += Time.deltaTime * 250;
            }
            else if (time > leftAngle && timerAux > 3)
            {
                //print("Go");
                time = 0;
                timerAux = -1;
                turnLeft = false;
                if (isNegative)
                {
                    isNegative = false;
                }
                
            }
        }
    }

    //Ataque 2 (Lluvia de lava):
    void MagmaRainAttack()
    {
        timer += Time.deltaTime;

        if (timer < 3 && timer > 0)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Upshot", true);
            animator.SetBool("Sideshot", false);
            animator.SetBool("Downshot", false);

            leftRainFire.Play();
            rightRainFire.Play();
        }
        else if (timer > 3) 
        {
            timer = -1;
        }

        if (timer > 2) 
        {
            leftRainFire.Stop();
            rightRainFire.Stop();
        }

        if (timer == -1) 
        {
            numAttack = 0;
            timer = 0;
        }
    }

    //Ataque 3 (Vuelo):

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

        if (referenceChoicePath.transform.position.x < gameObject.transform.position.x && numPath == 0)
        {
            numPath = 1;
        } 
        else if (referenceChoicePath.transform.position.x > gameObject.transform.position.x && numPath == 0) 
        {
            numPath = 2;
        }

        if (numPath == 1)
        {
            if (angle == 90 && ((gameObject.transform.localPosition.y + up) - lastVerticalPosition) >= 1 && gameObject.transform.position != referencePoints1[referencePoints1.Length - 1].transform.position)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                hasArrived = true;
                angle = 0;
                leftFlyFire.Play();
                rightFlyFire.Play();
            }

            if (hasArrived)
            {
                interpolateAmount += Time.deltaTime;
                interpolateAmount = Mathf.Clamp01(interpolateAmount);

                if (gameObject.transform.position != referencePoints1[index + 1].transform.position)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, QuadraticLerp(referencePoints1[index].transform.position, anchorPoints1[index].transform.position, referencePoints1[index + 1].transform.position, interpolateAmount), speedToMove);
                }

                if (gameObject.transform.position == referencePoints1[index + 1].transform.position)
                {
                    interpolateAmount = 0;

                    if (gameObject.transform.position == referencePoints1[referencePoints1.Length - 1].transform.position)
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

                        if (numAttack == 3)
                        {
                            numAttack = Random.Range(1, 3);
                            numPath = 0;
                            leftFlyFire.Stop();
                            rightFlyFire.Stop();
                        }

                    }
                    else if (index < referencePoints1.Length)
                    {
                        index += 1;
                    }
                }
            }
        } 
        else if (numPath == 2) 
        {
            if (angle == 90 && ((gameObject.transform.localPosition.y + up) - lastVerticalPosition) >= 1 && gameObject.transform.position != referencePoints2[referencePoints2.Length - 1].transform.position)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                hasArrived = true;
                angle = 0;
                leftFlyFire.Play();
                rightFlyFire.Play();
            }

            if (hasArrived)
            {
                interpolateAmount += Time.deltaTime;
                interpolateAmount = Mathf.Clamp01(interpolateAmount);

                if (gameObject.transform.position != referencePoints2[index + 1].transform.position)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, QuadraticLerp(referencePoints2[index].transform.position, anchorPoints2[index].transform.position, referencePoints2[index + 1].transform.position, interpolateAmount), speedToMove);
                }

                if (gameObject.transform.position == referencePoints2[index + 1].transform.position)
                {
                    interpolateAmount = 0;

                    if (gameObject.transform.position == referencePoints2[referencePoints2.Length - 1].transform.position)
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

                        if (numAttack == 3)
                        {
                            numAttack = Random.Range(1, 3);
                            numPath = 0;
                            leftFlyFire.Stop();
                            rightFlyFire.Stop();
                        }

                    }
                    else if (index < referencePoints2.Length)
                    {
                        index += 1;
                    }
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

    //Muerte:

    void DeathDrown() 
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);
        animator.SetBool("Death", true);

        leftFlyFire.Stop();
        rightFlyFire.Stop();
        beamFire.Stop();
        headFire.Stop();
        leftRainFire.Stop();
        rightRainFire.Stop();
    }
}
