﻿using System.Collections.Generic;
using VRageMath;
using static WeaponThread.Session.ShieldDefinition;
using static WeaponThread.Session.EventTriggers;

namespace WeaponThread
{
    partial class Weapons
    {
        internal List<Session.WeaponDefinition> Weapon = new List<Session.WeaponDefinition>();
        internal void ConfigFiles(params Session.WeaponDefinition[] defs)
        {
            foreach (var def in defs) Weapon.Add(def);
        }

        internal Session.WeaponDefinition[] ReturnDefs()
        {
            var weaponDefinitions = new Session.WeaponDefinition[Weapon.Count];
            for (int i = 0; i < Weapon.Count; i++) weaponDefinitions[i] = Weapon[i];
            Weapon.Clear();
            return weaponDefinitions;
        }

        internal Session.ParticleOptions Options(bool loop, bool restart, float distance, float duration, float scale)
        {
            return new Session.ParticleOptions
            {
                Loop = loop,
                Restart = restart,
                MaxDistance = distance,
                MaxDuration = duration,
                Scale = scale, 
            };
        }

        internal Session.Detonate Options(bool detonateOnEnd, bool armOnlyOnHit, float detonationDamage, float detonationRadius)
        {
            return new Session.Detonate
            {
                DetonateOnEnd = detonateOnEnd,
                ArmOnlyOnHit = armOnlyOnHit,
                DetonationDamage = detonationDamage,
                DetonationRadius = detonationRadius,
            };
        }

        internal Session.Explosion Options(bool noVisuals, bool noSound, float scale, string customParticle, string customSound)
        {
            return new Session.Explosion
            {
                NoVisuals = noVisuals,
                NoSound = noSound,
                Scale = scale,
                CustomParticle = customParticle,
                CustomSound = customSound,
            };
        }

        internal Session.GridSizeDefinition Options(float largeGridModifier, float smallGridModifier)
        {
            return new Session.GridSizeDefinition { Large = largeGridModifier, Small = smallGridModifier };
        }

        internal Session.ObjectsHit Options(int maxObjectsHit, bool countBlocks)
        {
            return new Session.ObjectsHit { MaxObjectsHit = maxObjectsHit, CountBlocks = countBlocks };
        }

        internal Session.Shrapnel Options(float baseDamage, int fragments, float maxTrajectory, bool noAudioVisual, bool noGuidance, Session.Shrapnel.ShrapnelShape shape)
        {
            return new Session.Shrapnel { BaseDamage = baseDamage, Fragments = fragments, MaxTrajectory = maxTrajectory, NoAudioVisual = noAudioVisual, NoGuidance = noGuidance, Shape = shape};
        }

        internal Session.HeatingEmissive Options(bool enable)
        {
            return new Session.HeatingEmissive { Enable = enable};
        }

        internal Session.FiringEmissive Options(bool enable, int stages, Vector4 color)
        {
            return new Session.FiringEmissive { Enable = enable, Color = color };
        }

        internal Session.TrackingEmissive Options(bool enable, Vector4 color)
        {
            return new Session.TrackingEmissive { Enable = enable, Color = color };
        }

        internal Session.ReloadingEmissive Options(bool enable, Vector4 color, bool pulse)
        {
            return new Session.ReloadingEmissive { Enable = enable, Color = color };
        }

        internal Session.CustomScalesDefinition SubTypeIds(bool ignoreOthers, params Session.CustomBlocksDefinition[] customDefScale)
        {
            return new Session.CustomScalesDefinition {IgnoreAllOthers = ignoreOthers, Types = customDefScale};
        }

        internal Session.ArmorDefinition Options(float armor, float light, float heavy, float nonArmor)
        {
            return new Session.ArmorDefinition { Armor = armor, Light = light, Heavy = heavy, NonArmor = nonArmor };
        }

        internal Session.OffsetEffect Options(double maxOffset, double minLength, double maxLength)
        {
            return new Session.OffsetEffect { MaxOffset = maxOffset, MinLength = minLength, MaxLength = maxLength};
        }

        internal Session.ShieldDefinition Options(float modifier, ShieldType type)
        {
            return new Session.ShieldDefinition { Modifier = modifier, Type = type };
        }

        internal Session.ShapeDefinition Options(Session.ShapeDefinition.Shapes shape, double diameter)
        {
            return new Session.ShapeDefinition { Shape = shape, Diameter = diameter };
        }

        internal Session.Pulse Options(int interval, int pulseChance)
        {
            return new Session.Pulse { Interval = interval, PulseChance = pulseChance };
        }

        internal Session.EwarFields Options(int duration, bool stackDuration, bool depletable)
        {
            return new Session.EwarFields { Duration = duration, StackDuration = stackDuration, Depletable = depletable};
        }

        internal Session.TrailDefinition Options(bool enable, string material, int decayTime, Vector4 color)
        {
            return new Session.TrailDefinition { Enable = enable, Material = material, DecayTime = decayTime };
        }

        internal Session.CustomBlocksDefinition Block(string subTypeId, float modifier)
        {
            return new Session.CustomBlocksDefinition { SubTypeId = subTypeId, Modifier = modifier };
        }

        internal Session.TracerBaseDefinition Base(bool enable, float length, float width, Vector4 color)
        {
            return new Session.TracerBaseDefinition { Enable = enable, Length = length, Width = width, Color = color};
        }

        internal Session.AimControlDefinition AimControl(bool trackTargets, bool turretAttached, bool turretController, float rotateRate, float elevateRate)
        {
            return new Session.AimControlDefinition { TrackTargets = trackTargets, TurretAttached = turretAttached, TurretController = turretController, RotateRate = rotateRate, ElevateRate = elevateRate };
        }

        internal Session.UiDefinition Display(bool rateOfFire, bool damageModifier, bool toggleGuidance, bool enableOverload)
        {
            return new Session.UiDefinition { RateOfFire = rateOfFire, DamageModifier = damageModifier, ToggleGuidance = toggleGuidance, EnableOverload = enableOverload };
        }

        internal Session.TargetingDefinition.BlockTypes[] Priority(params Session.TargetingDefinition.BlockTypes[] systems)
        {
            return systems;
        }

        internal Session.TargetingDefinition.Threat[] Valid(params Session.TargetingDefinition.Threat[] threats)
        {
            return threats;
        }

        internal Session.Randomize Random(float start, float end)
        {
            return new Session.Randomize { Start = start, End = end };
        }

        internal Vector4 Color(float red, float green, float blue, float alpha)
        {
            return new Vector4(red, green, blue, alpha);
        }

        internal Vector3D Vector(double x, double y, double z)
        {
            return new Vector3D(x, y, z);
        }

        internal Session.MountPoint MountPoint(string subTypeId, string aimPartId, string muzzlePartId)
        {
            return new Session.MountPoint { SubtypeId = subTypeId, AimPartId = aimPartId, MuzzlePartId = muzzlePartId};
        }

        internal Session.EventTriggers[] Events(params Session.EventTriggers[] events)
        {
            return events;
        }

        internal Session.XYZ Transformation(double X, double Y, double Z)
        {
            return new Session.XYZ { x = X, y = Y, z = Z };
        }

        internal Dictionary<Session.EventTriggers, uint> Delays(uint FiringDelay = 0, uint ReloadingDelay = 0, uint OverheatedDelay = 0, uint TrackingDelay = 0, uint LockedDelay =0, uint OnDelay = 0, uint OffDelay = 0)
        {
            return new Dictionary<Session.EventTriggers, uint>
            {
                [Firing] = FiringDelay,
                [Reloading] = ReloadingDelay,
                [Overheated] = OverheatedDelay,
                [Tracking] = TrackingDelay,
                [Locked] = LockedDelay,
                [TurnOn] = OnDelay,
                [TurnOff] = OffDelay
            };
        }

        internal string[] Names(params string[] names)
        {
            return names;
        }
    }
}
