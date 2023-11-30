## General Readme

This README details all the scripts of significance other than the interaction and effect scripts in the library.

These all are a bunch of varied scripts that each have their own importance as detailed in their description. Some are abstract classes that need to be implemented whilst others are standalone singletons that must persist as a single object in the scene.

## BaseInteraction.cs

### Description

The `BaseInteraction` script is an abstract MonoBehaviour class that serves as a base for interactable objects. It provides functionality for managing effects associated with the interaction.

### Fields

- **`handpickEffects`** (Type: `bool`): Determines whether effects are selected manually or obtained from components.
- **`_handpickedEffects`** (Type: `MonoBehaviour[]`, Serialized): An array of MonoBehaviour components representing handpicked effects. These effects will only take effect if `handpickEffects` is set to `true`.

### Internal Fields

- **`_effects`** (Type: `IEffect[]`): An array of components implementing the `IEffect` interface.

### Methods

- **`Awake()`**

  - Description: This method is called when the script instance is being loaded. It initializes the effects array based on the `handpickEffects` option.
  - Behavior:
    - If `handpickEffects` is `false`, it retrieves all components implementing the `IEffect` interface attached to the GameObject.
    - If `handpickEffects` is `true`, it populates the `_effects` array with manually selected effects.

- **`EnableAllEffects(bool temp = false)`**

  - Description: Enables all effects associated with the interaction.
  - Parameters:
    - **`temp`** (Type: `bool`, Default: `false`): Indicates whether the effects are temporary or permanent.

- **`DisableAllEffects()`**
  - Description: Disables all effects associated with the interaction.

### Usage

Inherit from this script to create specific interaction scripts for objects in the scene. Set the `handpickEffects` flag based on whether you want to manually select effects or automatically obtain them from components implementing the `IEffect` interface. Use the provided methods to enable or disable effects associated with the interaction.

### Example

```csharp
// Example usage in a derived interaction script
public class CustomInteraction : BaseInteraction
{
    void Start()
    {
        // Enable all effects (permanent)
        EnableAllEffects();
    }

    void OnDestroy()
    {
        // Disable all effects
        DisableAllEffects();
    }
}
```

## BaseEffect.cs

### Description

The `BaseEffect` script is an abstract MonoBehaviour class that serves as a base for defining various effects. It implements the `IEffect` interface and provides methods for enabling and disabling effects.

### Fields

- None

### Methods

- **`DisableEffect()`**

  - Description: This method is intended to disable the effect. It is marked as `virtual` and throws a `System.NotImplementedException` by default. This should be overridden in derived classes to implement specific disable behavior.

- **`EnableEffect(bool temp = false)`**

  - Description: Enables the effect. It accepts an optional `temp` parameter which indicates whether the effect is temporary or permanent. If `temp` is `true`, the effect will be enabled temporarily by calling `TempEnable()` as a coroutine.

- **`TempEnable()`**
  - Description: This is a coroutine method that enables the effect temporarily. It waits for 0.5 seconds before calling `DisableEffect()`.

### Usage

Inherit from this script to create specific effects for objects in the scene. Override the `DisableEffect()` method to implement custom disable behavior. Use the `EnableEffect(bool temp = false)` method to enable the effect, with the option to make it temporary.

### Example

```csharp
// Example usage in a derived effect script
public class CustomEffect : BaseEffect
{
    public override void DisableEffect()
    {
        // Custom disable behavior
    }

    void Start()
    {
        // Enable the effect permanently
        EnableEffect();
    }
}
```

## BrickStats.cs

### Description

The `BrickStats` script is a MonoBehaviour class used for configuring individual brick properties. Specifically, it provides a parameter to set the height of individual bricks, aiding in precise placement within the scene.

### Fields

- **`brickHeight`** (Type: `float`, Default: `0.0032f`)
  - Description: This field allows the user to specify the height of individual bricks. It serves as a reference value for accurately positioning bricks within the scene.
  - Tooltip:
    ```
    Plates = .0032 | Regular = .0096 | 2xRegular = .0192 | 3xRegular = .0288 | 4xRegular = .0384
    ```
    This tooltip provides guidance on commonly used brick heights, aiding in the selection of an appropriate value.

### Usage

Attach this script to individual brick GameObjects within the scene. Adjust the `brickHeight` parameter to match the specific dimensions of the brick being represented. This setting assists in precise placement and alignment of bricks.

### Example

```csharp
// Example usage in a script
void Start()
{
    // Set the height of this brick to match a regular-sized brick
    brickStats.brickHeight = 0.0096f;
}
```

## BrickGrid.cs

### Description

The `BrickGrid` script is a MonoBehaviour class that manages a grid system within the scene. It provides options to adjust the size, visualization, and placement of grid points for precise positioning of objects.

### Fields

- **`scaleAmount`** (Type: `float`, Default: `1f`)

  - Description: Multiplier for the size of the grid. Adjusting this value scales the entire grid.

- **`sizeX`, `sizeY`, `sizeZ`** (Type: `float`, Default: `1f`)

  - Description: Set values for the grid steps in each direction. A 1x1x1 grid makes each grid step 1 unit apart. Adjust these values individually for different effects.

- **`isGridRendered`** (Type: `bool`, Default: `false`)

  - Description: Determines whether the grid is visualized in the scene.

- **`pointType`** (Type: `PrimitiveType`, Default: `Cube`)

  - Description: Specifies the primitive type used to represent grid points.

- **`pointColor`** (Type: `Color`, Default: `Color.red`)

  - Description: Defines the color of the grid points.

- **`gridSizeX`, `gridSizeY`, `gridSizeZ`** (Type: `float`, Range: `0 - 20`, Default: `10`)

  - Description: Sets how far the grid is visualized in each direction. Be cautious not to create too many grid points, as it may cause performance issues.

- **`pointSize`** (Type: `float`, Range: `0 - 1`, Default: `0.25f`)
  - Description: Determines the size of the grid points.

### Methods

- **`GetNearestPointOnGrid(Vector3 position)`**

  - Description: Returns the nearest point on the grid to the provided position.

- **`Start()`**
  - Description: Initializes the grid system. Adjusts grid size and optionally renders the grid visually.

### Usage

Attach this script to a GameObject in the scene to manage the grid. Customize the grid parameters to suit the specific requirements of your project. The grid points can aid in precise object placement.

### Example

```csharp
// Example usage in a script
void Start()
{
    // Adjust the scale of the grid
    scaleAmount = 2f;

    // Enable grid visualization
    isGridRendered = true;

    // Customize grid parameters
    sizeX = 0.02f;
    sizeY = 0.02f;
    sizeZ = 0.02f;

    // Set grid point color
    pointColor = Color.blue;

    // Customize grid visualization range
    gridSizeX = 5;
    gridSizeY = 5;
    gridSizeZ = 5;

    // Adjust grid point size
    pointSize = 0.5f;
}
```

## CameraMove.cs

### Description

The `CameraMove` script is a MonoBehaviour class responsible for controlling the movement and rotation of the camera in a 3D environment. It allows the camera to be moved forward and backward, rotated, and enables/disables specific effects based on user input.

### Fields

- **`xSpeed`** (Type: `float`, Serialized, Default: `120.0f`)

  - Description: The speed at which the camera rotates around the Y-axis.

- **`ySpeed`** (Type: `float`, Serialized, Default: `120.0f`)

  - Description: The speed at which the camera rotates around the X-axis.

- **`maxSpeed`** (Type: `float`, Serialized, Default: `100f`)

  - Description: The maximum speed at which the camera can move.

- **`accelerationRate`** (Type: `float`, Serialized, Default: `10f`)

  - Description: The rate at which the camera accelerates when moving.

- **`decelerationRate`** (Type: `float`, Serialized, Default: `10f`)

  - Description: The rate at which the camera decelerates when stopping.

- **`targetSpeed`** (Type: `float`, Serialized, Default: `5f`)

  - Description: The target speed for the camera movement.

- **`LeftMouseButtonHeld`** (Type: `bool`)
  - Description: Indicates whether the left mouse button is held down. Enables specific effects based on user input.

### Methods

- **`Awake()`**

  - Description: Initializes references to `CursorSwitchEffect` and `CameraMoveEffect`.

- **`Start()`**

  - Description: Initializes the camera's starting rotation based on its current angles.

- **`GoForward()`**

  - Description: Initiates the coroutine for moving the camera forward.

- **`Stop()`**

  - Description: Stops all coroutines.

- **`GoForwardRoutine()`**

  - Description: Coroutine for continuously moving the camera forward at a controlled speed.

- **`Update()`**

  - Description: Updates the camera's position and rotation based on user input.

- **`EnableAllEffects()`**

  - Description: Enables specific effects when the left mouse button is held down.

- **`DisableAllEffects()`**
  - Description: Disables specific effects when the left mouse button is released.

### Usage

Attach this script to the camera GameObject in your scene. Customize the movement and rotation speeds to achieve the desired camera behavior. Additionally, assign appropriate effects to `_cursorSwitchEffect` and `_cameraMoveEffect` for enabling/disabling when needed.

## CameraZoom.cs

### Description

The `CameraZoom` script is a MonoBehaviour class responsible for controlling the zoom behavior of a camera in a 3D environment. It allows the camera to smoothly zoom in and out based on mouse scroll input. Additionally, it provides functionality to enable and disable specific effects associated with the zoom.

### Fields

- **`_movementTime`** (Type: `float`, Serialized, Default: `1.0f`)

  - Description: The duration, in seconds, it takes for the camera to reach the new zoom position.

- **`_speed`** (Type: `float`, Serialized, Default: `5.0f`)
  - Description: The speed at which the camera zooms in and out.

### Internal Fields

- **`_cam`** (Type: `Camera`): Reference to the main Camera in the scene.
- **`_cameraZoomEffect`** (Type: `CameraZoomEffect`): Reference to the `CameraZoomEffect` component for handling zoom effects.
- **`_newZoom`** (Type: `Vector3`): Stores the new zoom position of the camera.
- **`_effectEnabled`** (Type: `bool`): Indicates whether zoom effects are currently enabled.
- **`_scrollDirection`** (Type: `int`): Represents the direction of the zoom (1 for zoom in, -1 for zoom out).
- **`_prevDirection`** (Type: `int`): Stores the previous zoom direction.

### Methods

- **`Awake()`**

  - Description: Initializes references to the main Camera and `CameraZoomEffect` component.

- **`LateUpdate()`**

  - Description: Handles the camera zoom behavior and enables/disables effects based on user input.

- **`HandleMouseInput()`**

  - Description: Handles the mouse scroll input to determine the zoom direction and calculate the new zoom position.

- **`EnableAllEffects(int direction)`**

  - Description: Enables specific effects associated with zooming. Receives the zoom direction as a parameter.

- **`DisableAllEffects()`**
  - Description: Disables all effects associated with zooming.

### Usage

Attach this script to the camera GameObject in your scene. Customize the `_movementTime` and `_speed` parameters to control the zoom behavior. Assign the `CameraZoomEffect` component to `_cameraZoomEffect` if zoom effects are desired.

## CursorManager.cs

### Description

The `CursorManager` script is a MonoBehaviour class responsible for managing custom cursors in a Unity application. It provides methods to set custom cursors, restore previous cursors, and toggle cursor lock.

### Enums

- **`CursorHotspot`**
  - Description: An enumeration representing different cursor hotspot options.
  - Values:
    - `Default`: The cursor's hotspot is at its default position.
    - `Center`: The cursor's hotspot is at the center.

### Classes

- **`CustomCursor`**

  - Description: A class representing a custom cursor with a texture and hotspot.

  - Fields:
    - `texture` (Type: `Texture2D`): The custom cursor's texture.
    - `hotspot` (Type: `CursorHotspot`): The hotspot position of the custom cursor.

### Fields

- **`Instance`** (Type: `CursorManager`, Static)
  - Description: A static reference to the current instance of the `CursorManager`.

### Methods

- **`Awake()`**

  - Description: Initializes the `CursorManager` and ensures there is only one instance of it in the scene.

- **`SetCursor(CustomCursor customCursor, bool lockCursor = false)`**

  - Description: Sets the cursor to a custom cursor with the specified texture and hotspot.

  - Parameters:
    - `customCursor` (Type: `CustomCursor`): The custom cursor to set.
    - `lockCursor` (Type: `bool`, Default: `false`): Indicates whether to lock the cursor.

- **`RestorePreviousCursor(bool releaseLock = false)`**

  - Description: Restores the previous cursor. Optionally, it can release the cursor lock.

  - Parameters:
    - `releaseLock` (Type: `bool`, Default: `false`): Indicates whether to release the cursor lock.

- **`PrintStack()`**
  - Description: Prints the contents of the cursor stack to the Unity console for debugging purposes.

### Usage

Attach this script to a GameObject in the scene. Use the `SetCursor` method to change the cursor to a custom cursor. The `RestorePreviousCursor` method can be used to revert to the previous cursor. Optionally, you can lock and release the cursor using the `lockCursor` parameter.

### Example

```csharp
// Example usage in a script
void Start()
{
    // Create a custom cursor
    CustomCursor customCursor = new CustomCursor { texture = customTexture, hotspot = CursorHotspot.Center };

    // Set the custom cursor and lock it
    CursorManager.Instance.SetCursor(customCursor, lockCursor: true);

    // Restore the previous cursor and release the lock
    CursorManager.Instance.RestorePreviousCursor(releaseLock: true);
}
```

## GridMovementLock.cs

### Description

The `GridMovementLock` script is a MonoBehaviour class responsible for locking the movement of a GameObject to the nearest point on a grid. It achieves this by using a reference point (specified by the `magnetic` Transform) and adjusting the GameObject's position accordingly.

### Fields

- **`magnetic`** (Type: `Transform`)
  - Description: A reference to the Transform that serves as the magnetic point for aligning with the grid.

### Methods

- **`Update()`**

  - Description: This method is called once per frame. It updates the position of the GameObject to snap it to the nearest point on the grid.

  - Behavior:
    - Retrieves the position of the `magnetic` Transform.
    - Calculates the nearest grid point using `BrickGrid.Instance.GetNearestPointOnGrid()`.
    - Sets the position of the GameObject to the calculated grid point.

### Usage

Attach this script to a GameObject in the scene. Assign a Transform (`magnetic`) that will act as the reference point for alignment with the grid. During each frame, the GameObject's position will be adjusted to snap to the nearest point on the grid based on the position of the `magnetic` Transform.

## MagneticMovementLock.cs

### Description

The `MagneticMovementLock` script is a MonoBehaviour class responsible for locking the movement of a GameObject to the nearest point on a grid while also adjusting its height based on specified parameters. It uses a reference point (specified by the `magnetic` Transform) and calculates the nearest grid point with an added height offset.

### Fields

- **`magnetic`** (Type: `Transform`)

  - Description: A reference to the Transform that serves as the magnetic point for aligning with the grid.

- **`height`** (Type: `float`)

  - Description: The additional height offset applied to the GameObject's position.

- **`otherY`** (Type: `float`)
  - Description: The Y-position used to determine the final height of the GameObject.

### Methods

- **`Update()`**

  - Description: This method is called once per frame. It updates the position of the GameObject to snap it to the nearest point on the grid, while also adjusting its height based on the specified parameters.

  - Behavior:
    - Retrieves the position of the `magnetic` Transform.
    - Sets the Y-position of the `pos` vector to `otherY + height`.
    - Calculates the nearest grid point using `BrickGrid.Instance.GetNearestPointOnGrid()`.
    - Sets the position of the GameObject to the calculated grid point.

### Usage

Attach this script to a GameObject in the scene. Assign a Transform (`magnetic`) that will act as the reference point for alignment with the grid. Specify the `height` and `otherY` parameters to adjust the final height of the GameObject. During each frame, the GameObject's position will be adjusted to snap to the nearest point on the grid with the specified height offset.

## Billboard.cs

### Description

The `Billboard` script is a MonoBehaviour that enables a GameObject to face the camera or align with the camera's forward direction. It provides options to lock rotations on specific axes.

### Fields

- **`_billboardType`** (Type: `BillboardType`, Serialized): Specifies the type of billboard behavior. Options include LookAtCamera and CameraForward.
- **`_lockX`** (Type: `bool`, Serialized): Locks rotation around the X-axis.
- **`_lockY`** (Type: `bool`, Serialized): Locks rotation around the Y-axis.
- **`_lockZ`** (Type: `bool`, Serialized): Locks rotation around the Z-axis.

### Internal Fields

- **`_cam`** (Type: `Camera`): Reference to the main Camera in the scene.
- **`_initialRotation`** (Type: `Vector3`): Stores the initial rotation of the GameObject.

### Methods

- **`Awake()`**

  - Description: This method is called when the script instance is being loaded. It initializes references to the Camera and stores the initial rotation of the GameObject.
  - Behavior:
    - Gets the main Camera in the scene.
    - Stores the initial rotation of the GameObject.

- **`LateUpdate()`**
  - Description: This method is called once per frame after all Update calls. It updates the rotation of the GameObject based on the selected billboard type and handles rotation locking.
  - Behavior:
    - Depending on the `_billboardType`, it either makes the GameObject face the camera or aligns it with the camera's forward direction.
    - If any rotation axis is locked (`_lockX`, `_lockY`, `_lockZ`), it applies the initial rotation value to that axis.

### Usage

Attach this script to a GameObject in the scene. Customize the billboard behavior and rotation locking using the serialized fields. When the scene is played, the GameObject will behave according to the specified options.
