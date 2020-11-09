using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    CharacterController chara;

	void Start () {
        chara = GetComponent<CharacterController>();
	}
	
	void Update () {
        Move();
        Look();
	}

    void Move()
    {
        float motionX = Input.GetAxis("Horizontal") * Time.deltaTime;
        float motionZ = Input.GetAxis("Vertical") * Time.deltaTime; ;

        Vector3 motion = new Vector3(motionX, 0f, motionZ);

        chara.Move(motion);
    }

    
	void Look()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 lookDir = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
            Vector3 targetRotation = Quaternion.LookRotation(lookDir - transform.position).eulerAngles;
            targetRotation = new Vector3(0, targetRotation.y, 0);

            transform.rotation = Quaternion.Euler(targetRotation);
        }
    }
}
