using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector2 move, mouseLook, JoystickLook;
    private Vector2 moveInput = Vector2.zero;
    Vector3 rotationTarget;
    [SerializeField] Transform cmCamera;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] GameObject respawnScrn;
    [SerializeField] Animator animator;
    public float shootingSpeed = 20.0f;
    private bool isShooting = false;
    public bool isPc;
    public float health = 5;
    float recentHorizontal;
    float recentVertical;


    public static PlayerController Instance { get; private set; }

    void Update()
    {
        recentVertical = Mathf.MoveTowards(recentVertical, move.y, Time.deltaTime / 1.0f);
        recentHorizontal = Mathf.MoveTowards(recentHorizontal, move.x, Time.deltaTime / 1.0f);

        var direction = transform.right * recentHorizontal + transform.forward * recentVertical;
        float yRotation = Mathf.DeltaAngle(0, transform.rotation.eulerAngles.y);

        if (yRotation >= 55 && yRotation <= 135)
        {
            
            direction.z = -direction.z;
        }
        else if(yRotation <= -55 && yRotation >= -135)
        {
            direction.z *= -1;
        }
        
        Debug.Log(direction);
        
        if (move != Vector2.zero)
        {
            
            animator.Play("Moving");
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.z);

        }
        else
        {
            animator.Play("metarig_Idle");
            recentVertical = 0;
            recentHorizontal = 0;
        }
        
        
        if (isPc)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);
            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }
            MovePlayerWithAim();
        }
        else
        {
            if (JoystickLook.x == 0 && JoystickLook.y == 0)
            {
                MovePlayer();
            }
            else
            {
                MovePlayerWithAim();
            }
        }
        if (health <= 0)
        {
            respawnScrn.SetActive(true);
            Destroy(gameObject);
            Debug.Log("Player died");
        }
        hp.text = "HP:" + health.ToString();

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        //moveInput = value.Get<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        JoystickLook = context.ReadValue<Vector2>();
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void MovePlayerWithAim()
    {
        if (isPc)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }

        }
        else
        {
            Vector3 aimDirection = new Vector3(JoystickLook.x, 0f, JoystickLook.y);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }
}
