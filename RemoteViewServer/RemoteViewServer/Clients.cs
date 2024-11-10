using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.IO;
using System.Linq;

namespace RemoteViewServer
{
    public partial class Clients : Form
    {
        private string configFilePath = "client.ini";
        private bool hasChanges = false;

        public Clients()
        {
            InitializeComponent();
        }

        private void ClientName_Load(object sender, EventArgs e)
        {
            LoadClientNames();
        }

        private void Clients_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hasChanges)
            {
                RestartApplication();
            }
        }

        private void inputClientName_TextChanged(object sender, EventArgs e)
        {
            if (inputClientName.Text.Length > 20)
            {
                inputClientName.Text = inputClientName.Text.Substring(0, 20);
                inputClientName.SelectionStart = inputClientName.Text.Length;
            }
        }

        private void clientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = clientList.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedItem))
            {
                int equalsIndex = selectedItem.IndexOf('=');

                if (equalsIndex != -1)
                {
                    string ip = selectedItem.Substring(0, equalsIndex).Trim();
                    string name = selectedItem.Substring(equalsIndex + 1).Trim();

                    inputClientIP.Text = ip;
                    inputClientName.Text = name;
                }
                else
                {
                    inputClientIP.Text = selectedItem.Trim();
                    inputClientName.Clear();
                }

                inputClientIP.Enabled = true;
                inputClientName.Enabled = true;
            }
        }

        private void buttonClientNameSave_Click(object sender, EventArgs e)
        {
            string selectedItem = clientList.SelectedItem?.ToString();
            string inputIP = inputClientIP.Text.Trim();
            string inputName = inputClientName.Text.Trim();

            if (!string.IsNullOrEmpty(selectedItem) && !string.IsNullOrEmpty(inputIP))
            {
                var lines = File.ReadAllLines(configFilePath).ToList();

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith(inputIP + "=") || lines[i].StartsWith(inputIP))
                    {
                        if (string.IsNullOrEmpty(inputName))
                        {
                            lines[i] = $"{inputIP}";
                        }
                        else
                        {
                            lines[i] = $"{inputIP}={inputName}";
                        }
                        break;
                    }
                }

                File.WriteAllLines(configFilePath, lines);

                inputClientIP.Text = string.Empty;
                inputClientName.Text = string.Empty;

                inputClientIP.Enabled = false;
                inputClientName.Enabled = false;

                MessageBox.Show("Client adı güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadClientNames();

                hasChanges = true;
            }
            else
            {
                MessageBox.Show("Lütfen bir client seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClientNames()
        {
            clientList.Items.Clear();

            if (File.Exists(configFilePath))
            {
                string[] lines = File.ReadAllLines(configFilePath);

                foreach (string line in lines)
                {
                    string[] keyValue = line.Split('=');

                    if (keyValue.Length == 2)
                    {
                        clientList.Items.Add($"{keyValue[0].Trim()} = {keyValue[1].Trim()}");
                    }
                    else
                    {
                        clientList.Items.Add(line.Trim());
                    }
                }
            }
        }

        private void RestartApplication()
        {
            string executablePath = Application.ExecutablePath;
            Process.Start(executablePath);
            Application.Exit();
        }
    }
}
