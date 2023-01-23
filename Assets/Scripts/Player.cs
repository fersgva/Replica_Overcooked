using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5;
    bool running = false;
    float timeOfRunning = 0.3f;
    CharacterController controller;
    float turnVelocity;
    float turnSmooth = 0.05f;
    float chopDuration = 2f;
    [HideInInspector] public Vector3 moveDirection;
    [SerializeField] ParticleSystem steps;
    [HideInInspector] public Animator anim;

    [HideInInspector] public PlayerControls plControls;
    PlayerInteractions interactionsScript;
    PlayerDetections detectionScript;
    //float gravityFactor = -9.8f;
    Coroutine choppingCoroutine;
    //bool prueba = false;
    private void Awake()
    {
        plControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        interactionsScript = GetComponent<PlayerInteractions>();
        detectionScript = GetComponent<PlayerDetections>();
    }
    private void OnEnable()
    {
        plControls.Enable();
        plControls.Gameplay.Move.performed += ctx =>
        {
            Vector2 inputDirection = ctx.ReadValue<Vector2>();
            moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
            if(choppingCoroutine != null)
            {
                StopCoroutine(choppingCoroutine);
                anim.SetBool("chopping", false);
            }
        };

        //En un principio no haría falta, pero a veces se mueve sólo :S.
        plControls.Gameplay.Move.canceled += ctx => moveDirection = Vector3.zero;
        
        plControls.Gameplay.Dash.performed += ctx =>
        {
            Dash();
        };

        plControls.Gameplay.Interact.performed += ctx =>
        {
            interactionsScript.Interact();
        };

        plControls.Gameplay.Action.performed += ctx =>
        {
            if (detectionScript.closestTable!=null && detectionScript.closestTable.transform.childCount == 3)
            {
                anim.SetBool("chopping", true);
                Ingredient ingredientOnTable = detectionScript.closestTable.transform.GetChild(2).GetComponent<Ingredient>();
                    choppingCoroutine = StartCoroutine(ingredientOnTable.TriggerAction(this, detectionScript, chopDuration));
            }
        };
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovementAndRotation();
        Timers();
        UpdateDetections();
    }

    private void UpdateDetections()
    {
        if (moveDirection.sqrMagnitude != 0) //Sólo si hay movimiento.
        {
            detectionScript.UpdateClosestTable(detectionScript.closestTable);
            detectionScript.UpdateClosestPickable(detectionScript.closestPickable);
        }
    }

    void MovementAndRotation()
    {
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        if (moveDirection.sqrMagnitude != 0) //Sólo si hay movimiento.
        {
            anim.SetBool("walking", true);
            if (!steps.isPlaying)
                steps.Play();

            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angleSmoothed = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0, angleSmoothed, 0);
            //if (rotateTowardsDirCoroutine != null)
            //    StopCoroutine(RotateTowardsDirection());

            //rotateTowardsDirCoroutine = StartCoroutine(RotateTowardsDirection());
        }
        else
        {
            anim.SetBool("walking", false);
            steps.Stop();
        }
    }
    void Timers()
    {
        if(running)
        {
            timeOfRunning -= Time.deltaTime;
            if(timeOfRunning <= 0)
            {
                speed = 5;
                if(timeOfRunning <= -0.3f) //Tiempo extra de espera para no enlazar dashes.
                {
                    timeOfRunning = 0.2f;
                    running = false;

                }
            }
        }
    }

    //IEnumerator RotateTowardsDirection()
    //{
    //    Quaternion initRotation = transform.rotation;
    //    Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
    //    float timer = 0;
    //    float totalTime = 0.01f;
    //    while (timer < totalTime)
    //    {
    //        transform.rotation = Quaternion.Slerp(initRotation, targetRotation, timer / totalTime);
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    rotateTowardsDirCoroutine = null;
    //}
    void Dash()
    {
        if (!running)
        {
            speed = 12;
            running = true;
        }
    }
    //void ApplyGravity()
    //{
    //    Vector3 gravityVector = new Vector3(0, 0, 0);
    //    gravityVector.y += gravityFactor * Time.deltaTime;
    //    controller.Move(gravityVector * Time.deltaTime);
    //    //if(controller.isGrounded)
    //    //{
    //    //    gravityFactor.y = 0;
    //    //}

    //}

    private void OnDisable()
    {
        plControls.Disable();
    }

}
