using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraPhoto : MonoBehaviour
{
    Camera camera;
    [SerializeField]
    int num;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        StartCoroutine(_CloseRenderer());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator _CloseRenderer()
    {
        yield return new WaitForSeconds(0.2f);
        StageManager.playerIcons[num] = toTexture2D(camera.targetTexture);
        yield return new WaitForSeconds(0.2f);
        camera.targetTexture = null;
    }
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(256, 256, TextureFormat.RGBAFloat, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply(); 
        /*
        byte[] bytes = tex.EncodeToPNG();
        var dirPath = Application.dataPath + "/../SaveImages/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + "Image" + num + ".png", bytes);*/

        return tex;
    }
}
