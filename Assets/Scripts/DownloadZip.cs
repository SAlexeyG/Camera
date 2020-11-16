using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadZip : MonoBehaviour
{
    class NotMyData
    {
        public float speed;
        public int health;
        public string fullName;
        public string base64Texture;
    }

    [MenuItem("MyMenu/Download")]
    public static void Download()
    {
        string uri = "https://dminsky.com/settings.zip";
        Debug.Log(Application.persistentDataPath);

        UnityWebRequest request = UnityWebRequest.Get(uri);
        request.SendWebRequest().completed += (ao) =>
        {
            if (request.isNetworkError || request.isHttpError)
                Debug.Log("Error: " + request.error);
            else
            {
                try
                {
                    string outPath = Path.Combine(Application.persistentDataPath, "Task.zip");
                    File.WriteAllBytes(outPath, request.downloadHandler.data);

                    ZipFile.ExtractToDirectory(outPath, Application.persistentDataPath);

                    string inJsonPath = Path.Combine(Application.persistentDataPath, "settings.json");
                    NotMyData data = JsonUtility.FromJson<NotMyData>(File.ReadAllText(inJsonPath));

                    byte[] image = Convert.FromBase64String(data.base64Texture);
                    string outJpgPath = Path.Combine(Application.persistentDataPath, "image.jpg");
                    File.WriteAllBytes(outJpgPath, image);

                    byte[] textureData = File.ReadAllBytes(outJpgPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(textureData);

                    var player = GameObject.Find("Player");
                    player.GetComponent<Player>().Speed = data.speed;
                    player.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_BumpMap", texture);
                }
                catch(DirectoryNotFoundException e)
                {
                    EditorUtility.DisplayDialog("Error", "Directory not found", "OK");
                }
                catch(IOException e)
                {
                    EditorUtility.DisplayDialog("Error", e.Message, "OK");
                }
                catch(SecurityException e)
                {
                    EditorUtility.DisplayDialog("Error", "The caller does not have the required permission", "OK");
                }
                catch(FormatException e)
                {
                    EditorUtility.DisplayDialog("Error", "Format exception", "OK");
                }
            }
        };
    }
}
