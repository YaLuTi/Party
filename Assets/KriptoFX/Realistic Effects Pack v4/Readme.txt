My email is "kripto289@gmail.com"
You can contact me for any questions.

My English is not very good, and if there are any translation errors, you can let me know :)

Current readme only for HDRP rendering!

Pack includes prefabs of main effects (projectiles, aoe effect, etc) + collision effects (decals, particles, etc) + hand effects (like a particles attached to hands)

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Using on PC:

	Unity changed bloom in volume postprocessing. Right now the bloom works correctly with very big emission intencity (x100 times more) and ACES tonemaping.
	If you want effects which look like in the video you need use included postprocessing profile "Assets\KriptoFX\Realistic Effects Pack v4\PostEffects Profile.asset"

	In unity 2019.3+ added new "Threshold" parameter for bloom and you need change bloom settings (because default behaviour of bloom intencity was changed again):

	Threshold 1.5
	Intencity 0.5
	Scatter 0.9

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Using effects:

Simple using (without characters):

	1) Just drag and drop prefab of effect on scene and use that (for example, bufs or projectiles).

Using with characters and animations:

	You can see this video tutorial https://youtu.be/AKQCNGEeAaE

	1) You can use "animation events" for instantiating an effects in runtime using an animation. (I use this method in the demo scene)
	https://docs.unity3d.com/Manual/animeditor-AnimationEvents.html
	2) You need set the position and the rotation for an effects. I use hand bone position (or center position of arrow) and hand bone rotation.

For using effects in runtime, use follow code:

	"Instantiate(prefabEffect, position, rotation);"

Using projectile collision detection:

	Just add follow script on prefab of effect.

	void Start () {
        var physicsMotion = GetComponentInChildren<RFX4_PhysicsMotion>(true);
        if (physicsMotion != null) physicsMotion.CollisionEnter += CollisionEnter;

	    var raycastCollision = GetComponentInChildren<RFX4_RaycastCollision>(true);
        if(raycastCollision != null) raycastCollision.CollisionEnter += CollisionEnter;
    }

    private void CollisionEnter(object sender, RFX4_PhysicsMotion.RFX4_CollisionInfo e)
    {
        Debug.Log(e.HitPoint); //a collision coordinates in world space
        Debug.Log(e.HitGameObject.name); //a collided gameobject
        Debug.Log(e.HitCollider.name); //a collided collider :)
    }

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Effect modification:

All prefabs of effect have "EffectSetting" script with follow settings:

ParticlesBudget (range 0 - 1, default 1)
Allow change particles count of effect prefab. For example, particleBudget = 0.5 will reduce the number of particles in half

UseLightShadows
Some effect can use shadows and you can disable this setting for optimisation. Disabled by default for mobiles.

UseFastFlatDecals
If you use non-flat surfaces or  have z-fight problems you can use screen space decals instead of simple quad decals.
Disabled parameter will use screen space decals but it required depth texture!

UseCustomColor
You can override color of effect by HUE. (new color will used only in play mode)
If you want use black/white colors for effect, you need manualy change materials of effects.

IsVisible
Disable this parameter in runtime will smoothly turn off an effect.

FadeoutTime
Smooth turn off time


Follow physics settings visible only if type of effect is projectile

UseCollisionDetection
You can disable collision detection and an effect will fly through the obstacles.

LimitMaxDistance
Limiting the flight of effect (at the end the effect will just disappear)

Follow settings like in the rigidbody physics
Mass
Speed
AirDrag
UseGravity
