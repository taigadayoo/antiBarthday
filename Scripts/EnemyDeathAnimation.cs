using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{
    public void OnConpreteAnimation()
    {
        Destroy(this.gameObject);
    }
}
