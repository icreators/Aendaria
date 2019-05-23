using System.Collections;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public enum Type {Player, Enemy};

    public Type type;

    private bool checking = false;

    public void Check()
    {
        //StartCoroutine(Checking());
        Debug.Log(type);
    }

    private IEnumerator Checking()
    {
        checking = true;
        yield return new WaitForSeconds(0.1f);
        checking = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (checking)
        {
            if (type == Type.Player) collision.gameObject.GetComponent<EnemyStats>().TakeDamage(10);
            if (type == Type.Enemy) collision.gameObject.GetComponent<PlayerStats>().TakeDamage(10);

           // if (collision.transform.gameObject.tag == "Player" && type == Type.Enemy) collision.gameObject.GetComponent<PlayerStats>().TakeDamage(10);
        }
    
        checking = false;
    }   
}
