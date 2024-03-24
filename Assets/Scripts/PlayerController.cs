using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;
    Animator animator;
    private GameController gameController;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        animator = GetComponent<Animator>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);
        
        if (Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }

        MovePlayerWithAim();

    }

    public void MovePlayerWithAim()
    {
        var lookPosition = rotationTarget - transform.position;
        lookPosition.y = 0f;
        var rotation = Quaternion.LookRotation(lookPosition);

        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.y);

        if(aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        float moveMagnitude = movement.magnitude;
        animator.SetFloat("Speed", moveMagnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
