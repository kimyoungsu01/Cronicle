using UnityEngine;
using static Bullet;

public class GravityGun : MonoBehaviour
{
    public Transform firePoint; // 총구 위치
    public GameObject[] bulletPrefabs; // 총알 프리팹

    Bullet.GravityType gravitytype = Bullet.GravityType.Normal;
    public int currentBulletIndex = 0; // 현재 선택된 총알 인덱스

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Debug.Log("총알 발사");
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gravitytype = Bullet.GravityType.Fast;
            currentBulletIndex = 0;
            Debug.Log("1번키 눌림" + gravitytype);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gravitytype = Bullet.GravityType.Slow;
            currentBulletIndex = 0;
            Debug.Log("2번키 눌림" + gravitytype);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gravitytype = Bullet.GravityType.Stop;
            currentBulletIndex = 0;
            Debug.Log("3번키 눌림" + gravitytype);
        }

        
    }

    public void Shoot()
    {
        Debug.Log(currentBulletIndex);
        GameObject bullet = GunManager.instance.Get(currentBulletIndex, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().gravityType = gravitytype;
    }
}
