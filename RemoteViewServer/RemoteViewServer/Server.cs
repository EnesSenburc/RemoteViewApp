using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteViewServer
{
    public partial class Server : Form
    {
        string configFilePath = "config.ini";
        string clientFilePath = "client.ini";
        int serverPort;
        private TcpListener tcpListener;
        private bool isListening;
        private List<TcpClient> connectedClients;

        public Server()
        {
            InitializeComponent();
            connectedClients = new List<TcpClient>();

            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem config = new ToolStripMenuItem("Config");
            config.Click += ConfigMenuItem_Click;
            contextMenu.Items.Add(config);

            ToolStripMenuItem clients = new ToolStripMenuItem("Clients");
            clients.Click += ClientsMenuItem_Click;
            contextMenu.Items.Add(clients);

            this.ContextMenuStrip = contextMenu;
        }

        private void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            Config cnf = new Config();
            cnf.ShowDialog();
        }

        private void ClientsMenuItem_Click(object sender, EventArgs e)
        {
            Clients clns = new Clients();
            clns.ShowDialog();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            CheckConfigFile();
            CheckClientFile();
            StartServer();
            LoadClientIPs();
        }

        private void CheckConfigFile()
        {
            if (!File.Exists(configFilePath))
            {
                using (StreamWriter sw = File.CreateText(configFilePath))
                {
                    sw.WriteLine("ServerPort=5000");
                }
                serverPort = 5000;
            }
            else
            {
                string[] configLines = File.ReadAllLines(configFilePath);
                bool serverPortExists = false;

                for (int i = 0; i < configLines.Length; i++)
                {
                    if (configLines[i].StartsWith("ServerPort="))
                    {
                        serverPortExists = true;
                        string portValue = configLines[i].Substring("ServerPort=".Length);

                        if (int.TryParse(portValue, out int port))
                        {
                            if (port >= 0 && port <= 65535)
                            {
                                serverPort = port;
                            }
                            else
                            {
                                serverPort = 5000;
                                configLines[i] = "ServerPort=5000";
                            }
                        }
                        else
                        {
                            serverPort = 5000;
                            configLines[i] = "ServerPort=5000";
                        }
                        break;
                    }
                }

                if (!serverPortExists)
                {
                    using (StreamWriter sw = new StreamWriter(configFilePath, false))
                    {
                        sw.WriteLine("ServerPort=5000");
                    }
                    serverPort = 5000;
                }
                else
                {
                    File.WriteAllLines(configFilePath, configLines);
                }
            }
        }


        private void CheckClientFile()
        {
            if (!File.Exists(clientFilePath))
            {
                File.Create(clientFilePath).Close();
            }
        }

        private void LoadClientIPs()
        {
            if (!File.Exists(clientFilePath)) return;

            var lines = File.ReadAllLines(clientFilePath);
            int ipCount = lines.Length;

            int columns = 0;

            if (ipCount == 1) { columns = 1; }
            else if (ipCount <= 2) { columns = 2; }
            else if (ipCount <= 4) { columns = 2; }
            else if (ipCount <= 6) { columns = 3; }
            else if (ipCount <= 9) { columns = 3; }
            else if (ipCount <= 12) { columns = 4; }
            else { columns = 4; }

            int rows = (int)Math.Ceiling((double)ipCount / columns);

            tableLayoutPanel1.ColumnCount = columns;
            tableLayoutPanel1.RowCount = rows;
            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < rows; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            }

            tableLayoutPanel1.ColumnStyles.Clear();
            for (int i = 0; i < columns; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));
            }

            for (int i = 0; i < rows * columns; i++)
            {
                var pictureBox = new PictureBox
                {
                    Name = $"pictureBox_{i}",
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = Properties.Resources.connection_failed
                };

                if (i < ipCount)
                {
                    string line = lines[i];
                    string ipAddress;
                    string displayName;

                    var parts = line.Split(new[] { '=' }, 2);
                    ipAddress = parts[0];
                    displayName = parts.Length > 1 ? parts[1] : ipAddress;

                    pictureBox.Tag = ipAddress;

                    var ipLabel = new Label
                    {
                        Text = displayName,
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.FromArgb(20, 20, 20),
                        ForeColor = Color.LightGray,
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        Visible = true
                    };

                    pictureBox.Controls.Add(ipLabel);
                }
                else
                {
                    pictureBox.Visible = false;
                }

                tableLayoutPanel1.Controls.Add(pictureBox, i % columns, i / columns);
            }
        }


        private void StartServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, serverPort);
            isListening = true;
            tcpListener.Start();

            Thread listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }

        private void ListenForClients()
        {
            while (isListening)
            {
                try
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    connectedClients.Add(client);

                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(client);
                }
                catch { }
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[4096];
                int bytesRead;

                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string clientIP = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    if (!File.Exists(clientFilePath))
                    {
                        File.Create(clientFilePath).Close();
                    }

                    var lines = File.ReadAllLines(clientFilePath);
                    bool ipExists = Array.Exists(lines, line => line.StartsWith(clientIP + "=") || line.Equals(clientIP));

                    if (!ipExists)
                    {
                        File.AppendAllText(clientFilePath, clientIP + Environment.NewLine);
                        Invoke(new Action(LoadClientIPs));

                        foreach (var cnclients in connectedClients)
                        {
                            try
                            { 
                                byte[] exitMessage = Encoding.ASCII.GetBytes("exit");
                                stream.Write(exitMessage, 0, exitMessage.Length);
                                stream.Flush();
                            }
                            catch { }
                            finally
                            {
                                cnclients.Close();
                            }
                        }
                    }

                    UpdatePictureBox(clientIP, stream);
                }
                catch { }
                finally
                {
                    connectedClients.Remove(client);
                    client.Close();
                }
            }
        }

        private void UpdatePictureBox(string clientIP, NetworkStream stream)
        {
            PictureBox targetPictureBox = null;

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is PictureBox && control.Tag?.ToString() == clientIP)
                {
                    targetPictureBox = (PictureBox)control;
                    break;
                }
            }

            if (targetPictureBox == null) return;

            while (true)
            {
                try
                {
                    byte[] lengthBuffer = new byte[4];
                    int bytesRead = stream.Read(lengthBuffer, 0, 4);
                    if (bytesRead < 4)
                    {
                        if (targetPictureBox.InvokeRequired)
                        {
                            targetPictureBox.Invoke(new Action(() => targetPictureBox.Image = Properties.Resources.connection_failed));
                        }
                        else
                        {
                            targetPictureBox.Image = Properties.Resources.connection_failed;
                        }
                        break;
                    }

                    int imageLength = BitConverter.ToInt32(lengthBuffer, 0);
                    byte[] imageBuffer = new byte[imageLength];

                    int totalBytesRead = 0;
                    while (totalBytesRead < imageLength)
                    {
                        bytesRead = stream.Read(imageBuffer, totalBytesRead, imageLength - totalBytesRead);
                        if (bytesRead == 0) break;
                        totalBytesRead += bytesRead;
                    }

                    if (totalBytesRead != imageLength)
                    {
                        if (targetPictureBox.InvokeRequired)
                        {
                            targetPictureBox.Invoke(new Action(() => targetPictureBox.Image = Properties.Resources.connection_failed));
                        }
                        else
                        {
                            targetPictureBox.Image = Properties.Resources.connection_failed;
                        }
                        break;
                    }

                    using (var ms = new MemoryStream(imageBuffer))
                    {
                        var image = Image.FromStream(ms);
                        if (targetPictureBox.InvokeRequired)
                        {
                            targetPictureBox.Invoke(new Action(() => targetPictureBox.Image = image));
                        }
                        else
                        {
                            targetPictureBox.Image = image;
                        }
                    }
                }
                catch
                {
                    if (targetPictureBox.InvokeRequired)
                    {
                        targetPictureBox.Invoke(new Action(() => targetPictureBox.Image = Properties.Resources.connection_failed));
                    }
                    else
                    {
                        targetPictureBox.Image = Properties.Resources.connection_failed;
                    }
                    break;
                }
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            tcpListener.Stop();

            foreach (var client in connectedClients)
            {
                try
                {
                    if (client.Connected)
                    {
                        NetworkStream stream = client.GetStream();

                        byte[] exitMessage = Encoding.ASCII.GetBytes("exit");
                        stream.Write(exitMessage, 0, exitMessage.Length);
                        stream.Flush();

                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Mesaj gönderme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    client.Close();
                }
            }

            connectedClients.Clear();
        }
    }
}
