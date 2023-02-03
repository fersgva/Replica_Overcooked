//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputActions/ScreenShot.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ScreenShot : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ScreenShot()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ScreenShot"",
    ""maps"": [
        {
            ""name"": ""Screenshot"",
            ""id"": ""cf9afeca-20d7-44a4-8292-7ea58106cd35"",
            ""actions"": [
                {
                    ""name"": ""ScreenAction"",
                    ""type"": ""Button"",
                    ""id"": ""b66dd0b2-0531-4f98-9146-21474b74685b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6d3c4895-cf8f-4b06-8e03-5f784fca6d36"",
                    ""path"": ""<Keyboard>/comma"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Screenshot
        m_Screenshot = asset.FindActionMap("Screenshot", throwIfNotFound: true);
        m_Screenshot_ScreenAction = m_Screenshot.FindAction("ScreenAction", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Screenshot
    private readonly InputActionMap m_Screenshot;
    private IScreenshotActions m_ScreenshotActionsCallbackInterface;
    private readonly InputAction m_Screenshot_ScreenAction;
    public struct ScreenshotActions
    {
        private @ScreenShot m_Wrapper;
        public ScreenshotActions(@ScreenShot wrapper) { m_Wrapper = wrapper; }
        public InputAction @ScreenAction => m_Wrapper.m_Screenshot_ScreenAction;
        public InputActionMap Get() { return m_Wrapper.m_Screenshot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ScreenshotActions set) { return set.Get(); }
        public void SetCallbacks(IScreenshotActions instance)
        {
            if (m_Wrapper.m_ScreenshotActionsCallbackInterface != null)
            {
                @ScreenAction.started -= m_Wrapper.m_ScreenshotActionsCallbackInterface.OnScreenAction;
                @ScreenAction.performed -= m_Wrapper.m_ScreenshotActionsCallbackInterface.OnScreenAction;
                @ScreenAction.canceled -= m_Wrapper.m_ScreenshotActionsCallbackInterface.OnScreenAction;
            }
            m_Wrapper.m_ScreenshotActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ScreenAction.started += instance.OnScreenAction;
                @ScreenAction.performed += instance.OnScreenAction;
                @ScreenAction.canceled += instance.OnScreenAction;
            }
        }
    }
    public ScreenshotActions @Screenshot => new ScreenshotActions(this);
    public interface IScreenshotActions
    {
        void OnScreenAction(InputAction.CallbackContext context);
    }
}
