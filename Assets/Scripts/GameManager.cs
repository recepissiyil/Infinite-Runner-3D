using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Attached Game_Manager object at hierarchy(At Both Scene) */
public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount;

    private float _lastFrameFingerPositionX;

    private float _moveFactorX;

    public bool isTouching;

    public bool isPaintinginWall;

    public List<GameObject> whiteCubes;
    public List<GameObject> redCubes;

    public List<GameObject> obstacles;

    public float swerveAmount;

    public GameObject finishObject;
    #endregion

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Loading Obstacles
    public IEnumerator DelayLoadingObstacles()
    {
        while (true)
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    #endregion
    #region Receiving Finger Values
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
        }
        swerveAmount = Time.fixedDeltaTime * swerveSpeed * _moveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
    }
    #endregion
    #region Receiving Paint Values
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
        }
        if (redCubes.Count > 0)
        {
            float paintingPercentWall = (float)redCubes.Count / (float)whiteCubes.Count * 100;
            UIManager.instance.DisplayPaintingWallPercent(paintingPercentWall);
        }

    } 
    #endregion
}
