using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatToast : MonoBehaviour
{
    public TextMeshProUGUI message;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void EndToast()
    {
        Destroy(gameObject);
    }
}
