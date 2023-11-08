using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifeTime = 3;

    [Header("Splatter")] [SerializeField] private GameObject splatterPrefab;
    private Transform splatterparent; // object to set splatters as parent
    private Material bulletColor;


    void Awake()
    {
        Destroy(gameObject, lifeTime);

        splatterparent = GameObject.Find("SplatterParent").transform;
    }

    /// <summary>
    /// instantiate splatter on bullet hit
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        int splatterAmount = splatterparent.childCount;
        bulletColor = gameObject.GetComponent<Renderer>().material;

        //get parent to set as parent for splatter
        if (other.gameObject.CompareTag("Wall") | other.gameObject.CompareTag("EndBarrier"))
        {
            SoundManager.Instance.PlaySFX("Bullet_Splatter");

            ContactPoint contact = other.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            Vector3 position = contact.point;

            splatterPrefab.GetComponent<Renderer>().material = bulletColor;
            Instantiate(splatterPrefab, position, rotation, SetParent(other.gameObject));
        }

        //destroy splatter after {amount}
        if (splatterAmount >= 50)
        {
            var firstSplatter = splatterparent.GetChild(0);
            Destroy(firstSplatter.gameObject);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Set the parent which the splatter should be added to
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    private Transform SetParent(GameObject parent)
    {
        Transform newParent;
        if (parent.CompareTag("EndBarrier"))
        {
            newParent = parent.gameObject.transform.parent;
        }
        else
        {
            newParent = splatterparent;
        }

        return newParent;
    }
}