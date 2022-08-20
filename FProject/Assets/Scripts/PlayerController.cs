using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    float playerMovement;
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
        if (!anim)
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (!isGrounded)
            anim.SetBool("isGrounded", false);
        else
            anim.SetBool("isGrounded", true);
    }

    private void FixedUpdate()
    {
        playerMovement = Input.GetAxis("Horizontal");

        if (playerMovement != 0)
        {
            // player will move to the left
            if (playerMovement < 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                transform.Translate(playerMovement * Time.deltaTime * playerSpeed, 0, 0);
                anim.SetBool("isRunning", true);
            }

            // player will move to the right
            if (playerMovement > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                transform.Translate(-playerMovement * Time.deltaTime * playerSpeed, 0, 0);
                anim.SetBool("isRunning", true);
            }
        }
        else
        {
            // player is standing still
            anim.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}
