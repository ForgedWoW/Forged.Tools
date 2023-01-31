using Framework.Constants;
using Game.Conditions;
using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trait_Editor.Models;
using Trait_Editor.Models.DB2Records;

namespace Trait_Editor.Utils
{
    public static class TraitManager
    {
        public static Dictionary<int, Class> ClassSpecs = new();
        public static Dictionary<uint, TraitTree> TraitTrees = new();
        public static Dictionary<uint, List<TraitTreeLoadoutEntryRecord>> TraitTreeLoadoutsByChrSpecialization = new();
        public static Dictionary<uint, TraitNode> TraitNodes = new();
        public static Dictionary<uint, TraitDefinitionRecord> TraitDefinitionByNodeID = new();
        public static Dictionary<uint, List<uint>> TraitNodesByGroup = new();
        public static Dictionary<uint, List<uint>> TraitNodeGroupByTree = new();

        static Dictionary<uint, TraitNodeGroup> _traitGroups = new();
        static uint[] _skillLinesByClass = new uint[15];
        static Dictionary<uint, List<TraitTree>> _traitTreesBySkillLine = new();
        static Dictionary<uint, List<TraitTree>> _traitTreesByTraitSystem = new();
        static int _configIdGenerator = 0;
        static Dictionary<uint, List<TraitCurrencySourceRecord>> _traitCurrencySourcesByCurrency = new();
        static Dictionary<uint, List<TraitDefinitionEffectPointsRecord>> _traitDefinitionEffectPointModifiers = new();

        public static void Load()
        {
            BuildClassSpecs();

            Dictionary<uint, List<TraitCondRecord>> nodeEntryConditions = new();
            foreach (var traitNodeEntryXTraitCondRecord in DataAccess.TraitNodeEntryXTraitCondStorage)
                if (DataAccess.TraitCondStorage.ContainsKey(traitNodeEntryXTraitCondRecord.Value.TraitCondID))
                    nodeEntryConditions.AddListItem(traitNodeEntryXTraitCondRecord.Value.TraitNodeEntryID, DataAccess.TraitCondStorage[traitNodeEntryXTraitCondRecord.Value.TraitCondID]);

            Dictionary<uint, List<TraitCostRecord>> nodeEntryCosts = new();
            foreach (var traitNodeEntryXTraitCostRecord in DataAccess.TraitNodeEntryXTraitCostStorage)
                if (DataAccess.TraitCostStorage.ContainsKey(traitNodeEntryXTraitCostRecord.Value.TraitCostID))
                    nodeEntryCosts.AddListItem(traitNodeEntryXTraitCostRecord.Value.TraitNodeEntryID, DataAccess.TraitCostStorage[traitNodeEntryXTraitCostRecord.Value.TraitCostID]);

            Dictionary<uint, List<TraitCondRecord>> nodeGroupConditions = new();
            foreach (var traitNodeGroupXTraitCondRecord in DataAccess.TraitNodeGroupXTraitCondStorage)
                if (DataAccess.TraitCondStorage.ContainsKey(traitNodeGroupXTraitCondRecord.Value.TraitCondID))
                    nodeGroupConditions.AddListItem(traitNodeGroupXTraitCondRecord.Value.TraitNodeGroupID, DataAccess.TraitCondStorage[traitNodeGroupXTraitCondRecord.Value.TraitCondID]);

            Dictionary<uint, List<TraitCostRecord>> nodeGroupCosts = new();
            foreach (var traitNodeGroupXTraitCostRecord in DataAccess.TraitNodeGroupXTraitCostStorage)
                if (DataAccess.TraitCostStorage.ContainsKey(traitNodeGroupXTraitCostRecord.Value.TraitCostID))
                nodeGroupCosts.AddListItem(traitNodeGroupXTraitCostRecord.Value.TraitNodeGroupID, DataAccess.TraitCostStorage[traitNodeGroupXTraitCostRecord.Value.TraitCostID]);

            Dictionary<uint, List<uint>> traitNodegroupsByNode = new();
            TraitNodesByGroup = new();
            foreach (var traitNodeGroupXTraitNodeRecord in DataAccess.TraitNodeGroupXTraitNodeStorage)
            {
                traitNodegroupsByNode.AddListItem(traitNodeGroupXTraitNodeRecord.Value.TraitNodeID, traitNodeGroupXTraitNodeRecord.Value.TraitNodeGroupID);
                TraitNodesByGroup.AddListItem(traitNodeGroupXTraitNodeRecord.Value.TraitNodeGroupID, traitNodeGroupXTraitNodeRecord.Value.TraitNodeID);
            }

            Dictionary<uint, List<TraitCondRecord>> nodeConditions = new();
            foreach (var traitNodeXTraitCondRecord in DataAccess.TraitNodeXTraitCondStorage)
                if (DataAccess.TraitCondStorage.ContainsKey(traitNodeXTraitCondRecord.Value.TraitCondID))
                    nodeConditions.AddListItem(traitNodeXTraitCondRecord.Value.TraitNodeID, DataAccess.TraitCondStorage[traitNodeXTraitCondRecord.Value.TraitCondID]);

            Dictionary<uint, List<TraitCostRecord>> nodeCosts = new();
            foreach (var traitNodeXTraitCostRecord in DataAccess.TraitNodeXTraitCostStorage)
                if (DataAccess.TraitCostStorage.ContainsKey(traitNodeXTraitCostRecord.Value.TraitCostID))
                    nodeCosts.AddListItem(traitNodeXTraitCostRecord.Value.TraitNodeID, DataAccess.TraitCostStorage[traitNodeXTraitCostRecord.Value.TraitCostID]);

            Dictionary<uint, List<TraitNodeEntryRecord>> nodeEntries = new();
            foreach (var traitNodeXTraitNodeEntryRecord in DataAccess.TraitNodeXTraitNodeEntryStorage)
                nodeEntries.AddListItem(traitNodeXTraitNodeEntryRecord.Value.TraitNodeID, DataAccess.TraitNodeEntryStorage[traitNodeXTraitNodeEntryRecord.Value.TraitNodeEntryID]);

            Dictionary<uint, List<TraitCostRecord>> treeCosts = new();
            foreach (var traitTreeXTraitCostRecord in DataAccess.TraitTreeXTraitCostStorage)
                if (DataAccess.TraitCostStorage.ContainsKey(traitTreeXTraitCostRecord.Value.TraitCostID))
                    treeCosts.AddListItem(traitTreeXTraitCostRecord.Value.TraitTreeID, DataAccess.TraitCostStorage[traitTreeXTraitCostRecord.Value.TraitCostID]);

            Dictionary<uint, List<TraitCurrencyRecord>> treeCurrencies = new();
            foreach (var traitTreeXTraitCurrencyRecord in DataAccess.TraitTreeXTraitCurrencyStorage)
                if (DataAccess.TraitCurrencyStorage.ContainsKey(traitTreeXTraitCurrencyRecord.Value.TraitCurrencyID))
                    treeCurrencies.AddListItem(traitTreeXTraitCurrencyRecord.Value.TraitTreeID, DataAccess.TraitCurrencyStorage[traitTreeXTraitCurrencyRecord.Value.TraitCurrencyID]);

            Dictionary<uint, List<uint>> traitTreesIdsByTraitSystem = new();
            foreach (var traitTree in DataAccess.TraitTreeStorage)
            {
                TraitTree tree;
                if (TraitTrees.ContainsKey(traitTree.Value.Id))
                    tree = TraitTrees[traitTree.Value.Id];
                else
                {
                    tree = new();
                    TraitTrees[traitTree.Value.Id] = tree;
                }

                tree.Data = traitTree.Value;

                if (treeCosts.ContainsKey(traitTree.Value.Id))
                    tree.Costs = treeCosts[traitTree.Value.Id].ToList();

                if (treeCurrencies.ContainsKey(traitTree.Value.Id))
                    tree.Currencies = treeCurrencies[traitTree.Value.Id].ToList();

                if (traitTree.Value.TraitSystemID != 0)
                {
                    traitTreesIdsByTraitSystem.AddListItem(traitTree.Value.TraitSystemID, traitTree.Value.Id);
                    tree.ConfigType = TraitConfigType.Generic;
                }
            }

            foreach (var traitNodeGroup in DataAccess.TraitNodeGroupStorage)
            {
                TraitNodeGroup nodeGroup;
                if (_traitGroups.ContainsKey(traitNodeGroup.Value.Id))
                    nodeGroup = _traitGroups[traitNodeGroup.Value.Id];
                else
                {
                    nodeGroup = new();
                    _traitGroups[traitNodeGroup.Value.Id] = nodeGroup;
                }

                nodeGroup.Data = traitNodeGroup.Value;

                if (nodeGroupConditions.ContainsKey(traitNodeGroup.Value.Id))
                    nodeGroup.Conditions = nodeGroupConditions[traitNodeGroup.Value.Id].ToList();

                if (nodeGroupCosts.ContainsKey(traitNodeGroup.Value.Id))
                    nodeGroup.Costs = nodeGroupCosts[traitNodeGroup.Value.Id].ToList();

                if (!TraitNodeGroupByTree.ContainsKey(traitNodeGroup.Value.TraitTreeID))
                    TraitNodeGroupByTree[traitNodeGroup.Value.TraitTreeID] = new List<uint>();

                TraitNodeGroupByTree[traitNodeGroup.Value.TraitTreeID].AddIfDoesntExist(traitNodeGroup.Key);
            }

            foreach (var traitNode in DataAccess.TraitNodeStorage)
            {
                TraitNode node;
                if (TraitNodes.ContainsKey(traitNode.Value.Id))
                    node = TraitNodes[traitNode.Value.Id];
                else
                {
                    node = new();
                    TraitNodes[traitNode.Value.Id] = node;
                }

                node.Data = traitNode.Value;

                if (TraitTrees.ContainsKey(traitNode.Value.TraitTreeID))
                    TraitTrees[traitNode.Value.TraitTreeID].Nodes.Add(node);

                if (nodeEntries.ContainsKey(traitNode.Value.Id))
                    foreach (var traitNodeEntry in nodeEntries[traitNode.Value.Id])
                    {
                        if (node.Entries.Count > 0)
                        {
                            var record = node.Entries.Last();
                            record.Data = traitNodeEntry;

                            if (nodeEntryConditions.ContainsKey(traitNodeEntry.Id))
                                record.Conditions = nodeEntryConditions[traitNodeEntry.Id].ToList();

                            if (nodeEntryCosts.ContainsKey(traitNodeEntry.Id))
                                record.Costs = nodeEntryCosts[traitNodeEntry.Id].ToList();
                        }
                    }

                if (traitNodegroupsByNode.ContainsKey(traitNode.Value.Id))
                    foreach (var nodeGroupId in traitNodegroupsByNode[traitNode.Value.Id])
                    {
                        if (!_traitGroups.ContainsKey(nodeGroupId))
                            continue;

                        var nodeGroup = _traitGroups[nodeGroupId];
                        nodeGroup.Nodes.Add(node);
                        node.Groups.Add(nodeGroup);
                    }

                if (nodeConditions.ContainsKey(traitNode.Value.Id))
                    node.Conditions = nodeConditions[traitNode.Value.Id].ToList();

                if (nodeCosts.ContainsKey(traitNode.Value.Id))
                    node.Costs = nodeCosts[traitNode.Value.Id].ToList();
            }

            foreach (var traitEdgeRecord in DataAccess.TraitEdgeStorage)
            {
                if (!TraitNodes.ContainsKey(traitEdgeRecord.Value.LeftTraitNodeID)
                    || !TraitNodes.ContainsKey(traitEdgeRecord.Value.RightTraitNodeID))
                    continue;

                TraitNodes[traitEdgeRecord.Value.RightTraitNodeID].ParentNodes[TraitNodes[traitEdgeRecord.Value.LeftTraitNodeID]]
                    = (TraitEdgeType)traitEdgeRecord.Value.Type;
            }

            foreach (var skillLineXTraitTreeRecord in DataAccess.SkillLineXTraitTreeStorage)
            {
                if (!TraitTrees.ContainsKey(skillLineXTraitTreeRecord.Value.TraitTreeID) ||
                    !DataAccess.SkillLineStorage.ContainsKey(skillLineXTraitTreeRecord.Value.SkillLineID))
                    continue;

                var tree = TraitTrees[skillLineXTraitTreeRecord.Value.TraitTreeID];
                var skillLineRecord = DataAccess.SkillLineStorage[skillLineXTraitTreeRecord.Value.SkillLineID];

                _traitTreesBySkillLine.AddListItem(skillLineXTraitTreeRecord.Value.SkillLineID, tree);

                if (skillLineRecord.CategoryID == SkillCategory.Class)
                {
                    if (DataAccess.SkillRaceClassInfoSorted.ContainsKey(skillLineRecord.Id))
                        foreach (var skillRaceClassInfo in DataAccess.SkillRaceClassInfoSorted[skillLineRecord.Id])
                            for (int i = 1; i < 15; ++i)
                                if ((skillRaceClassInfo.ClassMask & (1 << (i - 1))) != 0)
                                    _skillLinesByClass[i] = skillLineXTraitTreeRecord.Value.SkillLineID;

                    tree.ConfigType = TraitConfigType.Combat;
                }
                else
                    tree.ConfigType = TraitConfigType.Profession;
            }

            foreach (var ids in traitTreesIdsByTraitSystem)
                foreach (uint traitTreeId in ids.Value)
                    if (TraitTrees.ContainsKey(traitTreeId))
                    {
                        _traitTreesByTraitSystem.AddListItem(ids.Key, TraitTrees[traitTreeId]);
                    }

            foreach (var traitCurrencySource in DataAccess.TraitCurrencySourceStorage)
                _traitCurrencySourcesByCurrency.AddListItem(traitCurrencySource.Value.TraitCurrencyID, traitCurrencySource.Value);

            foreach (var traitDefinitionEffectPoints in DataAccess.TraitDefinitionEffectPointsStorage)
                _traitDefinitionEffectPointModifiers.AddListItem(traitDefinitionEffectPoints.Value.TraitDefinitionID, traitDefinitionEffectPoints.Value);

            Dictionary<uint, List<TraitTreeLoadoutEntryRecord>> traitTreeLoadoutEntries = new();
            foreach (var traitTreeLoadoutRecord in DataAccess.TraitTreeLoadoutEntryStorage)
                traitTreeLoadoutEntries.AddListItem(traitTreeLoadoutRecord.Value.TraitTreeLoadoutID, traitTreeLoadoutRecord.Value);

            foreach (var traitTreeLoadout in DataAccess.TraitTreeLoadoutStorage)
            {
                if (traitTreeLoadoutEntries.ContainsKey(traitTreeLoadout.Value.Id))
                {
                    if (!TraitTreeLoadoutsByChrSpecialization.ContainsKey(traitTreeLoadout.Value.ChrSpecializationID))
                        TraitTreeLoadoutsByChrSpecialization.Add(traitTreeLoadout.Value.ChrSpecializationID, new List<TraitTreeLoadoutEntryRecord>());

                    // there should be only one loadout per spec, we take last one encountered
                    TraitTreeLoadoutsByChrSpecialization[traitTreeLoadout.Value.ChrSpecializationID] = traitTreeLoadoutEntries[traitTreeLoadout.Value.Id];
                }
            }

            foreach (var node in DataAccess.TraitNodeXTraitNodeEntryStorage)
            {
                var entry = DataAccess.TraitNodeEntryStorage[node.Value.TraitNodeEntryID];
                TraitDefinitionByNodeID[node.Value.TraitNodeID] = DataAccess.TraitDefinitionStorage[entry.TraitDefinitionID];
            }

            string test = "";
        }

        private static void BuildClassSpecs()
        {
            ClassSpecs[(int)SpecID.Arms] = Class.Warrior;
            ClassSpecs[(int)SpecID.Fury] = Class.Warrior;
            ClassSpecs[(int)SpecID.ProtectionWarrior] = Class.Warrior;

            ClassSpecs[(int)SpecID.HolyPaladin] = Class.Paladin;
            ClassSpecs[(int)SpecID.Retribution] = Class.Paladin;
            ClassSpecs[(int)SpecID.ProtectionPaladin] = Class.Paladin;

            ClassSpecs[(int)SpecID.Marksmanship] = Class.Hunter;
            ClassSpecs[(int)SpecID.Survival] = Class.Hunter;
            ClassSpecs[(int)SpecID.BeastMastery] = Class.Hunter;

            ClassSpecs[(int)SpecID.Assassination] = Class.Rogue;
            ClassSpecs[(int)SpecID.Outlaw] = Class.Rogue;
            ClassSpecs[(int)SpecID.Subtlety] = Class.Rogue;

            ClassSpecs[(int)SpecID.HolyPriest] = Class.Priest;
            ClassSpecs[(int)SpecID.Discipline] = Class.Priest;
            ClassSpecs[(int)SpecID.Shadow] = Class.Priest;

            ClassSpecs[(int)SpecID.Blood] = Class.Deathknight;
            ClassSpecs[(int)SpecID.FrostDK] = Class.Deathknight;
            ClassSpecs[(int)SpecID.Unholy] = Class.Deathknight;

            ClassSpecs[(int)SpecID.Elemental] = Class.Shaman;
            ClassSpecs[(int)SpecID.Enhancement] = Class.Shaman;
            ClassSpecs[(int)SpecID.RestorationShaman] = Class.Shaman;

            ClassSpecs[(int)SpecID.FrostMage] = Class.Mage;
            ClassSpecs[(int)SpecID.Fire] = Class.Mage;
            ClassSpecs[(int)SpecID.Arcane] = Class.Mage;

            ClassSpecs[(int)SpecID.Demonology] = Class.Warlock;
            ClassSpecs[(int)SpecID.Destruction] = Class.Warlock;
            ClassSpecs[(int)SpecID.Affliction] = Class.Warlock;

            ClassSpecs[(int)SpecID.Windwalker] = Class.Monk;
            ClassSpecs[(int)SpecID.Brewmaster] = Class.Monk;
            ClassSpecs[(int)SpecID.Mistweaver] = Class.Monk;

            ClassSpecs[(int)SpecID.Feral] = Class.Druid;
            ClassSpecs[(int)SpecID.Guardian] = Class.Druid;
            ClassSpecs[(int)SpecID.RestorationDruid] = Class.Druid;
            ClassSpecs[(int)SpecID.Balance] = Class.Druid;

            ClassSpecs[(int)SpecID.Havoc] = Class.DemonHunter;
            ClassSpecs[(int)SpecID.Vengeance] = Class.DemonHunter;

            ClassSpecs[(int)SpecID.Devastation] = Class.Evoker;
            ClassSpecs[(int)SpecID.Preservation] = Class.Evoker;
        }
    }
}
