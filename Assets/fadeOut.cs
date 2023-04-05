using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOut : MonoBehaviour
{
    void Update()
    {
        GetComponent<RawImage>().color -= new Color(0, 0, 0, 1f * Time.deltaTime);
    }
}
