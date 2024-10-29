using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLineRender : MonoBehaviour
{
    private LineRenderer hookLineRenderer;
    private GameObject playerGO;
    private GameObject circleHookGO;
    private bool isPlayerInsideHookZone = false;
    public DistanceJoint2D hookDistanceJoint;

    void Start()
    {
        hookLineRenderer = GetComponent<LineRenderer>();
        hookDistanceJoint = GetComponent<DistanceJoint2D>();

        playerGO = GameObject.FindWithTag("Player");

        // esto seguramente lo tendré que cambiar cuando vayan apareciendo más circleHooks. Seguramente puedo poner un collider desde el jugador que le
        // pase a este script el circleHook con el que debe cogerse
        circleHookGO = gameObject;
    }

    private void Update()
    {
        if(isPlayerInsideHookZone == true)
        {
            hookDistanceJoint.connectedBody = playerGO.GetComponent<Rigidbody2D>();
            hookDistanceJoint.enabled = true;

            hookLineRenderer.positionCount = 2;
            hookLineRenderer.SetPosition(0, playerGO.transform.position);
            hookLineRenderer.SetPosition(1, circleHookGO.transform.position);

        }
    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //isPlayerInsideHookZone = true;

            playerGO.GetComponent<PlayerMovement>().circleToHook = this.gameObject; // no hace falta el this pero me gusta ponerlo


            //DEBUG
            Debug.Log("circleToHook: " + playerGO.GetComponent<PlayerMovement>().circleToHook);
        }
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //isPlayerInsideHookZone = false;

            playerGO.GetComponent<PlayerMovement>().circleToHook = null; // no hace falta el this pero me gusta ponerlo

            //DEBUG
            Debug.Log("circleToHook: " + playerGO.GetComponent<PlayerMovement>().circleToHook);

        }
    }
    */
    
    
}
