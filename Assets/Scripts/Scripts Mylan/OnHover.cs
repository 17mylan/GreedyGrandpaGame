using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public float buttonSize, normalButtonSize;
    public void PointerEnter()
    {
        transform.localScale = new Vector2(buttonSize, buttonSize);
    }
    public void PointerExit()
    {
        transform.localScale = new Vector2(normalButtonSize, normalButtonSize);
    }
}
