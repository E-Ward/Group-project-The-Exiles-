using UnityEngine;

public class Cow_Controller_Tutorial : MonoBehaviour
{
    public float speed = 4;
    public float rotationSpeed = 80;
    float rot = 0f;
    float gravity = 8;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    public Transform playerBody;
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
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("Condition", 1);
                moveDir = new Vector3(-1, 0, 0);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                playerBody.transform.eulerAngles = new Vector3(0, -90, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                anim.SetInteger("Condition", 1);
                moveDir = new Vector3(1, 0, 0);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                playerBody.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("Condition", 1);
                moveDir = new Vector3(0, 0, -1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                playerBody.transform.eulerAngles = new Vector3(0, 180, 0);

            }

            if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("Condition", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                playerBody.transform.eulerAngles = new Vector3(0, 0, 0);

            }

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("Condition", 0);
            moveDir = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetInteger("Condition", 0);
            moveDir = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("Condition", 0);
            moveDir = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("Condition", 0);
            moveDir = new Vector3(0, 0, 0);
        }

        //rot += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
