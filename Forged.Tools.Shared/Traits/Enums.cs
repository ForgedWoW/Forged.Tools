﻿// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

namespace Forged.Tools.Shared.Traits
{
    public enum TraitCombatConfigFlags
    {
        None = 0x0,
        ActiveForSpec = 0x1,
        StarterBuild = 0x2,
        SharedActionBars = 0x4
    }

    public enum TraitCondFlags
    {
        None = 0x0,
        IsGate = 0x1,
        IsAlwaysMet = 0x2,
        IsSufficient = 0x4,
    }

    public enum TraitConditionType
    {
        Available = 0,
        Visible = 1,
        Granted = 2,
        Increased = 3
    }

    public enum TraitCurrencyType
    {
        Gold = 0,
        CurrencyTypesBased = 1,
        TraitSourced = 2
    }

    public enum TraitNodeEntryType
    {
        SpendHex = 0,
        SpendSquare = 1,
        SpendCircle = 2,
        SpendSmallCircle = 3,
        DeprecatedSelect = 4,
        DragAndDrop = 5,
        SpendDiamond = 6,
        ProfPath = 7,
        ProfPerk = 8,
        ProfPathUnlock = 9
    }

    public enum TraitNodeGroupFlag
    {
        None = 0x0,
        AvailableByDefault = 0x1
    }

    public enum TraitNodeType
    {
        Single = 0,
        Tiered = 1,
        Selection = 2
    }

    public enum TraitPointsOperationType
    {
        None = -1,
        Set = 0,
        Multiply = 1
    }

    public enum TraitTreeFlag
    {
        None = 0x0,
        CannotRefund = 0x1,
        HideSingleRankNumbers = 0x2
    }

    public enum SpecID
    {
        None = 0,
        Arcane = 62,
        Fire = 63,
        FrostMage = 64,
        HolyPaladin = 65,
        ProtectionPaladin = 66,
        Retribution = 70,
        Arms = 71,
        Fury = 72,
        ProtectionWarrior = 73,
        Balance = 102,
        Feral = 103,
        Guardian = 104,
        RestorationDruid = 105,
        Blood = 250,
        FrostDK = 251,
        Unholy = 252,
        BeastMastery = 253,
        Marksmanship = 254,
        Survival = 255,
        Discipline = 256,
        HolyPriest = 257,
        Shadow = 258,
        Assassination = 259,
        Outlaw = 260,
        Subtlety = 261,
        Elemental = 262,
        Enhancement = 263,
        RestorationShaman = 264,
        Affliction = 265,
        Demonology = 266,
        Destruction = 267,
        Brewmaster = 268,
        Windwalker = 269,
        Mistweaver = 270,
        Havoc = 577,
        Vengeance = 581,
        Devastation = 1467,
        Preservation = 1468
    }

    public enum ClassTraitCurrencyID
    {
        Mage = 2517,
        Evoker = 2629,
        Warlock = 2665,
        Deathknight = 2724,
        Hunter = 2770,
        Monk = 2778,
        Shaman = 2786,
        Paladin = 2795,
        Druid = 2801,
        Priest = 2805,
        Warrior = 2896,
        Rogue = 2900,
        DemonHunter = 2904
    }
}
