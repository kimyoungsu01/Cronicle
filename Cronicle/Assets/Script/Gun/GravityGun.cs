using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Transform firePoint; // 총구 위치
    public GameObject bulletPrefab; // 총알 프리팹

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        //GameManager.instance.Gun.transform.position = firePoint.position;
        GameObject bullet = GunManager.instance.Get(0, firePoint.position, firePoint.rotation);
    }
}
