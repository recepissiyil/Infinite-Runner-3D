using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Attached objects in Rivals at hierarchy (Only Second Scene)*/
public class RivalController : MonoBehaviour
{

    #region Variables
    private NavMeshAgent _agent;

    public Transform finishLine;

    private Rigidbody _rivalRigid;

    public bool isEffectiveObstacle;


    private bool _onGround;

    private Animator _rivalAnim;

    private Vector3 _startingPosition;

    private float _startingSpeed;

    private int _countHittingLine;

    public List<Transform> followingLines;
    #endregion

    #region Init
    private void Awake()
    {
        isEffectiveObstacle = true;

        _agent = GetComponent<NavMeshAgent>();

        _rivalAnim = GetComponent<Animator>();

        _rivalRigid = GetComponent<Rigidbody>();

        _startingPosition = transform.position;
        _startingSpeed = _agent.speed;
    }
    #endregion

    #region Rival Movement
    private void FixedUpdate()
    {
        if (_countHittingLine <= followingLines.Count - 1)
        {
            if (UIManager.instance.timer == 0)
            {
                _agent.SetDestination(followingLines[_countHittingLine].transform.position);
                _rivalAnim.SetTrigger("Running");
            }
        }

    }
    #endregion

    #region Rival Touching Objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FollowingLines")
        {
            if (_countHittingLine <= followingLines.Count - 1)
            {
                _countHittingLine++;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (isEffectiveObstacle)
            {
                _agent.speed = 0;
                _rivalRigid.isKinematic = true;
                _rivalAnim.SetBool("Death", true);
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
        else if (other.gameObject.tag == "EffectiveObstacle")
        {
            if (isEffectiveObstacle)
            {
                _agent.speed = 0;
                _rivalAnim.SetBool("Death", true);
                _rivalRigid.AddForce(new Vector3(0f, 1f, -1f) * 6, ForceMode.Impulse);
                isEffectiveObstacle = false;
                StartCoroutine(ReturnToStartingPoint());
            }

        }
        else if (other.gameObject.tag == "Ground")
        {
            _onGround = true;
        }
        else if (other.gameObject.tag == "FinishLine")
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _rivalAnim.SetTrigger("Dance");
            _agent.speed = 0;
        }

    }
    IEnumerator ReturnToStartingPoint()
    {
        yield return new WaitForSeconds(3.6f);
        _rivalRigid.isKinematic = false;
        _rivalAnim.SetBool("Death", false);
        _countHittingLine = 0;
        _agent.speed = _startingSpeed;
        _agent.Warp(_startingPosition);
    }
    IEnumerator EffectRotatingObstacle()
    {
        while (!_onGround)
        {
            yield return new WaitForSeconds(.5f);
            var randomValue = Random.Range(.01f, .05f);
            _rivalRigid.AddForce(new Vector3(randomValue, 0f, 0f) * 6, ForceMode.Impulse);
            if (_onGround)
            {
                break;
            }
        }
    } 
    #endregion
}
