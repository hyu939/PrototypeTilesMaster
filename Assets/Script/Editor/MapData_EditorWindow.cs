using UnityEngine;
using UnityEditor;

public class MapData_EditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected MapData[] mapData;
    protected string selectedPropertyPach;
    protected string selectedProperty;

    private Vector2 scrollPosition = Vector2.zero;


    // Tạo đường dẫn và mở Editor window trên thanh tab
    [MenuItem("MyTab/mapData")]
    protected static void ShowWindow()
    {
        GetWindow<MapData_EditorWindow>("mapData");
    }


    private void OnGUI()
    {
        // Tìm Mapdata
        mapData = GetAllInstances<MapData>();
        serializedObject = new SerializedObject(mapData[0]);

        EditorGUILayout.BeginHorizontal();

        // Danh sách map trên Editor
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSliderBar(mapData);
        EditorGUILayout.EndVertical();

        // Khu vực cho thông tin bên trong của Mapdata
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        // Tạo cái giúp kéo lên kéo xuống theo kích thước của Edittor :(
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Xuất các thông tin bên trong Mapdata
        if (selectedProperty != null)
        {
            for (int i = 0; i < mapData.Length; i++)
            {
                if (mapData[i].mapname == selectedProperty)
                {
                    serializedObject = new SerializedObject(mapData[i]);

                    SerializedProperty tilesProperty = serializedObject.FindProperty("tiles");

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("mapname"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("lv"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("time"));

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("TILES POOL");;

                    for (int j = 0; j < mapData[i].tiles.Count; j++)
                    {
                        SerializedProperty tileProperty = tilesProperty.GetArrayElementAtIndex(j);
                        SerializedProperty tilesPrefabProperty = tileProperty.FindPropertyRelative("tilesPrefab");
                        SerializedProperty chanceProperty = tileProperty.FindPropertyRelative("chance");

                        
                        EditorGUILayout.PropertyField(tilesPrefabProperty);
                        EditorGUILayout.PropertyField(chanceProperty);

                        EditorGUILayout.Space();
                    }
                }
            }
            
        }
        else
        {
            EditorGUILayout.LabelField("select an item from the list");
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
    }

    // Tìm thêm dữ liệu các MapData vào chuỗi
    public static T[] GetAllInstances<T>() where T : MapData
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }




    // Vẽ thuộc tính ra trên của sổ Editor
    protected void DrawProperties(SerializedProperty p)
    {

        while (p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);

        }


    }


    // Vẽ sliderbar cho danh sách map
    protected void DrawSliderBar(MapData[] prop)
    {
        foreach (MapData p in prop)
        {
            if (GUILayout.Button(p.mapname))
            {
                selectedPropertyPach = p.mapname;
            }
        }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }

        if (GUILayout.Button("new Map"))
        {
            MapData newMapData = MapData.CreateInstance<MapData>();
            CreateMapData newMapDataWindow = GetWindow<CreateMapData>("New Map");
            newMapDataWindow.newMapData = newMapData;

        }
    }

    // Cho phép thay đổi dữ liệu 
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
