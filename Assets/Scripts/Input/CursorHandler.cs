using JetBrains.Annotations;
using Travian.Math;
using UnityEngine;
using UnityEngine.Events;

namespace Travian.Input
{
    public class CursorHandler : MonoBehaviour
    {
        public UnityEvent<HexCoordinate> OnCursorOn;
        public UnityEvent OnCursorOff;
        
        private HexCoordinate clickCoordinate;
        private bool isOn;

        public void OnEnable()
        {
            isOn = false;
        }

        [UsedImplicitly]
        public void OnTerrainClicked(HexCoordinate coordinate)
        {
            if (!isOn || coordinate != clickCoordinate)
            {
                clickCoordinate = coordinate;
                isOn = true;
                OnCursorOn?.Invoke(coordinate);
            }
            else if (isOn && coordinate == clickCoordinate)
            {
                isOn = false;
                OnCursorOff?.Invoke();
            }
        }
    }
}