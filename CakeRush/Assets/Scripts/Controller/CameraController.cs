using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    RTSController rtsController;
    //Vector3 originPos;
    Transform playerTransform;
    float speed;
    bool isLock;

    void Awake()
    {
        //originPos = new Vector3(0.0f, 17.0f, -15.0f);
        rtsController = GameObject.FindWithTag("GameController").GetComponent<RTSController>();
        isLock = false;
        speed = 20f;
        // playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
        SetPosToSelectedEntity();
        PosLockToUnitPos();
        
    }

    void SetPosToSelectedEntity()
    {
        if(Input.GetKey(KeyCode.T) && rtsController.selectedEntity != null)
        {
            transform.position = new Vector3
            (
                rtsController.selectedEntity.transform.position.x,
                transform.position.y,
                rtsController.selectedEntity.transform.position.z - 11f
            );
        }
    }

    void PosLockToUnitPos()
    {
        if(isLock && rtsController.selectedEntity != null)
        {
            transform.position = new Vector3
            (
                rtsController.selectedEntity.transform.position.x,
                transform.position.y,
                rtsController.selectedEntity.transform.position.z - 11f
            );
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if(isLock)
            {
                isLock = false;
                return;
            }
            else
            {
                isLock = true;
                return;
            }
        }
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y >= Screen.height - 50 ))
        { 
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y <= 50 ))
        {  
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) ||  (Input.mousePosition.x <= 50))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }   
        else if (Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x >= Screen.width - 50))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
