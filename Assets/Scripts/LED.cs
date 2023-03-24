using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using CUESDK;
using UnityEngine.UI;
using TMPro;

public class LED : MonoBehaviour {
    public Slider sliderR;
    public Slider sliderG;
    public Slider sliderB;
    public Button crazyButtonBackground;
    public TMP_Text crazyButtonText;

    CorsairLedColor backgroundColor = new CorsairLedColor() { R = 0, G = 0, B = 255 };
    int deviceCount;
    bool crazyMode = false;
    public float duration = 0.5f;
    float lerpTime;
    Color targetColor;

    Dictionary<CorsairLedId, CorsairLedColor> keyColors = new Dictionary<CorsairLedId, CorsairLedColor>();

    private void Start() {
        CorsairLightingSDK.PerformProtocolHandshake();

        if (CorsairLightingSDK.GetLastError() != CorsairError.Success) {
            Debug.LogError("Failed to connect to iCUE");
            return;
        }

        CorsairLightingSDK.RequestControl(CorsairAccessMode.ExclusiveLightingControl);

        deviceCount = CorsairLightingSDK.GetDeviceCount();

        sliderB.value = 1;
        targetColor = Color.blue;
    }

    private void Update() {
        // Handle key presses and update custom key colors
        HandleKeyPressed();

        // Update background color based on slider values and crazy mode
        if (!crazyMode) {
            backgroundColor.R = (int)(sliderR.value * 255);
            backgroundColor.G = (int)(sliderG.value * 255);
            backgroundColor.B = (int)(sliderB.value * 255);
        }
        else {
            lerpTime += Time.deltaTime;

            if (lerpTime >= duration) {
                lerpTime = 0;
                targetColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            }

            Color lerpedColor = Color.Lerp(new Color(sliderR.value, sliderG.value, sliderB.value), targetColor, lerpTime / duration);
            sliderR.value = lerpedColor.r;
            sliderG.value = lerpedColor.g;
            sliderB.value = lerpedColor.b;
            backgroundColor.R = (int)(sliderR.value * 255);
            backgroundColor.G = (int)(sliderG.value * 255);
            backgroundColor.B = (int)(sliderB.value * 255);
        }

        // Update device colors based on custom key colors and background color
        SetDeviceColors();

        // Update the color of the crazy button background
        UpdateCrazyButtonBackgroundColor();
    }

    // Handle key press events and set custom key colors
    private void HandleKeyPressed() {
        if (Input.anyKeyDown) {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(key)) {
                    CorsairLedColor randomColor = new CorsairLedColor() {
                        R = (byte)UnityEngine.Random.Range(0, 256),
                        G = (byte)UnityEngine.Random.Range(0, 256),
                        B = (byte)UnityEngine.Random.Range(0, 256)
                    };
                    SetKeyPressedColor(key, randomColor);
                }
            }
        }
    }

    // Set custom color for the pressed key
    private void SetKeyPressedColor(KeyCode keyCode, CorsairLedColor color) {
        CorsairLedId ledId = KeyMapping.GetKeyCodeLedIdMapping(keyCode);
        if (ledId != CorsairLedId.Invalid) {
            keyColors[ledId] = color;
        }
    }

    // Set colors for all devices
    private void SetDeviceColors() {
        for (var i = 0; i < deviceCount; i++) {
            var deviceLeds = CorsairLightingSDK.GetLedPositionsByDeviceIndex(i);
            var buffer = new CorsairLedColor[deviceLeds.NumberOfLeds];
            for (var j = 0; j < deviceLeds.NumberOfLeds; j++) {
                CorsairLedId currentLedId = deviceLeds.LedPosition[j].LedId;
                if (keyColors.ContainsKey(currentLedId)) {
                    buffer[j] = keyColors[currentLedId];
                }
                else {
                    buffer[j] = backgroundColor;
                }
                buffer[j].LedId = currentLedId;
            }

            CorsairLightingSDK.SetLedsColorsBufferByDeviceIndex(i, buffer);
            CorsairLightingSDK.SetLedsColorsFlushBuffer();
        }
    }

    // Toggle crazy mode on and off
    public void ToggleCrazyMode() {
        crazyMode = !crazyMode;
        crazyButtonText.text = crazyMode ? "Stop Crazy Colors" : "Start Crazy Colors";
    }

    // Update the color of the crazy button background
    private void UpdateCrazyButtonBackgroundColor() {
        Color newColor = new Color(sliderR.value, sliderG.value, sliderB.value);
        crazyButtonBackground.GetComponent<Image>().color = newColor;
    }

    // Reset all custom key colors to the background color
    public void ResetKeyColors() {
        keyColors.Clear();
    }
}