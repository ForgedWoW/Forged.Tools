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

using Framework.Constants;
using System.Collections.Generic;

namespace Game.Networking.Packets
{
    public class InitWorldStates : ServerPacket
    {
        public InitWorldStates() : base(ServerOpcodes.InitWorldStates, ConnectionType.Instance) { }

        public override void Write()
        {
            _worldPacket.WriteUInt32(MapID);
            _worldPacket.WriteUInt32(AreaID);
            _worldPacket.WriteUInt32(SubareaID);

            _worldPacket.WriteInt32(Worldstates.Count);
            foreach (WorldStateInfo wsi in Worldstates)
            {
                _worldPacket.WriteUInt32(wsi.VariableID);
                _worldPacket.WriteInt32(wsi.Value);
            }
        }

        public void AddState(WorldStates variableID, uint value)
        {
            AddState((uint)variableID, value);
        }

        public void AddState(uint variableID, uint value)
        {
            Worldstates.Add(new WorldStateInfo(variableID, (int)value));
        }

        public void AddState(int variableID, int value)
        {
            Worldstates.Add(new WorldStateInfo((uint)variableID, value));
        }

        public void AddState(WorldStates variableID, bool value)
        {
            AddState((uint)variableID, value);
        }

        public void AddState(uint variableID, bool value)
        {
            Worldstates.Add(new WorldStateInfo(variableID, value ? 1 : 0));
        }

        public uint AreaID;
        public uint SubareaID;
        public uint MapID;

        List<WorldStateInfo> Worldstates = new();

        struct WorldStateInfo
        {
            public WorldStateInfo(uint variableID, int value)
            {
                VariableID = variableID;
                Value = value;
            }

            public uint VariableID;
            public int Value;
        }
    }

    public class UpdateWorldState : ServerPacket
    {
        public UpdateWorldState() : base(ServerOpcodes.UpdateWorldState, ConnectionType.Instance) { }

        public override void Write()
        {
            _worldPacket.WriteUInt32(VariableID);
            _worldPacket.WriteInt32(Value);
            _worldPacket.WriteBit(Hidden);
            _worldPacket.FlushBits();
        }

        public int Value;
        public bool Hidden; // @todo: research
        public uint VariableID;
    }
}
