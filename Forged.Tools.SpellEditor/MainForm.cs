using Forged.Tools.SpellEditor.Models;
using Forged.Tools.SpellEditor.Utils;
using System.Data;
using Game.DataStorage;
using Framework.Constants;
using Forged.Tools.Shared.Spells;
using Forged.Tools.Shared.Entities;
using Framework.Database;
using Framework.Dynamic;
using Forged.Tools.Shared.Forms;
using Framework.Configuration;
using System.IO.Compression;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Forged.Tools.SpellEditor
{
    public partial class MainForm : Form
    {
        #region Vairables

        public FullSpellInfo CurrentSpell = null;

        TabPage _defaultSpellEffectPage = null;
        GroupBox _defaultCurveEffect = null;
        string _iconFolder = "";
        string _currentNameSearch = "";
        string _currentIconSearch = "";
        uint _maxSpellSearch = 0;

        Dictionary<uint, int> _castTimeMap = new Dictionary<uint, int>();
        Dictionary<uint, int> _radiusMap = new Dictionary<uint, int>();
        Dictionary<uint, int> _totemCatMap = new Dictionary<uint, int>();
        Dictionary<uint, int> _currencyTypeMap = new Dictionary<uint, int>();
        Dictionary<uint, SpellXSpellVisualRecordMod> _dirtySpellVisuals = new Dictionary<uint, SpellXSpellVisualRecordMod>();
        Dictionary<uint, SpellReagentsCurrencyRecordMod> _dirtyCurrencyRecords = new Dictionary<uint, SpellReagentsCurrencyRecordMod>();

        #endregion

        #region Initialize

        public MainForm()
        {
            InitializeComponent();
            cmbIndexing.SelectedIndex = 0;

            numCurentMin.KeyDown += numCurentMinMax_KeyDown;
            numCurentMax.KeyDown += numCurentMinMax_KeyDown;
            numIdSearch.KeyDown += SearchId_KeyDown;
            txtSearch.KeyDown += Search_KeyDown;
            txtIconSearch.KeyDown += IconSearch_KeyDown;
            numIconPage.KeyDown += numIconPage_KeyDown;
            cmbIndexing.SelectedIndexChanged += cmbIndexing_SelectedIndexChanged;

            txtPwrCost1.MakeNumberBox();
            txtPwrCost2.MakeNumberBox();
            txtPwrCost3.MakeNumberBox();
            txtPwrCost4.MakeNumberBox();
            txtPwrCostMaxPct1.MakeNumberBox();
            txtPwrCostMaxPct2.MakeNumberBox();
            txtPwrCostMaxPct3.MakeNumberBox();
            txtPwrCostMaxPct4.MakeNumberBox();
            txtPwrCostPerSec1.MakeNumberBox();
            txtPwrCostPerSec2.MakeNumberBox();
            txtPwrCostPerSec3.MakeNumberBox();
            txtPwrCostPerSec4.MakeNumberBox();
            txtSpeed.MakeNumberBox();
            txtVisualProbability.MakeNumberBox();
            txtConeAngle.MakeNumberBox();
            txtWidth.MakeNumberBox();
            txtLaunchDelay.MakeNumberBox();

            var localDir = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("Forged.Tools.SpellEditor.dll", "");
            var versionFile = Path.Combine(localDir, "NewVersion.txt");
            var oldVersionFile = Path.Combine(localDir, "CurrentVersion.txt");

            bool newVersion = false;
        
            DownloadGoogleDriveFile.DriveDownloadFile("1jx6kFQnPR2GDSrLmwinwHGrheeD3SGUM", versionFile);

            if (!File.Exists(oldVersionFile))
            {
                newVersion = true;
                File.Copy(versionFile, oldVersionFile);
            }
            else
            {
                if (!int.TryParse(File.ReadAllText(versionFile).Trim(), out var newVers) ||
                    !int.TryParse(File.ReadAllText(oldVersionFile).Trim(), out var oldVer) ||
                    oldVer < newVers)
                {
                    newVersion = true;
                    File.Copy(versionFile, oldVersionFile);
                }
            }

            File.Delete(versionFile);

            Program.DataAccess.LoadStores(newVersion);
            _iconFolder = ConfigMgr.GetDefaultValue("Tools.IconDir", "{FullSpellEditorPath}").Replace("{FullSpellEditorPath}", localDir);

            if (!Directory.Exists(Path.Combine(_iconFolder, "Interface", "Icons")))
            {
                var icons = "1pRZ04T67qePO-pLT3fK2gZ8MtIHvhns9";
                var zipFile = Path.Combine(localDir, "icons.zip");

                if (File.Exists(zipFile))
                    File.Delete(zipFile);

                DownloadGoogleDriveFile.DriveDownloadFile(icons, zipFile);

                System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, _iconFolder);
                Thread.Sleep(1000);
                File.Delete(zipFile);
            }

            SpellManager.Instance.LoadSpellInfoStore(Program.DataAccess);

            listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
            GenerateDefaultSpellInfo();

            Load += MainForm_Load;
            FormClosed += MainForm_FormClosed;
        }

        private void GenerateDefaultSpellInfo()
        {
            // basic spell info
            cmbSpellFamily.Items.AddEnumNames(typeof(SpellFamilyNames));
            cmbSpellFamily.SelectedIndex = 0;

            DataTable durItems = MultiLineComboBox.GeneratteDataTable();
            foreach (var dur in CliDB.SpellDurationStorage.OrderBy(d => d.Value.Duration).ThenBy(d => d.Value.MaxDuration))
                durItems.Rows.Add(new object[] { dur.Key, $"{dur.Value.Duration} ms - {dur.Value.MaxDuration} ms",
                    $"Base: {dur.Value.Duration} ms{Environment.NewLine}Max: {dur.Value.MaxDuration} ms{Environment.NewLine}ID: {dur.Value.Id}" });

            mlcmbDuration.InitializeComboBox(durItems);
            mlcmbDuration.SetSelectedById(645);

            int counter = 0;
            foreach (var dur in CliDB.SpellCastTimesStorage.OrderBy(d => d.Value.Base).ThenBy(d => d.Value.Minimum))
            {
                cmbCastTime.Items.Add($"{dur.Value.Base} - Min: {dur.Value.Minimum}");
                _castTimeMap.Add(dur.Key, counter);
                counter++;
            }
            cmbCastTime.SelectedIndex = 1;

            DataTable rangeItems = MultiLineComboBox.GeneratteDataTable();
            foreach (var range in CliDB.SpellRangeStorage.OrderBy(d => d.Key))
                rangeItems.Rows.Add(new object[] { range.Key, $"{range.Value.RangeMax[0]} - {range.Value.RangeMax[1]}",
                    $"{range.Value.DisplayName}{Environment.NewLine}Min: {range.Value.RangeMin[0]} - {range.Value.RangeMin[1]}{Environment.NewLine}Max: {range.Value.RangeMax[0]} - {range.Value.RangeMax[1]}" });

            mlcmbRange.InitializeComboBox(rangeItems);
            mlcmbRange.SetSelectedById(0);

            cmbCasterAuraState.Items.AddEnumNames(typeof(AuraStateType));
            cmbCasterAuraState.SelectedIndex = 0;
            cmbTargetAuraState.Items.AddEnumNames(typeof(AuraStateType));
            cmbTargetAuraState.SelectedIndex = 0;
            cmbExCasterAuraState.Items.AddEnumNames(typeof(AuraStateType));
            cmbExCasterAuraState.SelectedIndex = 0;
            cmbExTargetAuraState.Items.AddEnumNames(typeof(AuraStateType));
            cmbExTargetAuraState.SelectedIndex = 0;

            // Spell Category Flags
            listCatFlags.Items.AddEnumNames(typeof(SpellCategoryFlags));

            cmbDispelType.Items.AddEnumNames(typeof(DispelType));
            cmbDispelType.Items.Remove("AllMask");
            cmbDispelType.SelectedIndex = 0;
            cmbMechanic.Items.AddEnumNames(typeof(Mechanics));
            cmbMechanic.SelectedIndex = 0;
            cmbPreventionType.Items.AddEnumNames(typeof(SpellPreventionType));
            cmbPreventionType.SelectedIndex = 0;
            cmbDamageType.Items.AddEnumNames(typeof(SpellDmgClass));
            cmbDamageType.SelectedIndex = 0;
            cmbDifficultyId.Items.AddEnumNames(typeof(Difficulty));
            cmbDifficultyId.SelectedIndex = 0;

            // power info
            foreach (TabPage tab in tabsPowerConfig.TabPages)
            {
                foreach (Control control in tab.Controls)
                {
                    if (control.Tag == "PowerType")
                    {
                        ((ComboBox)control).Items.AddEnumNames(typeof(PowerType));
                        ((ComboBox)control).SelectedIndex = 0;
                        break;
                    }
                }
            }

            listSpellSchool.Items.AddEnumNames(typeof(SpellSchoolMask));
            listSpellSchool.Items.Remove("None");
            listSpellSchool.Items.Remove("Spell");
            listSpellSchool.Items.Remove("Magic");
            listSpellSchool.Items.Remove("All");

            // Proc info
            listProcTargets.Items.AddEnumNames(typeof(SpellCastTargetFlags));
            listProcTargets.Items.Remove("UnitMask");
            listProcTargets.Items.Remove("GameobjectMask");
            listProcTargets.Items.Remove("CorpseMask");
            listProcTargets.Items.Remove("ItemMask");
            listProcFlags.Items.AddEnumNames(typeof(ProcFlags));
            listProcFlags.Items.Remove("AutoAttackMask");
            listProcFlags.Items.Remove("MeleeMask");
            listProcFlags.Items.Remove("RangedMask");
            listProcFlags.Items.Remove("SpellMask");
            listProcFlags.Items.Remove("DoneHitMask");
            listProcFlags.Items.Remove("TakenHitMask");
            listProcFlags.Items.Remove("ReqSpellPhaseMask");
            listProcFlags.Items.Remove("MeleeBasedTriggerMask");
            listProcFlags2.Items.AddEnumNames(typeof(ProcFlags2));

            cmbSpellFocus.Items.Add($"0 - None");
            foreach (var focus in CliDB.SpellFocusObjectStorage.OrderBy(a => a.Key))
                cmbSpellFocus.Items.Add($"{focus.Key} - {focus.Value.Name}");
            cmbSpellFocus.SelectedIndex = 0;

            // Spell effect stuff
            cmbSpellEffect.Items.AddEnumNames(typeof(SpellEffectName));
            cmbSpellEffect.SelectedIndex = 0;
            cmbEffMechanic.Items.AddEnumNames(typeof(Mechanics));
            cmbEffMechanic.Items.Remove("ImmuneToMovementImpairmentAndLossControlMask");
            cmbEffMechanic.SelectedIndex = 0;
            cmbTargetA.Items.AddEnumNames(typeof(Targets));
            cmbTargetA.SelectedIndex = 0;
            cmbTargetB.Items.AddEnumNames(typeof(Targets));
            cmbTargetB.SelectedIndex = 0;
            cmbApplyAura.Items.AddEnumNames(typeof(AuraType));
            cmbApplyAura.SelectedIndex = 0;

            counter = 0;
            foreach (var rec in CliDB.SpellRadiusStorage.OrderBy(r => r.Key))
            {
                _radiusMap.Add(rec.Key, counter);
                string item = $"ID {rec.Key}: {rec.Value.RadiusMin} - {rec.Value.RadiusMax} (Per Level: {rec.Value.RadiusPerLevel})";
                cmbRadiusMin.Items.Add(item);
                cmbRadiusMax.Items.Add(item);
                counter++;
            }
            cmbRadiusMin.SelectedIndex = 0;
            cmbRadiusMax.SelectedIndex = 0;

            cmbVisualDifficulty.Items.AddEnumNames(typeof(Difficulty));
            cmbVisualDifficulty.SelectedIndex = 0;

            _defaultSpellEffectPage = tabsSpellEffects.TabPages[0];
            tabsSpellEffects.TabPages.Clear();

            _defaultCurveEffect = grpCurveEffect;
            pnlCurves.Controls.Clear();

            // Spell Attribute tables
            listAttr0.Items.AddEnumNames(typeof(SpellAttr0));
            listAttr1.Items.AddEnumNames(typeof(SpellAttr1));
            listAttr2.Items.AddEnumNames(typeof(SpellAttr2));
            listAttr3.Items.AddEnumNames(typeof(SpellAttr3));
            listAttr4.Items.AddEnumNames(typeof(SpellAttr4));
            listAttr5.Items.AddEnumNames(typeof(SpellAttr5));
            listAttr6.Items.AddEnumNames(typeof(SpellAttr6));
            listAttr7.Items.AddEnumNames(typeof(SpellAttr7));
            listAttr8.Items.AddEnumNames(typeof(SpellAttr8));
            listAttr9.Items.AddEnumNames(typeof(SpellAttr9));
            listAttr10.Items.AddEnumNames(typeof(SpellAttr10));
            listAttr11.Items.AddEnumNames(typeof(SpellAttr11));
            listAttr12.Items.AddEnumNames(typeof(SpellAttr12));
            listAttr13.Items.AddEnumNames(typeof(SpellAttr13));
            listAttr14.Items.AddEnumNames(typeof(SpellAttr14));

            // flags
            listInterruptFlags.Items.AddEnumNames(typeof(SpellInterruptFlags));
            listAuraInterruptFlags.Items.AddEnumNames(typeof(SpellAuraInterruptFlags));
            listAuraInterruptFlags.Items.Remove("NotVictim");
            listAuraInterruptFlags2.Items.AddEnumNames(typeof(SpellAuraInterruptFlags2));
            listChannelInterruptFlags.Items.AddEnumNames(typeof(SpellAuraInterruptFlags));
            listChannelInterruptFlags.Items.Remove("NotVictim");
            listChannelInterruptFlags2.Items.AddEnumNames(typeof(SpellAuraInterruptFlags2));
            listTargetCreatureType.Items.AddEnumNames(typeof(CreatureType));
            listTargetCreatureType.Items.Remove("MaskDemonOrUndead");
            listTargetCreatureType.Items.Remove("MaskHumanoidOrUndead");
            listTargetCreatureType.Items.Remove("MaskMechanicalOrElemental");
            listStances.Items.AddEnumNames(typeof(ShapeShiftForm));
            listExStances.Items.AddEnumNames(typeof(ShapeShiftForm));

            counter = 1;
            cmbTotemCategory1.Items.Add("None");
            cmbTotemCategory2.Items.Add("None");
            _totemCatMap.Add(0, 0);
            // item stuff
            foreach (var cat in  CliDB.TotemCategoryStorage.OrderBy(a => a.Key))
            {
                _totemCatMap.Add(cat.Key, counter);
                counter++;
                cmbTotemCategory1.Items.Add(cat.Value.Name);
                cmbTotemCategory2.Items.Add(cat.Value.Name);
            }

            counter = 1;
            cmbCurrencyType.Items.Add("None");
            _currencyTypeMap.Add(0, 0);
            // item stuff
            foreach (var cat in CliDB.CurrencyTypesStorage.OrderBy(a => a.Key))
            {
                _currencyTypeMap.Add(cat.Key, counter);
                counter++;
                cmbCurrencyType.Items.Add(cat.Value.Name);
            }

            cmbEquippedItemClass.Items.AddEnumNames(typeof(ItemClass));
            cmbEquippedItemClass.Items.Remove("Max");
            listEquippedItemInvenType.Items.AddEnumNames(typeof(InventoryType));
            listEquippedItemInvenType.Items.Remove("Max");

            // icon stuff. 
            lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);

            DataTable editCatItems = MultiLineComboBox.GeneratteDataTable();
            editCatItems.Rows.Add(new object[] { 0, $"None", ChargeCategoryDisplay(new SpellCategoryRecord() { Name = "None" }) });

            var spellCats = CliDB.SpellCategoryStorage.OrderBy(a => a.Key);
            foreach (var spellCat in spellCats)
                editCatItems.Rows.Add(new object[] { spellCat.Key, spellCat.Value.Name, ChargeCategoryDisplay(spellCat.Value) });

            mlcmbEditCat.InitializeComboBox(editCatItems);
            mlcmbEditCat.SetSelectedById(0);

            DataTable ppmItems = MultiLineComboBox.GeneratteDataTable();
            ppmItems.Rows.Add(new object[] { 0, "PPM: 0", $"PPM: 0{Environment.NewLine}Flags: 0{Environment.NewLine}ID: 0" });
            var test = CliDB.SpellProcsPerMinuteStorage.OrderBy(a => a.Key);
            var test2 = CliDB.SpellProcsPerMinuteModStorage.OrderBy(a => a.Key).Where(a => a.Value.SpellProcsPerMinuteID == 420).ToList();
            foreach (var ppm in CliDB.SpellProcsPerMinuteStorage.OrderByDescending(d => d.Value.BaseProcRate))
                ppmItems.Rows.Add(new object[] { ppm.Key, $"PPM: {ppm.Value.BaseProcRate}", $"PPM: {ppm.Value.BaseProcRate}{Environment.NewLine}Flags: {ppm.Value.Flags}{Environment.NewLine}ID: {ppm.Value.Id}" });

            mlcmbPPM.InitializeComboBox(ppmItems);
            mlcmbPPM.SetSelectedById(0);
        }

        #endregion

        // clicked a spell
        private void listSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((string)listSpells.SelectedItem))
            {
                uint spellId = uint.Parse(((string)listSpells.SelectedItem).Split("-").First().Trim());
                if (!CliDB.SpellNameStorage.ContainsKey(spellId))
                    return;

                CurrentSpell = Program.DataAccess.GetSpellInfo(spellId);
                LoadCurrentSpell();
            }
        }

        private void LoadCurrentSpell()
        {
            lblCurrentSpellName.Text = Helpers.SpellDisplayName(CurrentSpell.SpellInfo.Id, CurrentSpell.SpellInfo.SpellName[Locale.enUS]);

            mlcmbEditCat.SetSelectedById(0);

            // update basic info
            txtSpellName.Text = CurrentSpell.SpellInfo.SpellName[Locale.enUS];
            spellIdChanger.Text = CurrentSpell.SpellInfo.Id.ToString();
            txtSpellNameSubtext.Text = CurrentSpell.SpellInfo.SpellDescriptions?.NameSubtext_lang;
            txtSpellDesc.Text = CurrentSpell.SpellInfo.SpellDescriptions?.Description_lang;
            txtAuraDesc.Text = CurrentSpell.SpellInfo.SpellDescriptions?.AuraDescription_lang;
            numStackAmount.Value = CurrentSpell.SpellInfo.StackAmount;
            txtSpeed.Text = CurrentSpell.SpellInfo.Speed.ToString();

            if (CurrentSpell.SpellInfo.DurationEntry != null)
                mlcmbDuration.SetSelectedById(CurrentSpell.SpellInfo.DurationEntry.Id);
            else
                mlcmbDuration.SetSelectedById(645);

            if (CurrentSpell.SpellInfo.RangeEntry != null)
                mlcmbRange.SetSelectedById(CurrentSpell.SpellInfo.RangeEntry.Id);
            else
                mlcmbRange.SetSelectedById(0);

            cmbCastTime.SelectedIndex = CurrentSpell.SpellInfo.CastTimeEntry != null && CurrentSpell.SpellInfo.CastTimeEntry.Id != 0
                ? _castTimeMap[CurrentSpell.SpellInfo.CastTimeEntry.Id] : 1;

            // class options
            cmbSpellFamily.SelectedItem = CurrentSpell.SpellInfo.SpellFamilyName.ToString();
            numFamilyFlags1.Value = CurrentSpell.SpellInfo.SpellFamilyFlags[0];
            numFamilyFlags2.Value = CurrentSpell.SpellInfo.SpellFamilyFlags[1];
            numFamilyFlags3.Value = CurrentSpell.SpellInfo.SpellFamilyFlags[2];
            numFamilyFlags4.Value = CurrentSpell.SpellInfo.SpellFamilyFlags[3];
            numModalNextSpell.Value = CurrentSpell.SpellInfo.ModalNextSpell;

            // category info
            numCategory.Value = CurrentSpell.SpellInfo.CategoryId;
            numCooldown.Value = CurrentSpell.SpellInfo.RecoveryTime;
            numCatCooldown.Value = CurrentSpell.SpellInfo.CategoryRecoveryTime;
            numChargeCat.Value = CurrentSpell.SpellInfo.ChargeCategoryId;
            numStartCooldownTime.Value = CurrentSpell.SpellInfo.StartRecoveryTime;
            cmbDispelType.SelectedItem = CurrentSpell.SpellInfo.Dispel.ToString();
            cmbMechanic.SelectedItem = CurrentSpell.SpellInfo.Mechanic.ToString();
            cmbDamageType.SelectedItem = CurrentSpell.SpellInfo.DmgClass.ToString();
            cmbDifficultyId.SelectedItem = CurrentSpell.SpellInfo.Difficulty.ToString();
            cmbPreventionType.SelectedItem = CurrentSpell.SpellInfo.PreventionType.ToString();

            // power info
            SetCurrentPowerInfo();

            // basic info 2
            numSpellLevel.Value = CurrentSpell.SpellInfo.SpellLevel;
            numMaxLevel.Value = CurrentSpell.SpellInfo.MaxLevel;
            numBaseLevel.Value = CurrentSpell.SpellInfo.BaseLevel;
            numMaxTargetLevel.Value = CurrentSpell.SpellInfo.MaxTargetLevel;
            numMaxTargets.Value = CurrentSpell.SpellInfo.MaxAffectedTargets;
            cmbCasterAuraState.SelectedItem = CurrentSpell.SpellInfo.CasterAuraState.ToString();
            cmbTargetAuraState.SelectedItem = CurrentSpell.SpellInfo.TargetAuraState.ToString();
            cmbExCasterAuraState.SelectedItem = CurrentSpell.SpellInfo.ExcludeCasterAuraState.ToString();
            cmbExTargetAuraState.SelectedItem = CurrentSpell.SpellInfo.ExcludeTargetAuraState.ToString();

            numCasterAura.Value = CurrentSpell.SpellInfo.CasterAuraSpell;
            numTargetAura.Value = CurrentSpell.SpellInfo.TargetAuraSpell;
            numExCasterAura.Value = CurrentSpell.SpellInfo.ExcludeCasterAuraSpell;
            numExTargetAura.Value = CurrentSpell.SpellInfo.ExcludeTargetAuraSpell;

            txtConeAngle.Text = CurrentSpell.SpellInfo.ConeAngle.ToString();
            txtWidth.Text = CurrentSpell.SpellInfo.Width.ToString();

            txtLabels.Text = String.Join(Environment.NewLine, CurrentSpell.SpellInfo.Labels);

            foreach (string item in cmbSpellFocus.Items)
                if (item.StartsWith(CurrentSpell.SpellInfo.RequiresSpellFocus.ToString() + " -"))
                {
                    cmbSpellFocus.SelectedItem = item;
                    break;
                }

            _dirtySpellVisuals.Clear();
            cmbSelectVisual.Items.Clear();
            cmbSelectVisual.Items.Add((uint)0);
            foreach (var visual in CurrentSpell.SpellInfo.GetSpellVisuals().OrderBy(a => a.Id))
            {
                _dirtySpellVisuals.Add(visual.Id, visual.Copy(true));
                cmbSelectVisual.Items.Add(visual.Id);
            }
            cmbSelectVisual.SelectedIndex = 0;

            btnVisualNew.Enabled = true;
            btnVisualSave.Enabled = true;
            btnVisualCopy.Enabled = true;
            btnVisualDelete.Enabled = false;

            listSpellSchool.SelectedItems.Clear();
            listSpellSchool.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.SchoolMask);
            numFacingCasterFlags.Value = CurrentSpell.SpellInfo.FacingCasterFlags;
            numRequiredAreaId.Value = CurrentSpell.SpellInfo.RequiredAreasID;

            listProcFlags.SelectedItems.Clear();
            listProcFlags2.SelectedItems.Clear();

            numContentTuningID.Value = CurrentSpell.SpellInfo.ContentTuningId;
            numShowFutureSpellPlayerConditionID.Value = CurrentSpell.SpellInfo.ShowFutureSpellPlayerConditionID;
            numMinScalingLevel.Value = CurrentSpell.SpellInfo.Scaling.MinScalingLevel;
            numMaxScalingLevel.Value = CurrentSpell.SpellInfo.Scaling.MaxScalingLevel;
            numScalesFromItemLevel.Value = CurrentSpell.SpellInfo.Scaling.ScalesFromItemLevel;

            // proc info
            if (CurrentSpell.SpellInfo.ProcFlags != null)
            {
                int flags = CurrentSpell.SpellInfo.ProcFlags[0];
                int flags2 = CurrentSpell.SpellInfo.ProcFlags[1];

                if (flags > 0)
                    listProcFlags.SelectedItems.AddSelectedBitEnum(flags, typeof(ProcFlags));
                else
                    listProcFlags.SelectedItems.Add("None");

                if (flags2 > 0)
                    listProcFlags2.SelectedItems.AddSelectedBitEnum(flags, typeof(ProcFlags2));
                else
                    listProcFlags2.SelectedItems.Add("None");
            }
            else
            {
                listProcFlags.SelectedItems.Add("None");
                listProcFlags2.SelectedItems.Add("None");
            }

            numProcChance.Value = CurrentSpell.SpellInfo.ProcChance;
            numProcCharges.Value = CurrentSpell.SpellInfo.ProcCharges;
            numProcCooldown.Value = CurrentSpell.SpellInfo.ProcCooldown;
            mlcmbPPM.SetSelectedById(0);

            listProcTargets.SelectedItems.Clear();
            listProcTargets.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.Targets);

            // spell effects
            CreateCurrentSpellEffectTabs();

            // update attribute lists
            listAttr0.SelectedItems.Clear();
            listAttr1.SelectedItems.Clear();
            listAttr2.SelectedItems.Clear();
            listAttr3.SelectedItems.Clear();
            listAttr4.SelectedItems.Clear();
            listAttr5.SelectedItems.Clear();
            listAttr6.SelectedItems.Clear();
            listAttr7.SelectedItems.Clear();
            listAttr8.SelectedItems.Clear();
            listAttr9.SelectedItems.Clear();
            listAttr10.SelectedItems.Clear();
            listAttr11.SelectedItems.Clear();
            listAttr12.SelectedItems.Clear();
            listAttr13.SelectedItems.Clear();
            listAttr14.SelectedItems.Clear();

            listAttr0.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.Attributes);
            listAttr1.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx);
            listAttr2.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx2);
            listAttr3.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx3);
            listAttr4.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx4);
            listAttr5.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx5);
            listAttr6.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx6);
            listAttr7.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx7);
            listAttr8.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx8);
            listAttr9.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx9);
            listAttr10.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx10);
            listAttr11.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx11);
            listAttr12.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx12);
            listAttr13.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx13);
            listAttr14.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AttributesEx14);

            // update flags
            listInterruptFlags.SelectedItems.Clear();
            listAuraInterruptFlags.SelectedItems.Clear();
            listAuraInterruptFlags2.SelectedItems.Clear();
            listChannelInterruptFlags.SelectedItems.Clear();
            listChannelInterruptFlags2.SelectedItems.Clear();
            listTargetCreatureType.SelectedItems.Clear();
            listStances.SelectedItems.Clear();
            listExStances.SelectedItems.Clear();
            cmbTotemCategory1.SelectedIndex = 0;
            cmbTotemCategory2.SelectedIndex = 0;
            cmbCurrencyType.SelectedIndex = 0;

            listInterruptFlags.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.InterruptFlags);
            listAuraInterruptFlags.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AuraInterruptFlags);
            listAuraInterruptFlags2.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.AuraInterruptFlags2);
            listChannelInterruptFlags.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.ChannelInterruptFlags);
            listChannelInterruptFlags2.SelectedItems.AddSelectedBitEnum(CurrentSpell.SpellInfo.ChannelInterruptFlags2);
            listTargetCreatureType.SelectedItems.AddSelectedIntEnum((CreatureType)CurrentSpell.SpellInfo.TargetCreatureType);
            listStances.SelectedItems.AddSelectedIntEnum((ShapeShiftForm)CurrentSpell.SpellInfo.Stances);
            listExStances.SelectedItems.AddSelectedIntEnum((ShapeShiftForm)CurrentSpell.SpellInfo.StancesNot);
            numTotem1.Value = CurrentSpell.SpellInfo.Totem[0];
            numTotem2.Value = CurrentSpell.SpellInfo.Totem[1];
            cmbTotemCategory1.SelectedIndex = _totemCatMap[CurrentSpell.SpellInfo.TotemCategory[0]];
            cmbTotemCategory2.SelectedIndex = _totemCatMap[CurrentSpell.SpellInfo.TotemCategory[1]];
            numReagent1.Value = CurrentSpell.SpellInfo.Reagent[0];
            numReagent2.Value = CurrentSpell.SpellInfo.Reagent[1];
            numReagent3.Value = CurrentSpell.SpellInfo.Reagent[2];
            numReagent4.Value = CurrentSpell.SpellInfo.Reagent[3];
            numReagent5.Value = CurrentSpell.SpellInfo.Reagent[4];
            numReagent6.Value = CurrentSpell.SpellInfo.Reagent[5];
            numReagent7.Value = CurrentSpell.SpellInfo.Reagent[6];
            numReagent8.Value = CurrentSpell.SpellInfo.Reagent[7];
            numReagentCount1.Value = CurrentSpell.SpellInfo.ReagentCount[0];
            numReagentCount2.Value = CurrentSpell.SpellInfo.ReagentCount[1];
            numReagentCount3.Value = CurrentSpell.SpellInfo.ReagentCount[2];
            numReagentCount4.Value = CurrentSpell.SpellInfo.ReagentCount[3];
            numReagentCount5.Value = CurrentSpell.SpellInfo.ReagentCount[4];
            numReagentCount6.Value = CurrentSpell.SpellInfo.ReagentCount[5];
            numReagentCount7.Value = CurrentSpell.SpellInfo.ReagentCount[6];
            numReagentCount8.Value = CurrentSpell.SpellInfo.ReagentCount[7];

            _dirtyCurrencyRecords.Clear();
            cmbSelectCurrency.Items.Clear();
            cmbSelectCurrency.Items.Add((uint)0);
            foreach (var currency in CurrentSpell.SpellInfo.ReagentsCurrency.OrderBy(a => a.Id))
            {
                _dirtyCurrencyRecords.Add(currency.Id, currency.Copy(true));
                cmbSelectCurrency.Items.Add(currency.Id);
            }
            cmbSelectCurrency.SelectedIndex = 0;

            btnCurrencyNew.Enabled = true;
            btnCurrencySave.Enabled = true;
            btnCurrencyCopy.Enabled = true;
            btnCurrencyDelete.Enabled = false;

            cmbEquippedItemClass.SelectedItem = CurrentSpell.SpellInfo.EquippedItemClass.ToString();
            listEquippedItemInvenType.SelectedItems.Clear();
            listEquippedItemInvenType.SelectedItems.AddSelectedIntEnum(CurrentSpell.SpellInfo.EquippedItemInventoryTypeMask, typeof(InventoryType));
            listEquippedItemSubClass.UpdateItemSubClass((ItemClass)Enum.Parse(typeof(ItemClass), (string)cmbEquippedItemClass.SelectedItem), CurrentSpell.SpellInfo.EquippedItemClass, CurrentSpell.SpellInfo.EquippedItemSubClassMask, true);
            DrawCurrentCurveInfo();

            btnCurIconUndo_Click(null, null);
            btnActiveIconUndo_Click(null, null);
        }

        private void DrawCurrentCurveInfo()
        {
            pnlCurves.Controls.Clear();

            foreach (var curve in CurrentSpell.DirtyCurves)
            {
                foreach (var cp in curve.CurvePoints)
                {
                    CreateCurveEffectBox(curve, cp);
                }
            }
        }

        private void CreateCurveEffectBox(SpellCurve curve, CurvePointRecord cp)
        {
            GroupBox bx = Helpers.CopyCurveEffect(_defaultCurveEffect, curve.TraitDefinitionEffectPoints.EffectIndex);
            bx.Parent = pnlCurves;

            int baseX = cp.Pos.X == 1 ? bx.Location.X : bx.Location.X + bx.Size.Width;
            int baseY = curve.TraitDefinitionEffectPoints.EffectIndex == 0 ? bx.Location.Y : bx.Location.Y + bx.Size.Height;
            int marginx = cp.Pos.X == 1 ? 0 : cp.OrderIndex * 10;
            int marginy = curve.TraitDefinitionEffectPoints.EffectIndex * 5;
            Point point = new((baseX * (int)cp.OrderIndex) + marginx, (baseY * (curve.TraitDefinitionEffectPoints.EffectIndex)) + marginy);
            bx.Location = point;
            bx.Tag = cp;

            bx.MouseClick += Bx_MouseClick;

            foreach (Control c in bx.Controls)
            {
                if (c.GetType() == typeof(Label))
                    continue;

                switch (c.Tag)
                {
                    // text boxes
                    case "CurveId":
                        ((TextBox)c).Text = curve.CurveRecord.Id.ToString();
                        break;
                    case "CurvePointId":
                        ((TextBox)c).Text = cp.Id.ToString();
                        break;
                    case "Rank":
                        ((TextBox)c).Text = cp.Pos.X.ToString();
                        break;
                    case "Points":
                        ((TextBox)c).Text = cp.Pos.Y.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private GroupBox _groupBoxContext = null;
        private void Bx_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _groupBoxContext = (GroupBox)sender;
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                contextMenu.Items.Add("Delete Effect");
                contextMenu.Items.Add("Delete Rank");
                contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(
                    contextMenu_ItemClicked);
                contextMenu.Show(MousePosition);
            }
        }

        private void contextMenu_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            if (_groupBoxContext == null)
                return;

            int curvepointid = -1;
            int curveid = -1;
            int rank = -1;

            foreach (Control control in _groupBoxContext.Controls)
            {
                switch (control.Tag)
                {
                    case "CurvePointId":
                        curvepointid = int.Parse(((TextBox)control).Text);
                        break;
                    case "CurveId":
                        curveid = int.Parse(((TextBox)control).Text);
                        break;
                    case "Rank":
                        rank = int.Parse(((TextBox)control).Text);
                        break;
                }
            }

            if (curvepointid != 0)
            {
                MessageBox.Show("Unable to delete existing effects or ranks.");
                _groupBoxContext = null;
                return;
            }

            switch (e.ClickedItem.Text)
            {
                case "Delete Effect":
                    if (curveid != 0)
                    {
                        MessageBox.Show("Unable to delete existing effects or ranks.");
                        _groupBoxContext = null;
                        return;
                    }

                    int effectId = int.Parse(_groupBoxContext.Text.Replace("Effect ", ""));

                    // delete effect
                    CurrentSpell.DirtyCurves.RemoveIf(a => a.TraitDefinitionEffectPoints.EffectIndex == effectId);

                    // need to clean up TraitDefinitionEffectPoints.EffectIndex for all of the effects above this one
                    if (CurrentSpell.DirtyCurves.Count >= effectId)
                    {
                        int newIndex = effectId;

                        foreach (var curve in CurrentSpell.DirtyCurves.OrderBy(a => a.TraitDefinitionEffectPoints.EffectIndex))
                        {
                            if (curve.TraitDefinitionEffectPoints.EffectIndex < effectId)
                                continue;

                            curve.TraitDefinitionEffectPoints.EffectIndex = newIndex;
                            newIndex++;
                        }
                    }
                    break;
                case "Delete Rank":
                    // loop to validate rank already exists and can not be deleted
                    foreach (var curve in CurrentSpell.DirtyCurves)
                    {
                        var cpr = curve.CurvePoints.Where(a => a.Pos.X == rank).FirstOrDefault();

                        if (cpr != null)
                        {
                            if (cpr.Id != 0)
                            {
                                MessageBox.Show("Unable to delete existing effects or ranks.");
                                _groupBoxContext = null;
                                return;
                            }
                        }
                    }

                    foreach (var curve in CurrentSpell.DirtyCurves)
                    {
                        // delete rank
                        curve.CurvePoints.RemoveIf(a => a.Pos.X == rank);
                        int newIndex = rank;

                        // correct higher ranks after deleting
                        foreach (var cpr in curve.CurvePoints.OrderBy(a => a.Pos.X))
                        {
                            if (cpr.Pos.X < rank)
                                continue;

                            cpr.OrderIndex = (byte)(newIndex - 1);
                            cpr.Pos = new System.Numerics.Vector2(newIndex, cpr.Pos.Y);
                            newIndex++;
                        }
                    }
                    break;
            }

            UpdateDirtyCurveEffectPoints();
            DrawCurrentCurveInfo();
            _groupBoxContext = null;
        }

        private void UpdateDirtyCurveEffectPoints()
        {
            foreach (Control control in pnlCurves.Controls)
            {
                if (control.GetType() != typeof(GroupBox))
                    return;

                int points = 0;

                foreach (Control bxControl in control.Controls)
                {
                    if (bxControl.Tag == "Points")
                    {
                        var cpr = (CurvePointRecord)control.Tag;
                        cpr.Pos = new System.Numerics.Vector2(cpr.Pos.X, float.Parse(bxControl.Text));
                    }
                }
            }
        }

        private void SetCurrentPowerInfo()
        {
            for (int i = 0; i < 4; i++)
            {
                var cost = CurrentSpell.SpellInfo.PowerCosts[i];
                var tab = tabsPowerConfig.TabPages[i];

                foreach (Control c in tab.Controls)
                {
                    if (c.GetType() == typeof(Label))
                        continue;

                    if (cost != null)
                        switch (c.Tag)
                        {
                            // combo boxes
                            case "PowerType":
                                ((ComboBox)c).SelectedItem = cost.PowerType.ToString();
                                break;

                            // numeric
                            case "ManaCost":
                                ((NumericUpDown)c).Value = cost.ManaCost;
                                break;
                            case "ManaCostPerLevel":
                                ((NumericUpDown)c).Value = cost.ManaCostPerLevel;
                                break;
                            case "ManaPerSecond":
                                ((NumericUpDown)c).Value = cost.ManaPerSecond;
                                break;
                            case "PowerDisplayID":
                                ((NumericUpDown)c).Value = cost.PowerDisplayID;
                                break;
                            case "AltPowerBarID":
                                ((NumericUpDown)c).Value = cost.AltPowerBarID;
                                break;
                            case "RequiredAuraID":
                                ((NumericUpDown)c).Value = cost.RequiredAuraSpellID;
                                break;
                            case "OptionalCost":
                                ((NumericUpDown)c).Value = cost.OptionalCost;
                                break;

                            // text
                            case "PowerCostPct":
                                ((TextBox)c).Text = cost.PowerCostPct.ToString();
                                break;
                            case "PowerCostMaxPct":
                                ((TextBox)c).Text = cost.PowerCostMaxPct.ToString();
                                break;
                            case "PowerPctPerSecond":
                                ((TextBox)c).Text = cost.PowerPctPerSecond.ToString();
                                break;

                            default:
                                break;
                        }
                    else
                    {
                        var type = c.GetType();
                        if (type == typeof(ComboBox))
                            ((ComboBox)c).SelectedIndex = 0;
                        else if (type == typeof(NumericUpDown))
                            ((NumericUpDown)c).Value = 0;
                        else if (type == typeof(TextBox))
                            ((TextBox)c).Text = "0";
                    }
                }
            }
        }

        private void CreateCurrentSpellEffectTabs()
        {
            tabsSpellEffects.TabPages.Clear();

            foreach (SpellEffectInfo eff in CurrentSpell.SpellInfo.GetEffects())
            {
                var tab = NewSpellEffectTab(eff.EffectIndex);
                tabsSpellEffects.TabPages.Add(tab);

                foreach (Control c in tab.Controls)
                {
                    if (c.GetType() == typeof(Label))
                        continue;

                    switch (c.Tag)
                    {
                        // combo boxes
                        case "SpellEffect":
                            ((ComboBox)c).SelectedItem = eff.Effect.ToString();
                            break;
                        case "EffMechanic":
                            ((ComboBox)c).SelectedItem = eff.Mechanic.ToString();
                            break;
                        case "TargetA":
                            ((ComboBox)c).SelectedItem = eff.TargetA.GetTarget().ToString();
                            break;
                        case "TargetB":
                            ((ComboBox)c).SelectedItem = eff.TargetB.GetTarget().ToString();
                            break;
                        case "RadiusMin":
                            if (eff.RadiusEntry != null)
                                ((ComboBox)c).SelectedIndex = _radiusMap[eff.RadiusEntry.Id];
                            else
                                ((ComboBox)c).SelectedIndex = 0;
                            break;
                        case "RadiusMax":
                            if (eff.MaxRadiusEntry != null)
                                ((ComboBox)c).SelectedIndex = _radiusMap[eff.MaxRadiusEntry.Id];
                            else
                                ((ComboBox)c).SelectedIndex = 0;
                            break;
                        case "ApplyAura":
                            ((ComboBox)c).SelectedItem = eff.ApplyAuraName.ToString();
                            break;

                        // numeric
                        case "AuraTickRate":
                            ((NumericUpDown)c).Value = eff.ApplyAuraPeriod;
                            break;
                        case "ScalingClass":
                            ((NumericUpDown)c).Value = eff.Scaling.Class;
                            break;
                        case "ChainTargets":
                            ((NumericUpDown)c).Value = eff.ChainTargets;
                            break;
                        case "TriggerSpell":
                            ((NumericUpDown)c).Value = eff.TriggerSpell;
                            break;
                        case "MiscValueA":
                            ((NumericUpDown)c).Value = eff.MiscValue;
                            break;
                        case "MiscValueB":
                            ((NumericUpDown)c).Value = eff.MiscValueB;
                            break;
                        case "EffItemType":
                            ((NumericUpDown)c).Value = eff.ItemType;
                            break;
                        case "EffTableID":
                            ((NumericUpDown)c).Value = eff.Id;
                            c.Enabled = false;
                            break;
                        case "ClassMask1":
                            if (eff.SpellClassMask != null)
                                ((NumericUpDown)c).Value = eff.SpellClassMask[0];
                            break;
                        case "ClassMask2":
                            if (eff.SpellClassMask != null)
                                ((NumericUpDown)c).Value = eff.SpellClassMask[1];
                            break;
                        case "ClassMask3":
                            if (eff.SpellClassMask != null)
                                ((NumericUpDown)c).Value = eff.SpellClassMask[2];
                            break;
                        case "ClassMask4":
                            if (eff.SpellClassMask != null)
                                ((NumericUpDown)c).Value = eff.SpellClassMask[3];
                            break;

                        // text boxes
                        case "Variance":
                            ((TextBox)c).Text = eff.Scaling.Variance.ToString();
                            break;
                        case "ScalingCoefficient":
                            ((TextBox)c).Text = eff.Scaling.Coefficient.ToString();
                            break;
                        case "ResourceCoefficient":
                            ((TextBox)c).Text = eff.Scaling.ResourceCoefficient.ToString();
                            break;
                        case "RealPoints":
                            ((TextBox)c).Text = eff.RealPointsPerLevel.ToString();
                            break;
                        case "PointsPerResource":
                            ((TextBox)c).Text = eff.PointsPerResource.ToString();
                            break;
                        case "Amplitude":
                            ((TextBox)c).Text = eff.Amplitude.ToString();
                            break;
                        case "ChainAmplitude":
                            ((TextBox)c).Text = eff.ChainAmplitude.ToString();
                            break;
                        case "BonusCoefficient":
                            ((TextBox)c).Text = eff.BonusCoefficient.ToString();
                            break;
                        case "BonusCoefficientFromAP":
                            ((TextBox)c).Text = eff.BonusCoefficientFromAP.ToString();
                            break;
                        case "PositionFacing":
                            ((TextBox)c).Text = eff.PositionFacing.ToString();
                            break;
                        case "BasePoints":
                            ((TextBox)c).Text = eff.BasePoints.ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private string ChargeCategoryDisplay(SpellCategoryRecord spellCat)
        {
            return $"{spellCat.Name}{Environment.NewLine}Charges: {spellCat.MaxCharges}{Environment.NewLine}CD: {spellCat.ChargeRecoveryTime}{Environment.NewLine}ID: {spellCat.Id}";
        }

        private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            this.Focus(); 
            this.Show();
        }

        private void SearchId_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                uint spellId = (uint)numIdSearch.Value;
                if (!CliDB.SpellNameStorage.ContainsKey(spellId))
                    return;

                CurrentSpell = Program.DataAccess.GetSpellInfo(spellId);
                LoadCurrentSpell();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void Search_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _currentNameSearch = txtSearch.Text;

                if (!string.IsNullOrWhiteSpace(_currentNameSearch))
                {
                    numCurentMax.Value = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);
                    numCurentMin.Value = 1;
                }

                listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void IconSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtIconSearch.Text) || txtIconSearch.Text != _currentIconSearch)
                    numIconPage.Value = 1;

                _currentIconSearch = txtIconSearch.Text;

                lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void numIconPage_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void numCurentMinMax_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void cmbIndexing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIndexing.SelectedIndex == 0)
                _maxSpellSearch = CliDB.SpellNameStorage.OrderBy(a => a.Value.Id).Last().Value.Id;
            else
                _maxSpellSearch = (uint)CliDB.SpellNameStorage.Count;

            listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
        }

        private void mlcmbEditCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint catId = mlcmbEditCat.SelectedItemId;

            if (catId != 0)
            {
                var cat = CliDB.SpellCategoryStorage[catId];

                txtCatName.Text = cat.Name;
                numCatId.Value = catId;
                numCatCharges.Value = cat.MaxCharges;
                numCatChargeCD.Value = cat.ChargeRecoveryTime;
                numCatUsesPerWeek.Value = cat.UsesPerWeek;
                numTypeMask.Value = cat.TypeMask;

                // category flags
                listCatFlags.SelectedItems.Clear();
                listCatFlags.SelectedItems.AddSelectedBitEnum(cat.Flags);
            }
            else
            {
                txtCatName.Text = string.Empty;
                numCatId.Value = 0;
                numCatCharges.Value = 0;
                numCatChargeCD.Value = 0;
                numCatUsesPerWeek.Value = 0;
                numTypeMask.Value = 0;
                listCatFlags.SelectedItems.Clear();
            }
        }

        // visual stuff
        private void cmbSelectVisual_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = (uint)((ComboBox)sender).SelectedItem;
            if (id > 0)
            {
                var visual = _dirtySpellVisuals[id];
                numVisualId.Value = id;
                numVisualId.Enabled = !visual.KeepRecord;
                btnVisualDelete.Enabled = !visual.KeepRecord;
                cmbVisualDifficulty.SelectedItem = ((Difficulty)visual.DifficultyID).ToString();
                numSpellVisualId.Value = visual.SpellVisualID;
                numVisualIconId.Value = visual.SpellIconFileID;
                numVisualActiveIconId.Value = visual.ActiveIconFileID;
                txtVisualProbability.Text = visual.Probability.ToString();
                numVisualPriority.Value = visual.Priority;
                numUnitViewer.Value = visual.ViewerUnitConditionID;
                numPlayerViewer.Value = visual.ViewerPlayerConditionID;
                numUnitCaster.Value = visual.CasterUnitConditionID;
                numPlayerCaster.Value = visual.CasterPlayerConditionID;
            }
            else
                SetVisual0();
        }

        private TabPage NewSpellEffectTab(uint effIndex, bool newEffect = false)
        {
            return Helpers.CopySpellEffectTab(_defaultSpellEffectPage, effIndex, newEffect);
        }

        private void lvIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvIcons.SelectedItems.Count > 0)
            {
                picSelectedIcon.BackgroundImage = lvIcons.SelectedItems[0].ImageList.Images[lvIcons.SelectedItems[0].ImageKey];
                lblSelIcon.Text = lvIcons.SelectedItems[0].Text;
            }
        }

        private void SetVisual0()
        {
            numVisualId.Value = 0;
            numVisualId.Enabled = true;
            btnVisualDelete.Enabled = false;
            cmbVisualDifficulty.SelectedIndex = 0;
            numSpellVisualId.Value = 0;
            numVisualIconId.Value = 0;
            numVisualActiveIconId.Value = 0;
            txtVisualProbability.Text = "0";
            numVisualPriority.Value = 0;
            numUnitViewer.Value = 0;
            numPlayerViewer.Value = 0;
            numUnitCaster.Value = 0;
            numPlayerCaster.Value = 0;
        }

        // currency stuff
        private void cmbSelectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint id = (uint)((ComboBox)sender).SelectedItem;

            if (id > 0)
            {
                var cur = _dirtyCurrencyRecords[id];
                numCurrencyId.Value = cur.Id;
                numCurrencyId.Enabled = !cur.KeepRecord;
                btnCurrencyDelete.Enabled = !cur.KeepRecord;
                numCurrencyCount.Value = cur.CurrencyCount;
                cmbCurrencyType.SelectedIndex = _currencyTypeMap[cur.CurrencyTypesID];
            }
            else
                SetCurrency0();
        }

        public void SetCurrency0()
        {
            numCurrencyId.Value = 0;
            numCurrencyId.Enabled = true;
            btnCurrencyDelete.Enabled = false;
            numCurrencyCount.Value = 0;
            cmbCurrencyType.SelectedIndex = 0;
        }

        private void cmbEquippedItemClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            listEquippedItemSubClass.UpdateItemSubClass((ItemClass)Enum.Parse(typeof(ItemClass), (string)cmbEquippedItemClass.SelectedItem), CurrentSpell.SpellInfo.EquippedItemClass, CurrentSpell.SpellInfo.EquippedItemSubClassMask, false);
        }

        public FullSpellInfo Validate()
        {
            FullSpellInfo ret = new();
            try
            {
                ret.SpellInfo.Id = CurrentSpell.SpellInfo.Id;
                ret.SpellInfo.Difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), cmbDifficultyId.SelectedItem.ToString());

                // spell
                ret.SpellInfo.SpellName = new LocalizedString();
                ret.SpellInfo.SpellName[Locale.enUS] = txtSpellName.Text;
                ret.SpellInfo.SpellDescriptions.Id = ret.SpellInfo.Id;
                ret.SpellInfo.SpellDescriptions.NameSubtext_lang = txtSpellNameSubtext.Text;
                ret.SpellInfo.SpellDescriptions.Description_lang = txtSpellDesc.Text;
                ret.SpellInfo.SpellDescriptions.AuraDescription_lang = txtAuraDesc.Text;

                // spell_misc
                ret.SpellInfo.SpellMiscId = CurrentSpell.SpellInfo.SpellMiscId;
                ret.SpellInfo.Attributes = (SpellAttr0)listAttr0.SelectedItems.CalculateBitValue<SpellAttr0>();
                ret.SpellInfo.AttributesEx = (SpellAttr1)listAttr1.SelectedItems.CalculateBitValue<SpellAttr1>();
                ret.SpellInfo.AttributesEx2 = (SpellAttr2)listAttr2.SelectedItems.CalculateBitValue<SpellAttr2>();
                ret.SpellInfo.AttributesEx3 = (SpellAttr3)listAttr3.SelectedItems.CalculateBitValue<SpellAttr3>();
                ret.SpellInfo.AttributesEx4 = (SpellAttr4)listAttr4.SelectedItems.CalculateBitValue<SpellAttr4>();
                ret.SpellInfo.AttributesEx5 = (SpellAttr5)listAttr5.SelectedItems.CalculateBitValue<SpellAttr5>();
                ret.SpellInfo.AttributesEx6 = (SpellAttr6)listAttr6.SelectedItems.CalculateBitValue<SpellAttr6>();
                ret.SpellInfo.AttributesEx7 = (SpellAttr7)listAttr7.SelectedItems.CalculateBitValue<SpellAttr7>();
                ret.SpellInfo.AttributesEx8 = (SpellAttr8)listAttr8.SelectedItems.CalculateBitValue<SpellAttr8>();
                ret.SpellInfo.AttributesEx9 = (SpellAttr9)listAttr9.SelectedItems.CalculateBitValue<SpellAttr9>();
                ret.SpellInfo.AttributesEx10 = (SpellAttr10)listAttr10.SelectedItems.CalculateBitValue<SpellAttr10>();
                ret.SpellInfo.AttributesEx11 = (SpellAttr11)listAttr11.SelectedItems.CalculateBitValue<SpellAttr11>();
                ret.SpellInfo.AttributesEx12 = (SpellAttr12)listAttr12.SelectedItems.CalculateBitValue<SpellAttr12>();
                ret.SpellInfo.AttributesEx13 = (SpellAttr13)listAttr13.SelectedItems.CalculateBitValue<SpellAttr13>();
                ret.SpellInfo.AttributesEx14 = (SpellAttr14)listAttr14.SelectedItems.CalculateBitValue<SpellAttr14>();

                ret.SpellInfo.CastTimeEntry = CliDB.SpellCastTimesStorage[_castTimeMap.ReverseLookup(cmbCastTime.SelectedIndex)];
                ret.SpellInfo.DurationEntry = CliDB.SpellDurationStorage[mlcmbDuration.SelectedItemId];
                ret.SpellInfo.RangeEntry = CliDB.SpellRangeStorage[mlcmbRange.SelectedItemId];
                ret.SpellInfo.Speed = float.Parse(txtSpeed.Text);
                ret.SpellInfo.LaunchDelay = float.Parse(txtLaunchDelay.Text);
                ret.SpellInfo.SchoolMask = (SpellSchoolMask)listSpellSchool.SelectedItems.CalculateBitValue<SpellSchoolMask>();
                ret.SpellInfo.IconFileDataId = lblCurIcon.Text != String.Empty ? uint.Parse(lblCurIcon.Text.Split('-')[1].Trim()) : 0;
                ret.SpellInfo.ActiveIconFileDataId = lblActiveIcon.Text != String.Empty ? uint.Parse(lblActiveIcon.Text.Split('-')[1].Trim()) : 0;

                ret.SpellInfo.ContentTuningId = (uint)numContentTuningID.Value;
                ret.SpellInfo.ShowFutureSpellPlayerConditionID = (uint)numShowFutureSpellPlayerConditionID.Value;

                // SpellScalingEntry
                ret.SpellInfo.Scaling.Id = CurrentSpell.SpellInfo.Scaling.Id;
                ret.SpellInfo.Scaling.MinScalingLevel = (uint)numMinScalingLevel.Value;
                ret.SpellInfo.Scaling.MaxScalingLevel = (uint)numMaxScalingLevel.Value;
                ret.SpellInfo.Scaling.ScalesFromItemLevel = (uint)numScalesFromItemLevel.Value;

                // SpellAuraOptionsEntry
                ret.SpellInfo.AuraOptionsId = CurrentSpell.SpellInfo.AuraOptionsId;
                ret.SpellInfo.ProcFlags = new ProcFlagsInit(new int[] { (int)listProcFlags.SelectedItems.CalculateBitValue<ProcFlags>(), (int)listProcFlags2.SelectedItems.CalculateBitValue<ProcFlags2>() });
                ret.SpellInfo.ProcChance = (uint)numProcChance.Value;
                ret.SpellInfo.ProcCharges = (uint)numProcCharges.Value;
                ret.SpellInfo.ProcCooldown = (uint)numProcCooldown.Value;
                ret.SpellInfo.StackAmount = (uint)numStackAmount.Value;
                ret.SpellInfo.SpellPPMId = (ushort)mlcmbPPM.SelectedItemId;

                if (ret.SpellInfo.SpellPPMId > 0)
                    ret.SpellInfo.ProcBasePPM = CliDB.SpellProcsPerMinuteStorage[ret.SpellInfo.SpellPPMId].BaseProcRate;

                // SpellAuraRestrictionsEntry
                ret.SpellInfo.AuraRestrictionsId = CurrentSpell.SpellInfo.AuraRestrictionsId;
                ret.SpellInfo.CasterAuraState = (AuraStateType)Enum.Parse(typeof(AuraStateType), (string)cmbCasterAuraState.SelectedItem);
                ret.SpellInfo.TargetAuraState = (AuraStateType)Enum.Parse(typeof(AuraStateType), (string)cmbTargetAuraState.SelectedItem);
                ret.SpellInfo.ExcludeCasterAuraState = (AuraStateType)Enum.Parse(typeof(AuraStateType), (string)cmbExCasterAuraState.SelectedItem);
                ret.SpellInfo.ExcludeTargetAuraState = (AuraStateType)Enum.Parse(typeof(AuraStateType), (string)cmbExTargetAuraState.SelectedItem);
                ret.SpellInfo.CasterAuraSpell = (uint)numCasterAura.Value;
                ret.SpellInfo.TargetAuraSpell = (uint)numTargetAura.Value;
                ret.SpellInfo.ExcludeCasterAuraSpell = (uint)numExCasterAura.Value;
                ret.SpellInfo.ExcludeTargetAuraSpell = (uint)numExTargetAura.Value;

                // SpellCastingRequirementsEntry
                ret.SpellInfo.SpellCastingRequirements = CurrentSpell.SpellInfo.SpellCastingRequirements;
                ret.SpellInfo.RequiresSpellFocus = uint.Parse(((string)cmbSpellFocus.SelectedItem).Split('-')[0].Trim());
                ret.SpellInfo.FacingCasterFlags = (uint)numFacingCasterFlags.Value;
                ret.SpellInfo.RequiredAreasID = (int)numRequiredAreaId.Value;

                // SpellCategoriesEntry
                ret.SpellInfo.SpellCategoriesId = CurrentSpell.SpellInfo.SpellCategoriesId;
                ret.SpellInfo.CategoryId = (uint)numCategory.Value;
                ret.SpellInfo.Dispel = (DispelType)Enum.Parse(typeof(DispelType), (string)cmbDispelType.SelectedItem);
                ret.SpellInfo.Mechanic = (Mechanics)Enum.Parse(typeof(Mechanics), (string)cmbMechanic.SelectedItem);
                ret.SpellInfo.StartRecoveryCategory = (uint)numStartRecCat.Value;
                ret.SpellInfo.DmgClass = (SpellDmgClass)Enum.Parse(typeof(SpellDmgClass), (string)cmbDamageType.SelectedItem);
                ret.SpellInfo.PreventionType = (SpellPreventionType)Enum.Parse(typeof(SpellPreventionType), (string)cmbPreventionType.SelectedItem);
                ret.SpellInfo.ChargeCategoryId = (uint)numChargeCat.Value;

                // SpellClassOptionsEntry
                ret.SpellInfo.ClassOptionsId = CurrentSpell.SpellInfo.ClassOptionsId;
                ret.SpellInfo.ModalNextSpell = (uint)numModalNextSpell.Value;
                ret.SpellInfo.SpellFamilyName = (SpellFamilyNames)Enum.Parse(typeof(SpellFamilyNames), (string)cmbSpellFamily.SelectedItem);
                ret.SpellInfo.SpellFamilyFlags = new FlagArray128((uint)numFamilyFlags1.Value, (uint)numFamilyFlags2.Value, (uint)numFamilyFlags3.Value, (uint)numFamilyFlags4.Value);

                // SpellCooldownsEntry
                ret.SpellInfo.SpellCooldownsId = CurrentSpell.SpellInfo.SpellCooldownsId;
                ret.SpellInfo.RecoveryTime = (uint)numCooldown.Value;
                ret.SpellInfo.CategoryRecoveryTime = (uint)numCatCooldown.Value;
                ret.SpellInfo.StartRecoveryTime = (uint)numStartCooldownTime.Value;

                // SpellEquippedItemsEntry
                ret.SpellInfo.SpellEquippedItemsId = CurrentSpell.SpellInfo.SpellEquippedItemsId;
                ret.SpellInfo.EquippedItemClass = (ItemClass)Enum.Parse(typeof(ItemClass), (string)cmbEquippedItemClass.SelectedItem);
                ret.SpellInfo.EquippedItemSubClassMask = (int)Helpers.GetItemSubClassValue((ItemClass)Enum.Parse(typeof(ItemClass), (string)cmbEquippedItemClass.SelectedItem), listEquippedItemSubClass.SelectedItems);
                ret.SpellInfo.EquippedItemInventoryTypeMask = (int)listEquippedItemInvenType.SelectedItems.CalculateIntValue<InventoryType>();

                // SpellInterruptsEntry
                ret.SpellInfo.SpellInterruptsId = CurrentSpell.SpellInfo.SpellInterruptsId;
                ret.SpellInfo.InterruptFlags = (SpellInterruptFlags)listInterruptFlags.SelectedItems.CalculateBitValue<SpellInterruptFlags>();
                ret.SpellInfo.AuraInterruptFlags = (SpellAuraInterruptFlags)listAuraInterruptFlags.SelectedItems.CalculateBitValue<SpellAuraInterruptFlags>();
                ret.SpellInfo.AuraInterruptFlags2 = (SpellAuraInterruptFlags2)listAuraInterruptFlags2.SelectedItems.CalculateBitValue<SpellAuraInterruptFlags2>();
                ret.SpellInfo.ChannelInterruptFlags = (SpellAuraInterruptFlags)listChannelInterruptFlags.SelectedItems.CalculateBitValue<SpellAuraInterruptFlags>();
                ret.SpellInfo.ChannelInterruptFlags2 = (SpellAuraInterruptFlags2)listChannelInterruptFlags2.SelectedItems.CalculateBitValue<SpellAuraInterruptFlags2>();

                var split = txtLabels.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                ret.SpellInfo.Labels.Clear();
                foreach (string label in split)
                    if (!string.IsNullOrEmpty(label))
                        ret.SpellInfo.Labels.Add(uint.Parse(label));

                // SpellLevelsEntry
                ret.SpellInfo.MaxLevel = (uint)numMaxLevel.Value;
                ret.SpellInfo.BaseLevel = (uint)numBaseLevel.Value;
                ret.SpellInfo.SpellLevel = (uint)numSpellLevel.Value;
                ret.SpellInfo.SpellLevelsId = CurrentSpell.SpellInfo.SpellLevelsId;
                ret.SpellInfo.MaxPassiveAuraLevel = CurrentSpell.SpellInfo.MaxPassiveAuraLevel;

                // SpellPowerEntry
                for (int i = 0; i < 4; i++)
                {
                    var tab = tabsPowerConfig.TabPages[i];

                    ret.SpellInfo.PowerCosts[i] = tab.ToSpellPowerRecord(CurrentSpell.SpellInfo.PowerCosts[i] != null ? CurrentSpell.SpellInfo.PowerCosts[i].Id : 0);
                    ret.SpellInfo.PowerCosts[i].SpellID = ret.SpellInfo.Id;
                    ret.SpellInfo.PowerCosts[i].OrderIndex = (byte)i;
                }

                // SpellReagentsEntry
                ret.SpellInfo.SpellReagentsId = CurrentSpell.SpellInfo.SpellReagentsId;
                ret.SpellInfo.Reagent[0] = (int)numReagent1.Value;
                ret.SpellInfo.Reagent[1] = (int)numReagent2.Value;
                ret.SpellInfo.Reagent[2] = (int)numReagent3.Value;
                ret.SpellInfo.Reagent[3] = (int)numReagent4.Value;
                ret.SpellInfo.Reagent[4] = (int)numReagent5.Value;
                ret.SpellInfo.Reagent[5] = (int)numReagent6.Value;
                ret.SpellInfo.Reagent[6] = (int)numReagent7.Value;
                ret.SpellInfo.Reagent[7] = (int)numReagent8.Value;
                ret.SpellInfo.ReagentCount[0] = (uint)numReagentCount1.Value;
                ret.SpellInfo.ReagentCount[1] = (uint)numReagentCount2.Value;
                ret.SpellInfo.ReagentCount[2] = (uint)numReagentCount3.Value;
                ret.SpellInfo.ReagentCount[3] = (uint)numReagentCount4.Value;
                ret.SpellInfo.ReagentCount[4] = (uint)numReagentCount5.Value;
                ret.SpellInfo.ReagentCount[5] = (uint)numReagentCount6.Value;
                ret.SpellInfo.ReagentCount[6] = (uint)numReagentCount7.Value;
                ret.SpellInfo.ReagentCount[7] = (uint)numReagentCount8.Value;

                ret.SpellInfo.ReagentsCurrency.Clear();
                foreach (var dirtyCur in _dirtyCurrencyRecords)
                    ret.SpellInfo.ReagentsCurrency.Add(dirtyCur.Value.ToBaseRecord());

                // SpellShapeshiftEntry
                ret.SpellInfo.ShapeshiftRecordId = CurrentSpell.SpellInfo.ShapeshiftRecordId;
                ret.SpellInfo.Stances = (ulong)listStances.SelectedItems.CalculateIntValue<ShapeShiftForm>();
                ret.SpellInfo.StancesNot = (ulong)listExStances.SelectedItems.CalculateIntValue<ShapeShiftForm>();
                ret.SpellInfo.StanceBarOrder = CurrentSpell.SpellInfo.StanceBarOrder;

                // SpellTargetRestrictionsEntry
                ret.SpellInfo.TargetRestrictionsId = CurrentSpell.SpellInfo.TargetRestrictionsId;
                ret.SpellInfo.Targets = (SpellCastTargetFlags)listProcTargets.SelectedItems.CalculateBitValue<SpellCastTargetFlags>();
                ret.SpellInfo.ConeAngle = float.Parse(txtConeAngle.Text);
                ret.SpellInfo.Width = float.Parse(txtWidth.Text);
                ret.SpellInfo.TargetCreatureType = (ushort)listTargetCreatureType.SelectedItems.CalculateIntValue<CreatureType>();
                ret.SpellInfo.MaxAffectedTargets = (byte)numMaxTargets.Value;
                ret.SpellInfo.MaxTargetLevel = (uint)numMaxTargetLevel.Value;

                // SpellTotemsEntry
                ret.SpellInfo.TotemRecordID = CurrentSpell.SpellInfo.TotemRecordID;
                ret.SpellInfo.Totem[0] = (uint)numTotem1.Value;
                ret.SpellInfo.Totem[1] = (uint)numTotem2.Value;
                ret.SpellInfo.TotemCategory[0] = cmbTotemCategory1.SelectedIndex != 0 ? (ushort)_totemCatMap.ReverseLookup(cmbTotemCategory1.SelectedIndex) : (ushort)0;
                ret.SpellInfo.TotemCategory[1] = cmbTotemCategory2.SelectedIndex != 0 ? (ushort)_totemCatMap.ReverseLookup(cmbTotemCategory2.SelectedIndex) : (ushort)0;

                // Visuals
                var visuals = ret.SpellInfo.GetSpellVisuals();
                visuals.Clear();
                foreach (var dirtyVis in _dirtySpellVisuals)
                    visuals.Add(dirtyVis.Value.ToBaseRecord());

                // spell effects
                var effects = ret.SpellInfo.GetEffects();
                effects.Clear();
                foreach (TabPage tab in tabsSpellEffects.TabPages)
                    effects.Add(tab.ToSpellEffectInfo(ret.SpellInfo, _radiusMap));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error while validating your spell.{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Error");
                return null;
            }
            
            return ret;
        }

        #region Button Clicks

        private void btnFindRelated_Click(object sender, EventArgs e)
        {
            if (CurrentSpell == null)
                return;

            List<uint> spells = SpellManager.Instance.GetRelatedSpells(CurrentSpell.SpellInfo);
            listSpells.Items.Clear();

            foreach (uint id in spells.Order())
                listSpells.Items.Add(Helpers.SpellDisplayName(CliDB.SpellNameStorage[id]));
        }

        private void btnAddEffect_Click(object sender, EventArgs e)
        {
            uint index = 0;

            if (tabsSpellEffects.TabPages.Count > 0)
                index = uint.Parse(tabsSpellEffects.TabPages[tabsSpellEffects.TabPages.Count - 1].Text.Replace("Effect ", ""));

            tabsSpellEffects.TabPages.Add(NewSpellEffectTab(index + 1, true));
            tabsSpellEffects.SelectedIndex = tabsSpellEffects.TabPages.Count - 1;
        }

        private void btnCurIconChange_Click(object sender, EventArgs e)
        {
            if (picSelectedIcon.BackgroundImage != null)
            {
                picCurIcon.BackgroundImage = picSelectedIcon.BackgroundImage;
                lblCurIcon.Text = lblSelIcon.Text;
            }
        }

        private void btnCurIconUndo_Click(object sender, EventArgs e)
        {
            if (CurrentSpell != null && CurrentSpell.SpellInfo.IconFileDataId > 0
                && Program.DataAccess.SpellIconStorage.TryGetValue(CurrentSpell.SpellInfo.IconFileDataId, out var iconRecord))
            {
                picCurIcon.BackgroundImage = iconRecord.GetImage();
                lblCurIcon.Text = $"{iconRecord.TextureFilename.Split('/').Last()} - {iconRecord.Id}";
            }
            else
            {
                picCurIcon.BackgroundImage = null;
                lblCurIcon.Text = string.Empty;
            }
        }

        private void btnActiveIconChange_Click(object sender, EventArgs e)
        {
            if (picSelectedIcon.BackgroundImage != null)
            {
                picActiveIcon.BackgroundImage = picSelectedIcon.BackgroundImage;
                lblActiveIcon.Text = lblSelIcon.Text;
            }
        }

        private void btnActiveIconUndo_Click(object sender, EventArgs e)
        {
            if (CurrentSpell != null && CurrentSpell.SpellInfo.ActiveIconFileDataId > 0
                && Program.DataAccess.SpellIconStorage.TryGetValue(CurrentSpell.SpellInfo.ActiveIconFileDataId, out var iconRecord))
            {
                picActiveIcon.BackgroundImage = iconRecord.GetImage();
                lblActiveIcon.Text = $"{iconRecord.TextureFilename.Split('/').Last()} - {iconRecord.Id}";
            }
            else
            {
                picActiveIcon.BackgroundImage = null;
                lblActiveIcon.Text = string.Empty;
            }
        }

        private void btnIconFirst_Click(object sender, EventArgs e)
        {
            if (numIconPage.Value > 1)
            {
                numIconPage.Value = 1;
                lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);
            }
        }

        private void btnIconPrevious_Click(object sender, EventArgs e)
        {
            if (numIconPage.Value > 1)
            {
                numIconPage.Value--;
                lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);
            }
        }

        private void btnIconNext_Click(object sender, EventArgs e)
        {
            if (numIconPage.Value < numIconPage.Maximum)
            {
                numIconPage.Value++;
                lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);
            }
        }

        private void btnIconLast_Click(object sender, EventArgs e)
        {
            if (numIconPage.Value < numIconPage.Maximum)
            {
                numIconPage.Value = numIconPage.Maximum;
                lvIcons.PopulateIconList(lblIconPageCount, numIconPage, _iconFolder, _currentIconSearch);
            }
        }

        private void btnVisualNew_Click(object sender, EventArgs e)
        {
            if (cmbSelectVisual.SelectedIndex != 0)
                cmbSelectVisual.SelectedIndex = 0;
            else
                SetVisual0();
        }

        private void btnVisualCopy_Click(object sender, EventArgs e)
        {
            numVisualId.Value = 0;
            numVisualId.Enabled = true;
            btnVisualDelete.Enabled = false;
        }

        private void btnVisualSave_Click(object sender, EventArgs e)
        {
            bool savingNew = numVisualId.Enabled && !btnVisualDelete.Enabled;
            uint id = savingNew ? (uint)numVisualId.Value : (uint)cmbSelectVisual.SelectedItem;

            if (id == 0)
            {
                MessageBox.Show("Invalid Visual Id.", "Error");
                return;
            }

            if (savingNew && (_dirtySpellVisuals.ContainsKey(id) || CliDB.SpellXSpellVisualStorage.ContainsKey(id)))
            {
                MessageBox.Show("Visual Id already in use.", "Error");
                return;
            }

            _dirtySpellVisuals[id] = new SpellXSpellVisualRecordMod()
            {
                Id = id,
                DifficultyID = (byte)Enum.Parse(typeof(Difficulty), cmbVisualDifficulty.SelectedItem.ToString()),
                SpellVisualID = (uint)numSpellVisualId.Value,
                SpellIconFileID = (int)numVisualIconId.Value,
                ActiveIconFileID = (int)numVisualActiveIconId.Value,
                Probability = float.Parse(txtVisualProbability.Text),
                Priority = (int)numVisualPriority.Value,
                ViewerUnitConditionID = (ushort)numUnitViewer.Value,
                ViewerPlayerConditionID = (uint)numPlayerViewer.Value,
                CasterUnitConditionID = (ushort)numUnitCaster.Value,
                CasterPlayerConditionID = (uint)numPlayerCaster.Value,
                SpellID = CurrentSpell.SpellInfo.Id,
                KeepRecord = CliDB.SpellXSpellVisualStorage.ContainsKey(id)
            };

            if (!cmbSelectVisual.Items.Contains(id))
                cmbSelectVisual.Items.Add(id);

            cmbSelectVisual.SelectedItem = id;
        }

        private void btnVisualDelete_Click(object sender, EventArgs e)
        {
            if (!numVisualId.Enabled)
                return;

            var id = (uint)cmbSelectVisual.SelectedItem;
            var visual = _dirtySpellVisuals[id];

            if (visual == null || visual.KeepRecord)
            {
                MessageBox.Show("Unable to delete existing records.", "Error");
                return;
            }

            cmbSelectVisual.SelectedIndex = 0;
            cmbSelectVisual.Items.Remove(id);
            _dirtySpellVisuals.Remove(id);
        }

        private void btnCurrencyNew_Click(object sender, EventArgs e)
        {
            if (cmbSelectCurrency.SelectedIndex != 0)
                cmbSelectCurrency.SelectedIndex = 0;
            else
                SetCurrency0();
        }

        private void btnCurrencyCopy_Click(object sender, EventArgs e)
        {
            numCurrencyId.Value = 0;
            numCurrencyId.Enabled = true;
            btnCurrencyDelete.Enabled = false;
        }

        private void btnCurrencySave_Click(object sender, EventArgs e)
        {
            bool savingNew = numCurrencyId.Enabled && !btnCurrencyDelete.Enabled;
            uint id = savingNew ? (uint)numCurrencyId.Value : (uint)cmbSelectCurrency.SelectedItem;

            if (id == 0)
            {
                MessageBox.Show("Invalid Currency Id.", "Error");
                return;
            }

            if (savingNew && (_dirtyCurrencyRecords.ContainsKey(id) || CliDB.SpellReagentsCurrencyStorage.ContainsKey(id)))
            {
                MessageBox.Show("Currency Id already in use.", "Error");
                return;
            }

            ushort currencytypeid = 0;

            foreach (var cat in CliDB.CurrencyTypesStorage)
            {
                if (cat.Value.Name == cmbCurrencyType.SelectedItem)
                {
                    currencytypeid = (ushort)cat.Key;
                    break;
                }
            }

            _dirtyCurrencyRecords[id] = new SpellReagentsCurrencyRecordMod()
            {
                Id = id,
                SpellID = (int)CurrentSpell.SpellInfo.Id,
                CurrencyCount = (ushort)numCurrencyCount.Value,
                CurrencyTypesID = currencytypeid,
                KeepRecord = CliDB.SpellReagentsCurrencyStorage.ContainsKey(id)
            };

            if (!cmbSelectVisual.Items.Contains(id))
                cmbSelectVisual.Items.Add(id);

            cmbSelectVisual.SelectedItem = id;
        }

        private void btnCurrencyDelete_Click(object sender, EventArgs e)
        {
            if (!numCurrencyId.Enabled)
                return;

            var id = (uint)cmbSelectCurrency.SelectedItem;
            cmbSelectCurrency.SelectedIndex = 0;
            cmbSelectCurrency.Items.Remove(id);
            _dirtyCurrencyRecords.Remove(id);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentSpell != null && CurrentSpell.SpellInfo.Id != 0)
            {
                FullSpellInfo validatedSpell = Validate();

                if (validatedSpell != null)
                {
                    validatedSpell.Save();
                    CurrentSpell = validatedSpell;
                    lblCurrentSpellName.Text = Helpers.SpellDisplayName(CurrentSpell.SpellInfo.Id, CurrentSpell.SpellInfo.SpellName[Locale.enUS]);

                    uint prevMax = _maxSpellSearch;
                    if (_maxSpellSearch < CurrentSpell.SpellInfo.Id)
                        _maxSpellSearch = CurrentSpell.SpellInfo.Id;

                    if (numCurentMax.Value == prevMax)
                    {
                        var range = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);
                        numCurentMax.Value = _maxSpellSearch;
                        numCurentMin.Value = _maxSpellSearch - range;
                    }

                    listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Create a new spell?", "New spell", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
                return;

            if (numNewSpellID.Value == 0)
            {
                var retryResult = MessageBox.Show("Id 0 is invalid. Would you like to try to create a spell with the selected spell's ID + 1?", "Error", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.No)
                    return;

                numNewSpellID.Value = CurrentSpell.SpellInfo.Id + 1;
            }
            else if(CliDB.SpellNameStorage.ContainsKey((uint)numNewSpellID.Value))
            {
                MessageBox.Show("New Spell Id already in use.", "Error");
                return;
            }

            CurrentSpell = new FullSpellInfo();
            CurrentSpell.SpellInfo.Id = (uint)numNewSpellID.Value;
            CurrentSpell.SpellInfo.SpellName[Locale.enUS] = "New Spell";
            LoadCurrentSpell();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (CurrentSpell != null && CurrentSpell.SpellInfo.Id != 0)
            {
                var confirmResult = MessageBox.Show($"Copy current spell (only saved changes will be copied)?", "Copy spell", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                    return;

                if (numNewSpellID.Value == 0)
                {
                    var retryResult = MessageBox.Show("Id 0 is invalid. Would you like to try to create the copied spell with the selected spell's ID + 1?", "Error", MessageBoxButtons.YesNo);

                    if (retryResult == DialogResult.No)
                        return;

                    numNewSpellID.Value = CurrentSpell.SpellInfo.Id + 1;
                }

                if (CliDB.SpellNameStorage.ContainsKey((uint)numNewSpellID.Value))
                {
                    MessageBox.Show("New Spell Id already in use.", "Error");
                    return;
                }

                CurrentSpell = CurrentSpell.Copy((uint)numNewSpellID.Value);
                LoadCurrentSpell();
            }
        }

        private void btnDeleteSpell_Click(object sender, EventArgs e)
        {
            if (CurrentSpell != null && CurrentSpell.SpellInfo.Id != 0)
            {
                var confirmResult = MessageBox.Show($"Delete current spell?{Environment.NewLine}This is not recommended for base WoW spells and will simply reset them to their default state. Creating a new spell with that base WoW spell ID may cause issues if done prior to restarting and refreshing the cache.", "Delete spell", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                    return;

                CurrentSpell.Delete();
                CurrentSpell = new FullSpellInfo();
                LoadCurrentSpell();
                listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (numCurentMax.Value >= _maxSpellSearch)
                return;

            decimal range = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);

            numCurentMin.Value += range;
            if (numCurentMin.Value > _maxSpellSearch)
                numCurentMin.Value = _maxSpellSearch - range - 1;

            numCurentMax.Value = numCurentMin.Value + range - 1;

            listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (numCurentMax.Value >= _maxSpellSearch)
                return;

            decimal range = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);

            numCurentMax.Value = _maxSpellSearch;
            numCurentMin.Value = numCurentMax.Value - range + 1;

            listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (numCurentMin.Value > 1)
            {
                decimal range = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);

                if (numCurentMin.Value < range + 1)
                    numCurentMin.Value = 1;
                else
                    numCurentMin.Value -= range;

                numCurentMax.Value = numCurentMin.Value + range - 1;

                listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (numCurentMin.Value > 1)
            {
                decimal range = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);

                numCurentMin.Value = 1;
                numCurentMax.Value = range;

                listSpells.PopulateSpellList(numCurentMin, numCurentMax, cmbIndexing.SelectedIndex, _currentNameSearch, ref _maxSpellSearch);
            }
        }

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            bool dirty = false;

            if (CliDB.SpellCategoryStorage.ContainsKey(numCatId.Value))
            {
                var cat = CliDB.SpellCategoryStorage[(uint)numCatId.Value];

                if (cat.UsesPerWeek != numCatUsesPerWeek.Value
                    || cat.MaxCharges != numCatCharges.Value
                    || cat.ChargeRecoveryTime != numCatChargeCD.Value
                    || cat.Name != txtCatName.Text
                    || cat.TypeMask != numTypeMask.Value
                    || Convert.ToInt64(cat.Flags) != listCatFlags.SelectedItems.EnumCollectionToInt64<SpellCategoryFlags>())
                {
                    dirty = true;
                }
            }
            else
                dirty = true;

            if (dirty)
            {
                Int64 flags = 0;

                foreach (string flag in listCatFlags.SelectedItems)
                    flags |= Convert.ToInt64((SpellCategoryFlags)Enum.Parse(typeof(SpellCategoryFlags), flag));

                SpellCategoryRecord record = new SpellCategoryRecord();
                record.Id = (uint)numCatId.Value;
                record.Name = txtCatName.Text;
                record.Flags = (SpellCategoryFlags)flags;
                record.UsesPerWeek = (byte)numCatUsesPerWeek.Value;
                record.MaxCharges = (byte)numCatCharges.Value;
                record.ChargeRecoveryTime = (int)numCatChargeCD.Value;
                record.TypeMask = (int)numTypeMask.Value;

                DB.Hotfix.Execute(string.Format(DataAccess.UPDATE_SPELL_CATEGORY, record.Id, record.Name, flags, record.UsesPerWeek, record.MaxCharges, record.ChargeRecoveryTime, record.TypeMask));
                CliDB.SpellCategoryStorage[record.Id] = record;

                DataTable editCatItems = MultiLineComboBox.GeneratteDataTable();
                editCatItems.Rows.Add(new object[] { 0, $"None", ChargeCategoryDisplay(new SpellCategoryRecord() { Name = "None" }) });

                var spellCats = CliDB.SpellCategoryStorage.OrderBy(a => a.Key);
                int selIndex = 0;
                foreach (var spellCat in spellCats)
                {
                    editCatItems.Rows.Add(new object[] { spellCat.Key, spellCat.Value.Name, ChargeCategoryDisplay(spellCat.Value) });

                    if (spellCat.Key == record.Id)
                        selIndex = editCatItems.Rows.Count - 1;
                }
                mlcmbEditCat.InitializeComboBox(editCatItems);
                mlcmbEditCat.SelectedIndex = selIndex;
            }
        }

        private void btnAddCurveEffect_Click(object sender, EventArgs e)
        {
            if (CurrentSpell == null)
                return;

            SpellCurve curve = new SpellCurve();
            SpellCurve firstCurve = CurrentSpell.DirtyCurves.Count > 0 ? CurrentSpell.DirtyCurves.First() : new();

            curve.TraitDefinition = firstCurve.TraitDefinition;
            curve.TraitDefinitionEffectPoints.TraitDefinitionID = (int)curve.TraitDefinition.Id;
            int newEffIndex = CurrentSpell.DirtyCurves.Count > 0 ? CurrentSpell.DirtyCurves.Max(a => a.TraitDefinitionEffectPoints.EffectIndex) + 1 : 0;

            curve.TraitDefinitionEffectPoints.EffectIndex = newEffIndex;

            int rankCount = firstCurve.CurvePoints.Count == 0 ? 1 : firstCurve.CurvePoints.Count;
            for (int i = 0; i < rankCount; i++)
            {
                CurvePointRecord cp = new()
                {
                    Pos = new System.Numerics.Vector2(i + 1, 0),
                    OrderIndex = (byte)i,
                    CurveID = 0,
                    Id = 0,
                    PreSLSquishPos = new System.Numerics.Vector2(0, 0)
                };
                curve.CurvePoints.Add(cp);

                CreateCurveEffectBox(curve, cp);
            }

            CurrentSpell.DirtyCurves.Add(curve);
        }

        private void btnAddCurveRank_Click(object sender, EventArgs e)
        {
            if (CurrentSpell == null)
                return;

            if (CurrentSpell.DirtyCurves.Count == 0)
                return;

            int rankCount = CurrentSpell.DirtyCurves.First().CurvePoints.Count;

            foreach (var curve in CurrentSpell.DirtyCurves)
            {
                CurvePointRecord cp = new()
                {
                    Pos = new System.Numerics.Vector2(rankCount + 1, 0),
                    OrderIndex = (byte)rankCount,
                    CurveID = (ushort)curve.CurveRecord.Id,
                    Id = 0,
                    PreSLSquishPos = new System.Numerics.Vector2(0, 0)
                };
                curve.CurvePoints.Add(cp);

                CreateCurveEffectBox(curve, cp);
            }
        }

        private void copySpellIdConst_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"public const uint {CurrentSpell.SpellInfo.SpellName[Locale.enUS].ToUpperInvariant().Replace(" ", "_")} = {CurrentSpell.SpellInfo.Id};");
        }

        #endregion
    }
}