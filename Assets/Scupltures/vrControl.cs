//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.0
//     from Assets/Scupltures/vrControl.inputactions
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

public partial class @VrControl: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @VrControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""vrControl"",
    ""maps"": [
        {
            ""name"": ""VR"",
            ""id"": ""27301fc4-569a-421f-863a-804fac0f728f"",
            ""actions"": [
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""d8386e72-b387-4c13-a80f-f8924c51e9ae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d645f3e4-d533-4e72-8c57-21c0338294bf"",
                    ""path"": ""<XRController>{RightHand}/triggerButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // VR
        m_VR = asset.FindActionMap("VR", throwIfNotFound: true);
        m_VR_Primary = m_VR.FindAction("Primary", throwIfNotFound: true);
    }

    ~@VrControl()
    {
        UnityEngine.Debug.Assert(!m_VR.enabled, "This will cause a leak and performance issues, VrControl.VR.Disable() has not been called.");
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

    // VR
    private readonly InputActionMap m_VR;
    private List<IVRActions> m_VRActionsCallbackInterfaces = new List<IVRActions>();
    private readonly InputAction m_VR_Primary;
    public struct VRActions
    {
        private @VrControl m_Wrapper;
        public VRActions(@VrControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Primary => m_Wrapper.m_VR_Primary;
        public InputActionMap Get() { return m_Wrapper.m_VR; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VRActions set) { return set.Get(); }
        public void AddCallbacks(IVRActions instance)
        {
            if (instance == null || m_Wrapper.m_VRActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_VRActionsCallbackInterfaces.Add(instance);
            @Primary.started += instance.OnPrimary;
            @Primary.performed += instance.OnPrimary;
            @Primary.canceled += instance.OnPrimary;
        }

        private void UnregisterCallbacks(IVRActions instance)
        {
            @Primary.started -= instance.OnPrimary;
            @Primary.performed -= instance.OnPrimary;
            @Primary.canceled -= instance.OnPrimary;
        }

        public void RemoveCallbacks(IVRActions instance)
        {
            if (m_Wrapper.m_VRActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IVRActions instance)
        {
            foreach (var item in m_Wrapper.m_VRActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_VRActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public VRActions @VR => new VRActions(this);
    public interface IVRActions
    {
        void OnPrimary(InputAction.CallbackContext context);
    }
}
