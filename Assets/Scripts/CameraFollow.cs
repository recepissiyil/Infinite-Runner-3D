using UnityEngine;

/*Attached MainCamera object at hierarchy(At Both Scenes)*/
public class CameraFollow : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    #endregion

    #region Camera following target
    void LateUpdate()
    {
        if (PlayerController.instance.isFinishLine)
        {
            transform.position = new Vector3(2.2f, 0.56f, 21.54f);
        }
        else
        {
            transform.position = target.transform.position + offset;
        }
    } 
    #endregion
}
