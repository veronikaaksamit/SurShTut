using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() //Physics update ...
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Animating(h, v);
        Move(h, v);
        Turning();
    }

    public void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime; //time between updates

        playerRigidbody.MovePosition(transform.position + movement);
    }

    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f);
        //if (walking)
        //{
        //    anim.SetBool("IsWalking", true);
        //}
        anim.SetBool("IsWalking", walking);
    }
}
