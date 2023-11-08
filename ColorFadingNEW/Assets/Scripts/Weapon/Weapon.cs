using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Weapon specifications")] [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField] private GameObject muzzle;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 50;

    private AudioSource audio;
    private AudioClip clip;
    
    private Transform cam;
    private float raycastRange = 2;

    [Header("MuzzleColor")] [SerializeField]
    private Color startColor;

    Color muzzleColor, otherColor;

    bool hitOnce;

    private void Start()
    {
        cam = Camera.main.transform;
        muzzle.GetComponent<Renderer>().material.color = startColor;
        
    }

    /// <summary>
    /// instantiate a bullet 
    /// </summary>
    /// <param name="context"></param>
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SoundManager.Instance.PlaySFX("Weapon_Shoot");
            //audio.PlayOneShot(clip);

            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Renderer>().material = muzzle.GetComponent<Renderer>().material;
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    /// <summary>
    /// set the color of the muzzle to the color of the bucket
    /// </summary>
    /// <param name="context"></param>
    public void PickUpColor(InputAction.CallbackContext context)
    {
        RaycastHit hit;


        int layerMaskColor = 1 << 6;
        int layerMaskWater = 1 << 4;

        int combinedLayerMasks = layerMaskColor | layerMaskWater;

        muzzleColor = muzzle.GetComponent<Renderer>().material.color;

        //set muzzle color
        switch (context.phase)
        {
            case InputActionPhase.Started:

                if (Physics.Raycast(cam.position, cam.forward, out hit, raycastRange, combinedLayerMasks))
                {
                    otherColor = hit.collider.GetComponent<Renderer>().material.color;

                    if (hit.collider.CompareTag("Water"))
                    {
                        ResetColor();
                        return;
                    }

                    if (muzzleColor == startColor)
                    {
                        //get color of the bucket
                        muzzle.GetComponent<Renderer>().material.color = otherColor;
                        SoundManager.Instance.PlaySFX("Color_Pickup");
                    }
                    else if (hitOnce)
                    {
                        return;
                    }
                    else
                    {
                        muzzle.GetComponent<Renderer>().material.color = MixColors(muzzleColor, otherColor);
                        SoundManager.Instance.PlaySFX("Color_Mix");
                    }
                }

                break;
            case InputActionPhase.Canceled:
                break;
        }
    }

    /// <summary>
    /// remove color of the muzzle
    /// </summary>
    public void ResetColor()
    {
        muzzle.GetComponent<Renderer>().material.color = startColor;
        hitOnce = false;
    }

    /// <summary>
    /// mix colors if muzzle already has a color
    /// </summary>
    /// <param name="_muzzleColor"></param>
    /// <param name="_otherColor"></param>
    /// <returns></returns>
    private Color MixColors(Color _muzzleColor, Color _otherColor)
    {
        //red muzzle
        if (_muzzleColor.r == 1 && _otherColor.g == 1)
        {
            _muzzleColor = new Color(1, 1, 0);
        }

        if (_muzzleColor.r == 1 && _otherColor.b == 1)
        {
            _muzzleColor = new Color(1, 0, 1);
        }

        //green muzzle
        if (_muzzleColor.g == 1 && _otherColor.r == 1)
        {
            _muzzleColor = new Color(1, 1, 0);
        }

        if (_muzzleColor.g == 1 && _otherColor.b == 1)
        {
            _muzzleColor = new Color(0, 1, 1);
        }

        //blue muzzle
        if (_muzzleColor.b == 1 && _otherColor.r == 1)
        {
            _muzzleColor = new Color(1, 0, 1);
        }

        if (_muzzleColor.b == 1 && _otherColor.g == 1)
        {
            _muzzleColor = new Color(0, 1, 1);
        }

        hitOnce = true;
        return _muzzleColor;
    }
}