using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UtilityFun : MonoBehaviour {
    public static UtilityFun instance;


	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
        }
	}
    /// <summary>
    /// get all Image reference in this game object
    /// </summary>
    /// <param name="Target GameObject"></param>
    /// <returns></returns>
    public List<Image> GetDisplayImage(GameObject gameObject, List<Image> RemoveImage = null)
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        List<Image> ListImages = images.ToList();

        if (RemoveImage == null)
        {

        }
        else
        {
            foreach (var item in RemoveImage)
            {
                ListImages.Remove(item);
            }
        }
        return ListImages;
    }
        public List<MeshRenderer> GetMeshRenders(GameObject gameObject, List<MeshRenderer> RemovemeshRenderers = null)
        {
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            List<MeshRenderer> ListmeshRenderers = meshRenderers.ToList();

            if (RemovemeshRenderers == null)
            {

            }
            else
            {
                foreach (var item in RemovemeshRenderers)
                {
                ListmeshRenderers.Remove(item);
                }
            }

            //foreach (var item in ListImages)
            //{
            //    Debug.Log(item.gameObject.name);
            //}
            return ListmeshRenderers;
    }

    public void ChangeListOfImageAlpha(List<Image> images, float to, float time) {
        foreach (var item in images)
        {
            Color CurrentColor = item.color;
            LeanTween.value(CurrentColor.a, to, time).setEase(LeanTweenType.easeInSine).setOnUpdate(delegate (float value)
            {
                item.color = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, value);
            });
        }
    }

    public void ChangeShineIntensity(ref float ShineIntensity, float toIntensity, float time,Color ShineColor, Image ShineMaterialimg,float alpha = -1) {
        float currentInstensity = ShineIntensity;

        LeanTween.value(ShineIntensity, toIntensity, time).setOnUpdate(delegate (float value)
        {

                Color color = new Color(ShineColor.r * value, ShineColor.g * value, ShineColor.b * value, ShineColor.a* value);
                ShineMaterialimg.material.SetColor("_OutlineColor", color);

        });
            ShineIntensity = toIntensity;

    }

   public float  Maping(float value, float inputMin, float inputMax, float outputMin, float outputMax, bool clamp) {
        float outVal = ((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin);

        if (clamp)
        {
            if (outputMax < outputMin)
            {
                if (outVal < outputMax) outVal = outputMax;
                else if (outVal > outputMin) outVal = outputMin;
            }
            else
            {
                if (outVal > outputMax) outVal = outputMax;
                else if (outVal < outputMin) outVal = outputMin;
            }
        }


        return outVal;
    }

}
