using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetCameraScript : MonoBehaviour
{
    PlayerMovement playerMovement;
    private Rigidbody2D targetCameraRB;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        targetCameraRB = GetComponent<Rigidbody2D>();

        targetCameraRB.velocity = new Vector3(playerMovement.playerVelocity, 0f, 0f);
    }
}
