using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Updates DataCtrl so it provides the most recent data.
/// </summary>
public class RefreshData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataCtrl.instance.RefreshData();
    }

}
