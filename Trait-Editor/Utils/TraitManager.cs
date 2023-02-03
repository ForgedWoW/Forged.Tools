using Framework.Constants;
using Game.Conditions;
using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static readonly Dictionary<int, Class> ClassSpecs = new();
        public static readonly Dictionary<uint, TraitTree> TraitTrees = new();
        public static readonly Dictionary<int, List<TraitTreeLoadoutEntryRecord>> TraitTreeLoadoutsByChrSpecialization = new();
        public static readonly Dictionary<uint, TraitNode> TraitNodes = new();
        public static readonly Dictionary<int, TraitDefinitionRecord> TraitDefinitionByNodeID = new();

        public static uint COMMIT_COMBAT_TRAIT_CONFIG_CHANGES_SPELL_ID = 384255u;
        public static uint MAX_COMBAT_TRAIT_CONFIGS = 10u;

        private static readonly Dictionary<int, TraitNodeGroup> _traitGroups = new();
        private static readonly int[] _skillLinesByClass = new int[(int)Class.Max];
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

            foreach (TraitNodeEntryXTraitCondRecord traitNodeEntryXTraitCondEntry in DataAccess.TraitNodeEntryXTraitCondStorage.Values)
            {
                TraitCondRecord traitCondEntry = DataAccess.TraitCondStorage.LookupByKey(traitNodeEntryXTraitCondEntry.TraitCondID);

                if (traitCondEntry != null)
                    nodeEntryConditions.Add((int)traitNodeEntryXTraitCondEntry.TraitNodeEntryID, traitCondEntry);
            }

            MultiMap<int, TraitCostRecord> nodeEntryCosts = new();

            foreach (TraitNodeEntryXTraitCostRecord traitNodeEntryXTraitCostEntry in DataAccess.TraitNodeEntryXTraitCostStorage.Values)
            {
                TraitCostRecord traitCostEntry = DataAccess.TraitCostStorage.LookupByKey(traitNodeEntryXTraitCostEntry.TraitCostID);

                if (traitCostEntry != null)
                    nodeEntryCosts.Add(traitNodeEntryXTraitCostEntry.TraitNodeEntryID, traitCostEntry);
            }

            MultiMap<int, TraitCondRecord> nodeGroupConditions = new();

            foreach (TraitNodeGroupXTraitCondRecord traitNodeGroupXTraitCondEntry in DataAccess.TraitNodeGroupXTraitCondStorage.Values)
            {
                TraitCondRecord traitCondEntry = DataAccess.TraitCondStorage.LookupByKey(traitNodeGroupXTraitCondEntry.TraitCondID);

                if (traitCondEntry != null)
                    nodeGroupConditions.Add(traitNodeGroupXTraitCondEntry.TraitNodeGroupID, traitCondEntry);
            }

            MultiMap<int, TraitCostRecord> nodeGroupCosts = new();

            foreach (TraitNodeGroupXTraitCostRecord traitNodeGroupXTraitCostEntry in DataAccess.TraitNodeGroupXTraitCostStorage.Values)
            {
                TraitCostRecord traitCondEntry = DataAccess.TraitCostStorage.LookupByKey(traitNodeGroupXTraitCostEntry.TraitCostID);

                if (traitCondEntry != null)
                    nodeGroupCosts.Add(traitNodeGroupXTraitCostEntry.TraitNodeGroupID, traitCondEntry);
            }

            MultiMap<int, int> nodeGroups = new();

            foreach (TraitNodeGroupXTraitNodeRecord traitNodeGroupXTraitNodeEntry in DataAccess.TraitNodeGroupXTraitNodeStorage.Values)
                nodeGroups.Add(traitNodeGroupXTraitNodeEntry.TraitNodeID, traitNodeGroupXTraitNodeEntry.TraitNodeGroupID);

            MultiMap<int, TraitCondRecord> nodeConditions = new();

            foreach (TraitNodeXTraitCondRecord traitNodeXTraitCondEntry in DataAccess.TraitNodeXTraitCondStorage.Values)
            {
                TraitCondRecord traitCondEntry = DataAccess.TraitCondStorage.LookupByKey(traitNodeXTraitCondEntry.TraitCondID);

                if (traitCondEntry != null)
                    nodeConditions.Add(traitNodeXTraitCondEntry.TraitNodeID, traitCondEntry);
            }

            MultiMap<uint, TraitCostRecord> nodeCosts = new();

            foreach (TraitNodeXTraitCostRecord traitNodeXTraitCostEntry in DataAccess.TraitNodeXTraitCostStorage.Values)
            {
                TraitCostRecord traitCostEntry = DataAccess.TraitCostStorage.LookupByKey(traitNodeXTraitCostEntry.TraitCostID);

                if (traitCostEntry != null)
                    nodeCosts.Add(traitNodeXTraitCostEntry.TraitNodeID, traitCostEntry);
            }

            MultiMap<int, TraitNodeEntryRecord> nodeEntries = new();

            foreach (TraitNodeXTraitNodeEntryRecord traitNodeXTraitNodeEntryEntry in DataAccess.TraitNodeXTraitNodeEntryStorage.Values)
            {
                TraitNodeEntryRecord traitNodeEntryEntry = DataAccess.TraitNodeEntryStorage.LookupByKey(traitNodeXTraitNodeEntryEntry.TraitNodeEntryID);

                if (traitNodeEntryEntry != null)
                    nodeEntries.Add(traitNodeXTraitNodeEntryEntry.TraitNodeID, traitNodeEntryEntry);
            }

            MultiMap<uint, TraitCostRecord> treeCosts = new();

            foreach (TraitTreeXTraitCostRecord traitTreeXTraitCostEntry in DataAccess.TraitTreeXTraitCostStorage.Values)
            {
                TraitCostRecord traitCostEntry = DataAccess.TraitCostStorage.LookupByKey(traitTreeXTraitCostEntry.TraitCostID);

                if (traitCostEntry != null)
                    treeCosts.Add(traitTreeXTraitCostEntry.TraitTreeID, traitCostEntry);
            }

            MultiMap<int, TraitCurrencyRecord> treeCurrencies = new();

            foreach (TraitTreeXTraitCurrencyRecord traitTreeXTraitCurrencyEntry in DataAccess.TraitTreeXTraitCurrencyStorage.Values)
            {
                TraitCurrencyRecord traitCurrencyEntry = DataAccess.TraitCurrencyStorage.LookupByKey(traitTreeXTraitCurrencyEntry.TraitCurrencyID);

                if (traitCurrencyEntry != null)
                    treeCurrencies.Add(traitTreeXTraitCurrencyEntry.TraitTreeID, traitCurrencyEntry);
            }

            MultiMap<int, int> traitTreesIdsByTraitSystem = new();

            foreach (TraitTreeRecord traitTree in DataAccess.TraitTreeStorage.Values)
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

            foreach (TraitNodeGroupRecord traitNodeGroup in DataAccess.TraitNodeGroupStorage.Values)
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

            foreach (TraitNodeRecord traitNode in DataAccess.TraitNodeStorage.Values)
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

            foreach (TraitEdgeRecord traitEdgeEntry in DataAccess.TraitEdgeStorage.Values)
            {
                TraitNode left = TraitNodes.LookupByKey(traitEdgeEntry.LeftTraitNodeID);
                TraitNode right = TraitNodes.LookupByKey(traitEdgeEntry.RightTraitNodeID);

                if (left == null ||
                    right == null)
                    continue;

                right.ParentNodes.Add(Tuple.Create(left, (TraitEdgeType)traitEdgeEntry.Type));
            }

            foreach (SkillLineXTraitTreeRecord skillLineXTraitTreeEntry in DataAccess.SkillLineXTraitTreeStorage.Values)
            {
                TraitTree tree = TraitTrees.LookupByKey(skillLineXTraitTreeEntry.TraitTreeID);

                if (tree == null)
                    continue;

                SkillLineRecord skillLineEntry = DataAccess.SkillLineStorage.LookupByKey(skillLineXTraitTreeEntry.SkillLineID);

                if (skillLineEntry == null)
                    continue;

                _traitTreesBySkillLine.Add(skillLineXTraitTreeEntry.SkillLineID, tree);

                if (skillLineEntry.CategoryID == SkillCategory.Class)
                {
                    foreach (SkillRaceClassInfoRecord skillRaceClassInfo in DataAccess.SkillRaceClassInfoSorted[skillLineEntry.Id])
                        for (int i = 1; i < (int)Class.Max; ++i)
                            if ((skillRaceClassInfo.ClassMask & (1 << (i - 1))) != 0)
                                _skillLinesByClass[i] = skillLineXTraitTreeEntry.SkillLineID;

                    tree.ConfigType = TraitConfigType.Combat;
                }
                else
                {
                    tree.ConfigType = TraitConfigType.Profession;
                }
            }

            foreach (var (traitSystemId, traitTreeId) in traitTreesIdsByTraitSystem)
            {
                TraitTree tree = TraitTrees.LookupByKey(traitTreeId);

                if (tree != null)
                    _traitTreesByTraitSystem.Add(traitSystemId, tree);
            }

            foreach (TraitCurrencySourceRecord traitCurrencySource in DataAccess.TraitCurrencySourceStorage.Values)
                _traitCurrencySourcesByCurrency.Add(traitCurrencySource.TraitCurrencyID, traitCurrencySource);

            foreach (TraitDefinitionEffectPointsRecord traitDefinitionEffectPoints in DataAccess.TraitDefinitionEffectPointsStorage.Values)
                _traitDefinitionEffectPointModifiers.Add(traitDefinitionEffectPoints.TraitDefinitionID, traitDefinitionEffectPoints);

            Dictionary<int, List<TraitTreeLoadoutEntryRecord>> traitTreeLoadoutEntries = new();

            foreach (TraitTreeLoadoutEntryRecord traitTreeLoadoutEntry in DataAccess.TraitTreeLoadoutEntryStorage.Values)
                traitTreeLoadoutEntries.AddListItem(traitTreeLoadoutEntry.TraitTreeLoadoutID, traitTreeLoadoutEntry);

            foreach (TraitTreeLoadoutRecord traitTreeLoadout in DataAccess.TraitTreeLoadoutStorage.Values)
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

            foreach (SpecSetMemberRecord specSetMember in DataAccess.SpecSetMemberStorage.Values)
                _specsBySpecSet.Add(Tuple.Create((int)specSetMember.SpecSetID, (uint)specSetMember.ChrSpecializationID));

            foreach (var node in DataAccess.TraitNodeXTraitNodeEntryStorage)
            {
                var entry = DataAccess.TraitNodeEntryStorage[node.Value.TraitNodeEntryID];
                TraitDefinitionByNodeID[node.Value.TraitNodeID] = DataAccess.TraitDefinitionStorage[entry.TraitDefinitionID];
            }
        }

        public static bool IsSpecSetMember(int specSetId, uint specId)
        {
            return _specsBySpecSet.Contains(Tuple.Create(specSetId, specId));
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

        public static List<TraitTree> GetTraitTreesBySpecID(int specID)
        {
            ChrSpecializationRecord chrSpecializationEntry = DataAccess.ChrSpecializationStorage.LookupByKey(specID);

            if (chrSpecializationEntry != null)
                return _traitTreesBySkillLine.LookupByKey(_skillLinesByClass[chrSpecializationEntry.ClassID]);

            return null;
        }
    }
}
