# Custom Keyboard LED Controller for Unity and Corsair iCUE

![image](https://user-images.githubusercontent.com/58029218/227429613-d6d5b7ea-18f3-452c-8bd5-fa7ed9e027b1.png)

This project is a custom keyboard LED controller for Unity and Corsair iCUE-compatible devices. It enables users to change the background color of their Corsair keyboard using RGB sliders, apply a random color to individual keys when pressed, and toggle a "crazy mode" that continuously cycles the background color through random colors.

## Features

- **RGB Sliders**: Adjust the Red, Green, and Blue sliders to set the background color of the keyboard.
- **Key Press Color**: When a key is pressed, its LED color is changed to a random color.
- **Crazy Mode**: When enabled, the background color of the keyboard cycles through random colors continuously.
- **Reset Button**: Resets all custom key colors to the background color.

## Usage

1. Clone the repository and open the project in Unity.
2. Add the LED script to a GameObject in your scene.
3. Set up a UI with RGB sliders, a "Crazy Mode" button, and a "Reset" button, and link them to the respective public variables in the LED script.
4. Run the project and start interacting with the UI to control the LED colors on your Corsair iCUE-compatible keyboard.

## Requirements

- Unity version 2020.3.0f1 or later.
- A Corsair iCUE-compatible keyboard connected to your computer.

## Dependencies

This project uses the Corsair Utility Engine SDK (CUESDK) to communicate with Corsair iCUE-compatible devices. The SDK can be found in the Plugins folder of the project.
