using UnityEngine;

public class Solution_W4_BulletPoolObject : MonoBehaviour
{
    private Solution_W4_OPool pool;
    public Solution_W4_OPool Pool { get => pool; set => pool = value; }

    public void Release()
    {
        Debug.Log("[PooledObject] Release");
        pool.ReturnBulletToPool(this);//
    }
}
