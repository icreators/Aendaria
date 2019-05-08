//Player Controller for Incognito Creators by pimpek32

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    private CharacterController playerChar;
    [SerializeField] private float speedWalk;
    [SerializeField] private float forceJump;
    [SerializeField] private float rotateSensivity;
    [SerializeField] private float animSensivity;
    private Transform mesh;
    private Animator anim;
    private Transform cam;
    private Transform camPivot;
    private Transform camParent;
    private Quaternion lastrot;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private LayerMask camRayLayer;
    void Start ()
    {
        playerChar = GetComponent<CharacterController>();
        mesh = GameObject.Find("PlayerMesh").transform;
        anim = mesh.GetComponent<Animator>();
        cam = transform.Find("_Camera/Camera");
        camPivot = transform.Find("_Camera/Pivot");

        camParent = transform.Find("_Camera");
    }
	
	void Update ()
    {
        //Move

        if (playerChar.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speedWalk;

            if (Input.GetButton("Jump")) moveDirection.y = forceJump;
        }

        anim.SetBool("walk", moveDirection.x > 0.1f || moveDirection.x < -0.1f || moveDirection.z > 0.1f || moveDirection.z < -0.1f);
        moveDirection.y = moveDirection.y - (9.81f * Time.deltaTime);
        playerChar.Move(moveDirection * Time.deltaTime);

        lastrot = mesh.rotation;
        mesh.rotation = Quaternion.Lerp(lastrot, transform.rotation, animSensivity);
        mesh.position = transform.position;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSensivity, 0);
        

        //Camera

        RaycastHit camray;
        if (Physics.Raycast(camParent.position, camParent.TransformDirection(-Vector3.forward), out camray, 3, camRayLayer))
            cam.position = camray.point;
        else
            cam.position = Vector3.Lerp(cam.position, camPivot.position, 0.5f);
    }

}
