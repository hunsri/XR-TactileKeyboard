# XR-TactileKeyboard
**A simple Unity package project for enabling key entries in XR environments via a 3D keyboard**</br>
Please note that this package is meant to be a baseline for own implementations.</br>
*Expect a limited feature set!*</br>
However, it can be used to facilitate basic keyboard inputs in XR.

## Installation
Download the Unity Package and install it into your project.</br>
Done!

## Demo Scene
A demo scene can be found under `Assets/XR-TactileKeyboard/Scenes`</br>
Upon entering you will be asked to install `Text Mesh Pro`</br>

With compatible controllers you can use the grip buttons to control the finger pose! 

## The project depends on the following packages:

| Unity Package | tested version* |
| -------- | -------- |
| ```com.unity.xr.interaction.toolkit```   | `2.4.3`   |
| ```com.unity.xr.management```   | `4.4.0`   |
| ```com.unity.xr.openxr```   | `1.8.2`   |
*the version used in the latest build

**Important:**</br>
* make sure the starter assets for the `com.unity.xr.interaction.toolkit` are present (you can install it via the Unity packet manager)
* install `Text Mesh Pro` into the project when asked during scene opening

## Layouts

Right now the only available mapping is German. </br>
However you can change the existing keys to a mapping of your choice!</br>
Add a new Map type under `Scripts\KeyboardScripts\KeyMaps\Mappings` and add it to the `KeyMappingType` enum. After that you simply need to reference it in the `KeyboardInfo` class.

## Functionality

**What it can do**</br>
Basic input, such as characters and their "shifted" variants.</br>

**What it can NOT do**</br>
Input that requires to press Control, Alt or any other tertiary keys.</br>
