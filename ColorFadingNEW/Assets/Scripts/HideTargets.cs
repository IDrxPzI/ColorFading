using UnityEngine;

public class HideTargets : MonoBehaviour
{
    [SerializeField] private Transform targets;

    /// <summary>
    /// disable Targets so player cant shoot them 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < targets.childCount; i++)
            {
                targets.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    
    /// <summary>
    /// enable Targets so player can shoot them 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < targets.childCount; i++)
            {
                targets.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}