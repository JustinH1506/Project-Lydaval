//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/+++Workdata/Scripts/Other/MenuControllerMap.inputactions
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

public partial class @MenuControllerMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuControllerMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuControllerMap"",
    ""maps"": [
        {
            ""name"": ""Pause_Inventory"",
            ""id"": ""1248b715-60b1-4ab6-b66c-1f2cfc8b15a0"",
            ""actions"": [
                {
                    ""name"": ""Inventroy"",
                    ""type"": ""Button"",
                    ""id"": ""228679b5-e358-469e-8711-4f22149a77cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1fbd1b7f-11d3-4d77-a670-913990016b95"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventroy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Pause_Inventory
        m_Pause_Inventory = asset.FindActionMap("Pause_Inventory", throwIfNotFound: true);
        m_Pause_Inventory_Inventroy = m_Pause_Inventory.FindAction("Inventroy", throwIfNotFound: true);
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

    // Pause_Inventory
    private readonly InputActionMap m_Pause_Inventory;
    private List<IPause_InventoryActions> m_Pause_InventoryActionsCallbackInterfaces = new List<IPause_InventoryActions>();
    private readonly InputAction m_Pause_Inventory_Inventroy;
    public struct Pause_InventoryActions
    {
        private @MenuControllerMap m_Wrapper;
        public Pause_InventoryActions(@MenuControllerMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Inventroy => m_Wrapper.m_Pause_Inventory_Inventroy;
        public InputActionMap Get() { return m_Wrapper.m_Pause_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Pause_InventoryActions set) { return set.Get(); }
        public void AddCallbacks(IPause_InventoryActions instance)
        {
            if (instance == null || m_Wrapper.m_Pause_InventoryActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Pause_InventoryActionsCallbackInterfaces.Add(instance);
            @Inventroy.started += instance.OnInventroy;
            @Inventroy.performed += instance.OnInventroy;
            @Inventroy.canceled += instance.OnInventroy;
        }

        private void UnregisterCallbacks(IPause_InventoryActions instance)
        {
            @Inventroy.started -= instance.OnInventroy;
            @Inventroy.performed -= instance.OnInventroy;
            @Inventroy.canceled -= instance.OnInventroy;
        }

        public void RemoveCallbacks(IPause_InventoryActions instance)
        {
            if (m_Wrapper.m_Pause_InventoryActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPause_InventoryActions instance)
        {
            foreach (var item in m_Wrapper.m_Pause_InventoryActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Pause_InventoryActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Pause_InventoryActions @Pause_Inventory => new Pause_InventoryActions(this);
    public interface IPause_InventoryActions
    {
        void OnInventroy(InputAction.CallbackContext context);
    }
}
