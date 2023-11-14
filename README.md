# MotionInput-UnityLibrary

## Architectural Design

The implementation of this library is organized into three types of scripts:

1. [Interaction scripts](./Assets/Documentation/InteractionScriptsReadme.md)
2. [Effect scripts](./Assets/Documentation/EffectScriptsReadme.md)
3. [General scripts](./Assets/Documentation/GeneralScriptsReadme.md)

This design was chosen to facilitate the creation of a wide range of interactions and effects, allowing virtually any interaction to trigger any effect.

## General Usage

To quickly get started, follow these steps:

1. Open the 'Demo' scene located at `Assets/Scenes/Demo.unity`.

2. Create a 3D object in the scene.

3. Attach one of the interaction scripts to that object and adjust the settings according to your use case.

4. Attach any of the effect scripts to the same object and adjust their settings to fit your use case.

By default, all interactions effects on any GameObject will fetch all the effects on that GameObject and play them during interaction. However, this behavior can be customized.

The `Handpick Effects` boolean on any interaction effect allows you to specify a particular interaction effect to be played on that GameObject. Set the value of the boolean to `True` and then add the effects you want to be played to the `Handpicked Effects` list.

Most effects have fallback properties that they load at runtime from the resources folder. However, these fallback effects may not be ideal for your scenario. It is recommended to configure the required components in the scene beforehand. For example, if there is no `BrickGrid` object in the scene when played, one will be created at runtime. But it will have default properties, which may result in bricks not aligning properly.

---

_Note: Ensure you follow these guidelines for smooth integration and functioning of the library._
