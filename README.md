# Custom Keyboard LED Controller for Unity and Corsair iCUE

![image](https://user-images.githubusercontent.com/58029218/227429613-d6d5b7ea-18f3-452c-8bd5-fa7ed9e027b1.png)

This project is a custom keyboard LED controller for Unity and Corsair iCUE-compatible devices. It enables users to change the background color of their Corsair keyboard using RGB sliders, apply a random color to individual keys when pressed, and toggle a "crazy mode" that continuously cycles the background color through random colors.

## Features

- **RGB Sliders**: Adjust the Red, Green, and Blue sliders to set the background color of the keyboard.
- **Key Press Color**: When a key is pressed, its LED color is changed to a random color.
- **Crazy Mode**: When enabled, the background color of the keyboard cycles through random colors continuously.
- **Reset Button**: Resets all custom key colors to the background color.

## Requirements

- Unity version 2020.3.0f1 or later.
- A Corsair iCUE-compatible keyboard connected to your computer.

## Dependencies

This project uses the Corsair Utility Engine SDK (CUESDK) to communicate with Corsair iCUE-compatible devices. The SDK can be found in the Plugins folder of the project.
