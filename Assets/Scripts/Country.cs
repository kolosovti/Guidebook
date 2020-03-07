using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Guidebook.CountrySelection;

namespace Guidebook.Countries 
{
    public enum Countries 
    {
        Russia, 
        France, 
        Germany, 
        Poland, 
        US
    }

    public enum CountryFields
    {
        Name,
        Area,
        Gdp,
        Population
    }

    public class Country : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float longTapDuration = 0.5f;
        public Countries countryName;
        public int countryArea;
        public int countryGdp;
        public int countryPopulation;

        private bool isTextureChanged;
        private bool isSelected;
        private float clickTime;
        private bool isClicked;
        private GameObject selectedIcon;

        void Start()
        {
            selectedIcon = Resources.Load<GameObject>("Prefabs/Selected");
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            clickTime = Time.time;
            isClicked = true;
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            if (isClicked)
                CountrySelectionManager.Instance.ManageCountryClick(ClickType.Tap, this);
            isClicked = false;
        }

        void FixedUpdate()
        {
            if(Time.time > clickTime + longTapDuration && isClicked)
            {
                CountrySelectionManager.Instance.ManageCountryClick(ClickType.LongTap, this);
                isClicked = false;
                Debug.Log("LongTap");
            }
        }

        public void ChangeTexture()
        {
            GameObject newGameObject = null;

            if (isTextureChanged)
                newGameObject = Resources.Load<GameObject>("Prefabs/Geotag");
            else
                newGameObject = Resources.Load<GameObject>("Prefabs/" + countryName.ToString());

            if (newGameObject)
            {
                Transform oldTransform = gameObject.GetComponent<Transform>();
                Transform newTransform = newGameObject.GetComponent<Transform>();
                oldTransform.localScale = newTransform.localScale;

                MeshFilter oldMeshFilter = gameObject.GetComponent<MeshFilter>();
                MeshFilter newMeshFilter = newGameObject.GetComponent<MeshFilter>();
                oldMeshFilter.sharedMesh = newMeshFilter.sharedMesh;

                MeshRenderer oldTexture = gameObject.GetComponent<MeshRenderer>();
                MeshRenderer newTexture = newGameObject.GetComponent<MeshRenderer>();
                oldTexture.sharedMaterials = newTexture.sharedMaterials;
                isTextureChanged = isTextureChanged ? false : true;
            }
        }

        public void ChangeSelecting()
        {
            if (isSelected)
                Destroy(transform.GetChild(0).gameObject);
            else
                Instantiate(selectedIcon, gameObject.GetComponent<Transform>());
            isSelected = isSelected ? false : true;
        }
    }
}