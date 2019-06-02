using System;
using System.Collections.Generic;
using ProtoBuf;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Utils;
using VRageMath;

namespace WeaponThread
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation | MyUpdateOrder.AfterSimulation, int.MinValue + 1)]
    public partial class Session : MySessionComponentBase
    {
        public override void LoadData()
        {
            MyAPIGateway.Utilities.RegisterMessageHandler(7772, Handler);
            Init();
            SendModMessage();
        }

        protected override void UnloadData()
        {
            MyAPIGateway.Utilities.UnregisterMessageHandler(7772, Handler);
        }

        void Handler(object o)
        {
            if (o == null) SendModMessage();
        }

        void SendModMessage()
        {
            MyAPIGateway.Utilities.SendModMessage(7771, Storage);
        }

        internal byte[] Storage;
        internal void Init()
        {
            try
            {
                Storage = MyAPIGateway.Utilities.SerializeToBinary(WeaponDefinitions);
            }
            catch (Exception ex) {; }
        }

        [ProtoContract]
        public struct GraphicDefinition
        {
            public enum EffectType
            {
                Spark,
                Lance,
                Orb,
                Custom
            }
            [ProtoMember(1)] internal bool ShieldHitDraw;
            [ProtoMember(2)] internal bool ProjectileTrail;
            [ProtoMember(3)] internal bool ParticleTrail;
            [ProtoMember(4)] internal float ProjectileWidth;
            [ProtoMember(5)] internal float VisualProbability;
            [ProtoMember(6)] internal float ParticleRadiusMultiplier;
            [ProtoMember(7)] internal MyStringId ProjectileMaterial;
            [ProtoMember(8)] internal MyStringId ModelName;
            [ProtoMember(9)] internal Vector4 ProjectileColor;
            [ProtoMember(10)] internal Vector4 ParticleColor;
            [ProtoMember(11)] internal EffectType Effect;
            [ProtoMember(12)] internal string CustomEffect;
        }

        [ProtoContract]
        public struct AudioDefinition
        {
            [ProtoMember(1)] internal float AmmoTravelSoundRange;
            [ProtoMember(2)] internal float AmmoTravelSoundVolume;
            [ProtoMember(3)] internal float AmmoHitSoundRange;
            [ProtoMember(4)] internal float AmmoHitSoundVolume;
            [ProtoMember(5)] internal float ReloadSoundRange;
            [ProtoMember(6)] internal float ReloadSoundVolume;
            [ProtoMember(7)] internal float FiringSoundRange;
            [ProtoMember(8)] internal float FiringSoundVolume;
            [ProtoMember(9)] internal string AmmoTravelSound;
            [ProtoMember(10)] internal string AmmoHitSound;
            [ProtoMember(11)] internal string ReloadSound;
            [ProtoMember(12)] internal string FiringSound;
        }

        [ProtoContract]
        public struct TurretDefinition
        {
            [ProtoMember(1)] internal KeyValuePair<string, string>[] MountPoints;
            [ProtoMember(2)] internal string[] Barrels;
            [ProtoMember(3)] internal string DefinitionId;
            [ProtoMember(4)] internal bool TurretMode;
            [ProtoMember(5)] internal bool TrackTarget;
            [ProtoMember(6)] internal int RotateBarrelAxis;
            [ProtoMember(7)] internal int ReloadTime;
            [ProtoMember(8)] internal int RateOfFire;
            [ProtoMember(9)] internal int BarrelsPerShot;
            [ProtoMember(10)] internal int SkipBarrels;
            [ProtoMember(11)] internal int ShotsPerBarrel;
            [ProtoMember(12)] internal int HeatPerRoF;
            [ProtoMember(13)] internal int MaxHeat;
            [ProtoMember(14)] internal int HeatSinkRate;
            [ProtoMember(15)] internal int MuzzleFlashLifeSpan;
            [ProtoMember(16)] internal float RotateSpeed;
            [ProtoMember(17)] internal float DeviateShotAngle;
            [ProtoMember(18)] internal float ReleaseTimeAfterFire;

        }

        [ProtoContract]
        public struct AmmoDefinition
        {
            internal enum GuidanceType
            {
                None,
                Remote,
                Seeking,
                Lock,
                Smart
            }

            internal enum ShieldType
            {
                Bypass,
                Emp,
                Energy,
                Kinetic
            }

            [ProtoMember(1)] internal bool UseRandomizedRange;
            [ProtoMember(2)] internal bool RealisticDamage;
            [ProtoMember(3)] internal float Mass;
            [ProtoMember(4)] internal float Health;
            [ProtoMember(5)] internal float ProjectileLength;
            [ProtoMember(6)] internal float InitalSpeed;
            [ProtoMember(7)] internal float AccelPerSec;
            [ProtoMember(8)] internal float DesiredSpeed;
            [ProtoMember(9)] internal float SpeedVariance;
            [ProtoMember(10)] internal float MaxTrajectory;
            [ProtoMember(11)] internal float BackkickForce;
            [ProtoMember(12)] internal float RangeMultiplier;
            [ProtoMember(13)] internal float ThermalDamage;
            [ProtoMember(14)] internal float AreaEffectYield;
            [ProtoMember(15)] internal float AreaEffectRadius;
            [ProtoMember(16)] internal float ShieldDmgMultiplier;
            [ProtoMember(17)] internal float DefaultDamage;
            [ProtoMember(18)] internal ShieldType ShieldDamage;
            [ProtoMember(19)] internal GuidanceType Guidance;
        }

        [ProtoContract]
        public struct WeaponDefinition
        {
            [ProtoMember(1)] internal bool HasAreaEffect;
            [ProtoMember(2)] internal bool HasThermalEffect;
            [ProtoMember(3)] internal bool HasKineticEffect;
            [ProtoMember(4)] internal bool SkipAcceleration;
            [ProtoMember(5)] internal float KeenScaler;
            [ProtoMember(6)] internal float ComputedBaseDamage;
            [ProtoMember(7)] internal TurretDefinition TurretDef;
            [ProtoMember(8)] internal AmmoDefinition AmmoDef;
            [ProtoMember(9)] internal GraphicDefinition GraphicDef;
            [ProtoMember(10)] internal AudioDefinition AudioDef;
        }
    }
}
