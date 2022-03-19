using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public float h, v;
    float speed = 5;
    float gravityFactor = -9.8f;
    bool running = false;
    float timeOfRunning = 0.3f;
    CharacterController controller;
    float turnVelocity;
    float turnSmooth = 0.05f;
    [HideInInspector] public Vector3 direction;
    [SerializeField] ParticleSystem steps;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        MovementAndRotation();
        Timers();
    }
    void Inputs()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(KeyCode.LeftShift) && !running)
        {
            speed = 12;
            running = true;
        }
    }
    void MovementAndRotation()
    {
        direction = new Vector3(h, 0, v);
        controller.Move(direction.normalized * speed * Time.deltaTime);
        if (direction.sqrMagnitude != 0) //Sólo si hay movimiento.
        {
            anim.SetBool("walking", true);
            float dotProduct = Vector3.Dot(transform.forward, direction);
            if (!steps.isPlaying && dotProduct >= 1f)
                steps.Play();
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angleSmoothed = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0, angleSmoothed, 0);
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
    void ApplyGravity()
    {
        Vector3 gravityVector = new Vector3(0, 0, 0);
        gravityVector.y += gravityFactor * Time.deltaTime;
        controller.Move(gravityVector * Time.deltaTime);
        //if(controller.isGrounded)
        //{
        //    gravityFactor.y = 0;
        //}

    }

}
