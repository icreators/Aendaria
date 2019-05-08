using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ICTools.ICSceneBuilder
{

    [System.Serializable]
    public class ICScenBuilderSettings : ScriptableObject
    {
        [HideInInspector]
        public List<Category> categories = new List<Category>();
        public PlaceSettings placeSettings;

        public GameObject parent;

        public int selectedCategory=0;
        public int selectedPrefab=0;

        public bool AddingToParent;
        public bool autoSelectionPlacedObjects;
        public bool prefabPreview;
        public bool usingKeys;

        public PlaceSettingsGetFrom placeSettingsGetFrom;

        public KeyCode key_NextPrefab = KeyCode.G;
        public KeyCode key_UndoPrefab = KeyCode.F;
        public KeyCode key_Placekey = KeyCode.E;
        public KeyCode key_ChangeMode = KeyCode.C;


        // Functions

        public PrefabSettings SelectedPrefab
        {
            get
            {
               return categories[selectedCategory].prefabSettings[selectedPrefab];
            }

            set
            {
                categories[selectedCategory].prefabSettings[selectedPrefab] = value;
            }

        }

        public Category SelectedCategory
        {
            get
            {
                return categories[selectedCategory];
            }

            set
            {
                categories[selectedCategory]=value;
            }

        }

        public bool AnyCategory
        {
            get
            {
                return (categories.Count > 0) ? true : false;
            }

        }

        public bool AnyPrefab
        {
            get
            {
                return (AnyCategory) ? (SelectedCategory.prefabSettings.Count >0): false;
            }

        }

        public void TestIndicators() {
            TestIndicator(ref selectedCategory,categories.Count);
            if(AnyCategory)
            TestIndicator(ref selectedPrefab, SelectedCategory.prefabSettings.Count);
        }

        private void TestIndicator(ref int indicator,int count)
         {
            if (indicator < 0)
                indicator = count - 1;
            else if (indicator >= count)
                indicator = 0;
         }

        public string SelectedCategoryLabel
        {
            get
            {
                if (AnyCategory)
                    return SelectedCategory.name;
                else
                    return "No Any Category Added";

            }
        }

        public string SelectedPrefabLabel {
            get
            {
                if (AnyPrefab)
                    return SelectedPrefab.name;
                else
                    return "No Any Prefab Added";

            }

        }

        public bool ObjectExist
        {
            get
            {
                return (AnyPrefab) ? (SelectedPrefab.gameObject!=null) : false; 
            }
        }

        public PlaceSettings PSBySettings
        {
            get
            {
                return  (placeSettingsGetFrom==PlaceSettingsGetFrom.Prefabs) ?  SelectedPrefab.placeSettings :(placeSettingsGetFrom==PlaceSettingsGetFrom.Category)? SelectedCategory.placeSettings : placeSettings;
            }
            set
            {
                if (placeSettingsGetFrom == PlaceSettingsGetFrom.Prefabs)
                    SelectedPrefab.placeSettings = value;
                else if (placeSettingsGetFrom == PlaceSettingsGetFrom.Category)
                    SelectedCategory.placeSettings = value;
                else
                    placeSettings = value;
            }
        }

        public bool CategoryImported(string Name)
        {
            bool ret = false;
            if (AnyCategory)
                foreach (Category C in categories)
                {
                    if (Name.ToUpper() == C.name.ToUpper()) ret = true;
                }

            return ret;
        }

    }

    public enum PlaceSettingsGetFrom
    {
        Prefabs,
        Category,
        Uniwersal
    }


    [System.Serializable]
    public class Category
    {
        public string name;
        public List<PrefabSettings> prefabSettings = new List<PrefabSettings>();
        public string path;
        public PlaceSettings placeSettings = new PlaceSettings();

    }

    [System.Serializable]
    public class PrefabSettings
    {
        public string name;
        public PlaceSettings placeSettings;
        public GameObject gameObject;

        public PrefabSettings(GameObject Obj, PlaceSettings PlaceSettings)
        {
            gameObject = Obj;
            placeSettings = PlaceSettings;
            name = Obj.name;
        }
    }

    [System.Serializable]
    public class PlaceSettings
    {
        public bool addingPosition;
        public Vector3 AddPosition;

        public MinMaxRandVector3 rotation = new MinMaxRandVector3(0, 360, 0);
        public MinMaxRandVector3 scale = new MinMaxRandVector3(1, 10, 1);
    }

    [System.Serializable]
    public class MinMaxRandVector3
    {
        public bool locked=false;
        public bool random;
        public bool randX;
        public bool randY;
        public bool randZ;

        public Vector3 min;
        public Vector3 max;
        public Vector3 normal;


        public MinMaxRandVector3(float Min, float Max, float Normal)
        {
            min = new Vector3(Min, Min, Min);
            max = new Vector3(Max, Max, Max);
            normal = new Vector3(Normal, Normal, Normal);

        }

        public Vector3 GetVector()
        {
            Vector3 returnValue=normal;

            if (random && !locked)
            {
                returnValue.x = (randX) ? Random.Range(min.x, max.x) : normal.x;
                returnValue.y = (randY) ? Random.Range(min.y, max.y) : normal.y;
                returnValue.z = (randZ) ? Random.Range(min.z, max.z) : normal.z;
            }
            else if (random && locked)
            {
                float f = Random.Range(min.x, max.x);
                returnValue = new Vector3(f,f,f);
            }
            
            return returnValue;
        }

    }


}
