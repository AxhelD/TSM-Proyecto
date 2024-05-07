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
    private float angle1;
    private float distanceToPlayer;
    private float up;
    private float speedToMove = 1;
    private float distanceMoved;
    private float initialVerticalPosition;
    private float interpolateAmount;
    private Quaternion prevRot;

    private Vector3 posx;
    private bool proof = false;
    private bool hasWalking = true;
    private bool hasPass = false;
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
            if (numAttack == 1)
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
                MoveShitaiFly();
            }

            //print(numAttack);


        }
    }

    void IdleShitai()
    {
        distanceToPlayer = (player.transform.localPosition - gameObject.transform.localPosition).x - 23.46185f;

        if (hasWalking) 
        {
            StartCoroutine(DelayWalkShitai());
        }

        if(!hasWalking) 
        {
            if (gameObject.transform.position.x < player.transform.position.x && (gameObject.transform.eulerAngles.y < 90))
            {
                if (time <= 180)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, -90 + time, 0);
                    time += Time.deltaTime * 60;
                }
                else if (time > 180)
                {
                    time = 0;
                }
            }

            if (gameObject.transform.position.x > player.transform.position.x && (gameObject.transform.eulerAngles.y < 270))
            {
                if (time <= 180)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 90 - time, 0);
                    time += Time.deltaTime * 60;
                }
                else if (time > 180)
                {
                    time = 0;
                }
            }
        }
        

        if (!hasWalking && (distanceToPlayer >= 16 || distanceToPlayer <= -16)) 
        {
            StartCoroutine(WalkingShitai());
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
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", true);
        animator.SetBool("Downshot", false);
    }

    IEnumerator DelayWalkShitai() 
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);

        //print("1");

        yield return new WaitForSeconds(2f);

        hasWalking = false;
    }

    IEnumerator WalkingShitai()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Upshot", false);
        animator.SetBool("Sideshot", false);
        animator.SetBool("Downshot", false);

        float movementFrame = 4 * Time.deltaTime;

        //print(time);

        posx = new Vector3(player.transform.position.x, 0, 0);       

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posx, movementFrame);

       
        //print("2");

        yield return new WaitForSeconds(5f);

        numAttack = 3;
        hasWalking = true;
    }

    void FlyAndAttack()
    {
        angle += Time.deltaTime * 250.0f;
        up += Time.deltaTime * 2f;

        float lastVerticalPosition = gameObject.transform.localPosition.y;

        if (angle <= 90)
        {
            if (((gameObject.transform.localPosition.y + up) - lastVerticalPosition) <= 1)
            {
                gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + up);
            }

            gameObject.transform.rotation = Quaternion.Euler(angle, -90, 0);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Upshot", false);
            animator.SetBool("Sideshot", false);
            animator.SetBool("Downshot", true);
        }

        if (angle == 90 && ((gameObject.transform.localPosition.y + up) - lastVerticalPosition) >= 1)
        {
            prevRot = gameObject.transform.rotation;
            hasArrived = true;
            //leftFlyFire.Play();
            //rightFlyFire.Play();
        }
    }

    void MoveShitaiFly()
    {
        if (hasArrived)
        {
            interpolateAmount += Time.deltaTime;
            interpolateAmount = Mathf.Clamp01(interpolateAmount);

            if (gameObject.transform.position != referencePoints[index + 1].transform.position)
            {
                gameObject.transform.rotation = Quaternion.Euler(prevRot.eulerAngles.x, transform.rotation.eulerAngles.y, prevRot.eulerAngles.z);

                Vector3 direction = QuadraticLerp(referencePoints[index].transform.position, anchorPoints[index].transform.position, referencePoints[index + 1].transform.position, interpolateAmount) - QuadraticLerp(referencePoints[index].transform.position, anchorPoints[index].transform.position, referencePoints[index + 1].transform.position, interpolateAmount - 0.1f);
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, speedToMove * Time.deltaTime);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, QuadraticLerp(referencePoints[index].transform.position, anchorPoints[index].transform.position, referencePoints[index + 1].transform.position, interpolateAmount), speedToMove);
                
            }

            if (gameObject.transform.position == referencePoints[index + 1].transform.position)
            {
                interpolateAmount = 0;

                if (gameObject.transform.position == referencePoints[referencePoints.Length - 1].transform.position)
                {

                    angle1 += Time.deltaTime * 250.0f;

                    if (angle1 <= 90) 
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, angle1, 0);                      
                    }

                    if (angle1 == 90) 
                    {
                        angle1 = 0;
                        hasArrived = false;
                        proof = true;
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
