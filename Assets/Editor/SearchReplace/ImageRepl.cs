using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ImageRepl : EditorWindow
{
    private GameObject[] allGameObjectsOnScene;
    private GameObject[] gameObjectsWithImage;

    private Sprite replacementTexture;

    private Texture2D noneTexture = null;
    private byte[] byteNone;
    private string path = "Assets/Editor/SearchReplace/None.png";

    private GUIContent[] gridImages;
    public Texture2D[] textures;
    

    private int selectedTexture = 0;
    private int counter = -1;
    private bool trigger = false;

    Rect texturesButtonsSection;
    Rect changeTexture;
    Rect cell;
    Rect scrollView;

    private Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Window/Image Replacement v2")]
    static void OpenWindow()
    {
        ImageRepl window = (ImageRepl)GetWindow(typeof(ImageRepl));
        window.minSize = new Vector2(580, 570);
        window.Show();
    }

    void OnEnable()
    {
        Initialize();

        for (var i = 0; i < allGameObjectsOnScene.Length; i++)
        {
            if (allGameObjectsOnScene[i].GetComponent<Image>() != null || allGameObjectsOnScene[i].GetComponent<SpriteRenderer>() != null)
            {
                counter++;
                gameObjectsWithImage[counter] = allGameObjectsOnScene[i];
            }
        }

        Debug.LogWarning("GameObjects in progect: " + allGameObjectsOnScene.Length);
        FindObjects();
    }

    void Initialize()
    {
        //allGameObjectsOnScene = (GameObject[])FindObjectsOfType(typeof(GameObject));  // only active in scene
        allGameObjectsOnScene = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));  // UISprite Error
        gameObjectsWithImage = new GameObject[allGameObjectsOnScene.Length];


        textures = new Texture2D[allGameObjectsOnScene.Length];

        if (File.Exists(path))
        {
            byteNone = File.ReadAllBytes(path);
            noneTexture = new Texture2D(100, 100);
            noneTexture.LoadImage(byteNone);
        }

    }

    void OnGUI()
    {
        
        DrawLayouts();
        DrawTextureList();
    }

    void DrawLayouts()
    {

        texturesButtonsSection.x = 0;
        texturesButtonsSection.y = 0;
        texturesButtonsSection.width = Screen.width - 60;
        texturesButtonsSection.height = Screen.height;

        changeTexture.x = texturesButtonsSection.width;
        changeTexture.y = 0;
        changeTexture.width = 60;
        changeTexture.height = Screen.height;

        cell.x = 10;
        cell.y = 10;
        cell.width = texturesButtonsSection.width - 35;
        cell.height = Screen.height;

        scrollView.x = 10;
        scrollView.y = 10;
        scrollView.width = 520;
        scrollView.height = texturesButtonsSection.height;

        GUI.DrawTexture(texturesButtonsSection, new Texture2D(1, 1));
    }

    void DrawTextureList()
    {

    GUILayout.BeginArea(texturesButtonsSection);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(Screen.width - 60), GUILayout.Height(texturesButtonsSection.height));

        if (trigger && gridImages != null)
        {
            selectedTexture = GUILayout.SelectionGrid(selectedTexture, gridImages, 3, GUILayout.Width(Screen.width - 80), GUILayout.Height(gridImages.Length/3*150));
        }

        GUILayout.EndScrollView();
    GUILayout.EndArea();

    GUILayout.BeginArea(changeTexture);

        replacementTexture = (Sprite)EditorGUILayout.ObjectField("", replacementTexture, typeof(Sprite), false);

        //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2); // texture2d -> sprite

        if (GUILayout.Button("Change", GUILayout.Width(60), GUILayout.Height(30)))
        {
            if (gameObjectsWithImage[selectedTexture].GetComponent<Image>() != null)
            {
                gameObjectsWithImage[selectedTexture].GetComponent<Image>().sprite = replacementTexture;
            }
            else if (gameObjectsWithImage[selectedTexture].GetComponent<SpriteRenderer>() != null)
            {
                gameObjectsWithImage[selectedTexture].GetComponent<SpriteRenderer>().sprite = replacementTexture;
            }
            else { return; }
            trigger = false;
            FindObjects();
            
           
        }

    GUILayout.EndArea();
    }

    void FindObjects()
    {
        gridImages = new GUIContent[counter];
        var count = -1;
        for (int g = 0; g < gridImages.Length; g++)
        {
            var imageComponent = gameObjectsWithImage[g].GetComponent<Image>();
            var spriteRendComponent = gameObjectsWithImage[g].GetComponent<SpriteRenderer>();

            if (imageComponent != null && imageComponent.sprite != null && imageComponent.sprite.name != "Background"
                && imageComponent.sprite.name != "UISprite" && imageComponent.sprite.name != "Checkmark"
                && imageComponent.sprite.name != "InputFieldBackground" && imageComponent.sprite.name != "UIMask"
                && imageComponent.sprite.name != "DropdownArrow")
            {
                textures[g] = new Texture2D((int)imageComponent.sprite.textureRect.width,
                                               (int)imageComponent.sprite.textureRect.height);

                var pixels = imageComponent.sprite.texture.GetPixels(
                                                                (int)imageComponent.sprite.textureRect.x,
                                                                (int)imageComponent.sprite.textureRect.y,
                                                                (int)imageComponent.sprite.textureRect.width,
                                                                (int)imageComponent.sprite.textureRect.height
                                                             );
                textures[g].SetPixels(pixels);
                textures[g].Apply();
                count++;
                gridImages[count] = new GUIContent(textures[g], gameObjectsWithImage[g].name);

            }
            else if (spriteRendComponent != null && spriteRendComponent.sprite.name != "None")
            {
                count++;
                gridImages[count] = new GUIContent(AssetPreview.GetAssetPreview(gameObjectsWithImage[g]), gameObjectsWithImage[g].name);
            }
            else
            {
                count++;
                gridImages[count] = new GUIContent(noneTexture, gameObjectsWithImage[g].name);
                Debug.LogWarning("null sprite in object: " + gameObjectsWithImage[g].name);
            }
            
        }
        // gridImages = gridImages.Where(val => val != null).ToArray();
        Debug.LogWarning("gridImages length: " + gridImages.Length);
        trigger = true;

    }
}
