using System.Collections.Generic;
using UnityEngine;

public class Solution_W4_OPool : MonoBehaviour
{
    [SerializeField]
    private uint initPoolSize;
    [SerializeField]
    private Solution_W4_BulletPoolObject bulletObjectToPool;

    private Stack<Solution_W4_BulletPoolObject> stack;

    private void Start()
    {
        SetupBulletPool();
    }

    private void SetupBulletPool()
    {
        stack = new Stack<Solution_W4_BulletPoolObject>();
        Solution_W4_BulletPoolObject instance = null;

        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(bulletObjectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    public Solution_W4_BulletPoolObject GetBulletPooledObject()
    {
        //Debug.Log("[OP] Getting Bullet" + stack.Count);

        if (stack.Count == 0)
        {
            Solution_W4_BulletPoolObject instance = Instantiate(bulletObjectToPool);
            instance.Pool = this;
            return instance;
        }

        Solution_W4_BulletPoolObject nextInstance = stack.Peek();

        /* if (nextInstance.gameObject.activeInHierarchy) //if the next instance is already active..
         {
             stack.Pop();//pop it out of the stack. BUGFIX for reusing objects that are alraedy active
             nextInstance = stack.Pop(); //pop the next object
         }*/

        while (nextInstance.gameObject.activeInHierarchy) //keep looking until we find an inactive object
        {
            if (!stack.TryPop(out Solution_W4_BulletPoolObject firstResult))
            {
                Debug.LogWarning("[OP] Getting Bullet EMPTY!");
                nextInstance = CreateNewBullet();
                //nextInstance = null;
                break;
            }

            //stack.Pop();//pop it out of the stack. BUGFIX for reusing objects that are alraedy active

            if (stack.TryPop(out Solution_W4_BulletPoolObject secondResult))
                nextInstance = secondResult;

            //nextInstance = stack.TryPop(); //pop the next object
        }


        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    private Solution_W4_BulletPoolObject CreateNewBullet()
    {
        Solution_W4_BulletPoolObject instance = Instantiate(bulletObjectToPool);
        instance.Pool = this;
        return instance;
    }

    public void ReturnBulletToPool(Solution_W4_BulletPoolObject pooledObject)
    {
        Debug.Log("[OP] Returning Bullet");

        pooledObject.gameObject.SetActive(false);

        stack.Push(pooledObject);

        
    }
}
