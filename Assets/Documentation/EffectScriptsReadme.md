## Effects Readme

This README details all the effect scripts in the library.   
There is a base setup for effects where every effect except for camera specific effects namely [CameraMoveEffect](#camerazoomeffectcs) and [CameraZoomEffect](#camerazoomeffectcs) extends the BaseEffect class which in turn implements that IEffect interface. This setup makes certain that each effect has an implementation for EnableEffect and DisableEffect methods.  
Most effects have serialized properties that can be set as per requirement for example [GlowEffect](#gloweffectcs) has a float property 'intensity' which sets the intensity of the glow. All effects are self contained and are not interdependant and handle any errors gracefully. All effects are also marked with the Unity attribute '[DisallowMultipleComponent]' so only a single effect of the same type may be applicable on an object. 

## CursorSwitchEffect.cs

### Description
The `CursorSwitchEffect` script is a MonoBehaviour that implements the `IEffect` interface. It allows for switching the game cursor to a custom texture.

### Fields

- **`_cursorTexture`** (Type: `Texture2D`, Serialized): The custom texture to be used as the cursor.
- **`_hotspot`** (Type: `CursorHotspot`, Serialized): The hotspot position of the custom cursor.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It ensures that a `CursorManager` is available in the scene. If not, it creates one.
  - Behavior:
    - Checks if `CursorManager.Instance` is null.
    - If true, creates a new `GameObject` named "CursorManager" and attaches a `CursorManager` component to it.

- **`DisableEffect()`**
  - Description: Disables the custom cursor effect and restores the previous cursor.
  - Behavior:
    - Calls `CursorManager.Instance.RestorePreviousCursor(true)` to restore the previous cursor.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the custom cursor effect, using the specified texture and hotspot.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the cursor change is temporary or permanent.
  - Behavior:
    - Checks if `_cursorTexture` is not null.
    - If true, calls `CursorManager.Instance.SetCursor(...)` with a `CustomCursor` object created from `_cursorTexture` and `_hotspot`.

### Usage
Attach this script to a GameObject in the scene. Set the `_cursorTexture` field to the desired custom cursor texture, and optionally adjust the `_hotspot` field for cursor positioning. When the effect is enabled, it will replace the standard cursor with the custom one. When disabled, it restores the previous cursor.

### Requirements
- The `CursorManager` script for proper functionality.

### Example
```csharp
// Example usage in another script
void Start()
{
    CursorSwitchEffect cursorEffect = GetComponent<CursorSwitchEffect>();
    cursorEffect.EnableEffect();
}

void OnDestroy()
{
    CursorSwitchEffect cursorEffect = GetComponent<CursorSwitchEffect>();
    cursorEffect.DisableEffect();
}
```

## OutlineEffect.cs

### Description
The `OutlineEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for adding an outline effect to a GameObject.

### Fields

- **`_outlineMode`** (Type: `Outline.Mode`, Serialized): The mode of the outline effect. It determines when the outline is visible.
- **`_outlineColor`** (Type: `Color`, Serialized): The color of the outline.
- **`_outlineWidth`** (Type: `float`, Serialized): The width or thickness of the outline.

### Internal Fields

- **`_outline`** (Type: `Outline`): Reference to the `Outline` component added to the GameObject.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It sets up the outline effect by adding an `Outline` component and configuring its properties.
  - Behavior:
    - Creates and assigns an `Outline` component to the GameObject.
    - Sets the outline mode, color, and width based on the serialized fields.
    - Disables the outline effect initially.

- **`DisableEffect()`**
  - Description: Disables the outline effect, making it invisible.
  - Behavior:
    - Checks if `_outline` is not null.
    - If true, sets `_outline.enabled` to false.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the outline effect, making it visible.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the outline effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Checks if `_outline` is not null.
    - If true, sets `_outline.enabled` to true.

### Usage
Attach this script to a GameObject in the scene. Set the desired outline mode, color, and width using the serialized fields. When the effect is enabled, it adds a visible outline to the GameObject. When disabled, the outline becomes invisible.

### Requirements
- The free `Quick Outline` asset from the asset store https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488

### Example
```csharp
// Example usage in another script
void Start()
{
    OutlineEffect outlineEffect = GetComponent<OutlineEffect>();
    outlineEffect.EnableEffect();
}

void OnDestroy()
{
    OutlineEffect outlineEffect = GetComponent<OutlineEffect>();
    outlineEffect.DisableEffect();
}
```

## ColorSwitchEffect.cs

### Description
The `ColorSwitchEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for smoothly transitioning the color of a `Renderer` component using the DOTween animation library.

### Fields

- **`_duration`** (Type: `float`, Serialized): The duration of the color transition animation in seconds.
- **`_color`** (Type: `Color`, Serialized): The target color to transition to.

### Internal Fields

- **`_renderer`** (Type: `Renderer`): Reference to the `Renderer` component attached to the GameObject.
- **`_originalColor`** (Type: `Color`): Stores the original color of the `Renderer` material.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes the references to the `Renderer` and stores the original color of the material.
  - Behavior:
    - Gets the `Renderer` component attached to the GameObject.
    - Stores the original color of the material.

- **`DisableEffect()`**
  - Description: Disables the color switch effect by smoothly transitioning back to the original color.
  - Behavior:
    - Uses DOTween to animate the transition from the current color to the original color over the specified duration.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the color switch effect by smoothly transitioning to the target color.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the color switch is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Uses DOTween to animate the transition from the current color to the target color over the specified duration.

### Usage
Attach this script to a GameObject in the scene. Set the desired transition duration and target color using the serialized fields. When the effect is enabled, it smoothly transitions the color of the attached `Renderer` component. When disabled, it smoothly reverts the color back to its original state.

### Requirements
- The free `DOTween (HOTween v2)` asset from the asset store https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676

### Example
```csharp
// Example usage in another script
void Start()
{
    ColorSwitchEffect colorEffect = GetComponent<ColorSwitchEffect>();
    colorEffect.EnableEffect();
}

void OnDestroy()
{
    ColorSwitchEffect colorEffect = GetComponent<ColorSwitchEffect>();
    colorEffect.DisableEffect();
}
```

## GlowEffect.cs

### Description
The `GlowEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for adding a glowing effect to a GameObject by manipulating its emission property.

### Fields

- **`_intensity`** (Type: `float`, Serialized): The intensity of the glow effect.

### Internal Fields

- **`_renderer`** (Type: `Renderer`): Reference to the `Renderer` component attached to the GameObject.
- **`_originalIntensity`** (Type: `float`): Stores the original intensity of the emission.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes the references to the `Renderer` and sets up the emission property for the glow effect.
  - Behavior:
    - Gets the `Renderer` component attached to the GameObject.
    - Disables the "_EMISSION" keyword to start with no emission.
    - Sets the "_EmissionColor" property of the material to enhance the glow effect.

- **`DisableEffect()`**
  - Description: Disables the glow effect by turning off the emission property.
  - Behavior:
    - Disables the "_EMISSION" keyword, effectively turning off the emission property.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the glow effect by turning on the emission property.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the glow effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Enables the "_EMISSION" keyword, activating the emission property.

### Usage
Attach this script to a GameObject in the scene. Set the desired intensity of the glow effect using the serialized `_intensity` field. When the effect is enabled, it adds a glowing effect to the attached GameObject. When disabled, the glow effect is turned off.

### Requirements
- A global post-processing volume must be present in the scene.
- The post-processing volume should have a "Bloom" effect added to it.
- The main camera in the scene must have post-processing enabled to display the glow effect properly.

### Example
```csharp
// Example usage in another script
void Start()
{
    GlowEffect glowEffect = GetComponent<GlowEffect>();
    glowEffect.EnableEffect();
}

void OnDestroy()
{
    GlowEffect glowEffect = GetComponent<GlowEffect>();
    glowEffect.DisableEffect();
}
```

## TransparencyEffect.cs

### Description
The `TransparencyEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for changing the transparency of a GameObject's material.

### Fields

- **`_transparency`** (Type: `int`, Serialized): The transparency value, ranging from 0 to 255, where 0 is completely transparent and 255 is fully opaque.

### Internal Fields

- **`_renderer`** (Type: `Renderer`): Reference to the `Renderer` component attached to the GameObject.
- **`_originalMat`** (Type: `Material`): Stores the original material of the GameObject.
- **`_mat`** (Type: `Material`): The modified material with adjusted transparency.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes the references to the `Renderer` and sets up the materials for adjusting transparency.
  - Behavior:
    - Gets the `Renderer` component attached to the GameObject.
    - Loads a material named "mat_transparent" from the Resources folder and assigns it to `_mat`.
    - Adjusts the transparency of `_mat` based on the serialized `_transparency` field.
    
- **`DisableEffect()`**
  - Description: Disables the transparency effect by reverting the material to its original state.
  - Behavior:
    - Sets the `Renderer` material to `_originalMat`.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the transparency effect by applying the modified material with adjusted transparency.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the transparency effect is temporary or permanent.
  - Behavior:
    - Sets the `Renderer` material to `_mat`.
    - If `temp` is true, it temporarily enables the effect for 0.5 seconds before disabling it.

- **`TempEnable()`**
  - Description: Coroutine to temporarily enable the transparency effect.
  - Behavior:
    - Waits for 0.5 seconds and then calls `DisableEffect()`.

### Usage
Attach this script to a GameObject in the scene. Set the desired transparency level using the serialized `_transparency` field. When the effect is enabled, it adjusts the transparency of the attached GameObject's material. If `temp` is set to true, the effect will be temporary.

### Requirements
- A material named "mat_transparent" must be available in the Resources folder.
- The material "mat_transparent" should have its surface type set to "Opaque".

### Example
```csharp
// Example usage in another script
void Start()
{
    TransparencyEffect transparencyEffect = GetComponent<TransparencyEffect>();
    transparencyEffect.EnableEffect();
}

void OnDestroy()
{
    TransparencyEffect transparencyEffect = GetComponent<TransparencyEffect>();
    transparencyEffect.DisableEffect();
}
```

## ScalePunchEffect.cs

### Description
The `ScalePunchEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for applying a punching scale effect to a GameObject using the DOTween animation library.

### Fields

- **`_punch`** (Type: `float`, Serialized): The magnitude of the punch effect, ranging from 0.0 to 1.0.
- **`_duration`** (Type: `float`, Serialized): The duration of the punch animation in seconds.

### Internal Fields

- **`_initialScale`** (Type: `Vector3`): Stores the initial scale of the GameObject.
- **`_punchScale`** (Type: `Vector3`): Stores the scaled punch value, used for the effect.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes the `_initialScale` and `_punchScale` vectors based on the provided punch value.
  - Behavior:
    - Sets `_initialScale` to the local scale of the GameObject.
    - Calculates `_punchScale` as a scaled version of `_initialScale` with negative punch magnitude.

- **`DisableEffect()`**
  - Description: This method is intentionally left empty as this effect does not require a specific action for disabling.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the scale punch effect by animating the GameObject's scale using DOTween.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Uses DOTween to animate a punch scale effect on the GameObject over the specified duration.

### Usage
Attach this script to a GameObject in the scene. Set the desired punch magnitude and duration using the serialized fields. When the effect is enabled, it applies a punching scale animation to the attached GameObject.

### Requirements
- The free `DOTween (HOTween v2)` asset from the asset store https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676

### Example
```csharp
// Example usage in another script
void Start()
{
    ScalePunchEffect scalePunchEffect = GetComponent<ScalePunchEffect>();
    scalePunchEffect.EnableEffect();
}
```

## RotationPunchEffect.cs

### Description
The `RotationPunchEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for applying a punching rotation effect to a GameObject using the DOTween animation library.

### Fields

- **`_punch`** (Type: `Vector3`, Serialized): The amount of rotation to apply for the punch effect, specified as a Vector3.
- **`_duration`** (Type: `float`, Serialized): The duration of the punch animation in seconds.

### Internal Fields

- **`_initialRotation`** (Type: `Vector3`): Stores the initial rotation of the GameObject in Euler angles.
- **`_punchRotation`** (Type: `Vector3`): Stores the target punch rotation by adding `_punch` to `_initialRotation`.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes `_initialRotation` and `_punchRotation` based on the provided punch value.
  - Behavior:
    - Sets `_initialRotation` to the Euler angles of the GameObject's rotation.
    - Calculates `_punchRotation` by adding `_punch` to `_initialRotation`.

- **`DisableEffect()`**
  - Description: This method is intentionally left empty as this effect does not require a specific action for disabling.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the rotation punch effect by animating the GameObject's rotation using DOTween.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Uses DOTween to animate a punch rotation effect on the GameObject over the specified duration.

### Usage
Attach this script to a GameObject in the scene. Set the desired punch rotation and duration using the serialized fields. When the effect is enabled, it applies a punching rotation animation to the attached GameObject.

### Example
```csharp
// Example usage in another script
void Start()
{
    RotationPunchEffect rotationPunchEffect = GetComponent<RotationPunchEffect>();
    rotationPunchEffect.EnableEffect();
}
```

## PositionPunchEffect.cs

### Description
The `PositionPunchEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for applying a punching position effect to a GameObject using the DOTween animation library.

### Fields

- **`_punch`** (Type: `Vector3`, Serialized): The amount of position change to apply for the punch effect, specified as a Vector3.
- **`_duration`** (Type: `float`, Serialized): The duration of the punch animation in seconds.

### Internal Fields

- **`_initialPosition`** (Type: `Vector3`): Stores the initial position of the GameObject.
- **`_punchPosition`** (Type: `Vector3`): Stores the target punch position by adding `_punch` to `_initialPosition`.
- **`_tween`** (Type: `Tween`): Stores the active punch position tween.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes `_initialPosition` and `_punchPosition` based on the provided punch value.
  - Behavior:
    - Sets `_initialPosition` to the position of the GameObject.
    - Calculates `_punchPosition` by adding `_punch` to `_initialPosition`.

- **`DisableEffect()`**
  - Description: This method is intentionally left empty as this effect does not require a specific action for disabling.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the position punch effect by animating the GameObject's position using DOTween.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Checks if there is an active tween; if not, it initiates a punch position animation.
    - The animation is set to complete in the specified duration, and once completed, it nullifies the active tween.

### Usage
Attach this script to a GameObject in the scene. Set the desired punch position and duration using the serialized fields. When the effect is enabled, it applies a punching position animation to the attached GameObject.

### Example
```csharp
// Example usage in another script
void Start()
{
    PositionPunchEffect positionPunchEffect = GetComponent<PositionPunchEffect>();
    positionPunchEffect.EnableEffect();
}
```

## WireframeEffect.cs

### Description
The `WireframeEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for rendering a wireframe effect on a GameObject's mesh. The wireframe effect can be of different types, such as transparent, culled transparent, or solid.

### Fields

- **`wireframeType`** (Type: `WireframeType`, Serialized): Specifies the type of wireframe effect to apply. Options include TransparentWireframe, TransparentCulledWireframe, and SolidWireframe.
- **`_wireColor`** (Type: `Color`, Serialized): The color of the wireframe.
- **`_baseColor`** (Type: `Color`, Serialized): The base color of the material.
- **`_wireThickness`** (Type: `float`, Serialized, Range: 0 to 800): The thickness of the wireframe lines.
- **`_wireSmoothness`** (Type: `float`, Serialized, Range: 0 to 20): The smoothness of the wireframe lines.

### Internal Fields

- **`_wireframeMat`** (Type: `Material`): Stores the wireframe material.
- **`_renderer`** (Type: `Renderer`): Reference to the `Renderer` component attached to the GameObject.
- **`_originalMat`** (Type: `Material`): Stores the original material of the GameObject.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes references to the `Renderer` and the original material, and sets up the wireframe material.
  - Behavior:
    - Gets the `Renderer` component attached to the GameObject.
    - Initializes the `_originalMat` with the current material of the GameObject.
    - Calls `InitWireframeMaterial()` to set up the wireframe material.

- **`DisableEffect()`**
  - Description: Disables the wireframe effect by reverting the material to its original state.
  - Behavior:
    - Sets the `Renderer` material to `_originalMat`.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the wireframe effect by applying the wireframe material.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent.
  - Behavior:
    - Checks if the `_wireframeMat` is not null, and if so, sets the `Renderer` material to `_wireframeMat`.

- **`InitWireframeMaterial()`**
  - Description: Initializes the wireframe material based on the selected `wireframeType`.
  - Behavior:
    - Determines the material name based on `wireframeType` and loads it from the Resources folder.
    - Configures the wireframe material with specified colors, thickness, and smoothness.

### Usage
Attach this script to a GameObject in the scene. Customize the wireframe effect using the serialized fields. When the effect is enabled, it applies the selected wireframe effect to the attached GameObject.

### Requirements
- Ensure that the Resources folder contains the necessary wireframe materials (mat_wireframe_transparent, mat_wireframe_transparent_culled, and mat_wireframe_solid).
- Three shaders should be present in the project: WireframeShaded-Unlit, WireframeTransparent, and WireframeTransparentCulled.

### Example
```csharp
// Example usage in another script
void Start()
{
    WireframeEffect wireframeEffect = GetComponent<WireframeEffect>();
    wireframeEffect.EnableEffect();
}

void OnDestroy()
{
    WireframeEffect wireframeEffect = GetComponent<WireframeEffect>();
    wireframeEffect.DisableEffect();
}
```

## ColorFlickerEffect.cs

### Description
The `ColorFlickerEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for creating a flickering color effect on a GameObject's material using the DOTween animation library.

### Fields

- **`_duration`** (Type: `float`, Serialized): The duration of each flicker cycle in seconds.
- **`_flickerColor`** (Type: `Color`, Serialized): The color to flicker to.

### Internal Fields

- **`_renderer`** (Type: `Renderer`): Reference to the `Renderer` component attached to the GameObject.
- **`_originalColor`** (Type: `Color`): Stores the original color of the GameObject's material.
- **`_targetColor`** (Type: `Color`): The target color for the flicker effect.
- **`_flickerCoroutine`** (Type: `Coroutine`): Stores the reference to the flicker coroutine.
- **`_tween`** (Type: `Tween`): Stores the active color tween.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes references to the `Renderer` and stores the original material color.
  - Behavior:
    - Gets the `Renderer` component attached to the GameObject.
    - Stores the original color of the GameObject's material.

- **`DisableEffect()`**
  - Description: Disables the color flicker effect by stopping the flicker coroutine and restoring the original material color.
  - Behavior:
    - Checks if the flicker coroutine is running, if so, it stops it.
    - Kills the active color tween, if any.
    - Sets the `Renderer` material color back to the original color.
    - Resets internal variables.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the color flicker effect by starting the flicker coroutine.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Checks if the flicker coroutine is not already running, if not, it starts it.

- **`Flicker()`**
  - Description: Coroutine to handle the color flickering effect.
  - Behavior:
    - Loops indefinitely.
    - Tweens the material color to the target flicker color.
    - Waits for the specified duration.
    - Switches the target color between the flicker color and the original color.

### Usage
Attach this script to a GameObject in the scene. Set the desired flicker duration and color using the serialized fields. When the effect is enabled, it applies a flickering color animation to the attached GameObject.

### Example
```csharp
// Example usage in another script
void Start()
{
    ColorFlickerEffect colorFlickerEffect = GetComponent<ColorFlickerEffect>();
    colorFlickerEffect.EnableEffect();
}

void OnDestroy()
{
    ColorFlickerEffect colorFlickerEffect = GetComponent<ColorFlickerEffect>();
    colorFlickerEffect.DisableEffect();
}
```

## OutlineFlickerEffect.cs

### Description
The `OutlineFlickerEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for applying a flickering outline effect to a GameObject using the Outline component.

### Fields

- **`_outlineMode`** (Type: `Outline.Mode`, Serialized): The mode of the outline (e.g., OutlineVisible).
- **`_outlineColor`** (Type: `Color`, Serialized): The color of the outline.
- **`_flickerColor`** (Type: `Color`, Serialized): The color to flicker to.
- **`_outlineWidth`** (Type: `float`, Serialized): The width of the outline.
- **`_duration`** (Type: `float`, Serialized): The duration of each flicker cycle in seconds.

### Internal Fields

- **`_outline`** (Type: `Outline`): The Outline component attached to the GameObject.
- **`_originalColor`** (Type: `Color`): Stores the original outline color.
- **`_targetColor`** (Type: `Color`): The target color for the flicker effect.
- **`_flickerCoroutine`** (Type: `Coroutine`): Stores the reference to the flicker coroutine.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes the Outline component and sets initial parameters.
  - Behavior:
    - Adds the Outline component to the GameObject.
    - Sets the outline mode, color, width, and disables the outline.
    - Stores the original outline color.

- **`DisableEffect()`**
  - Description: Disables the outline flicker effect by stopping the flicker coroutine and restoring the original outline color.
  - Behavior:
    - Checks if the Outline component is present.
    - Disables the outline.
    - Stops the flicker coroutine if running.
    - Restores the original outline color.
    - Resets internal variables.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the outline flicker effect by starting the flicker coroutine.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Checks if the Outline component is present.
    - Enables the outline.
    - Checks if the flicker coroutine is not already running, if not, it starts it.

- **`Flicker()`**
  - Description: Coroutine to handle the outline flickering effect.
  - Behavior:
    - Loops indefinitely.
    - Sets the outline color to the target flicker color.
    - Waits for the specified duration.
    - Switches the target color between the flicker color and the original color.

### Usage
Attach this script to a GameObject in the scene. Customize the outline effect using the serialized fields. When the effect is enabled, it applies a flickering outline animation to the attached GameObject.

### Example
```csharp
// Example usage in another script
void Start()
{
    OutlineFlickerEffect outlineFlickerEffect = GetComponent<OutlineFlickerEffect>();
    outlineFlickerEffect.EnableEffect();
}

void OnDestroy()
{
    OutlineFlickerEffect outlineFlickerEffect = GetComponent<OutlineFlickerEffect>();
    outlineFlickerEffect.DisableEffect();
}
```

## ShadowSwitchEffect.cs

### Description
The `ShadowSwitchEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for dynamically changing the shadow casting mode of a GameObject's Renderer component.

### Fields

- **`_shadowMode`** (Type: `ShadowCastingMode`, Serialized): The shadow casting mode to switch to.

### Internal Fields

- **`_renderer`** (Type: `Renderer`): Reference to the `Renderer` component attached to the GameObject.
- **`_originalMode`** (Type: `ShadowCastingMode`): Stores the original shadow casting mode.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes references to the `Renderer` and stores the original shadow casting mode.
  - Behavior:
    - Gets the `Renderer` component attached to the GameObject.
    - Stores the original shadow casting mode of the Renderer.

- **`DisableEffect()`**
  - Description: Disables the shadow switch effect by setting the shadow casting mode back to its original value.
  - Behavior:
    - Sets the `Renderer` shadow casting mode to the original mode.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the shadow switch effect by setting the shadow casting mode to the specified value.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Sets the `Renderer` shadow casting mode to the specified `_shadowMode`.

### Usage
Attach this script to a GameObject in the scene. Set the desired shadow casting mode using the serialized field. When the effect is enabled, it dynamically changes the shadow casting mode of the attached GameObject's Renderer component.

### Example
```csharp
// Example usage in another script
void Start()
{
    ShadowSwitchEffect shadowSwitchEffect = GetComponent<ShadowSwitchEffect>();
    shadowSwitchEffect.EnableEffect();
}

void OnDestroy()
{
    ShadowSwitchEffect shadowSwitchEffect = GetComponent<ShadowSwitchEffect>();
    shadowSwitchEffect.DisableEffect();
}
```

## TextLabelEffect.cs

### Description
The `TextLabelEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for displaying a text label associated with a GameObject. The label can be customized through the serialized fields.

### Fields

- **`_text`** (Type: `TMP_Text`, Serialized): The TextMeshPro (TMP) text component used to display the label.
- **`_labelText`** (Type: `string`, Serialized): The text content of the label.

### Internal Fields

- **`_text`** (Type: `TMP_Text`): Reference to the TextMeshPro (TMP) text component used to display the label.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It sets up the text label based on the provided configuration.
  - Behavior:
    - Checks if `_text` is not referenced, if so, attempts to find the first TMP_Text component in the children.
    - If no TMP_Text component is found in children, it attempts to create one at runtime using a prefab named 'TextLabelEffect_LabelPrefab' from the Resources folder.
    - If successful, sets up the TMP_Text component with the specified label text and disables it.

- **`DisableEffect()`**
  - Description: Disables the text label effect by hiding the TMP_Text component.
  - Behavior:
    - Checks if `_text` is not null, if so, hides the TMP_Text component by setting its `enabled` property to false.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the text label effect by showing the TMP_Text component.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Checks if `_text` is not null, if so, shows the TMP_Text component by setting its `enabled` property to true.

### Usage
Attach this script to a GameObject in the scene. Customize the text label using the serialized fields. If a TMP_Text component is not provided or found in the children, it can be created at runtime using a prefab named 'TextLabelEffect_LabelPrefab' in the Resources folder. When the effect is enabled, it displays the label on the attached GameObject.

### Requirements
- Ensure that there is a prefab named 'TextLabelEffect_LabelPrefab' in the Resources folder for proper backup functioning.

### Example
```csharp
// Example usage in another script
void Start()
{
    TextLabelEffect textLabelEffect = GetComponent<TextLabelEffect>();
    textLabelEffect.EnableEffect();
}

void OnDestroy()
{
    TextLabelEffect textLabelEffect = GetComponent<TextLabelEffect>();
    textLabelEffect.DisableEffect();
}
```

## AudioPlaybackEffect.cs

### Description
The `AudioPlaybackEffect` script is a MonoBehaviour that implements the `IEffect` interface. It provides functionality for playing an AudioClip when the effect is enabled.

### Fields

- **`_clip`** (Type: `AudioClip`, Serialized): The audio clip to be played.

### Internal Fields

None

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It checks if an audio clip is provided. If not, it attempts to load a fallback audio clip from the Resources folder.
  - Behavior:
    - Checks if `_clip` is not null, if so, it doesn't perform any further action.
    - If `_clip` is null, it attempts to load a fallback audio clip named 'AudioPlaybackEffect_Fallback' from the Resources folder.

- **`DisableEffect()`**
  - Description: This method intentionally does nothing for this particular effect. It is included to implement the `IEffect` interface, but audio playback is not meant to be disabled once started.

- **`EnableEffect(bool temp = false)`**
  - Description: Enables the audio playback effect by playing the specified audio clip.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effect is temporary or permanent. (Note: Not used in this script)
  - Behavior:
    - Checks if `_clip` is not null, if so, it plays the audio clip at the position (0, 0, 0) using `AudioSource.PlayClipAtPoint`.

### Usage
Attach this script to a GameObject in the scene. Set the desired audio clip to be played using the serialized field. When the effect is enabled, it plays the specified audio clip.

### Example
```csharp
// Example usage in another script
void Start()
{
    AudioPlaybackEffect audioPlaybackEffect = GetComponent<AudioPlaybackEffect>();
    audioPlaybackEffect.EnableEffect();
}
```

## CameraMoveEffect.cs

### Description
The `CameraMoveEffect` script is a MonoBehaviour that enables the application of a vignette effect using Universal Render Pipeline's Post Processing. It allows for smoothly transitioning the intensity of the vignette effect on the camera.

### Fields
- **Post Process Volume** (_Volume_):
    - The Volume component used for applying post-processing effects. If not provided, the script will attempt to find one in the scene.
- **Intensity** (_float_):
    - A value between -1 and 1 representing the intensity of the vignette effect.
- **Duration** (_float_):
    - The time in seconds it takes for the intensity to transition from its current value to the target value.

### Methods
- **Awake()**:
    - Initializes the script by obtaining the necessary references, such as the Post Process Volume and the Vignette effect.

- **DisableEffect()**:
    - Gradually reduces the intensity of the vignette effect to zero, effectively disabling it.

- **EnableEffect()**:
    - Gradually increases the intensity of the vignette effect to the specified value, enabling it.

- **LerpIntensity(float targetIntensity)**:
    - Interpolates the intensity of the vignette effect over time to smoothly transition from the current value to the target value.

### Usage
1. Attach this script to a GameObject in the scene.
2. Assign a Post Process Volume to the `_postProcessVolume` field in the Inspector. If none is provided, the script will attempt to find one automatically.
3. Set the desired `Intensity` value to control the strength of the vignette effect.
4. Adjust the `Duration` to determine how quickly the effect should transition.

### Note
- Ensure that the Universal Render Pipeline (URP) is correctly set up in the project for the post-processing effects to work.

### Example
```csharp
// Example usage in another script
void Start()
{
    CameraMoveEffect cameraMoveEffect = GetComponent<CameraMoveEffect>();
    cameraMoveEffect.EnableEffect();
}

void OnDestroy()
{
    CameraMoveEffect cameraMoveEffect = GetComponent<CameraMoveEffect>();
    cameraMoveEffect.DisableEffect();
}
```

## CameraZoomEffect.cs

### Description
The `CameraZoomEffect` script is a MonoBehaviour that provides functionality for applying a lens distortion effect to the camera using Universal Render Pipeline's Post-Processing Stack.

### Fields

- **`_postProcessVolume`** (Type: `Volume`, Serialized): The Post-Processing Volume component that contains the lens distortion effect.
- **`_intensity`** (Type: `float`, Range: [-1.0f, 1.0f], Serialized): The intensity of the lens distortion effect.
- **`_duration`** (Type: `float`, Serialized): The duration over which the intensity of the lens distortion effect is changed.

### Internal Fields

- **`_lensDistortion`** (Type: `LensDistortion`): Reference to the Lens Distortion effect in the Post-Processing Profile.

### Methods

- **`Awake()`**
  - Description: This method is called when the script instance is being loaded. It initializes the `_lensDistortion` effect and disables it.

- **`EnableEffect(int direction)`**
  - Description: Enables the lens distortion effect by lerping its intensity to the specified value over a specified duration. The direction parameter determines whether to zoom in (negative value) or out (positive value).
  - Parameters:
    - **`direction`** (Type: `int`): The direction of the zoom effect.

- **`DisableEffect()`**
  - Description: Disables the lens distortion effect by lerping its intensity to 0 over a specified duration.

- **`LerpIntensity(float targetIntensity)`**
  - Description: Lerps the intensity of the lens distortion effect from its current value to the target intensity over a specified duration.

### Usage
Attach this script to a GameObject in the scene. Assign a Post-Processing Volume to the `_postProcessVolume` field. Adjust the intensity and duration parameters to control the lens distortion effect. When the effect is enabled, it applies the lens distortion effect to the camera.

### Example
```csharp
// Example usage in another script
void Start()
{
    CameraZoomEffect cameraZoomEffect = GetComponent<CameraZoomEffect>();
    cameraZoomEffect.EnableEffect(-1); // Zoom in
}

void OnDestroy()
{
    CameraZoomEffect cameraZoomEffect = GetComponent<CameraZoomEffect>();
    cameraZoomEffect.DisableEffect();
}
```