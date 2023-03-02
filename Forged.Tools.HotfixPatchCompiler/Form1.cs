// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Forged.Tools.HotfixPatchCompiler.Models;
using Forged.Tools.HotfixPatchCompiler.Utils;
using Framework.Database;
using System.Text;
using static Forged.Tools.HotfixPatchCompiler.Enums;

namespace Forged.Tools.HotfixPatchCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormClosed += MainForm_FormClosed;
        }

        private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var retryResult = MessageBox.Show("Are you sure? This process can not be reversed.", "Confirm", MessageBoxButtons.YesNo);

            if (retryResult == DialogResult.No)
                return;
            try
            {
                int newVersion = (int)numVersion.Value;
                var ranges = ParseRanges(txtRanges.Text);

                int minVersion = (int)DataAccess.GetHotfixValue(new PreparedStatement("SELECT VerifiedBuild FROM hotfix_data ORDER BY VerifiedBuild DESC LIMIT 1;"));
                if (newVersion <= minVersion)
                {
                    MessageBox.Show($"Your new version number must be greater than {minVersion}.", "Error");
                    return;
                }

                if (ranges.Count == 0)
                {
                    MessageBox.Show($"There are no valid ranges to be updated to the new version.", "Error");
                    return;
                }

                List<HotfixDataRecord> hotfixData = new List<HotfixDataRecord>();
                var tables = DataAccess.GetHotfixTables();
                var tableHashNames = Enum.GetNames(typeof(DB2Hash));

                foreach (var table in tables)
                {
                    // parse name for enum to get hash
                    string tblremUnderscores = table.Replace("_", "").ToLower();
                    string tblLower = table.ToLower();

                    var selected = tableHashNames.Where(a => a.ToLower() == tblLower);

                    if (selected.Count() == 0)
                        selected = tableHashNames.Where(a => a.ToLower() == tblremUnderscores);

                    if (selected.Count() == 0)
                    {
                        continue;
                    }

                    uint tableHash = Convert.ToUInt32(Enum.Parse<DB2Hash>(selected.First()));

                    string selectQuery = $"SELECT ID FROM `{table}` WHERE";
                    string updateQuery = $"UPDATE `{table}` SET `VerifiedBuild` = {newVersion} WHERE";

                    StringBuilder whereValues = new();
                    foreach (var range in ranges)
                        whereValues.Append(" `VerifiedBuild` >= ").Append(range[0].ToString()).Append(" AND `VerifiedBuild` <= ").Append(range[1].ToString());
                    whereValues.Append(";");

                    selectQuery += whereValues.ToString();
                    updateQuery += whereValues.ToString();

                    var tblIds = DataAccess.GetHotfixValues<int>(selectQuery);

                    foreach (int id in tblIds)
                    {
                        HotfixDataRecord record = new HotfixDataRecord();
                        record.TableHash = tableHash;
                        record.Id = newVersion;
                        record.VerifiedBuild = newVersion;
                        record.UniqueId = (uint)newVersion;
                        record.Status = Status.Valid;
                        record.RecordId = id;

                        hotfixData.Add(record);
                    }

                    DB.Hotfix.Execute(updateQuery);

                    foreach(var hotfix in hotfixData)
                    {
                        PreparedStatement stmt = new PreparedStatement(DataAccess.INSERT_HOTFIX_DATA);
                        stmt.AddValue(0, hotfix.Id);
                        stmt.AddValue(1, hotfix.UniqueId);
                        stmt.AddValue(2, hotfix.TableHash);
                        stmt.AddValue(3, hotfix.RecordId);
                        stmt.AddValue(4, (ushort)hotfix.Status);
                        stmt.AddValue(5, hotfix.VerifiedBuild);

                        DB.Hotfix.Execute(stmt);
                    }
                }

                MessageBox.Show("Operation is complete.", "Complete");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was a problem, here is the error:{Environment.NewLine}{ex.Message}", "Error");
            }
        }

        private List<int[]> ParseRanges(string text)
        {
            List<int[]> ret = new List<int[]>();

            var split = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var range in split)
            {
                string trimmed = range.Trim();
                
                if (trimmed.EndsWith("+"))
                {
                    trimmed = trimmed.Remove(trimmed.Length - 1);
                    if (int.TryParse(trimmed, out int toEnd))
                        ret.Add(new int[] { toEnd, int.MaxValue });
                    else
                        throw new Exception("There was a problem parsing your build number ranges. The value preceeding your range ending in + was not an int.");
                }
                else if (trimmed.Contains("-"))
                {
                    if (trimmed.Where(a => a == '-').Count() != 1)
                        throw new Exception("There was a problem parsing your build number ranges. One of your ranges contains more than 1 -.");

                    var splitRange = trimmed.Split("-");
                    var newRange = new int[2];

                    for (int i = 0; i < 2; i++)
                    {
                        var splitRangeValue = splitRange[i].Trim();

                        if (string.IsNullOrEmpty(splitRangeValue))
                            throw new Exception("There was a problem parsing your build number ranges. Part of your range including a - did not have a value.");


                    }

                    ret.Add(newRange);
                }
                else if (int.TryParse(trimmed, out int single))
                {
                    ret.Add(new int[] { single, single });
                }
                else
                    throw new Exception("There was a problem parsing your build number ranges.");
            }

            return ret;
        }
    }
}