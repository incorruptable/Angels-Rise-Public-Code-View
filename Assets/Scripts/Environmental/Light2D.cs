using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>
/// Beginning steps of writing a lighting system to properly interact with Normal Maps
/// </summary>
public class AngelLighting : MonoBehaviour
{
    public float intensity = 1.0f;
    public float radius = 5.0f;
    public Color lightColor = Color.white;
    public float falloffExponent = 1.0f;
    public float bias = 0.1f;

    double[][] vector1 = { };
    double[][] vector2 = { };

    void OnDrawGizmos()
    {
        Gizmos.color = lightColor * 0.5f;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    Vector3 LightDirectionVector(GameObject targetObject)
    {
        Vector3 lightPosition = transform.position;
        Vector3 objectPosition = targetObject.transform.position;

        Vector3 lightToObjectDir = objectPosition - lightPosition;
        return lightToObjectDir.normalized;
    }

    void CalculateLighting(GameObject targetObject, Vector2 uv)
    {
        Vector3 lightToObjectDir = LightDirectionVector(targetObject);

        Vector3 objectNormal;
        Renderer targetRenderer = targetObject.GetComponent<Renderer>();
        Material targetMaterial = targetRenderer != null ? targetRenderer.material : null;

        if (targetMaterial != null && targetMaterial.HasProperty("_BumpMap"))
        {
            Texture2D normalMap = targetMaterial.GetTexture("_BumpMap") as Texture2D;
            if (normalMap != null)
            {
                Color normalColor = normalMap.GetPixelBilinear(uv.x, uv.y);
                objectNormal = new Vector3(normalColor.r * 2.0f - 1.0f, normalColor.g * 2.0f - 1.0f, normalColor.b * 2.0f - 1.0f);
            }
            else
            {
                objectNormal = targetObject.transform.up;
            }
        }
        else
        {
            objectNormal = targetObject.transform.up;
        }

        objectNormal.Normalize();

        Debug.DrawLine(targetObject.transform.position, targetObject.transform.position + objectNormal, Color.green);
        Debug.DrawLine(transform.position, targetObject.transform.position, Color.yellow);

        float angleFactor = Vector3.Dot(objectNormal, lightToObjectDir);
        float lightContribution = Mathf.Max(0, angleFactor + bias);

        float distance = Vector3.Distance(transform.position, targetObject.transform.position);
        float attenuation = Mathf.Pow(1.0f / (1.0f + falloffExponent * distance), falloffExponent);

        float finalIntensity = intensity * lightContribution * attenuation;

        Debug.Log("Final Light Intensity: " + finalIntensity);
    }
}
