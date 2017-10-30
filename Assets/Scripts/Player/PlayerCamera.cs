using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    
    Vector2 mouseLook;
    Vector2 smoothV;

    public float Sensitivity = 2.5f;
    public float smoothing = 2.0f;
    
    public bool rotationLock = false;

    public GameObject target;

    private BaseController controller;

    void Start() {
        // View everything but target - thou shalt not see thyself
        // Note: Set appropriate layer for attached parent
        this.GetComponent<Camera>().cullingMask = (int) (0xffffffff ^ (1 << target.layer));
        controller = GetComponentInParent<BaseController>();
        rotationLock = false;
    }

    void Update() {
        if (!rotationLock) {
            //Vector2 camTurn = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 camTurn = new Vector2(controller.getXLook(), controller.getYLook());

            camTurn = Vector2.Scale(camTurn, new Vector2(Sensitivity * smoothing, Sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, camTurn.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, camTurn.y, 1f / smoothing);
            mouseLook += smoothV;

            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            target.transform.rotation = Quaternion.AngleAxis(mouseLook.x, target.transform.up);
        }
    }

    public void setBodyPointVector(Transform other) {
        smoothV.x = 0;
        smoothV.y = 0;
        mouseLook.x = other.rotation.eulerAngles.y;
        mouseLook.y = 0;
        Update();
    }

    public void setRotation()
    {
        rotationLock = !rotationLock;
    }
}