using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
[Serializable]
public class ScoreEvent : UnityEvent<Player> { }
public class ScoreTrigger : MonoBehaviour
{
    [SerializeField]
    private ScoreEvent onScored;

    private float myHeight;

    private List<Collider> validColliders = new List<Collider>();

    private void Awake()
    {
        myHeight = transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position.y > myHeight) 
        {
            validColliders.Add(other);
        }
    }
    private void OnTriggerExit(Collider other) //To make sure a ball has corretly entered verify that it has an initial height higher than the trigger and a final height lower than the trigger
    {
        if (validColliders.Contains(other))
        {
            validColliders.Remove(other);

            if (other.transform.position.y < myHeight) 
            {
                Player player = other.transform.root.GetComponent<Player>();
                if (player)
                {
                    player.OnScoreTrigger();
                    onScored.Invoke(player);
                }
            }
        }
    }
}