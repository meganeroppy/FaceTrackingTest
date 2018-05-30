using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float spinSpeed = 1.0f;

	enum Movement
	{
		ReferObject,
		MousePosition,
		Input,
	}

	[SerializeField]
	Movement myMovement = Movement.MousePosition;

	[SerializeField]
	Transform referredObject;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;
	Vector2 input = Vector2.zero;

	[SerializeField]
	bool log = false;

    // Use this for initialization
    void Start()
    {
        // Canera get Start Position from Player
        nowPos = transform.position;

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Debug.Log("player didn't setting. Auto search 'Player' tag.");
        }

        mouse.y = 0.5f; // start mouse y pos ,0.5f is half
		input = Vector2.one * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
		if( Input.GetKeyDown( KeyCode.R ) )
		{
			mouse = Vector2.zero;
			input = Vector2.zero;
		}

		float x = 0, y = 0;

		// 入力値を使用
		if( myMovement == Movement.Input )
		{
			x = input.x += Input.GetAxis("Horizontal") * Time.deltaTime * spinSpeed;
			y = input.y += Input.GetAxis("Vertical") * Time.deltaTime * spinSpeed;
		}

		// マウスの位置を使用
		else if( myMovement == Movement.MousePosition )
		{
	        // Get MouseMove
	        mouse += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;

			x = mouse.x;
			y = mouse.y;
		}

		// オブジェクトの位置を参照
		else
		{
			x = referredObject.position.x;
			y = referredObject.position.y;
		}

		// Clamp Y move
		y = Mathf.Clamp(y, -0.3f + 0.5f, 0.3f + 0.5f);

		if( log )
			Debug.Log( "( " +x.ToString() + ", "  + y.ToString() + " )" );

        // sphere coordinates
        pos.x = Mathf.Sin(y * Mathf.PI) * Mathf.Cos(x * Mathf.PI);
        pos.y = Mathf.Cos(y * Mathf.PI);
        pos.z = Mathf.Sin(y * Mathf.PI) * Mathf.Sin(x * Mathf.PI);

        // r and upper
        pos *= nowPos.z;

        pos.y += nowPos.y;
        pos.x += nowPos.x; // if u need a formula,pls remove comment tag.

		transform.position = pos + target.position;
	//	transform.position = pos;
        transform.LookAt(target.position);
    }
}