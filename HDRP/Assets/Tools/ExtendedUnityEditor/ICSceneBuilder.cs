using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
 
//V0.10

namespace ICTools.ICSceneBuilder
{

    public class ICSceneBuilder : EditorWindow
    {

        public static SceneBuilderPages page;
        public SceneBuilderMode mode;

        public static ICScenBuilderSettings settings;

        private Vector2 toolScroll;

        private Color defaultColor;

        private string creatingString;
        private GameObject createObject;
        private int CreatorPage = 0;
        private bool creatingCategory;
        private bool creatingPrefab;

        [MenuItem("Tools/ICSceneBuilder/Open Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ICSceneBuilder), false, "ICSceneBuilder").maxSize = new Vector2(500, 5000);
            if (settings = null) Load();
        }

        [MenuItem("Tools/ICSceneBuilder/Settings")]
        public static void Seetings()
        {
            if (page != SceneBuilderPages.Creator && settings != null)
                page = SceneBuilderPages.Setting;
        }

        void OnEnable()
        {
            defaultColor = GUI.color;
            autoRepaintOnSceneChange = true;

            SceneView.onSceneGUIDelegate += SceneGUI;

            Load();
        }

        void OnDestroy()
        {
            SceneView.onSceneGUIDelegate -= SceneGUI;
            ReimportAll();
        }
   
        void SceneGUI(SceneView sceneView)
        {
            InputKeys();
        }

        #region SettingsFile

        private void ImportCategory(string path) {
            Category category = new Category
            {
                name = path,
                path = path
            };

            settings.categories.Add(category);
            ReimportAll();
        }

        public static void Load()
        {
            settings = (ICScenBuilderSettings)AssetDatabase.LoadAssetAtPath("Assets/Resources/ICTools/ICScenBuilderSettings.asset", typeof(ICScenBuilderSettings));
            if (settings == null)
            {
                page = SceneBuilderPages.Creator;
                Debug.Log("404");
            }
            else
            {
                page = SceneBuilderPages.Tool;
                ReimportAll();
            }

        }

        public static void Save()
        {
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/ICSceneBuilder/Save and Reimport")]
        public static void ReimportAll()
        {
            if (settings != null)
            {
          /*      for (int i = 0; i < settings.categories.Count; i++)
                {
                    Directory.Exists("Assets/Resources")
                }

           */
                foreach (Category category in settings.categories)
                {
                    Object[] resources = Resources.LoadAll<GameObject>("Prefabs/" + category.name); ;

                    // Remove
                    for (int i = 0; i < category.prefabSettings.Count; i++)
                    {

                        if (category.prefabSettings[i].gameObject == null)
                        {
                            category.prefabSettings.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            if (category.prefabSettings[i].name == "")
                                category.prefabSettings[i].name = category.prefabSettings[i].gameObject.name;
                        }
                    }

                    //ADD
                    foreach (Object asset in resources)
                    {
                        bool add = true;
                        foreach (PrefabSettings obj in category.prefabSettings)
                        {
                            if (obj.gameObject.name.ToUpper() == ((GameObject)asset).name.ToUpper()) { add = false; }

                        }
                        if (add) category.prefabSettings.Add(new PrefabSettings(((GameObject)asset), new PlaceSettings()));

                    }

                }
                Debug.Log("Reimported ALL ");
                settings.TestIndicators();
                Save();
            }
  
        }

        void CreateSettingsfile()
        {

            if (!Directory.Exists(Application.dataPath + "/Resources/"))
                Directory.CreateDirectory(Application.dataPath + "/Resources/");

            if (!Directory.Exists(Application.dataPath + "/Resources/ICTools"))
                Directory.CreateDirectory(Application.dataPath + "/Resources/ICTools");

            if (!Directory.Exists(Application.dataPath + "/Resources/Prefabs"))
                Directory.CreateDirectory(Application.dataPath + "/Resources/Prefabs");

            settings = ScriptableObject.CreateInstance<ICScenBuilderSettings>();


            AssetDatabase.CreateAsset(settings, "Assets/Resources/ICTools/ICScenBuilderSettings.asset");
            AssetDatabase.SaveAssets();

            settings = (ICScenBuilderSettings)AssetDatabase.LoadAssetAtPath("Assets/Resources/ICTools/ICScenBuilderSettings.asset", typeof(ICScenBuilderSettings));

        }

        #endregion

        #region GUI

        void OnGUI()
        {
            Debug.Log(page);
            if (page == SceneBuilderPages.Tool)
            {
                DrawToolUI();
            }
            else if (page == SceneBuilderPages.Setting)
            {
                DrawSettingsUI();
            }
            else if (page == SceneBuilderPages.Creator)
            {
                DrawCreatorUI();
            }
        }

        void DrawToolUI()
        {
            GUI.backgroundColor = (settings.ObjectExist) ? ((mode == SceneBuilderMode.Placing) ? Color.green : (mode == SceneBuilderMode.Disabled) ? defaultColor : Color.red) : Color.red;

            EditorGUILayout.Separator();

            if (GUILayout.Button((settings.ObjectExist) ? ("Mode:" + ((mode == SceneBuilderMode.Placing) ? "Place" : (mode == SceneBuilderMode.Disabled) ? "Disabled" : "Destroy")) : "NO OBJECT TO PLACE"))
            {
                /*  if (mode == SceneBuilderMode.Destroy)
                      mode = SceneBuilderMode.Disabled;
                  else
                      mode++;
              */
                if (mode == SceneBuilderMode.Placing)
                    mode = SceneBuilderMode.Disabled;
                else
                    mode = SceneBuilderMode.Placing;
            }

            GUI.backgroundColor = (settings.AnyCategory) ? defaultColor : Color.red;
            EditorGUILayout.Separator();
            GUILayout.Label("Category", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("<", EditorStyles.miniButtonLeft))
                {
                    settings.selectedCategory--;
                    settings.TestIndicators();
                }
                if (GUILayout.Button(settings.SelectedCategoryLabel, EditorStyles.miniButtonMid))
                {

                }
                if (GUILayout.Button(">", EditorStyles.miniButtonRight))
                {
                    settings.selectedCategory++;
                    settings.TestIndicators();
                }

                GUI.backgroundColor = (settings.AnyCategory) ? defaultColor : Color.green;

                if (GUILayout.Button("+", EditorStyles.miniButton))
                {
                    creatingString   = "Category Name";
                    creatingCategory = !creatingCategory;
                    creatingPrefab = false;
                }
                GUI.backgroundColor = defaultColor;

                if (settings.AnyCategory)
                    if (GUILayout.Button("-", EditorStyles.miniButton))
                    {
                        creatingPrefab = false;
                        creatingCategory = false;
                        if (EditorUtility.DisplayDialog("Are you sure?",
                               "Are you sure want delete current Category ? ",
                               "Yes,delete",
                               "No plis my prefab"))
                        {
                            settings.categories.Remove(settings.SelectedCategory);
                            ReimportAll();
                        }
                    }  
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            { 
            if (creatingCategory)
                    DrawCreateCategory();

            if (settings.AnyCategory)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Prefabs", EditorStyles.boldLabel);
                        settings.prefabPreview = EditorGUILayout.ToggleLeft("Preview", settings.prefabPreview);
                    }
                    EditorGUILayout.EndHorizontal();

                    GUI.backgroundColor = defaultColor;

                    if (settings.prefabPreview)
                    {
                        if (GUILayout.Button(settings.SelectedPrefabLabel, EditorStyles.miniButtonMid))
                        {
                            if (settings.ObjectExist)
                            {
                                Selection.activeObject = settings.SelectedPrefab.gameObject;
                            }                     
                        }
                        if (settings.ObjectExist)
                        {
                            if (GUILayout.Button(AssetPreview.GetAssetPreview(settings.SelectedPrefab.gameObject)))
                                Selection.activeObject = settings.SelectedPrefab.gameObject;
                        }
                    }

                    EditorGUILayout.BeginHorizontal();
                    {                  
                        if (GUILayout.Button("<", EditorStyles.miniButtonLeft))
                        {
                            settings.selectedPrefab--;
                            settings.TestIndicators();
                        }

                        if (!settings.prefabPreview)
                            if (GUILayout.Button(settings.SelectedPrefabLabel, EditorStyles.miniButtonMid))
                            {
                                if (settings.ObjectExist)
                                {
                                    Selection.activeObject = settings.SelectedPrefab.gameObject;
                                }
                            }

                        if (GUILayout.Button(">", EditorStyles.miniButtonRight))
                        {
                            settings.selectedPrefab++;
                            settings.TestIndicators();
                        }

                        GUI.backgroundColor = (settings.AnyPrefab) ? defaultColor : Color.green;

                        if (GUILayout.Button((settings.prefabPreview) ? "Create new" : "+", EditorStyles.miniButton))
                        {
                            creatingString = "Prefab Name";
                            creatingPrefab = !creatingPrefab;
                            creatingCategory = false;
                        }

                        GUI.backgroundColor = defaultColor; 

                        if(settings.ObjectExist)              
                        if (GUILayout.Button((settings.prefabPreview) ? "Delete" : "-", EditorStyles.miniButton))
                        {
                            if (EditorUtility.DisplayDialog("Are you sure?",
                                 "Are you sure want delete prefab ? ",
                                 "Yes,delete",
                                 "No plis my prefab"))
                            {
                                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(settings.SelectedPrefab.gameObject));
                                ReimportAll();
                            }
                        }  
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginVertical();
                    {
                        if (creatingPrefab)
                         DrawCreateNewObjectPrefab();

                        EditorGUILayout.BeginVertical();

                        if (settings.ObjectExist)
                        {
                            GUILayout.Label("Place Options ", EditorStyles.boldLabel);                       
                            DrawPlaceOptions(settings.PSBySettings);
                        }
                    }
               
                    EditorGUILayout.BeginVertical();
                    {
                        GUILayout.Label("Coming Soon New Options ", EditorStyles.boldLabel);
                    }
                    
                }
              
            }

        }
       
        void DrawSettingsUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            EditorGUILayout.Separator();

            settings.placeSettingsGetFrom = (PlaceSettingsGetFrom)EditorGUILayout.EnumPopup("Place Setings Get From", settings.placeSettingsGetFrom);

            settings.key_Placekey = (KeyCode)EditorGUILayout.EnumPopup("Place Key", settings.key_Placekey);

            EditorGUILayout.Separator();

            settings.prefabPreview = EditorGUILayout.ToggleLeft("Prefab Preview", settings.prefabPreview);
            settings.autoSelectionPlacedObjects = EditorGUILayout.ToggleLeft("Auto Select Placed Objects", settings.autoSelectionPlacedObjects);
            settings.usingKeys = EditorGUILayout.ToggleLeft("Using Keys for controll ", settings.usingKeys);

            EditorGUILayout.BeginHorizontal();
            settings.AddingToParent = EditorGUILayout.ToggleLeft((!settings.AddingToParent) ? "Adding to parent" : " Parent Prefab:", settings.AddingToParent);
            if (settings.AddingToParent)
            {
                settings.parent = (GameObject)EditorGUILayout.ObjectField(settings.parent, typeof(GameObject));
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Separator();

            if (GUILayout.Button("Go To Tool"))
            {
                page = SceneBuilderPages.Tool;
                ReimportAll();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }

        void DrawCreatorUI()
        {
            if (CreatorPage == 0)
            {
                EditorGUILayout.LabelField("Error 404 ICSceneBuilder's settings file not found. ", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Do you want to create it ?", EditorStyles.boldLabel);

                if (GUILayout.Button("Create"))
                {
                    CreatorPage++;
                    CreateSettingsfile();
                }

            }
            else if (CreatorPage == 1)
            {
                EditorGUILayout.LabelField("File Created", EditorStyles.boldLabel);

                if (GUILayout.Button("Customize"))
                {
                    page = SceneBuilderPages.Setting;
                }
                if (GUILayout.Button("Go To Tool"))
                {
                    page = SceneBuilderPages.Tool;
                }
            }

        }

        void DrawPlaceOptions(PlaceSettings pSettings)
        {

            EditorGUILayout.BeginVertical();
            if (!pSettings.rotation.random)
                pSettings.rotation.normal = EditorGUILayout.Vector3Field("Rotation:", pSettings.rotation.normal);

            if (!pSettings.scale.random)
                pSettings.scale.normal = EditorGUILayout.Vector3Field("Scale:", pSettings.scale.normal);
            if (pSettings.addingPosition)
                pSettings.AddPosition = EditorGUILayout.Vector3Field("Position to add", pSettings.AddPosition);


            pSettings.addingPosition = EditorGUILayout.BeginToggleGroup("Adding position", pSettings.addingPosition);


            EditorGUILayout.BeginVertical();
            EditorGUILayout.EndToggleGroup();
            pSettings.rotation.random = EditorGUILayout.BeginToggleGroup("Random Rotation", pSettings.rotation.random);

            if (pSettings.rotation.random)
            {
                DrawMinMaxSlider("Random Rotation Range X", ref pSettings.rotation.min.x, ref pSettings.rotation.max.x, -360, 360, ref pSettings.rotation.randX, ref pSettings.rotation.normal.x);
                DrawMinMaxSlider("Random Rotation Range Y", ref pSettings.rotation.min.y, ref pSettings.rotation.max.y, -360, 360, ref pSettings.rotation.randY, ref pSettings.rotation.normal.y);
                DrawMinMaxSlider("Random Rotation Range Z", ref pSettings.rotation.min.z, ref pSettings.rotation.max.z, -360, 360, ref pSettings.rotation.randZ, ref pSettings.rotation.normal.z);

            }

            EditorGUILayout.EndToggleGroup();

            EditorGUILayout.BeginVertical();
            pSettings.scale.random = EditorGUILayout.BeginToggleGroup("Random Scale", pSettings.scale.random);

            if (pSettings.scale.random)
            {
                if (pSettings.scale.locked)
                {
                    DrawMinMaxSlider("Random Scale Range", ref pSettings.scale.min.x, ref pSettings.scale.max.x, 1, 10, ref pSettings.scale.locked, ref pSettings.scale.normal.x);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Lock X Y Z To One Size");
                    pSettings.scale.locked = EditorGUILayout.Toggle(pSettings.scale.locked);
                    EditorGUILayout.EndHorizontal();
                    DrawMinMaxSlider("Random Scale Range X", ref pSettings.scale.min.x, ref pSettings.scale.max.x, 1, 10, ref pSettings.scale.randX, ref pSettings.scale.normal.x);
                    DrawMinMaxSlider("Random Scale Range Y", ref pSettings.scale.min.y, ref pSettings.scale.max.y, 1, 10, ref pSettings.scale.randY, ref pSettings.scale.normal.y);
                    DrawMinMaxSlider("Random Scale Range Z", ref pSettings.scale.min.z, ref pSettings.scale.max.z, 1, 10, ref pSettings.scale.randZ, ref pSettings.scale.normal.z);
                }
            }

            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
           settings.PSBySettings = pSettings;
        }

        void DrawMinMaxSlider(string Label, ref float minFloat, ref float maxFloat, float min, float max, ref bool toggle, ref float Float)
        {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(Label + " " + System.Math.Round(minFloat,1) + "/" + System.Math.Round(maxFloat,1));
            toggle = EditorGUILayout.Toggle(toggle);
            if (toggle) EditorGUILayout.MinMaxSlider(ref minFloat, ref maxFloat, min, max); else Float = EditorGUILayout.FloatField(Float);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginVertical();


        }

        #endregion

        #region UIVoids

        void DrawCreateCategory()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Adding Category", EditorStyles.boldLabel);
            creatingString = GUILayout.TextField(creatingString);

            bool Imported = settings.CategoryImported(creatingString);

            if (!Directory.Exists("Assets/Resources/Prefabs/" + creatingString) || Imported)
                EditorGUILayout.LabelField((!Directory.Exists("Assets/Resources/Prefabs/" + creatingString)) ? "Folder not found - Create ?" : "Category realy imported,reimport all? ", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            {

                GUI.backgroundColor = Color.green + Color.gray * .5f;

                if (Directory.Exists("Assets/Resources/Prefabs/" + creatingString))
                {
                    if (GUILayout.Button((Imported) ? "Reimport" : "Import", EditorStyles.miniButton))
                    {
                        if (Imported)
                            ReimportAll();
                        else
                            ImportCategory(creatingString);

                        creatingCategory = false;
                    }
                }
                else
                {
                    if (GUILayout.Button("Create", EditorStyles.miniButton))
                    {
                        Directory.CreateDirectory("Assets/Resources/Prefabs/" + creatingString);
                        ImportCategory(creatingString);
                        settings.selectedCategory = settings.categories.Count - 1;
                    }
                }
                GUI.backgroundColor = Color.red + Color.gray * .4f;
                if (GUILayout.Button("Cencel", EditorStyles.miniButton))
                {
                    creatingCategory = false;
                }
                GUI.backgroundColor = defaultColor;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

        }

        void DrawCreateNewObjectPrefab()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Creating Prefab", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            GUI.backgroundColor = (creatingString == "Prefab Name" || creatingString == "" || creatingString == " ") ? Color.red : Color.green;
            creatingString = GUILayout.TextField(creatingString);

            GUI.backgroundColor = (createObject != null) ? Color.green : Color.red;
            createObject = (GameObject)EditorGUILayout.ObjectField(createObject, typeof(GameObject));

            EditorGUILayout.EndHorizontal();
            if (createObject == null)
            EditorGUILayout.LabelField("Select Object To Create Prefab", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            GUI.backgroundColor = ((createObject != null) ? Color.green + Color.gray * .5f : (Color.green * .5f));
            if (GUILayout.Button("Create", EditorStyles.miniButton) && createObject != null&& creatingString!="")
            {
                string path = "Assets/resources/Prefabs/" + settings.SelectedCategory.path + "/" + creatingString + ".prefab";

                creatingPrefab = false;
                settings.selectedPrefab = settings.SelectedCategory.prefabSettings.Count- 1;

                if (AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)))
                {
                    if (EditorUtility.DisplayDialog("Are you sure?",
                         "The prefab already exists. Do you want to overwrite it?",
                         "Yes,overwrite",
                         "No"))
                    {
                        CreateNewPrefab(createObject, path);
                    }
                }
                else
                {
                    CreateNewPrefab(createObject, path);
                }
                creatingPrefab = false;
                ReimportAll();
            }
            GUI.backgroundColor = Color.red + Color.gray * .4f;
            if (GUILayout.Button("Cencel", EditorStyles.miniButton))
            {
                creatingPrefab = false;


            }
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Separator();

        }

        static void CreateNewPrefab(GameObject obj, string localPath)
        {
            Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
            PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }

        #endregion

        #region Place

        void InputKeys()
        {

            if (Event.current.type == EventType.KeyDown)
            {
                if (settings.usingKeys)
                {
                    if (Event.current.keyCode == settings.key_NextPrefab)
                    {

                    }
                    if (Event.current.keyCode == settings.key_UndoPrefab)
                    {

                    }
                    if (Event.current.keyCode == settings.key_ChangeMode)
                    {
                        if (mode == SceneBuilderMode.Destroy)
                            mode = SceneBuilderMode.Disabled;
                        else
                            mode++;
                    }
                }          
            }
            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == settings.key_Placekey && settings.ObjectExist && mode == SceneBuilderMode.Placing)
                {
                    PlaceObject();
                }
            }
        }

        void PlaceObject() {
            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(((Object)settings.SelectedPrefab.gameObject));
            obj.transform.position = RayLocation();
            SetTransform(obj,settings.PSBySettings);

            obj.name = settings.SelectedPrefab.name;

            if (settings.AddingToParent && settings.parent != null)
                obj.transform.parent = settings.parent.transform;

        }

        void SetTransform(GameObject gameObject,PlaceSettings settings)
        {
            if (settings.addingPosition)
            gameObject.transform.position += settings.AddPosition;

            gameObject.transform.rotation = Quaternion.identity;

            Vector3 vec = settings.rotation.GetVector();
            gameObject.transform.Rotate(vec.x, vec.y, vec.z);

            gameObject.transform.localScale = settings.scale.GetVector();          
        }

        Vector3 RayLocation()
        {
            RaycastHit rh;
            Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin, HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).direction, out rh);
            Vector3 r = rh.point;
            return rh.point;
        }

        #endregion
    }

     #region Enums

    public enum SceneBuilderPages {
        Tool,
        Setting,
        Creator,
       }

    public enum SceneBuilderMode
    {
        Disabled,
        Placing,
        Destroy
    }

    #endregion

}