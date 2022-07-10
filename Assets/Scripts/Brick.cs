using UnityEngine;


/*Attached cube objects in Wall object at hierarchy(At Only First Scene)*/
public class Brick : MonoBehaviour
{
    #region Painting Wall
    private void OnMouseOver()
    {
        if (GameManager.instance.isPaintinginWall)
        {
            if (GameManager.instance.isTouching)
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                if (!GameManager.instance.redCubes.Contains(this.gameObject))
                {
                    GameManager.instance.redCubes.Add(this.gameObject);
                }

            }
        }

    } 
    #endregion
}
