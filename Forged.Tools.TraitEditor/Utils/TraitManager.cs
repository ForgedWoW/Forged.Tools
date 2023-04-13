// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Forged.Tools.Shared.Traits;
using Forged.Tools.Shared.Utils;
using Framework.Constants;
using Game.DataStorage;

namespace Forged.Tools.TraitEditor.Utils
{
    public static class TraitManager
    {
        public static readonly Dictionary<int, PlayerClass> ClassSpecs = new();
        public static readonly Dictionary<uint, TraitTree> TraitTrees = new();
        public static readonly Dictionary<int, List<TraitTreeLoadoutEntryRecord>> TraitTreeLoadoutsByChrSpecialization = new();
        public static readonly Dictionary<uint, TraitNode> TraitNodes = new();
        public static readonly Dictionary<int, TraitDefinitionRecord> TraitDefinitionByNodeID = new();

        public static uint COMMIT_COMBAT_TRAIT_CONFIG_CHANGES_SPELL_ID = 384255u;
        public static uint MAX_COMBAT_TRAIT_CONFIGS = 10u;

        private static readonly Dictionary<int, TraitNodeGroup> _traitGroups = new();
        private static readonly int[] _skillLinesByClass = new int[(int)PlayerClass.Max];
        private static readonly MultiMap<int, TraitTree> _traitTreesBySkillLine = new();
        private static readonly MultiMap<int, TraitTree> _traitTreesByTraitSystem = new();
        private static int _configIdGenerator;
        private static readonly MultiMap<int, TraitCurrencySourceRecord> _traitCurrencySourcesByCurrency = new();
        private static readonly MultiMap<int, TraitDefinitionEffectPointsRecord> _traitDefinitionEffectPointModifiers = new();
        private static readonly List<Tuple<int, uint>> _specsBySpecSet = new();

        public static void Load()
        {
            BuildClassSpecs();
            _configIdGenerator = int.MaxValue;

            MultiMap<int, TraitCondRecord> nodeEntryConditions = new();

            foreach (TraitNodeEntryXTraitCondRecord traitNodeEntryXTraitCondEntry in CliDB.TraitNodeEntryXTraitCondStorage.Values)
            {
                TraitCondRecord traitCondEntry = CliDB.TraitCondStorage.LookupByKey(traitNodeEntryXTraitCondEntry.TraitCondID);

                if (traitCondEntry != null)
                    nodeEntryConditions.Add((int)traitNodeEntryXTraitCondEntry.TraitNodeEntryID, traitCondEntry);
            }

            MultiMap<int, TraitCostRecord> nodeEntryCosts = new();

            foreach (TraitNodeEntryXTraitCostRecord traitNodeEntryXTraitCostEntry in CliDB.TraitNodeEntryXTraitCostStorage.Values)
            {
                TraitCostRecord traitCostEntry = CliDB.TraitCostStorage.LookupByKey(traitNodeEntryXTraitCostEntry.TraitCostID);

                if (traitCostEntry != null)
                    nodeEntryCosts.Add(traitNodeEntryXTraitCostEntry.TraitNodeEntryID, traitCostEntry);
            }

            MultiMap<int, TraitCondRecord> nodeGroupConditions = new();

            foreach (TraitNodeGroupXTraitCondRecord traitNodeGroupXTraitCondEntry in CliDB.TraitNodeGroupXTraitCondStorage.Values)
            {
                TraitCondRecord traitCondEntry = CliDB.TraitCondStorage.LookupByKey(traitNodeGroupXTraitCondEntry.TraitCondID);

                if (traitCondEntry != null)
                    nodeGroupConditions.Add(traitNodeGroupXTraitCondEntry.TraitNodeGroupID, traitCondEntry);
            }

            MultiMap<int, TraitCostRecord> nodeGroupCosts = new();

            foreach (TraitNodeGroupXTraitCostRecord traitNodeGroupXTraitCostEntry in CliDB.TraitNodeGroupXTraitCostStorage.Values)
            {
                TraitCostRecord traitCondEntry = CliDB.TraitCostStorage.LookupByKey(traitNodeGroupXTraitCostEntry.TraitCostID);

                if (traitCondEntry != null)
                    nodeGroupCosts.Add(traitNodeGroupXTraitCostEntry.TraitNodeGroupID, traitCondEntry);
            }

            MultiMap<int, int> nodeGroups = new();

            foreach (TraitNodeGroupXTraitNodeRecord traitNodeGroupXTraitNodeEntry in CliDB.TraitNodeGroupXTraitNodeStorage.Values)
                nodeGroups.Add(traitNodeGroupXTraitNodeEntry.TraitNodeID, traitNodeGroupXTraitNodeEntry.TraitNodeGroupID);

            MultiMap<int, TraitCondRecord> nodeConditions = new();

            foreach (TraitNodeXTraitCondRecord traitNodeXTraitCondEntry in CliDB.TraitNodeXTraitCondStorage.Values)
            {
                TraitCondRecord traitCondEntry = CliDB.TraitCondStorage.LookupByKey(traitNodeXTraitCondEntry.TraitCondID);

                if (traitCondEntry != null)
                    nodeConditions.Add(traitNodeXTraitCondEntry.TraitNodeID, traitCondEntry);
            }

            MultiMap<uint, TraitCostRecord> nodeCosts = new();

            foreach (TraitNodeXTraitCostRecord traitNodeXTraitCostEntry in CliDB.TraitNodeXTraitCostStorage.Values)
            {
                TraitCostRecord traitCostEntry = CliDB.TraitCostStorage.LookupByKey(traitNodeXTraitCostEntry.TraitCostID);

                if (traitCostEntry != null)
                    nodeCosts.Add(traitNodeXTraitCostEntry.TraitNodeID, traitCostEntry);
            }

            MultiMap<int, TraitNodeEntryRecord> nodeEntries = new();

            foreach (TraitNodeXTraitNodeEntryRecord traitNodeXTraitNodeEntryEntry in CliDB.TraitNodeXTraitNodeEntryStorage.Values)
            {
                TraitNodeEntryRecord traitNodeEntryEntry = CliDB.TraitNodeEntryStorage.LookupByKey(traitNodeXTraitNodeEntryEntry.TraitNodeEntryID);

                if (traitNodeEntryEntry != null)
                    nodeEntries.Add(traitNodeXTraitNodeEntryEntry.TraitNodeID, traitNodeEntryEntry);
            }

            MultiMap<uint, TraitCostRecord> treeCosts = new();

            foreach (TraitTreeXTraitCostRecord traitTreeXTraitCostEntry in CliDB.TraitTreeXTraitCostStorage.Values)
            {
                TraitCostRecord traitCostEntry = CliDB.TraitCostStorage.LookupByKey(traitTreeXTraitCostEntry.TraitCostID);

                if (traitCostEntry != null)
                    treeCosts.Add(traitTreeXTraitCostEntry.TraitTreeID, traitCostEntry);
            }

            MultiMap<int, TraitCurrencyRecord> treeCurrencies = new();

            foreach (TraitTreeXTraitCurrencyRecord traitTreeXTraitCurrencyEntry in CliDB.TraitTreeXTraitCurrencyStorage.Values)
            {
                TraitCurrencyRecord traitCurrencyEntry = CliDB.TraitCurrencyStorage.LookupByKey(traitTreeXTraitCurrencyEntry.TraitCurrencyID);

                if (traitCurrencyEntry != null)
                    treeCurrencies.Add(traitTreeXTraitCurrencyEntry.TraitTreeID, traitCurrencyEntry);
            }

            MultiMap<int, int> traitTreesIdsByTraitSystem = new();

            foreach (TraitTreeRecord traitTree in CliDB.TraitTreeStorage.Values)
            {
                TraitTree tree = new();
                tree.Data = traitTree;

                var costs = treeCosts.LookupByKey(traitTree.Id);

                if (costs != null)
                    tree.Costs = costs;

                var currencies = treeCurrencies.LookupByKey(traitTree.Id);

                if (currencies != null)
                    tree.Currencies = currencies;

                if (traitTree.TraitSystemID != 0)
                {
                    traitTreesIdsByTraitSystem.Add(traitTree.TraitSystemID, (int)traitTree.Id);
                    tree.ConfigType = TraitConfigType.Generic;
                }

                TraitTrees[traitTree.Id] = tree;
            }

            foreach (TraitNodeGroupRecord traitNodeGroup in CliDB.TraitNodeGroupStorage.Values)
            {
                TraitNodeGroup nodeGroup = new();
                nodeGroup.Data = traitNodeGroup;

                var conditions = nodeGroupConditions.LookupByKey(traitNodeGroup.Id);

                if (conditions != null)
                    nodeGroup.Conditions = conditions;

                var costs = nodeGroupCosts.LookupByKey(traitNodeGroup.Id);

                if (costs != null)
                    nodeGroup.Costs = costs;

                _traitGroups[(int)traitNodeGroup.Id] = nodeGroup;
            }

            foreach (TraitNodeRecord traitNode in CliDB.TraitNodeStorage.Values)
            {
                TraitNode node = new();
                node.Data = traitNode;

                TraitTree tree = TraitTrees.LookupByKey(traitNode.TraitTreeID);

                tree?.Nodes.Add(node);

                foreach (var traitNodeEntry in nodeEntries.LookupByKey(traitNode.Id))
                {
                    TraitNodeEntry entry = new();
                    entry.Data = traitNodeEntry;

                    var conditions = nodeEntryConditions.LookupByKey(traitNodeEntry.Id);

                    if (conditions != null)
                        entry.Conditions = conditions;

                    var costs = nodeEntryCosts.LookupByKey(traitNodeEntry.Id);

                    if (costs != null)
                        entry.Costs = costs;

                    node.Entries.Add(entry);
                }

                foreach (var nodeGroupId in nodeGroups.LookupByKey(traitNode.Id))
                {
                    TraitNodeGroup nodeGroup = _traitGroups.LookupByKey(nodeGroupId);

                    if (nodeGroup == null)
                        continue;

                    nodeGroup.Nodes.Add(node);
                    node.Groups.Add(nodeGroup);
                }

                var conditions1 = nodeConditions.LookupByKey(traitNode.Id);

                if (conditions1 != null)
                    node.Conditions = conditions1;

                var costs1 = nodeCosts.LookupByKey(traitNode.Id);

                if (costs1 != null)
                    node.Costs = costs1;

                TraitNodes[traitNode.Id] = node;
            }

            foreach (TraitEdgeRecord traitEdgeEntry in CliDB.TraitEdgeStorage.Values)
            {
                TraitNode left = TraitNodes.LookupByKey(traitEdgeEntry.LeftTraitNodeID);
                TraitNode right = TraitNodes.LookupByKey(traitEdgeEntry.RightTraitNodeID);

                if (left == null ||
                    right == null)
                    continue;

                right.ParentNodes.Add(Tuple.Create(left, (TraitEdgeType)traitEdgeEntry.Type));
            }

            foreach (SkillLineXTraitTreeRecord skillLineXTraitTreeEntry in CliDB.SkillLineXTraitTreeStorage.Values)
            {
                TraitTree tree = TraitTrees.LookupByKey(skillLineXTraitTreeEntry.TraitTreeID);

                if (tree == null)
                    continue;

                SkillLineRecord skillLineEntry = CliDB.SkillLineStorage.LookupByKey(skillLineXTraitTreeEntry.SkillLineID);

                if (skillLineEntry == null)
                    continue;

                _traitTreesBySkillLine.Add(skillLineXTraitTreeEntry.SkillLineID, tree);

                if (skillLineEntry.CategoryID == SkillCategory.Class)
                {
                    foreach (SkillRaceClassInfoRecord skillRaceClassInfo in Program.DataAccess.SkillRaceClassInfoSorted[skillLineEntry.Id])
                        for (int i = 1; i < (int)PlayerClass.Max; ++i)
                            if ((skillRaceClassInfo.ClassMask & (1 << (i - 1))) != 0)
                                _skillLinesByClass[i] = skillLineXTraitTreeEntry.SkillLineID;

                    tree.ConfigType = TraitConfigType.Combat;
                }
                else
                {
                    tree.ConfigType = TraitConfigType.Profession;
                }
            }

            foreach (var (traitSystemId, traitTreeId) in traitTreesIdsByTraitSystem.KeyValueList)
            {
                TraitTree tree = TraitTrees.LookupByKey(traitTreeId);

                if (tree != null)
                    _traitTreesByTraitSystem.Add(traitSystemId, tree);
            }

            foreach (TraitCurrencySourceRecord traitCurrencySource in CliDB.TraitCurrencySourceStorage.Values)
                _traitCurrencySourcesByCurrency.Add(traitCurrencySource.TraitCurrencyID, traitCurrencySource);

            foreach (TraitDefinitionEffectPointsRecord traitDefinitionEffectPoints in CliDB.TraitDefinitionEffectPointsStorage.Values)
                _traitDefinitionEffectPointModifiers.Add(traitDefinitionEffectPoints.TraitDefinitionID, traitDefinitionEffectPoints);

            Dictionary<int, List<TraitTreeLoadoutEntryRecord>> traitTreeLoadoutEntries = new();

            foreach (TraitTreeLoadoutEntryRecord traitTreeLoadoutEntry in CliDB.TraitTreeLoadoutEntryStorage.Values)
                traitTreeLoadoutEntries.AddListItem(traitTreeLoadoutEntry.TraitTreeLoadoutID, traitTreeLoadoutEntry);

            foreach (TraitTreeLoadoutRecord traitTreeLoadout in CliDB.TraitTreeLoadoutStorage.Values)
            {
                if (traitTreeLoadoutEntries.ContainsKey(traitTreeLoadout.Id))
                {
                    if (!TraitTreeLoadoutsByChrSpecialization.ContainsKey(traitTreeLoadout.ChrSpecializationID))
                        TraitTreeLoadoutsByChrSpecialization.Add(traitTreeLoadout.ChrSpecializationID, new List<TraitTreeLoadoutEntryRecord>());

                    var entries = traitTreeLoadoutEntries[(int)traitTreeLoadout.Id];
                    entries.Sort((left, right) => { return left.OrderIndex.CompareTo(right.OrderIndex); });

                    // there should be only one loadout per spec, we take last one encountered
                    TraitTreeLoadoutsByChrSpecialization[traitTreeLoadout.ChrSpecializationID] = entries;
                }
            }

            foreach (SpecSetMemberRecord specSetMember in CliDB.SpecSetMemberStorage.Values)
                _specsBySpecSet.Add(Tuple.Create((int)specSetMember.SpecSetID, (uint)specSetMember.ChrSpecializationID));

            foreach (var node in CliDB.TraitNodeXTraitNodeEntryStorage)
            {
                var entry = CliDB.TraitNodeEntryStorage[(uint)node.Value.TraitNodeEntryID];
                TraitDefinitionByNodeID[node.Value.TraitNodeID] = CliDB.TraitDefinitionStorage[(uint)entry.TraitDefinitionID];
            }
        }

        public static bool IsSpecSetMember(int specSetId, uint specId)
        {
            return _specsBySpecSet.Contains(Tuple.Create(specSetId, specId));
        }

        private static void BuildClassSpecs()
        {
            ClassSpecs[(int)SpecID.Arms] = PlayerClass.Warrior;
            ClassSpecs[(int)SpecID.Fury] = PlayerClass.Warrior;
            ClassSpecs[(int)SpecID.ProtectionWarrior] = PlayerClass.Warrior;

            ClassSpecs[(int)SpecID.HolyPaladin] = PlayerClass.Paladin;
            ClassSpecs[(int)SpecID.Retribution] = PlayerClass.Paladin;
            ClassSpecs[(int)SpecID.ProtectionPaladin] = PlayerClass.Paladin;

            ClassSpecs[(int)SpecID.Marksmanship] = PlayerClass.Hunter;
            ClassSpecs[(int)SpecID.Survival] = PlayerClass.Hunter;
            ClassSpecs[(int)SpecID.BeastMastery] = PlayerClass.Hunter;

            ClassSpecs[(int)SpecID.Assassination] = PlayerClass.Rogue;
            ClassSpecs[(int)SpecID.Outlaw] = PlayerClass.Rogue;
            ClassSpecs[(int)SpecID.Subtlety] = PlayerClass.Rogue;

            ClassSpecs[(int)SpecID.HolyPriest] = PlayerClass.Priest;
            ClassSpecs[(int)SpecID.Discipline] = PlayerClass.Priest;
            ClassSpecs[(int)SpecID.Shadow] = PlayerClass.Priest;

            ClassSpecs[(int)SpecID.Blood] = PlayerClass.Deathknight;
            ClassSpecs[(int)SpecID.FrostDK] = PlayerClass.Deathknight;
            ClassSpecs[(int)SpecID.Unholy] = PlayerClass.Deathknight;

            ClassSpecs[(int)SpecID.Elemental] = PlayerClass.Shaman;
            ClassSpecs[(int)SpecID.Enhancement] = PlayerClass.Shaman;
            ClassSpecs[(int)SpecID.RestorationShaman] = PlayerClass.Shaman;

            ClassSpecs[(int)SpecID.FrostMage] = PlayerClass.Mage;
            ClassSpecs[(int)SpecID.Fire] = PlayerClass.Mage;
            ClassSpecs[(int)SpecID.Arcane] = PlayerClass.Mage;

            ClassSpecs[(int)SpecID.Demonology] = PlayerClass.Warlock;
            ClassSpecs[(int)SpecID.Destruction] = PlayerClass.Warlock;
            ClassSpecs[(int)SpecID.Affliction] = PlayerClass.Warlock;

            ClassSpecs[(int)SpecID.Windwalker] = PlayerClass.Monk;
            ClassSpecs[(int)SpecID.Brewmaster] = PlayerClass.Monk;
            ClassSpecs[(int)SpecID.Mistweaver] = PlayerClass.Monk;

            ClassSpecs[(int)SpecID.Feral] = PlayerClass.Druid;
            ClassSpecs[(int)SpecID.Guardian] = PlayerClass.Druid;
            ClassSpecs[(int)SpecID.RestorationDruid] = PlayerClass.Druid;
            ClassSpecs[(int)SpecID.Balance] = PlayerClass.Druid;

            ClassSpecs[(int)SpecID.Havoc] = PlayerClass.DemonHunter;
            ClassSpecs[(int)SpecID.Vengeance] = PlayerClass.DemonHunter;

            ClassSpecs[(int)SpecID.Devastation] = PlayerClass.Evoker;
            ClassSpecs[(int)SpecID.Preservation] = PlayerClass.Evoker;
        }

        public static List<TraitTree> GetTraitTreesBySpecID(int specID)
        {
            ChrSpecializationRecord chrSpecializationEntry = CliDB.ChrSpecializationStorage.LookupByKey(specID);

            if (chrSpecializationEntry != null)
                return _traitTreesBySkillLine.LookupByKey(_skillLinesByClass[chrSpecializationEntry.ClassID]);

            return null;
        }
    }
}
