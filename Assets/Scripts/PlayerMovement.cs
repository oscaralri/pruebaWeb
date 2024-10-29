using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // PLAYER VARIABLES
    [SerializeField] public float playerVelocity = 10f;
    [SerializeField] private float playerDownVelocity = 5;
    [SerializeField] Vector2 playerJumpForce = new Vector2(0f, 0.2f);
    private Rigidbody2D playerRB;
    private bool isPlayerGrounded = false;
    private bool isPlayerSliding = false;
    private bool isPlayerHooked = false;

    // OTROS
    public GameObject circleToHook;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // SALTO
        if(Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            isPlayerGrounded = false;
            isPlayerSliding = false;
            playerRB.AddForce(playerJumpForce, ForceMode2D.Impulse);
        }
        // SLIDE
        if(Input.GetKeyDown(KeyCode.DownArrow) && isPlayerGrounded && !isPlayerSliding)
        {
            isPlayerSliding = true;
            gameObject.transform.Rotate(new Vector3(0f, 0f, 90f), Space.World);
            StartCoroutine(FinishSliding());
        }
        // DOWN_VELOCITY
        else if(Input.GetKeyDown(KeyCode.DownArrow) && !isPlayerGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, -playerDownVelocity);
        }


        // HOOK
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isPlayerHooked && circleToHook)
        {
            circleToHook.GetComponent<HookLineRender>().hookDistanceJoint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            circleToHook.GetComponent<HookLineRender>().hookDistanceJoint.enabled = true;

            isPlayerHooked = true;

            // debug
            Debug.Log("COGER Hook");


        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && isPlayerHooked && circleToHook)
        {
            // debug
            Debug.Log("Soltar Hook");
            Debug.Log("circleHook en soltar: " + circleToHook);

            circleToHook.GetComponent<HookLineRender>().hookDistanceJoint.enabled = false;
            circleToHook = null;

        }
    }

    private void FixedUpdate()
    {
        // realmente creo que podría asignarle esto una sola vez en el start pero por si acaso lo hago todo el rato
        //playerRB.velocity = new Vector2(playerVelocity, playerRB.velocity.y); // MOVIMIENTO, DESCOMENTARLO

        // DEBUG
        //Debug.Log("isPlayerHooked: " + isPlayerHooked + "circleToHook: " + circleToHook);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isPlayerGrounded = true;
        }
    }

    private IEnumerator FinishSliding()
    {
        yield return new WaitForSeconds(1f);

        gameObject.transform.Rotate(new Vector3(0f, 0f, -90f), Space.World);
        isPlayerSliding = false;
    }
}
