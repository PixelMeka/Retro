using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float tempSpeed;
    public float dashSpeed;
    public float rotSpeed;
    public float air = 4.3f;
    public float jump;
    public float verticalVelocity;
    public float gravity;

    public float firstJump;
    public float dashJumpTimer = 0.1f;
    public float jumpTimer;
    public float firstStomp;
    public float dashDuration = 0.2f;
    public float dashTimer;
    public float attackTimer;
    public bool doubleJump = false;
    public bool stomp = false;
    public bool stomping = false;
    public bool height = false;
    public bool dash = false;
    public bool dashed = false;
    public bool dashing = false;
    public bool dashJump = false;
    public bool dashJumped = false;
    public bool dashJumpTimerActive = false;
    public bool dashStopper = false;
    public bool dashStop = false;
    public bool doubleJanim = false;
    public bool attack = false;
    public bool groundedPlayer = true;
    public bool moving;
    public bool camFOVAdd = false;
    public bool camFOVSub = false;
    public bool dashFOVending = false;
    public bool stompLand = false;
    public bool stompLanded = false;
    float defaultFOV = 60f;
    float dashFOV = 65f;
    float stompFOV = 70f;
    public float tempFOV;
    public float tempFOV2;
    public float t = 0.0f;
    public float t2 = 0.0f;

    public bool isCrouching = false;
    public bool isAir = false;
    public bool landed = true;
    bool dance = false;
    public bool levelEnded = false;

    public Camera cam;
    CharacterController controller;
    Animator anim;
    public RaycastHit hit;

    public GameObject fireDashLeft;
    public GameObject fireDashRight;
    public GameObject fireDJumpLeft;
    public GameObject fireDJumpRight;
    public GameObject stompLeft;
    public GameObject stompRight;
    public bool dashParticled = false;
    public GameObject dashParticle;
    public GameObject dashParticleLeft;
    public GameObject dashParticleRight;
    public GameObject dJumpParticle;
    public GameObject dJumpParticleLeft;
    public GameObject dJumpParticleRight;
    public GameObject stompParticle;
    public GameObject stompParticleLeft;
    public GameObject stompParticleRight;
    public GameObject fireParticleStartLeft;
    public GameObject fireParticleStartRight;
    public GameObject fireParticleLeft;
    public GameObject fireParticleRight;
    public GameObject fireAttackParticleLeft;
    public GameObject fireAttackParticleRight;
    public GameObject stompLandParticle;

    public GameObject hitCol;
    public GameObject dashCol;
    public GameObject stompCol;

    public GameObject startShadow;
    public GameObject shadow;
    public bool shadowSet = false;

    public float initialTimer = 0f;
    public float pauseTimer = 0f;
    public bool dontLandAtStart = true;
    public bool dontLandAfterPause = false;

    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        tempFOV = defaultFOV;
        tempFOV2 = defaultFOV;
        tempSpeed = speed;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>(); //Find that component in the player's "children"
    }

    // Update is called once per frame
    void Update()
    {
        //Wait for the initial animation to finish before gaining control of the character
        if (this.GetComponentInChildren<FireOff>().startEnded || anim.GetBehaviour<FlyInEnd>().startAnimEnded)
        {
            if(!shadowSet)
            {
                startShadow.SetActive(false);
                shadow.SetActive(true);
                shadowSet = true;
            }

            //To fix the landing animation playing at the start
            if(dontLandAtStart)
            {
                initialTimer += Time.deltaTime;
                if (initialTimer >= 0.07f)
                {
                    dontLandAtStart = false;
                }
            }

            //To fix the landing animation playing after pausing
            if (dontLandAfterPause)
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= 0.07f)
                {
                    dontLandAfterPause = false;
                    pauseTimer = 0f;
                }
            }

            //Ground Stomp height check
            Ray heightRay = new Ray(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), -Vector3.up);

            if (!Physics.Raycast(heightRay, out hit, 5f))
            {
                height = true;
            }
            if (Physics.Raycast(heightRay, out hit, 5f))
            {
                height = false;
            }

            Vector3 horizontalVel = controller.velocity;
            horizontalVel = new Vector3(controller.velocity.x, 0, controller.velocity.z);

            Vector3 allVel = controller.velocity;
            allVel = new Vector3(controller.velocity.x, controller.velocity.y, controller.velocity.z);

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer)
            {
                isAir = false;
                stomp = false;
                dashJumped = false;
                doubleJanim = false;
                stomping = false;
                firstJump = 0;
                jumpTimer = 0;
                firstStomp = 0.05f;
                air = 4.3f;
            }

            if (groundedPlayer && verticalVelocity < 0)
            {
                // hit ground
                verticalVelocity = -8f;
            }

            // apply gravity always, to let us track down ramps properly
            verticalVelocity -= gravity * Time.deltaTime;

            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            //For slopes
            var ray = new Ray(transform.position, Vector3.down);

            Ray downRay = new Ray(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), -Vector3.up);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.0f))
            {
                var slopeRot = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                var adjustedVel = slopeRot * moveDir;

                if (adjustedVel.y < 0)
                {
                    moveDir = adjustedVel;
                }
            }

            //For ground movement
            if (groundedPlayer)
            {
                controller.Move(moveDir * Time.deltaTime * speed);

                stompLeft.SetActive(false);
                stompRight.SetActive(false);

                fireParticleLeft.GetComponent<ParticleSystem>().Stop();
                fireParticleRight.GetComponent<ParticleSystem>().Stop();

                // scale by speed
                moveDir *= speed;
            }
            //For air movement
            if (!groundedPlayer)
            {
                if (air > 0)
                {
                    air -= Time.deltaTime;
                }
                if (air < 0)
                {
                    air = 0;
                }

                controller.Move(moveDir * Time.deltaTime * air);

                // scale by speed
                moveDir *= air;
            }

            if (moveDir.magnitude > 0.05f && !paused)
            {
                gameObject.transform.forward = moveDir;
            }

            if (groundedPlayer == false && !dontLandAtStart && !dontLandAfterPause)
            {
                isAir = true;

                //Ground Stomp timer
                if (firstStomp > 0)
                {
                    firstStomp -= Time.deltaTime;
                }
                if (firstStomp <= 0 && stomp == false)
                {
                    firstStomp = 0;
                    stomp = true;
                }
            }

            //If the player is moving
            if (horizontalVel != Vector3.zero)
            {
                if (groundedPlayer == true)
                {
                    anim.SetInteger("state", 1);
                    dance = false;
                }
            }

            //If you are not moving, the condition is set to 0 which is the idle animation
            if (allVel == Vector3.zero)
            {
                moving = false;
                if (dance == false)
                {
                    anim.SetInteger("state", 0);
                }
            }

            //Attack
            if (Input.GetKeyDown(KeyCode.X) && attack == false && !paused)
            {
                if (stomping == false && dashing == false)
                {
                    anim.SetTrigger("Attack");
                    attackTimer = 0.6f;
                    attack = true;
                    doubleJanim = false;
                    dance = false;

                    hitCol.SetActive(true);

                    fireParticleStartLeft.GetComponent<ParticleSystem>().Play();
                    fireParticleStartRight.GetComponent<ParticleSystem>().Play();
                    fireAttackParticleLeft.GetComponent<ParticleSystem>().Play();
                    fireAttackParticleRight.GetComponent<ParticleSystem>().Play();
                    fireParticleLeft.GetComponent<ParticleSystem>().Stop();
                    fireParticleRight.GetComponent<ParticleSystem>().Stop();

                    fireDJumpLeft.SetActive(false);
                    fireDJumpRight.SetActive(false);
                }
            }

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            if (attackTimer <= 0 && attack == true)
            {
                attackTimer = 0;
                attack = false;
            }

            //If space is pressed - jump and play the jump animation
            if (Input.GetButtonDown("Jump") && groundedPlayer && !paused)
            {
                if (doubleJump == false)
                {
                    dance = false;

                    if (dashJump)
                    {
                        anim.SetTrigger("DJump");
                        verticalVelocity += Mathf.Sqrt(jump * 4f * gravity);
                        dashJumped = true;

                        dJumpParticle.GetComponent<ParticleSystem>().Play();
                        dJumpParticleLeft.GetComponent<ParticleSystem>().Play();
                        dJumpParticleRight.GetComponent<ParticleSystem>().Play();
                        fireDashLeft.SetActive(false);
                        fireDashRight.SetActive(false);
                        fireDJumpLeft.SetActive(true);
                        fireDJumpRight.SetActive(true);
                    }
                    else
                    {
                        anim.SetTrigger("Jump");
                        // Physics dynamics formula for calculating jump up velocity based on height and gravity
                        verticalVelocity += Mathf.Sqrt(jump * 2 * gravity);
                    }

                    firstJump = 0.1f;
                    jumpTimer = 0.6f;

                    fireAttackParticleLeft.GetComponent<ParticleSystem>().Stop();
                    fireAttackParticleRight.GetComponent<ParticleSystem>().Stop();
                }
            }

            //Timer to fix instant double jump
            if (firstJump > 0)
            {
                firstJump -= Time.deltaTime;
            }
            if (firstJump <= 0 && doubleJump == false)
            {
                firstJump = 0;
                doubleJump = true;
            }

            //Timer to fix instant dash jump
            if (dashJumpTimerActive)
            {
                dashJumpTimer -= Time.deltaTime;
            }
            if (dashJumpTimer <= 0 && dashJump == false)
            {
                dashJumpTimerActive = false;
                dashJumpTimer = 0.1f;
                dashJump = true;
            }

            //Double Jump timer
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
            }
            if (jumpTimer <= 0 && doubleJump == true)
            {
                jumpTimer = 0;
                doubleJump = false;
            }

            //Double Jump
            if (Input.GetButtonDown("Jump") && !groundedPlayer)
            {
                if (doubleJump == true && !dashJumped)
                {
                    if (!dashJump)
                    {
                        fireParticleStartLeft.GetComponent<ParticleSystem>().Play();
                        fireParticleStartRight.GetComponent<ParticleSystem>().Play();
                        fireParticleLeft.GetComponent<ParticleSystem>().Play();
                        fireParticleRight.GetComponent<ParticleSystem>().Play();
                        fireAttackParticleLeft.GetComponent<ParticleSystem>().Stop();
                        fireAttackParticleRight.GetComponent<ParticleSystem>().Stop();

                        doubleJanim = true;
                        air = 6f;
                        verticalVelocity += jump * 3;
                        jumpTimer = 0;
                        doubleJump = false;
                    }
                }
            }

            //Dash
            if (horizontalVel != Vector3.zero && !moving)
            {
                moving = true;
            }

            if (Input.GetKey(KeyCode.LeftShift) && dash == false && !paused || levelEnded == true)
            {
                if (moving && !stomping || levelEnded == true)
                {
                    anim.SetInteger("state", 5);
                    speed = dashSpeed;
                    air = 20f;
                    camFOVAdd = true;

                    dashCol.SetActive(true);

                    if (dashParticled == false)
                    {
                        dashParticle.GetComponent<ParticleSystem>().Play();
                        dashParticleLeft.GetComponent<ParticleSystem>().Play();
                        dashParticleRight.GetComponent<ParticleSystem>().Play();
                        fireDashLeft.SetActive(true);
                        fireDashRight.SetActive(true);
                        dashParticled = true;
                    }

                    fireParticleLeft.GetComponent<ParticleSystem>().Stop();
                    fireParticleRight.GetComponent<ParticleSystem>().Stop();
                    fireAttackParticleLeft.GetComponent<ParticleSystem>().Stop();
                    fireAttackParticleRight.GetComponent<ParticleSystem>().Stop();

                    dashing = true;
                    dashJumpTimerActive = true;
                    doubleJanim = false;
                    dashStopper = true;

                    //Dash max duration timer
                    if (dashDuration > 0)
                    {
                        dashDuration -= Time.deltaTime;
                    }
                    if (dashDuration <= 0 && !levelEnded)
                    {
                        dashDuration = 0.2f;
                        dash = true;
                    }
                }
            }

            //To fix dashing into walls
            if (dashStopper && !moving)
            {
                dashStopper = false;
                dashStop = true;

            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || dash == true || dashStop == true && !levelEnded && !paused)
            {
                if (dashing)
                {
                    dash = true;
                    dashing = false;
                    moving = false;
                    dashJump = false;
                    dashStop = false;
                    dashStopper = false;
                    dashParticled = false;
                    dashJumpTimerActive = false;
                    dashJumpTimer = 0.1f;
                    dashDuration = 0.2f;

                    dashCol.SetActive(false);

                    if(!paused)
                    {
                        fireDashLeft.SetActive(false);
                        fireDashRight.SetActive(false);
                    }

                    if (!dashed)
                    {
                        speed = tempSpeed;
                        air = 4.3f;
                        dashTimer = 1f;
                        dashed = true;
                        camFOVAdd = false;
                        camFOVSub = true;
                        t = 0.0f;
                    }
                }
            }

            //Dash recharge timer
            if (dashTimer > 0 && dash == true)
            {
                dashTimer -= Time.deltaTime;
            }
            if (dashTimer <= 0 && dash == true || levelEnded)
            {
                dashTimer = 0;
                dash = false;
                dashed = false;
            }

            //Dash camera FOV
            if (camFOVAdd && !stomping)
            {
                cam.fieldOfView = Mathf.Lerp(tempFOV, dashFOV, t);
                tempFOV2 = cam.fieldOfView;

                t += 3.5f * 2 * Time.deltaTime;
            }

            if (camFOVSub && !stomping)
            {
                cam.fieldOfView = Mathf.Lerp(tempFOV2, defaultFOV, t);
                tempFOV = cam.fieldOfView;

                t += 1.5f * 2 * Time.deltaTime;

                dashFOVending = true;

                if (t >= 1.2f)
                {
                    camFOVSub = false;
                    dashFOVending = false;
                    tempFOV = defaultFOV;
                    tempFOV2 = defaultFOV;
                    t = 0.0f;
                }
            }

            //Ground Stomp
            if (Input.GetKeyDown(KeyCode.LeftControl) && stomp == true && !paused)
            {
                if (height == true && stomping == false)
                {
                    verticalVelocity -= gravity * 1.5f;
                    stomp = false;
                    stomping = true;
                    stompLand = true;
                    t = 0.0f;

                    fireDJumpLeft.SetActive(false);
                    fireDJumpRight.SetActive(false);
                    stompLeft.SetActive(true);
                    stompRight.SetActive(true);

                    stompParticle.GetComponent<ParticleSystem>().Play();
                    stompParticleLeft.GetComponent<ParticleSystem>().Play();
                    stompParticleRight.GetComponent<ParticleSystem>().Play();

                    fireParticleLeft.GetComponent<ParticleSystem>().Stop();
                    fireParticleRight.GetComponent<ParticleSystem>().Stop();
                    fireAttackParticleLeft.GetComponent<ParticleSystem>().Stop();
                    fireAttackParticleRight.GetComponent<ParticleSystem>().Stop();

                    stompCol.SetActive(true);
                }
            }

            //Ground Stomp camera FOV
            if (stomping)
            {
                cam.fieldOfView = Mathf.Lerp(tempFOV, stompFOV, t);
                tempFOV2 = cam.fieldOfView;

                camFOVSub = false;

                if (dashFOVending || dashing)
                {
                    t += 3f * Time.deltaTime;
                }

                if (!dashFOVending)
                {
                    t += 5f * Time.deltaTime;
                }
            }

            if (stompLand && groundedPlayer)
            {
                t = 0.0f;
                stompLanded = true;
                stompLand = false;
                dashFOVending = false;
            }

            if (stompLanded)
            {
                stompLandParticle.GetComponent<ParticleSystem>().Play();

                stompCol.SetActive(false);

                cam.fieldOfView = Mathf.Lerp(tempFOV2, defaultFOV, t);
                tempFOV = cam.fieldOfView;

                t += 2.6f * 2 * Time.deltaTime;

                if (t >= 1.2f)
                {
                    camFOVSub = false;
                    tempFOV = defaultFOV;
                    tempFOV2 = defaultFOV;
                    t = 0.0f;
                    stompLanded = false;
                }
            }

            //If player is in the air, play the falling animation
            if (isAir == true && !dashing && !paused)
            {
                if (doubleJanim == true && !stomping)
                {
                    anim.SetInteger("state", 4);
                }
                else if (stomping == true)
                {
                    anim.SetInteger("state", 6);
                }
                else
                {
                    anim.SetInteger("state", 2);
                }

                landed = false;
            }

            if (isAir == false && landed == false)
            {
                landed = true;
                anim.SetTrigger("Land");
            }

            //Secret animation :)
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (groundedPlayer && allVel == Vector3.zero)
                {
                    dance = true;
                    anim.SetInteger("state", 3);
                }
            }

            //Pause the game
            if (Input.GetKeyDown(KeyCode.Escape) && !levelEnded)
            {
                dontLandAfterPause = true;
                if (paused)
                {
                    Time.timeScale = 1f;
                    paused = false;
                    fireDashLeft.SetActive(false);
                    fireDashRight.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0f;
                    paused = true;
                }
            }

            if(levelEnded)
            {
                anim.SetInteger("state", 5);
                fireDashLeft.SetActive(true);
                fireDashRight.SetActive(true);
                fireDJumpLeft.SetActive(false);
                fireDJumpRight.SetActive(false);
                stompLeft.SetActive(false);
                stompRight.SetActive(false);
            }

            moveDir.y = verticalVelocity;
            controller.Move(moveDir * Time.deltaTime);
        }
    }    
}


/*float rightleft = Input.GetAxis("Horizontal");   //Right and left, sideways
  float forback = Input.GetAxis("Vertical");      //Forward and backward

  heightJump += -10 * Time.deltaTime;

  Vector3 moveDir = new Vector3(rightleft, 0, forback);

  moveDir.Normalize();

  float magnitude = moveDir.magnitude;
  magnitude = Mathf.Clamp01(magnitude);
  transform.Translate(moveDir * speed * Time.deltaTime, Space.World);

  controller.SimpleMove(moveDir * magnitude * speed);

  Vector3 direction = moveDir * magnitude;
  direction.y = heightJump;
  transform.Translate(direction * Time.deltaTime);
  controller.Move(direction * Time.deltaTime);*/

/* must have been grounded recently to allow jump
if (groundedTimer > 0)
{
    anim.SetTrigger("Jump");
    dance = false;
    // no more until we recontact ground
    groundedTimer = 0;

    // Physics dynamics formula for calculating jump up velocity based on height and gravity
    verticalVelocity += Mathf.Sqrt(jump * 2 * gravity);
}*/