using System;
using System.IO;
using ProtoBuf;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRageMath;

namespace WeaponThread
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate, int.MinValue + 1)]
    public class Session : MySessionComponentBase
    {
        internal WeaponDefinition[] WeaponDefinitions;

        public override void LoadData()
        {
            Log.Init("weapon.log");
            Log.CleanLine($"Logging Started at: {DateTime.Now:MM-dd-yy_HH-mm-ss-fff}");
            MyAPIGateway.Utilities.RegisterMessageHandler(7772, Handler);
            Init();
            SendModMessage();
        }

        protected override void UnloadData()
        {
            Log.CleanLine($"Logging stopped at: {DateTime.Now:MM-dd-yy_HH-mm-ss-fff}");
            Log.Close();
            MyAPIGateway.Utilities.UnregisterMessageHandler(7772, Handler);
            Array.Clear(Storage, 0, Storage.Length);
            Storage = null;
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
            var weapons = new Weapons();
            WeaponDefinitions = weapons.ReturnDefs();
            for (int i = 0; i < WeaponDefinitions.Length; i++)
                WeaponDefinitions[i].ModPath = ModContext.ModPath;
            Storage = MyAPIGateway.Utilities.SerializeToBinary(WeaponDefinitions);
            Array.Clear(WeaponDefinitions, 0, WeaponDefinitions.Length);
            WeaponDefinitions = null;
        }

        [ProtoContract]
        public struct WeaponDefinition
        {
            [ProtoMember(1)] internal HardPointDefinition HardPoint;
            [ProtoMember(2)] internal AmmoDefinition Ammo;
            [ProtoMember(3)] internal GraphicDefinition Graphics;
            [ProtoMember(4)] internal AudioDefinition Audio;
            [ProtoMember(5)] internal ModelAssignments Assignments;
            [ProtoMember(6)] internal UiDefinition Ui;
            [ProtoMember(7)] internal DamageScaleDefinition DamageScales;
            [ProtoMember(8)] internal string ModPath;
        }


        [ProtoContract]
        public struct ModelAssignments
        {
            [ProtoMember(1)] internal MountPoint[] MountPoints;
            [ProtoMember(2)] internal string[] Barrels;
        }

        [ProtoContract]
        public struct UiDefinition
        {
            [ProtoMember(1)] internal Slider RateOfFire;
            [ProtoMember(2)] internal Slider DamageModifier;
            [ProtoMember(3)] internal bool SelectableProjectileColor;
        }

        [ProtoContract]
        public struct HardPointDefinition
        {
            public enum Prediction
            {
                Off,
                Basic,
                Accurate,
                Advanced,
            }

            [ProtoMember(1)] internal string DefinitionId;
            [ProtoMember(2)] internal string AmmoMagazineId;
            [ProtoMember(3)] internal bool IsTurret;
            [ProtoMember(4)] internal bool TurretController;
            [ProtoMember(5)] internal bool TrackTargets;
            [ProtoMember(6)] internal int DelayCeaseFire;
            [ProtoMember(7)] internal int RotateBarrelAxis;
            [ProtoMember(8)] internal float RotateSpeed;
            [ProtoMember(9)] internal float ElevationSpeed;
            [ProtoMember(10)] internal float DeviateShotAngle;
            [ProtoMember(11)] internal float EnergyCost;
            [ProtoMember(12)] internal double AimingTolerance;
            [ProtoMember(13)] internal Prediction TargetPrediction;
            [ProtoMember(14)] internal AmmoLoading Loading;
        }

        [ProtoContract]
        public struct AmmoLoading
        {
            [ProtoMember(1)] internal int ReloadTime;
            [ProtoMember(2)] internal int RateOfFire;
            [ProtoMember(3)] internal int BarrelsPerShot;
            [ProtoMember(4)] internal int SkipBarrels;
            [ProtoMember(5)] internal int TrajectilesPerBarrel;
            [ProtoMember(6)] internal int HeatPerRoF;
            [ProtoMember(7)] internal int MaxHeat;
            [ProtoMember(8)] internal int HeatSinkRate;
            [ProtoMember(9)] internal int DelayUntilFire;
            [ProtoMember(10)] internal int ShotsInBurst;
            [ProtoMember(11)] internal int DelayAfterBurst;
        }

        [ProtoContract]
        public struct MountPoint
        {
            [ProtoMember(1)] internal string SubtypeId;
            [ProtoMember(2)] internal string SubpartId;
        }

        [ProtoContract]
        public struct AmmoDefinition
        {
            [ProtoMember(1)] internal float DefaultDamage;
            [ProtoMember(2)] internal float AreaEffectYield;
            [ProtoMember(3)] internal float AreaEffectRadius;
            [ProtoMember(4)] internal bool DetonateOnEnd;
            [ProtoMember(5)] internal float Mass;
            [ProtoMember(6)] internal float Health;
            [ProtoMember(7)] internal int MaxObjectsHit;
            [ProtoMember(8)] internal float BackKickForce;
            [ProtoMember(9)] internal AmmoTrajectory Trajectory;
        }

        [ProtoContract]
        public struct AmmoTrajectory
        {
            internal enum GuidanceType
            {
                None,
                Remote,
                TravelTo,
                Smart
            }

            [ProtoMember(1)] internal float MaxTrajectory;
            [ProtoMember(2)] internal float AccelPerSec;
            [ProtoMember(3)] internal float DesiredSpeed;
            [ProtoMember(4)] internal double SmartsFactor;
            [ProtoMember(5)] internal double SmartsTrackingDelay;
            [ProtoMember(6)] internal double SmartsMaxLateralThrust;
            [ProtoMember(7)] internal float TargetLossDegree;
            [ProtoMember(8)] internal int TargetLossTime;
            [ProtoMember(9)] internal Randomize SpeedVariance;
            [ProtoMember(10)] internal Randomize RangeVariance;
            [ProtoMember(11)] internal GuidanceType Guidance;
        }

        [ProtoContract]
        public struct GraphicDefinition
        {
            [ProtoMember(1)] internal bool ShieldHitDraw;
            [ProtoMember(2)] internal float VisualProbability;
            [ProtoMember(3)] internal string ModelName;
            [ProtoMember(4)] internal ParticleDefinition Particles;
            [ProtoMember(5)] internal LineDefinition Line;
        }

        [ProtoContract]
        public struct ParticleDefinition
        {
            [ProtoMember(1)] internal Particle Ammo;
            [ProtoMember(2)] internal Particle Hit;
            [ProtoMember(3)] internal Particle Barrel1;
            [ProtoMember(4)] internal Particle Barrel2;
        }

        [ProtoContract]
        public struct Particle
        {
            [ProtoMember(1)] internal string Name;
            [ProtoMember(2)] internal Vector4 Color;
            [ProtoMember(3)] internal Vector3D Offset;
            [ProtoMember(4)] internal ParticleOptions Extras;
        }

        [ProtoContract]
        public struct ParticleOptions
        {
            [ProtoMember(1)] internal float Scale;
            [ProtoMember(2)] internal float MaxDistance;
            [ProtoMember(3)] internal float MaxDuration;
            [ProtoMember(4)] internal bool Loop;
            [ProtoMember(5)] internal bool Restart;
        }

        [ProtoContract]
        public struct LineDefinition
        {
            [ProtoMember(1)] internal bool Trail;
            [ProtoMember(2)] internal float Length;
            [ProtoMember(3)] internal float Width;
            [ProtoMember(4)] internal string Material;
            [ProtoMember(5)] internal Vector4 Color;
            [ProtoMember(6)] internal Randomize ColorVariance;
            [ProtoMember(7)] internal Randomize WidthVariance;
        }

        [ProtoContract]
        public struct Slider
        {
            [ProtoMember(1)] internal bool Enable;
            [ProtoMember(2)] internal double Min;
            [ProtoMember(3)] internal double Max;
        }

        [ProtoContract]
        public struct Randomize
        {
            [ProtoMember(1)] internal float Start;
            [ProtoMember(2)] internal float End;
        }

        [ProtoContract]
        public struct AudioDefinition
        {
            [ProtoMember(1)] internal AudioHardPointDefinition HardPoint;
            [ProtoMember(2)] internal AudioAmmoDefinition Ammo;
        }

        [ProtoContract]
        public struct AudioAmmoDefinition
        {
            [ProtoMember(1)] internal string TravelSound;
            [ProtoMember(2)] internal string HitSound;
        }

        [ProtoContract]
        public struct AudioHardPointDefinition
        {
            [ProtoMember(1)] internal string ReloadSound;
            [ProtoMember(2)] internal string NoAmmoSound;
            [ProtoMember(3)] internal string HardPointRotationSound;
            [ProtoMember(4)] internal string BarrelRotationSound;
            [ProtoMember(5)] internal string FiringSound;
            [ProtoMember(6)] internal bool FiringSoundPerShot;
        }

        [ProtoContract]
        public struct DamageScaleDefinition
        {
            [ProtoMember(1)] internal float Large;
            [ProtoMember(2)] internal float Small;
            [ProtoMember(3)] internal ArmorDefinition Armor;
            [ProtoMember(4)] internal float MaxIntegrity;
            [ProtoMember(5)] internal bool DamageVoxels;
            [ProtoMember(6)] internal ShieldDefinition Shield;
            [ProtoMember(7)] internal float Player;
        }

        [ProtoContract]
        public struct ArmorDefinition
        {
            [ProtoMember(1)] internal float Armor;
            [ProtoMember(2)] internal float Heavy;
            [ProtoMember(3)] internal float Light;
            [ProtoMember(4)] internal float NonArmor;
        }


        [ProtoContract]
        public struct ShieldDefinition
        {
            internal enum ShieldType
            {
                Bypass,
                Emp,
                Energy,
                Kinetic
            }

            [ProtoMember(1)] internal float DamageModifer;
            [ProtoMember(2)] internal ShieldType Type;
        }

        public class Log
        {
            private static Log _instance = null;
            private TextWriter _file = null;

            private static Log GetInstance()
            {
                return _instance ?? (_instance = new Log());
            }

            public static void Init(string name)
            {
                if (GetInstance()._file == null)
                    GetInstance()._file = MyAPIGateway.Utilities.WriteFileInLocalStorage(name, typeof(Log));
            }

            public static void CleanLine(string text)
            {
                if (GetInstance()._file == null) return;
                GetInstance()._file.WriteLine(text);
                GetInstance()._file.Flush();
            }

            public static void Close()
            {
                if (GetInstance()._file == null) return;
                GetInstance()._file.Flush();
                GetInstance()._file.Close();
            }
        }
    }
}

