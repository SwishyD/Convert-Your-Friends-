using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    public float force = 25;
    public float facingForce = 50;
    protected CharacterInput input;
    protected Vector3 currentFacing = Vector3.zero;
    //
    public Rigidbody chestBody;
    protected Vector3 inputDirection = Vector3.zero;
    public CharacterLegs legs;
    public CharacterUpright chestUpright;
    public CharacterMaintainHeight maintainHeight;
    public CharacterMaintainHeight[] otherMaintainHeight;
    public CharacterUpright[] otherUprights;
    public CharacterFaceDirection faceDirection;
    public Rigidbody[] feetBodies;
    public ConstantForce conForce;
    public float maintainHeightStanding = 1;
    public float maintainHeightCrouching = 0.6f;
    //
    //
    protected float jumpCounter = 0;
    public float jumpDelay = 0.4f;
    public float airTimeDelay = 2;
    protected bool jumpAnticipation = false;
    protected bool inAir = false;
    public bool ragdoll;
    public bool grabbed;
    public float jumpForce = 100;
    public float jumpForwardForce = 50;
    public float jumpDownForce = 250;
    protected float facePlantM = 1;
    protected float getUpCounter = 0;

    public NavMeshAgent nav;
    public Transform[] waypoints = new Transform[5];
    Vector3 destination;
    Vector3[] path;
    int currentPathPos;
    public Transform npcHips;
    //public Rigidbody hipsRB;
    float distRemaining;
    float destinationTimer;
    //
    void Start()
    {
        //input = GetComponent<CharacterInput>();
        nav = GetComponent<NavMeshAgent>();
        nav.updatePosition = false;
        nav.updateRotation = false;
        ragdoll = false;
        grabbed = false;
        SetPath();
    }
    //
    void Update()
    {
        //
        
        if (getUpCounter > 0)
        {
            getUpCounter -= Time.deltaTime;
            //
            // *****************  LIFT ARMS OFF OF THE GROUND SLOWLY WHEN GETTING UP ************
            //
            foreach (CharacterMaintainHeight h in otherMaintainHeight)
            {
                h.desiredHeight = Mathf.Lerp(h.desiredHeight, 0.2f, Time.deltaTime * 3);
            }
        }
        if (grabbed) {
        
            CharacterGrabbed();
        }
        else if (ragdoll)
        {
            //***********************************  CROUCHING BEFORE JUMP **********************
            //
            StartRagdoll();
        }
        else if (inAir)
        {
            //***********************************  AIR BORNE **********************
            //
            jumpCounter += Time.deltaTime;
            if (jumpCounter >= airTimeDelay)
            {
                GetUpFromJump();
            }
            //
        }
        else
        {
            //***********************************  STANDING ON GROUND **********************
            /*nav.nextPosition = npcHips.position;
            if (Vector3.Distance(nav.destination, nav.nextPosition) <= 0.1f)
            {
                SetPath();
            }
            
            
                distRemaining = Vector3.Distance(path[currentPathPos], nav.nextPosition);

        */

            /*if (Vector3.Distance(npcHips.position, nav.nextPosition) <= 0.5)
            {
                if (Vector3.Distance(npcHips.position, nav.destination) <= 0.5)
                {
                    SetPath();
                }
                else
                {
                    currentPathPos++;
                    nav.nextPosition = path[currentPathPos];
                }
            }
            */

            destinationTimer += Time.deltaTime;
            if (destinationTimer >= 6)
            {
                SetPath();
            }

            inputDirection = Vector3.zero;

                inputDirection += nav.desiredVelocity;
                inputDirection.Normalize();

                currentFacing = chestBody.transform.forward;
                currentFacing.y = 0;
                currentFacing.Normalize();

                if (!legs.walking)
                {
                    legs.StartWalking();
                }

                
                faceDirection.facingDirection = inputDirection;

            nav.nextPosition = npcHips.transform.position;
            if (nav.remainingDistance <= 0.2)
                SetPath();
            /*
            if (distRemaining <= 0.2)
                {
                    currentPathPos++;
                }
                
                if (path[currentPathPos] != null)
                {
                    nav.nextPosition = path[currentPathPos];
                }
                else
                {
                    nav.nextPosition = nav.destination;
                }
                if (Vector3.Distance(npcHips.position, nav.nextPosition) <= 0.5f)
                {
                    currentPathPos++;
                }
                */


        }
    }

    private void SetPath()
    {
        int nextWaypoint = Random.Range(0, waypoints.Length-1);
        destination = waypoints[nextWaypoint].position;
        nav.SetDestination(destination);
        path = nav.path.corners;
        //Debug.Log(path.Length);
        currentPathPos = 0;
        destinationTimer = 0;
        
    }

    private void GetUpFromJump()
    {
        // ***********************  STAND UP AFTER BEING A RAGDOLL *******
        
        foreach (CharacterMaintainHeight h in otherMaintainHeight)
        {
            h.enabled = true;
            h.desiredHeight = -3; // **** START ARMS ON GROUND AND THEN LERP THIS VALUE TO NORMAL HEIGHT ****
        }
        foreach (CharacterUpright h in otherUprights)
        {
            h.enabled = true;
        }
        //
        getUpCounter = 3; // ** JUST USED TO SETTLE THE ARMS ***
        //
        //
        // **** NEXT: REACTIVATE ALL THE OTHER COMPONENTS THAT MOVE THE LIMBS AND TORSO ****
        //
        inAir = false;
        maintainHeight.enabled = true;
        maintainHeight.desiredHeight = maintainHeightStanding;
        faceDirection.enabled = true;
        legs.enabled = true;
        chestUpright.enabled = true;
        conForce.enabled = true;
        //
        // *** DO A SMALL HOP UPWARD TO START GETTING UP ***
        //
        chestBody.AddForceAtPosition((chestBody.transform.forward * -1 + Vector3.up) * 20, chestBody.transform.TransformPoint(Vector3.up * 0.2f), ForceMode.Impulse);
    }
    private void Jump()
    {
        // ***********************  ACTUALLY JUMP - Launch into the air *******
        //
        //
        // **** DISABLE SOME CONTROLLING COMPONENTS (the height maintaining script on the torso and upright forces on feet) ****
        //
        foreach (CharacterMaintainHeight h in otherMaintainHeight)
        {
            h.enabled = false;
        }
        foreach (CharacterUpright h in otherUprights)
        {
            h.enabled = false;
        }
        // **** LAUNCH INTO AIR HERE :
        //
        chestBody.AddForce(Vector3.up * jumpForce + chestBody.transform.forward * jumpForwardForce, ForceMode.Impulse);
        //
        // **** NEXT: DISABLE ALL THE OTHER CONTROLLING COMPONENTS AND ESSENTIALLY BECOME A RAGDOLL ****
        //
        maintainHeight.enabled = false;
        jumpCounter = 0;
        jumpAnticipation = false;
        inAir = true;
        legs.enabled = false;
        chestUpright.enabled = false;
        faceDirection.enabled = false;
        conForce.enabled = false;
        //
        // ****  SOMETIMES THE FACEPLANT IS GOING TO HAVE MORE FORCE ON IT, BECAUSE RANDOM STRENGTH FACEPLANTS ARE COOL ***
        //
        facePlantM = 0.9f + Random.value * 0.4f;
    }
    //
    private void StartJumpAnticipation()
    {
        // ***********************  CROUCH A BIT UNTIL THE ACTUAL JUMP *******
        legs.StopWalking();
        jumpAnticipation = true;
        maintainHeight.desiredHeight = maintainHeightCrouching;
        jumpCounter = 0;
    }

    private void StartRagdoll()
    {
        Debug.Log("ragdoll");
        maintainHeight.enabled = false;
        jumpCounter = 0;
        jumpAnticipation = false;
        ragdoll = false;
        inAir = true;
        legs.enabled = false;
        chestUpright.enabled = false;
        faceDirection.enabled = false;
        conForce.enabled = false;
    }

    private void CharacterGrabbed()
    {
        Debug.Log("grabbed");
        maintainHeight.enabled = false;
        jumpCounter = 0;
        jumpAnticipation = false;
        ragdoll = false;
        inAir = false;
        legs.enabled = false;
        chestUpright.enabled = false;
        faceDirection.enabled = false;
        conForce.enabled = false;
    }
    //
    void FixedUpdate()
    {
        // *************  I FIND APPLYING FORCES IN FIXED UPDATE TO BE MORE RELIABLE THAN IN UPDATE ****
        //
        if (!inAir)
        {
            // ****  APPLY DRAGS ****
            //
            ApplyStandingAndWalkingDrag();
            //
            if (!jumpAnticipation)
            {
                if (inputDirection != Vector3.zero)
                {
                    // *********************  MOVE CHEST IN THE INPUT DIRECTION *******
                    //
                    // *** (THIS IS ZERO IN THE PROJECT BY DEFAULT, I PREFER HAVING THE LEGS PULL THE BODY FORWARD ***
                    //
                    chestBody.AddForceAtPosition(force * inputDirection * Time.deltaTime, chestBody.transform.TransformDirection(Vector3.forward * 2), ForceMode.Impulse);
                    //                   
                    //                    
                }
            }

        }
        else if (inAir)
        {
            //
            // *******************************************  TOWARDS END OF JUMP, FORCE A FACEPLANT *****
            //
            if (jumpCounter > airTimeDelay * 0.15f && jumpCounter < airTimeDelay * 0.4f)
            {
                chestBody.AddForceAtPosition((chestBody.transform.forward + Vector3.down) * jumpDownForce * facePlantM * Time.deltaTime, chestBody.transform.TransformPoint(Vector3.up * 2), ForceMode.Impulse);
                //
                foreach (Rigidbody f in feetBodies)
                {
                    f.AddForce(Vector3.up * 5 * Time.deltaTime, ForceMode.Impulse);
                }
            }
        }
    }

    private void ApplyStandingAndWalkingDrag()
    {
        // ***********  APPLY DRAGS! **
        //
        // THIS, along with the powerful facing direction forces, ACTUALLY MAKES THE CHARACTERS LESS INTERACTIBLE, BECAUSE THEY CAN'T PUSH EACH OTHER MUCH *****
        // SOFTER FORCES CAN BE BETTER, BUT THOSE NEED MORE TWEEKING, IDEALLY JUST ENOUGH FORCE TO ACHIEVE THE EFFECT WITHOUT BECOMING LOCKED INTO THAT POSITION OR DIRECTION ***
        //
        if (inputDirection == Vector3.zero)
        {
            // ***** WHEN STANDING STILL, APPLY A DRAG BASED ON HOW FAST THE TORSO IS TRAVELLING ***
            //
            Vector3 horizontalVelocity = chestBody.velocity;
            horizontalVelocity.y = 0;
            //
            float speed = horizontalVelocity.magnitude;
            //
            chestBody.velocity *= (1 - Mathf.Clamp(speed * 20f + 10, 0, 50) * Time.fixedDeltaTime);
        }
        else
        {
            // ***** APPLY A POWERFUL DRAG FORCE IF THE TORSO ISN'T TRAVELLING IN THE INPUT DIRECITON, ALLOWS FOR TIGHT TURNS ***
            //
            Vector3 horizontalVelocity = chestBody.velocity;
            horizontalVelocity.y = 0;
            //
            float m = 1 - (1 + Vector3.Dot(horizontalVelocity.normalized, inputDirection)) / 2f;
            chestBody.velocity *= (1 - (m * 30) * Time.fixedDeltaTime);
        }
        //
    }
}