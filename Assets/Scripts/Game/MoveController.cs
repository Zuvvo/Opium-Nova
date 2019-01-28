using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MoveController : MonoBehaviour
{
    public Camera Camera;
    public static bool IsPlayerBlocked = true;
    public ShipObject ShipObject;
    public float PlayerSpeed;

    public bool Move;

    public Text MoveDebug;
    
    private Vector2 mouseVector;

    private Rigidbody2D _rigidBody;
    private float _moveX;
    private Vector3 _playerPos;
    private Vector3 _desitnation;

    private float _borderXminus = -6f;
    private float _borderXplus = 8.1f;
    private float _borderYminus = -3.3f;
    private float _borderYplus = 5.2f;

    public Joystick Joystick;
    public GameObject Ball;
    public float CircleRadius;
    private Vector3 _ballStartPosition;
    private Vector3 _moveVector;
    private bool _clickedInsideCircle = false;

    private MoveMode _moveMode;

    public void SetMoveController(ShipObject shipObject, float playerSpeed)
    {
        ShipObject = shipObject;
        PlayerSpeed = playerSpeed;
        _rigidBody = ShipObject.GetComponent<Rigidbody2D>();
        ChangeMoveMode(3);
    }

    public void JoystickMoveButton()
    {
        Debug.Log(Input.mousePosition);
        Joystick.gameObject.transform.position = Input.mousePosition;
        Joystick.StartPosition = Input.mousePosition;
    }

    private void Start()
    {
        _ballStartPosition = Ball.transform.position;
    }

    private void FixedUpdate()
    {
        if(!IsPlayerBlocked && Input.GetMouseButton(0))
        {
            
            switch (_moveMode)
            {
                case MoveMode.FullSpeedAllCircle:
                    MoveMode1();
                    break;
                case MoveMode.FullSpeedEdgeCircle:
                    MoveMode2();
                    break;
                case MoveMode.GrowingSpeedAllCircle:
                    MoveMode3();
                    break;
            }
             //   JoystickMovePlayer();
           // MoveMode2();
            //  MouseMovePlayer();
        }
        else
        {
            Ball.transform.position = _ballStartPosition; // wywalić to z update
        }

        if (Input.GetMouseButtonUp(0))
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }

    private bool IsInsideMoveRectangle()
    {
        return true;
    }
    private void MoveMode1()
    {
      //  Debug.Log("MoveMode1");
        Vector2 moveVector = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        _rigidBody.velocity = moveVector.normalized * PlayerSpeed;
        _rigidBody.position = new Vector3(Mathf.Clamp(_rigidBody.position.x, _borderXminus, _borderXplus), Mathf.Clamp(_rigidBody.position.y, _borderYminus, _borderYplus), 0);
    }
    private void MoveMode2()
    {
       // Debug.Log("MoveMode2");
        Vector2 moveVector = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        if (Joystick.DistanceFromCenter >= 40)
        {
            _rigidBody.velocity = moveVector.normalized * PlayerSpeed;
        }
        else
        {
            _rigidBody.velocity = Vector2.zero;
        }
        _rigidBody.position = new Vector3(Mathf.Clamp(_rigidBody.position.x, _borderXminus, _borderXplus), Mathf.Clamp(_rigidBody.position.y, _borderYminus, _borderYplus), 0);
    }

    private void MoveMode3()
    {
      //  Debug.Log("MoveMode3");
        Vector2 moveVector = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        _rigidBody.velocity = moveVector.normalized * PlayerSpeed * Joystick.DistanceFromCenter / 70;
        _rigidBody.position = new Vector3(Mathf.Clamp(_rigidBody.position.x, _borderXminus, _borderXplus), Mathf.Clamp(_rigidBody.position.y, _borderYminus, _borderYplus), 0);
      //  Debug.Log(string.Format("move vector: {0}, axisX: {1}, axisY: {2}, distance from center: {3} ", moveVector, CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), Joystick.DistanceFromCenter));
    }

    private void JoystickMovePlayer()
    {
        _moveVector = Input.mousePosition - _ballStartPosition;
        if (_moveVector.magnitude > CircleRadius)
        {
            Ball.transform.position = (_moveVector / (_moveVector.magnitude / CircleRadius)) + _ballStartPosition;
        }
        else
        {
            Ball.transform.position = Input.mousePosition;
        }
        _rigidBody.velocity = _moveVector.normalized * PlayerSpeed;
        _rigidBody.position = new Vector3(Mathf.Clamp(_rigidBody.position.x, _borderXminus, _borderXplus), Mathf.Clamp(_rigidBody.position.y, _borderYminus, _borderYplus),0);
    }

    private void MouseMovePlayer()
    {

        _desitnation = Camera.ScreenToWorldPoint(Input.mousePosition);
        _desitnation.z = 0;
        _rigidBody.position = Vector3.MoveTowards(_rigidBody.position, _desitnation, PlayerSpeed * Time.deltaTime);
    }

    private void KeyboardMovePlayer()
    {
        _playerPos = _rigidBody.position;
        _playerPos.x = Mathf.Clamp(_playerPos.x, -8, 8);
        _rigidBody.position = _playerPos;
        _rigidBody.velocity = new Vector3(_moveX * PlayerSpeed, _rigidBody.velocity.y);
    }

    public void ChangeMoveMode(int mode)
    {
        switch (mode)
        {
            case 1:
                _moveMode = MoveMode.FullSpeedAllCircle;
                MoveDebug.text = "Debug: Pełna prędkość cały czas.";
                break;
            case 2:
                _moveMode = MoveMode.FullSpeedEdgeCircle;
                MoveDebug.text = "Debug: Pełna prędkość tylko na krawędziach";
                break;
            case 3:
                _moveMode = MoveMode.GrowingSpeedAllCircle;
                MoveDebug.text = "Debug: Wzrastająca prędkość - bliżej obwodu to większa prędkość";
                break;
        }
    }
}

public enum MoveMode
{
    FullSpeedAllCircle,
    GrowingSpeedAllCircle,
    FullSpeedEdgeCircle
}
