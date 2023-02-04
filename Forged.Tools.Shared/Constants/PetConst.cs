﻿/*
 * Copyright (C) 2012-2020 CypherCore <http://github.com/CypherCore>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace Forged.Tools.Shared.Constants
{
    public enum CharmType
    {
        Charm,
        Possess,
        Vehicle,
        Convert
    }

    public enum PetType
    {
        Summon = 0,
        Hunter = 1,
        Max = 4
    }

    public enum PetSaveMode
    {
        AsDeleted = -2,                        // not saved in fact
        AsCurrent = -3,                        // in current slot (with player)
        FirstActiveSlot = 0,
        LastActiveSlot = FirstActiveSlot + SharedConst.MaxActivePets,
        FirstStableSlot = 5,
        LastStableSlot = FirstStableSlot + SharedConst.MaxPetStables, // last in DB stable slot index
        NotInSlot = -1,                       // for avoid conflict with stable size grow will use negative value
    }

    public enum PetSpellState
    {
        Unchanged = 0,
        Changed = 1,
        New = 2,
        Removed = 3
    }

    public enum PetSpellType
    {
        Normal = 0,
        Family = 1,
        Talent = 2
    }

    public enum PetActionFeedback
    {
        None = 0,
        Dead = 1,
        NoTarget = 2,
        InvalidTarget = 3,
        NoPath = 4
    }

    public enum PetTalk
    {
        SpecialSpell = 0,
        Attack = 1
    }

    public enum CommandStates
    {
        Stay = 0,
        Follow = 1,
        Attack = 2,
        Abandon = 3,
        MoveTo = 4
    }

    public enum PetNameInvalidReason
    {
        // custom, not send
        Success = 0,

        Invalid = 1,
        NoName = 2,
        TooShort = 3,
        TooLong = 4,
        MixedLanguages = 6,
        Profane = 7,
        Reserved = 8,
        ThreeConsecutive = 11,
        InvalidSpace = 12,
        ConsecutiveSpaces = 13,
        RussianConsecutiveSilentCharacters = 14,
        RussianSilentCharacterAtBeginningOrEnd = 15,
        DeclensionDoesntMatchBaseName = 16
    }

    public enum PetStableinfo : byte
    {
        Active = 1,
        Inactive = 2
    }

    public enum StableResult
    {
        NotEnoughMoney = 1,                              // "you don't have enough money"
        InvalidSlot = 3,                              // "That slot is locked"
        StableSuccess = 8,                              // stable success
        UnstableSuccess = 9,                              // unstable/swap success
        BuySlotSuccess = 10,                             // buy slot success
        CantControlExotic = 11,                             // "you are unable to control exotic creatures"
        InternalError = 12,                             // "Internal pet error"
    }

    public enum PetTameResult
    {
        Ok = 0,
        InvalidCreature = 1,
        TooMany = 2,
        CreatureAlreadyOwned = 3,
        NotTameable = 4,
        AnotherSummonActive = 5,
        UnitsCantTame = 6,
        NoPetAvailable = 7,
        InternalError = 8,
        TooHighLevel = 9,
        Dead = 10,
        NotDead = 11,
        CantControlExotic = 12,
        InvalidSlot = 13,
        EliteTooHighLevel = 14
    }
}
