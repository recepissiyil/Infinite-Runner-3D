using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* Attached FinishLine object at hierarchy (Only Second Scene) */
public class LeaderboardController : MonoBehaviour
{
    #region Variables
    public Transform finishLine;
    public List<Transform> players;
    #endregion

    #region Singleton
    public static LeaderboardController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Sort Players
    void Update()
    {
        players = players.OrderBy(
    x => finishLine.transform.position.z - x.transform.position.z
   ).ToList();

    }
    #endregion

    #region Colllision Objects
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Rival")
        {
            UIManager.instance.players.RemoveAt(0);
            players.RemoveAt(0);
        }
    } 
    #endregion
}
