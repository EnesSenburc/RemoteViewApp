using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteViewServer
{
    public partial class Config : Form
    {
        private string configFilePath = "config.ini";

        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            string localIPAddress = GetLocalIPAddress();
            if (!string.IsNullOrEmpty(localIPAddress))
            {
                inputServerIP.Text = localIPAddress;
            }

            if (File.Exists(configFilePath))
            {
                string[] configLines = File.ReadAllLines(configFilePath);
                foreach (string line in configLines)
                {
                    if (line.StartsWith("ServerPort="))
                    {
                        string portValue = line.Substring("ServerPort=".Length);
                        inputServerPort.Text = portValue;
                        break;
                    }
                }
            }
        }

        private void inputServerPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void inputServerPort_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (int.TryParse(inputServerPort.Text, out int port))
            {
                if (port < 0 || port > 65535)
                {
                    MessageBox.Show($"Lütfen {0} ile {65535} arasında bir port numarası girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir port numarası girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void buttonServerSave_Click(object sender, EventArgs e)
        {
            string newPortValue = inputServerPort.Text;

            if (string.IsNullOrWhiteSpace(newPortValue))
            {
                MessageBox.Show("Lütfen geçerli bir port numarası girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(configFilePath))
            {
                string[] configLines = File.ReadAllLines(configFilePath);
                bool serverPortUpdated = false;

                for (int i = 0; i < configLines.Length; i++)
                {
                    if (configLines[i].StartsWith("ServerPort="))
                    {
                        configLines[i] = "ServerPort=" + newPortValue;
                        serverPortUpdated = true;
                        break;
                    }
                }

                if (!serverPortUpdated)
                {
                    Array.Resize(ref configLines, configLines.Length + 1);
                    configLines[configLines.Length - 1] = "ServerPort=" + newPortValue;
                }

                File.WriteAllLines(configFilePath, configLines);
            }
            else
            {
                using (StreamWriter sw = File.CreateText(configFilePath))
                {
                    sw.WriteLine("ServerPort=" + newPortValue);
                }
            }

            MessageBox.Show("Server portu başarıyla kaydedildi. Uygulama yeniden başlatılıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RestartApplication();
        }

        private void RestartApplication()
        {
            string executablePath = Application.ExecutablePath;

            Process.Start(executablePath);

            Application.Exit();
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("IP adresi bulunamadı!");
        }
    }
}
