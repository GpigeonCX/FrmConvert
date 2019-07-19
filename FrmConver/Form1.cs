using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FrmConver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPending.Text))
                {
                    string[] strList = txtPending.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    List<string> results = new List<string>();

                    foreach (var item in new List<string>(strList))
                    {
                        if (item.Length > 32)
                        {
                            results.AddRange(new List<string>(Regex.Split(item, "\\s+", RegexOptions.IgnoreCase)));
                        }
                        else
                        {
                            results.Add(item);
                        }
                    }
                    for (int i = 0; i < results.Count; i++)
                    {
                        Match match = Regex.Match(results[i], @"\d([0-9]|x|X)+");
                        //Match match = Regex.Match(results[i], @"((\d{14}$)|(\d{17}([0-9]|X|x)$))+");
                        results[i] = GetMatch(match).Value;
                        //= Regex.Replace(results[i], @"[^0-9]+", "");
                    }

                    List<String> listResults = new List<string>();

                    foreach (var item in results)
                    {
                        if (!string.IsNullOrEmpty(item) && item.Length > 13)
                            listResults.Add(item);
                    }
                    for (int i = 0; i < listResults.Count; i++)
                    {
                        if (listResults[i].Length < 18)
                        {
                            listResults[i] = listResults[i].Substring(0, 14);
                        }
                        else
                        {
                            listResults[i] = listResults[i].Substring(0, 18);
                        }
                    }
                    txtResult.Text = string.Join("\r\n", listResults);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错了，请重试~");
            }
        }

        public Match GetMatch(Match math)
        {
            if (!string.IsNullOrEmpty(math.Value))
            {
                return math.Value.Length >= 14 ? math : GetMatch(math.NextMatch());
            }
            return math;
        }

        private void txtResult_DoubleClick(object sender, EventArgs e)
        {
            txtResult.SelectAll();
        }

        private void txtPending_DoubleClick(object sender, EventArgs e)
        {
            txtPending.SelectAll();
        }
    }
}
