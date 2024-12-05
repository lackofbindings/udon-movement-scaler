
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;
using System;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class MovementScaler : UdonSharpBehaviour
{
    private float defaultRunSpeed = 0;
    private float defaultWalkSpeed = 0;
    private float defaultStrafeSpeed = 0;
    private float defaultJumpImpulse = 0;
    private float scaleFactor = 1;
    public float maxFactor = 2;
    public float minFactor = 0.5f;
    public bool includeJump = false;
    public TMP_Text debugOutput;
    private VRCPlayerApi localPlayer;

    void Start()
    {

    }

    // Must use OnPlayerJoined instead of Start so VRCWorld has time to apply defaults
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            // Record default values
            localPlayer = player;
            defaultWalkSpeed = player.GetWalkSpeed();
            defaultRunSpeed = player.GetRunSpeed();
            defaultStrafeSpeed = player.GetStrafeSpeed();
            defaultJumpImpulse = player.GetJumpImpulse();

            // Update the debug display
            if (Utilities.IsValid(debugOutput))
            {
                // ClientSim may get stuck here until scale is changed since they aren't simulating an avatar.
                debugOutput.text = "Got defaults, waiting for first run";
            }
        }
    }

    public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
    {
        if (player.isLocal)
        {
            ApplyScalings();
        }
    }

    private void OnEnable()
    {
        ApplyScalings();
    }

    private void OnDisable()
    {
        if (Utilities.IsValid(localPlayer))
        {
            // Reset to defaults when disabled
            localPlayer.SetWalkSpeed(defaultWalkSpeed);
            localPlayer.SetRunSpeed(defaultRunSpeed);
            localPlayer.SetStrafeSpeed(defaultStrafeSpeed);
            localPlayer.SetJumpImpulse(defaultJumpImpulse);
        }
    }

    private void ApplyScalings()
    {
        if (Utilities.IsValid(localPlayer))
        {
            if (localPlayer.GetAvatarEyeHeightAsMeters() != 0)
            {
                // Calculate where current height lands on range between min and max allowed
                // (ie. Convert eye height as meters into eye height as factor)
                float normalizedHeight = Mathf.InverseLerp(localPlayer.GetAvatarEyeHeightMinimumAsMeters(), localPlayer.GetAvatarEyeHeightMaximumAsMeters(), localPlayer.GetAvatarEyeHeightAsMeters());
                // Re-map our range to lie between the configurable min and max multipliers
                scaleFactor = Mathf.Lerp(minFactor, maxFactor, normalizedHeight);
                // Multiply default speeds by new scale factor
                localPlayer.SetWalkSpeed(defaultWalkSpeed * scaleFactor);
                localPlayer.SetRunSpeed(defaultRunSpeed * scaleFactor);
                localPlayer.SetStrafeSpeed(defaultStrafeSpeed * scaleFactor);
                if (includeJump && defaultJumpImpulse != 0)
                {
                    localPlayer.SetJumpImpulse(defaultJumpImpulse * scaleFactor);
                }
                // Update the debug display
                if (Utilities.IsValid(debugOutput))
                {
                    debugOutput.text = "Scale Factor: " + scaleFactor;
                }
            }
        }
    }
}
