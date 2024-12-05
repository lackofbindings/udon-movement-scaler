# Udon Movement Scaler

An Udon prefab for VRChat that scales player walk speed based on the player's height. This way small avatars don't feel like they are moving super twitchy and fast, and big avatars don't feel like they're moving slow and sluggish.

## Setup

### Dependencies

- Unity 2022.3.22f1 (or latest supported version).
- VRC SDK 3.2.2 or later (minimum version to support Avatar Scaling).

Any dependencies should be installed automatically by the VCC when you add this package to your project.

### Install

1. Go to the [VPM Listing](https://lackofbindings.github.io/udon-movement-scaler/) for this repo and hit "Add to VCC".
   
   - If the "Add to VCC" button does not work you can manually enter the following url into the Packages settings page of the VCC `https://lackofbindings.github.io/udon-movement-scaler/index.json` 

   - If you do not have access to the VCC, there are also unitypackage versions available in the [Releases](https://github.com/lackofbindings/udon-movement-scaler/releases/latest).

2. Once you have the repo added to your VCC, you can add the Udon Movement Scaler package to your project from the Mange Project screen.

### Setup

1. Ensure that the VRC SDK including U# is set up and working in your project before proceeding.

2. To install just drag the `Movement Scaler` prefab anywhere into your scene. 

      - If a window pops up asking you about Text Mesh Pro just click the first button and wait for it to complete.

- If you don't want the debug display it is safe to remove it or set it to EditorOnly.

- If you want to toggle it in-game it is safe to use any script/animation to toggle the enabled state on the root game object.

- ⚠️ Does not work in Client Sim, may throw errors. (tested in ClientSim v1.2.6)

- There are a few configuration variables exposed:
  - `maxFactor`: When the player is at their maximum allowed scale their movement speed will be multiplied by this value.
  - `minFactor`: When the player is at their minimum allowed scale their movement speed will be multiplied by this value.
  - `includeJump`: If enabled, the script will also adjust the player's jump height.
  - `debugOutput` (optional): A reference to a Text Mesh Pro text component to display debug information.
