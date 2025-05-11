using System;

[Flags]
public enum WeaponFlag
{
    Default = 0,
    Rapid = 1,
    Spread = 2,
    Rocket = 4,
    Beam = 8,
}

public enum UpgradeFlag
{
    ExtraLife = 0,
    ExtraDamage = 1,
    WeaponChange = 2,
    PlayerShield = 4,
}
