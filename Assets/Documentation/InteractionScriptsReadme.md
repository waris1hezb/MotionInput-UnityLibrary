## Interactions Readme

This README details all the interaction scripts in the library.

There is a base setup for effects where every interaction extends the [BaseInteraction](GeneralScriptsReadme.md#baseinteractioncs) class. This setup gives each interaction the ability to cherry pick effects or pick all based on the boolean value of serialised 'handpickEffects' property. The BaseInteraction class also gives provides two methods EnableAllEffects(bool temp) and DisableAllEffects(). The temp argument in the EnableAllEffects method allows for interactions that are temporary to play the effects for a preconfigured amount of time as specified in [BaseEffect](GeneralScriptsReadme.md#baseeffectcs) class set by default to 0.5 seconds. For example the [ClickInteraction](#clickinteractioncs) sets temp to true.

Some interactions might have serialised properties as well such as the [FocusInteraction](#focusinteractioncs) which has the float property 'hoverDuration'. The BaseInteraction script is easily extendable to create new forms of interactions.

## HoverInteraction.cs

### Description

The `HoverInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for enabling and disabling effects associated with mouse hover events.

### Methods

- **`OnMouseEnter()`**

  - Description: This method is called when the mouse pointer enters the GameObject's collider. It enables all effects associated with the interaction.
  - Behavior:
    - Calls the `EnableAllEffects()` method inherited from the `BaseInteraction` class.

- **`OnMouseExit()`**
  - Description: This method is called when the mouse pointer exits the GameObject's collider. It disables all effects associated with the interaction.
  - Behavior:
    - Calls the `DisableAllEffects()` method inherited from the `BaseInteraction` class.

### Usage

Attach this script to a GameObject in the scene that you want to have hover interactions. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the hover events to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the mouse pointer enters the collider of the GameObject, the `OnMouseEnter()` method is called, enabling all associated effects. Conversely, when the mouse pointer exits the collider, the `OnMouseExit()` method is called, disabling the effects.

## ClickInteraction.cs

### Description

The `ClickInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for enabling effects associated with a mouse click event.

### Methods

- **`OnMouseDown()`**
  - Description: This method is called when the user presses the mouse button while over the GameObject. It enables all effects associated with the interaction, marking them as temporary.
  - Behavior:
    - Calls the `EnableAllEffects(temp: true)` method inherited from the `BaseInteraction` class.

### Usage

Attach this script to a GameObject in the scene that you want to have click interactions. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the click event to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the user presses the mouse button while over the GameObject, the `OnMouseDown()` method is called, enabling all associated effects with a temporary flag.

## DoubleClickInteraction.cs

### Description

The `DoubleClickInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for enabling effects associated with a double-click event.

### Fields

- **`_doubleClickStart`** (Type: `float`): Records the time when a click event starts.

### Methods

- **`OnMouseUp()`**

  - Description: This method is called when the user releases the mouse button after a click. It checks the time between consecutive clicks to determine if it's a double-click event. If so, it triggers the `OnDoubleClick()` method.
  - Behavior:
    - If the time between consecutive clicks is less than 0.3 seconds, it's considered a double-click, and `OnDoubleClick()` is called.
    - If not, it records the time of the first click.

- **`OnDoubleClick()`**
  - Description: This method is called when a double-click event is detected. It enables all effects associated with the interaction, marking them as temporary.
  - Behavior:
    - Calls the `EnableAllEffects(temp: true)` method inherited from the `BaseInteraction` class.

### Usage

Attach this script to a GameObject in the scene that you want to have double-click interactions. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the click events to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the user releases the mouse button after a click, the `OnMouseUp()` method is called. If a second click follows within 0.3 seconds, it's considered a double-click, and the `OnDoubleClick()` method is triggered, enabling all associated effects with a temporary flag.

## RotationInteraction.cs

### Description

The `RotationInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for rotating the GameObject in response to mouse input.

### Fields

- **`_speed`** (Type: `float`, Serialised): The speed at which the GameObject rotates in response to mouse movement.

- **`_cam`** (Type: `Camera`): Reference to the main camera in the scene.

### Methods

- **`Awake()`**

  - Description: This method is called when the script instance is being loaded. It initializes the `_cam` variable with a reference to the main camera.

- **`OnMouseDown()`**

  - Description: This method is called when the user presses the mouse button while over the GameObject. It enables all effects associated with the interaction.

- **`OnMouseDrag()`**

  - Description: This method is called while the user holds down the mouse button and moves the mouse. It calculates the rotation based on mouse movement and updates the GameObject's rotation accordingly.
  - Behavior:
    - Calculates the rotation angles based on mouse movement using the `_speed` parameter.
    - Utilizes the camera's position and orientation to determine the rotation axis.
    - Updates the GameObject's rotation.

- **`OnMouseUp()`**
  - Description: This method is called when the user releases the mouse button after a drag operation. It disables all effects associated with the interaction.

### Usage

Attach this script to a GameObject in the scene that you want to have rotation interactions. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the mouse events to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the user presses the mouse button while over the GameObject, the `OnMouseDown()` method is called, enabling all associated effects. As the user drags the mouse, the `OnMouseDrag()` method calculates and applies the rotation. When the user releases the mouse button, the `OnMouseUp()` method is called, disabling all associated effects.

### Compatibility Note

The `RotationPunchEffect` is incompatible with this interaction. Using both scripts on the same GameObject may result in unexpected behavior.

## DragInteraction.cs

### Description

The `DragInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for dragging the GameObject in response to mouse input.

### Fields

- **`_cam`** (Type: `Camera`): Reference to the main camera in the scene.

- **`_offset`** (Type: `Vector3`): The offset between the mouse position and the center of the GameObject when dragging starts.

- **`_zCoord`** (Type: `float`): The z-coordinate of the screen space position of the GameObject.

- **`_lastPos`** (Type: `Vector3`): The last position of the GameObject before a drag operation.

### Methods

- **`Awake()`**

  - Description: This method is called when the script instance is being loaded. It initializes the `_cam` variable with a reference to the main camera.

- **`OnMouseDown()`**

  - Description: This method is called when the user presses the mouse button while over the GameObject. It calculates and stores the offset between the mouse position and the center of the GameObject.

- **`GetMouseWorldPos()`**

  - Description: Converts the mouse position from screen space to world space.

- **`OnMouseDrag()`**
  - Description: This method is called while the user holds down the mouse button and moves the mouse. It updates the position of the GameObject based on the mouse movement, triggers effects if the position changes, and disables effects if the position remains the same.

### Usage

Attach this script to a GameObject in the scene that you want to have drag interactions. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the mouse events to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the user presses the mouse button while over the GameObject, the `OnMouseDown()` method is called, storing the necessary information for dragging. As the user drags the mouse, the `OnMouseDrag()` method updates the position of the GameObject and triggers effects if the position changes. If the position remains the same, effects are disabled.

### Compatibility Note

The `PositionPunchEffect` is incompatible with this interaction. Using both scripts on the same GameObject may result in unexpected behavior.

## FocusInteraction.cs

### Description

The `FocusInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for triggering effects after a specified duration of continuous hovering over the GameObject.

### Fields

- **`_hoverDuration`** (Type: `float`, Serialised): The duration (in seconds) required for continuous hovering to trigger the focus.

- **`_isHovering`** (Type: `bool`): Indicates if the mouse pointer is currently over the GameObject.

- **`_isFocused`** (Type: `bool`): Indicates if the focus condition has been met.

- **`_hoverTimer`** (Type: `float`): Tracks the duration of continuous hovering.

### Methods

- **`OnMouseEnter()`**

  - Description: This method is called when the mouse pointer enters the collider of the GameObject. It sets `_isHovering` to true.

- **`OnMouseOver()`**

  - Description: This method is called while the mouse pointer is over the GameObject. It checks if the hover condition has been met and triggers the focus effect if so.
  - Behavior:
    - If `_isHovering` is true and `_isFocused` is false, it increments `_hoverTimer` by the time elapsed since the last frame.
    - If `_hoverTimer` exceeds `_hoverDuration`, it sets `_isFocused` to true and enables all associated effects.

- **`OnMouseExit()`**
  - Description: This method is called when the mouse pointer exits the collider of the GameObject. It resets all relevant variables and disables all effects.

### Usage

Attach this script to a GameObject in the scene that you want to have focus interactions. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the mouse events to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the mouse pointer enters the collider of the GameObject, the `OnMouseEnter()` method is called, setting `_isHovering` to true. While the mouse pointer remains over the GameObject, the `OnMouseOver()` method checks if the hover duration exceeds the specified threshold. If so, it triggers the focus effect. When the mouse pointer exits the collider of the GameObject, the `OnMouseExit()` method is called, resetting all relevant variables and disabling all effects.

## DragGridInteraction.cs

### Description

The `DragGridInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It provides functionality for dragging a GameObject in response to mouse input, with an associated "ghost" object that represents the potential final position.

### Fields

- **`_ghostTransparency`** (Type: `int`, Serialised, Range: 0 to 255): Specifies the transparency level of the ghost object.

- **`_cam`** (Type: `Camera`): Reference to the main camera in the scene.

- **`_offset`** (Type: `Vector3`): The offset between the mouse position and the center of the GameObject when dragging starts.

- **`_zCoord`** (Type: `float`): The z-coordinate of the screen space position of the GameObject.

- **`_lastPos`** (Type: `Vector3`): The last position of the GameObject before a drag operation.

- **`_ghost`** (Type: `GameObject`): The ghost object that represents the potential final position.

### Methods

- **`Awake()`**

  - Description: This method is called when the script instance is being loaded. It initializes the `_cam` variable with a reference to the main camera, and ensures that a `Grid` component is present in the scene.

- **`OnMouseDown()`**

  - Description: This method is called when the user presses the mouse button while over the GameObject. It calculates and stores the offset between the mouse position and the center of the GameObject.

- **`InitGhost()`**

  - Description: Initializes the ghost object by creating a new GameObject and adding necessary components (MeshRenderer, MeshFilter, GridMovementLock). It sets up the material and transparency of the ghost object.

- **`GetMouseWorldPos()`**

  - Description: Converts the mouse position from screen space to world space.

- **`OnMouseDrag()`**

  - Description: This method is called while the user holds down the mouse button and moves the mouse. It updates the position of the GameObject, enables effects, and activates the ghost object.

- **`OnMouseUp()`**
  - Description: This method is called when the user releases the mouse button. It deactivates the ghost object and sets the final position of the GameObject.

### Usage

Attach this script to a GameObject in the scene that you want to have drag interactions with a ghost representation. Ensure that the GameObject has a collider component (e.g., a BoxCollider, SphereCollider, etc.) for the mouse events to trigger. Additionally, make sure that this GameObject has a `BaseInteraction` or its derived class attached to it.

When the user presses the mouse button while over the GameObject, the `OnMouseDown()` method is called, storing the necessary information for dragging. As the user drags the mouse, the `OnMouseDrag()` method updates the position of the GameObject and activates the ghost object. When the user releases the mouse button, the `OnMouseUp()` method deactivates the ghost object and sets the final position of the GameObject.

### Requirements

- The project should contain the following scripts:
  - `GridMovementLock.cs`
  - `Grid.cs`
  - `Singleton.cs`

### Compatibility Note

The `PositionPunchEffect` is incompatible with this interaction. Using both scripts on the same GameObject may result in unexpected behavior.

## DragMagneticInteraction.cs

### Description

The `DragMagneticInteraction` script is a MonoBehaviour that extends the `BaseInteraction` class. It enables dragging a GameObject in response to mouse input. It also provides a "ghost" object for previewing the potential final position. This script includes functionality to interact with a magnetic grid and adjust the position accordingly.

### Requirements

- **Rigidbody Component**: The GameObject this script is attached to should have a Rigidbody component.

### Fields

- **Ghost Transparency** (_int_): Adjusts the transparency of the ghost object.

### Methods

- **Awake()**:

  - Overrides the base class method to initialize the script.
  - Sets up the main camera and initializes the ghost object.

- **OnMouseDown()**:

  - Handles mouse down event to initiate the dragging process.
  - Calculates the initial offset between the mouse position and the GameObject's position.

- **InitGhost()**:

  - Creates and initializes the ghost object.
  - Adds required components like MeshRenderer, MeshFilter, and MagneticMovementLock.
  - Sets up ghost object properties including material and transparency.

- **GetMouseWorldPos()**:

  - Calculates the world position of the mouse pointer.

- **OnMouseDrag()**:

  - Handles the mouse drag event to update the position of the GameObject.
  - Enables/disables effects and controls the visibility of the ghost object.

- **OnMouseUp()**:

  - Handles the mouse up event.
  - Deactivates the ghost object and finalizes the position.

- **OnTriggerEnter(Collider other)**:

  - Detects when the GameObject collides with other objects.
  - Specifically looks for objects with the BrickStats component.
  - Adjusts the magnetic lock based on the height of the brick.

- **OnTriggerExit(Collider other)**:
  - Detects when the GameObject is no longer colliding with an object.
  - Resets the magnetic lock.

### Usage

1. Attach this script to a GameObject with a Rigidbody component.
2. Ensure the project contains the following scripts:
   - GridMovementLock.cs
   - BrickStats.cs
   - BaseInteraction.cs
3. Set the desired Ghost Transparency value to control the visibility of the ghost object.
4. Interact with the GameObject using the mouse to drag it around the scene. The ghost object will preview the potential final position based on the magnetic grid interaction.

### Compatibility Note

The `PositionPunchEffect` is incompatible with this interaction. Using both scripts on the same GameObject may result in unexpected behavior.
