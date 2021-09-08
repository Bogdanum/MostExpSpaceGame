using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

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

    [MenuItem("Bogdanum/Image Replacement")]
    static void OpenWindow()
    {
        ImageRepl window = (ImageRepl)GetWindow(typeof(ImageRepl), false, "");
        window.maxSize = new Vector2(620, 570);
        window.minSize = new Vector2(620, 570);
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
        allGameObjectsOnScene = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
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
        texturesButtonsSection.width = Screen.width - 100;
        texturesButtonsSection.height = Screen.height;

        changeTexture.x = texturesButtonsSection.width;
        changeTexture.y = 0;
        changeTexture.width = 100;
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
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(scrollView.width), GUILayout.Height(texturesButtonsSection.height));

        if (trigger && gridImages != null)
        {
            selectedTexture = GUILayout.SelectionGrid(selectedTexture, gridImages, 3, GUILayout.Width(Screen.width - 120), GUILayout.Height(gridImages.Length/3*150));
            Selection.activeGameObject = gameObjectsWithImage[selectedTexture];
        }

        GUILayout.EndScrollView();
    GUILayout.EndArea();

    GUILayout.BeginArea(changeTexture);

        replacementTexture = (Sprite)EditorGUILayout.ObjectField("", replacementTexture, typeof(Sprite), false, GUILayout.MinWidth(100), GUILayout.MinHeight(100));

        GUILayout.Space(10);

        if (GUILayout.Button("Заменить", GUILayout.Width(100), GUILayout.Height(30)))
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
    /*
            FileStream fileStream = File.Open("Assets/CarouselGames/Resources/CurrentStore.txt", FileMode.Create);
            StreamWriter output = new StreamWriter(fileStream);
            output.Write("Huawei"); // enum с выбором в gui
            output.Close();
    */
       }

        GUILayout.Space(30);
        GUILayout.Label(" Если в списке\n  отсутствует\n     нужный\n  вам спрайт,\n  нажмите на\n  кнопку ниже.");

        if (GUILayout.Button(new GUIContent("Выбрать PNG и\nпреобразовать\nв sprite",
        "Откроется окно, в котором вам нужно выбрать PNG файл. Если в вы столкнулись с проблемой и вам нужна помощь -> Бог поможет (или дед Абдул с ПР-73)."),
        GUILayout.Width(100), GUILayout.Height(80)))
        {
            string path = EditorUtility.OpenFilePanel("Выберите png файл", Application.dataPath, "png");
            if (path != null)
            {
                if (path.Contains("png"))
                {
                    path = path.Remove(0, path.IndexOf("Assets", StringComparison.InvariantCulture));
                    AssetDatabase.Refresh();
                    AssetDatabase.ImportAsset(path);
                    TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                    if (textureImporter.textureType != TextureImporterType.Sprite)
                    {
                        textureImporter.textureType = TextureImporterType.Sprite;
                        textureImporter.isReadable = true;
                        AssetDatabase.WriteImportSettingsIfDirty(path);
                        AssetDatabase.Refresh();
                        EditorUtility.DisplayDialog("Complete!", "PNG преобразован в Sprite. Теперь вы можете выбрать его в списке.", "OK");
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Упс...", "Похоже выбранный вами файл уже является спрайтом. Выберите его в списке.", "Вернуться");
                    }
                    FindObjects();
                }           
            }
            
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

                if (imageComponent != null
                    && imageComponent.sprite != null
                    && imageComponent.sprite.texture.isReadable)
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
                }
        }
        Debug.LogWarning("gridImages length: " + gridImages.Length);
        trigger = true;

    }
}
