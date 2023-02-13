using System;
using JetBrains.Annotations;
using TMPro;
using Travian.Math;
using Travian.Renderer;
using Travian.WorldMap;
using UnityEngine;
using UnityEngine.UI;

namespace Travian.Input
{
    public class ConstructionHandler : MonoBehaviour
    {
        public enum ActionType
        {
            None = 0,
            Construct = 1,
            Destroy = 2
        }
        
        [SerializeField] private MapRenderer mapRenderer;
        [SerializeField] private BuildingsRenderer buildingsRenderer;
        [SerializeField] private Button createDestroyButton;
        [SerializeField] private RectTransform destroyIcon;
        
        private BuildingConstructor buildingConstructor;
        private HexCoordinate cursorCoordinate;
        private ActionType actionType;
        
        private const string CreateString = "Create";
        private const string DestroyString = "Destroy";
        
        public void SetBuilder(BuildingConstructor buildingConstructor)
        {
            this.buildingConstructor = buildingConstructor;
        }

        [UsedImplicitly]
        public void OnCursorOn(HexCoordinate coordinate)
        {
            cursorCoordinate = coordinate;
            UpdateButton(cursorCoordinate);
        }

        [UsedImplicitly]
        public void OnCursorOff()
        {
            createDestroyButton.gameObject.SetActive(false);
            actionType = ActionType.None;
        }

        [UsedImplicitly]
        public void OnCreateDestroyButtonClicked()
        {
            switch (actionType)
            {
                case ActionType.Construct:
                    mapRenderer.SetTileVisibility(cursorCoordinate, false);
                    buildingConstructor.CreateBuilding(cursorCoordinate, BuildingType.House);
                    buildingsRenderer.CreateBuilding(cursorCoordinate, BuildingType.House);
                    UpdateButton(cursorCoordinate);
                    break;
                case ActionType.Destroy:
                    mapRenderer.SetTileVisibility(cursorCoordinate, true);
                    buildingConstructor.DestroyBuilding(cursorCoordinate);
                    buildingsRenderer.DestroyBuilding(cursorCoordinate);
                    UpdateButton(cursorCoordinate);
                    break;
            }
        }

        private void Start()
        {
            OnCursorOff();
        }
        
        private void UpdateButton(HexCoordinate coordinate)
        {
            bool isTileEmpty = buildingConstructor.IsTileEmpty(coordinate);
            bool canBuildHouse = buildingConstructor.CanTileHoldBuilding(coordinate, BuildingType.House);
            
            if (isTileEmpty && canBuildHouse)
            {
                bool hasEnoughResources = buildingConstructor.HasEnoughResourcesToBuild(BuildingType.House);
                createDestroyButton.gameObject.SetActive(true);
                createDestroyButton.interactable = hasEnoughResources;
                destroyIcon.gameObject.SetActive(false);
                actionType = ActionType.Construct;
            }
            else if (buildingConstructor.CanDestroyBuilding(coordinate))
            {
                createDestroyButton.gameObject.SetActive(true);
                createDestroyButton.interactable = true;
                destroyIcon.gameObject.SetActive(true);
                actionType = ActionType.Destroy;
            }
            else
            {
                createDestroyButton.gameObject.SetActive(false);
                actionType = ActionType.None;
            }
        }
    }
}