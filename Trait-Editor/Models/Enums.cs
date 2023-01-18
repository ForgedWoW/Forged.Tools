using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models
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

    public enum TraitConfigType
    {
        Invalid = 0,
        Combat = 1,
        Profession = 2,
        Generic = 3
    }

    public enum TraitCurrencyType
    {
        Gold = 0,
        CurrencyTypesBased = 1,
        TraitSourced = 2
    }

    public enum TraitEdgeType
    {
        VisualOnly = 0,
        DeprecatedRankConnection = 1,
        SufficientForAvailability = 2,
        RequiredForAvailability = 3,
        MutuallyExclusive = 4,
        DeprecatedSelectionOption = 5
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
}
