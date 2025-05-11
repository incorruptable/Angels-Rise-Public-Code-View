using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBackground : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = 0.5f;

    Material m_Material;
    Vector2 offSet;
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        offSet = new Vector2(scrollSpeed,0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_Material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
