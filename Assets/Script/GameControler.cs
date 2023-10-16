using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    bool isTouch;
    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;
    }

    RaycastHit raycastHit;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1 && isTouch == false)
        {
            isTouch = true;
            OnTouch(Input.GetTouch(0).position);
        }

        if (Input.touchCount == 0 && isTouch == true)
        {
            isTouch = false;
            if (raycastHit.transform != null && raycastHit.transform.tag == "Tile")
            {
                raycastHit.transform.GetComponent<Tile>().Unselect();
            }
        }
    }

    void OnTouch(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.tag == "Tile")
            {
                raycastHit.transform.GetComponent<Tile>().Select();
                SlotManager.getInstance().addTile(raycastHit.transform.GetComponent<Tile>());
            }
        }

    }
}
