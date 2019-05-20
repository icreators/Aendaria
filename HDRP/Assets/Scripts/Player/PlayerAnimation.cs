using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public float inputX;
    public float inputY;

    public float runSpeed;

    public bool isRunning = false;
    public float YrunSpeed = 0;
    public float XrunSpeed = 0;

    public float YSpeed = 0;
    public float XSpeed = 0;
    public float RSpeed = 0;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) InterfaceManager.instance.DrawSword = !InterfaceManager.instance.DrawSword; //#optymalnie
    }

    void FixedUpdate()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");

        runSpeed = Input.GetAxis("Run");

        if (Input.GetButton("Vertical")) YSpeed += inputY / 10;
        else if (YSpeed > 0)
        {
            if (YSpeed > .1f) YSpeed -= .1f;
            else YSpeed = 0;
        }
        else if (YSpeed < 0)
        {
            if (YSpeed < -.1f) YSpeed += .1f;
            else YSpeed = 0;
        }

        YSpeed = Mathf.Clamp(YSpeed, -1, 1);
        YSpeed = (float)Math.Round(YSpeed, 2);



        if (Input.GetButton("Horizontal")) XSpeed += inputX / 10;
        else if (XSpeed > 0)
        {
            //Debug.Log("zmiejszanie predkosci");
            if (XSpeed > .1f) XSpeed -= .1f;
            else XSpeed = 0;
        }
        else if (XSpeed < 0)
        {
            if (XSpeed < -.1f) XSpeed += .1f;
            else XSpeed = 0;
        }

        if (Input.GetButton("Run") && (YSpeed >= .9f || XSpeed >= .9f || XSpeed <= -.9f)) RSpeed += runSpeed / 10;
        else if (RSpeed > 0)
        {
            if (RSpeed > .1f) RSpeed -= .1f;
            else RSpeed = 0;
        }

        RSpeed = Mathf.Clamp(RSpeed, 0, 1);
        RSpeed = (float)Math.Round(RSpeed, 2);


        XSpeed = Mathf.Clamp(XSpeed, -1, 1);
        XSpeed = (float)Math.Round(XSpeed, 2);

        if (YSpeed >= .9f && Input.GetButton("Vertical"))
        {
            //Debug.Log("Bieganie do przodu");
            YSpeed += RSpeed;
        }

        if (XSpeed >= .9f && Input.GetButton("Horizontal"))
        {
            //Debug.Log("Bieganie w prawo");
            XSpeed += RSpeed;
        }

        if (XSpeed <= -.9f && Input.GetButton("Horizontal"))
        {
            //Debug.Log("Bieganie w lewo");
            XSpeed -= RSpeed;
        }

        animator.SetFloat("InputY", YSpeed);
        animator.SetFloat("InputX", XSpeed);

        if (Input.GetButtonDown("Jump") && YSpeed >= .1) { animator.SetTrigger("RunJump"); /*Debug.Log("anim run");*/ }
        else if (Input.GetButtonDown("Jump")){ animator.SetTrigger("Jump"); /*Debug.Log("anim non");*/ }

        if (Input.GetButton("Crouch")) animator.SetBool("IsCrouching", true);
        else animator.SetBool("IsCrouching", false);

        if (InterfaceManager.instance.DrawSword)
        {
            if (!InterfaceManager.instance.isAnyActiveInterface)
            {
                if (Input.GetMouseButtonDown(0)) // Do refaktoryzacji to gowno xd /jk
                {
                    int xd = Random.Range(0, 2);// Dodac funkcje RandomAnimation() i przepisac ten kod dam
                    if (xd == 1) xd = 2;
                    animator.SetInteger("AttackNumber", xd);
                    animator.SetTrigger("BaseAttack");
                }
                if (Input.GetMouseButton(1)) animator.SetBool("ShieldBlock", true);
                else animator.SetBool("ShieldBlock", false);
            }
        }
    }
}
