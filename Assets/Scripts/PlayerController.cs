using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Attached Napoléon object at hierarchy (At Both Scenes)*/
public class PlayerController : MonoBehaviour
{
    #region Variables
    private Rigidbody _playerRigid;

    private Animator _playerAnim;

    public float speed;

    private float _currentSpeed;

    private bool _onGround;

    public bool isEffectiveObstacle;

    public bool isFinishLine;

    private Vector3 _startingPosition;

    public static PlayerController instance;
    #endregion

    #region Init
    private void Awake()
    {
        instance = this;
        isEffectiveObstacle = true;
        _playerRigid = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _startingPosition = this.gameObject.transform.position;
        _currentSpeed = speed;
    }
    #endregion

    #region Player Movement
    private void FixedUpdate()
    {
        if (UIManager.instance.timer == 0 && !GameManager.instance.isPaintinginWall && !isFinishLine)
        {
            _playerRigid.velocity = new Vector3(GameManager.instance.swerveAmount, 0f, speed);
            _playerAnim.SetTrigger("Running");
        }
        if (transform.position.y > .09f)
        {
            _playerRigid.AddForce(new Vector3(0f, 0, -1f) * 6, ForceMode.Impulse);
        }
    }
    #endregion

    #region Napoléon Touching Objects
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (isEffectiveObstacle)
            {
                _playerRigid.isKinematic = true;
                _playerAnim.SetBool("Death", true);
                StartCoroutine(ReturnToStartingPoint());
            }

        }
        else if (other.gameObject.tag == "EffectiveObstacle")
        {
            if (isEffectiveObstacle)
            {
                speed = 0;
                _playerAnim.SetBool("Death", true);
                _playerRigid.AddForce(new Vector3(0f, 1f, -1f) * 6, ForceMode.Impulse);
                isEffectiveObstacle = false;
                StartCoroutine(ReturnToStartingPoint());
            }

        }
        else if (other.gameObject.tag == "RotObsBlue")
        {
            if (_onGround)
            {
                _onGround = false;
                StartCoroutine(EffectRotatingObstacle());
            }
        }
        else if (other.gameObject.tag == "Ground")
        {
            _onGround = true;
        }
        else if (other.gameObject.tag == "FinishLine")
        {
            isFinishLine = true;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _playerAnim.SetTrigger("Dance");
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                GameManager.instance.isPaintinginWall = true;
                GameManager.instance.finishObject.SetActive(false);
                UIManager.instance.StartCoroutine(UIManager.instance.DisableAndEnablePaintingAlertText());
            }

        } 
    }

    IEnumerator ReturnToStartingPoint()
    {
        yield return new WaitForSeconds(3.6f);
        isEffectiveObstacle = true;
        _playerRigid.isKinematic = false;
        _playerAnim.SetBool("Death", false);
        transform.position = _startingPosition;
        speed = _currentSpeed;
    }
    IEnumerator EffectRotatingObstacle()
    {
        while (!_onGround)
        {
            _playerRigid.AddForce(new Vector3(.8f, 0f, 0f) * 6, ForceMode.Impulse);
            yield return new WaitForSeconds(.5f);
            if (_onGround)
            {
                break;
            }
        }
    }
    #endregion

}
