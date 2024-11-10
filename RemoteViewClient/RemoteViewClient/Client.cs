using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RemoteViewClient
{
    class Client
    {
        static string appName = "RemoteViewClient";
        static string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string serverIP = "";
        static string serverPort = "";
        static bool connection = false;
        static TcpClient client;
        static NetworkStream stream;

        static void Main(string[] args)
        {
            bool createdNew;
            using (Mutex mutex = new Mutex(true, appName, out createdNew))
            {
                if (!createdNew)
                {
                    MessageBox.Show("Uygulama zaten çalışıyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AddToStartup();
                CheckConfig();
                ConnectionTCP();
            }
        }

        static void AddToStartup()
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

                if (rk.GetValue(appName) == null)
                {
                    rk.SetValue(appName, appPath);
                }

                rk.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Başlangıç ekleme hatası: {ex.Message}");
            }
        }

        static void CheckConfig()
        {
            string filePath = "config.ini";
            string defaultContent = "ServerIP=192.168.1.1\nServerPort=5000";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, defaultContent);
                Console.WriteLine("config.ini dosyası oluşturuldu ve varsayılan içerik yazıldı.");
            }
            else
            {
                var lines = File.ReadAllLines(filePath);
                bool hasServerIP = false;
                bool hasServerPort = false;

                foreach (var line in lines)
                {
                    if (line.StartsWith("ServerIP="))
                    {
                        hasServerIP = true;
                        serverIP = line.Substring("ServerIP=".Length);
                    }

                    if (line.StartsWith("ServerPort="))
                    {
                        hasServerPort = true;
                        serverPort = line.Substring("ServerPort=".Length);
                    }
                }

                if (!hasServerIP || !hasServerPort)
                {
                    File.WriteAllText(filePath, defaultContent);
                    Console.WriteLine("Eksik veya hatalı yapılandırma tespit edildi. config.ini dosyası sıfırlandı ve varsayılan içerik yazıldı.");
                }
                else
                {
                    Console.WriteLine($"ServerIP: {serverIP}");
                    Console.WriteLine($"ServerPort: {serverPort}");
                }
            }
        }

        static void ConnectionTCP()
        {
            if (!string.IsNullOrEmpty(serverIP) && !string.IsNullOrEmpty(serverPort))
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Bağlantı isteği gönderiliyor...");
                        client = new TcpClient();
                        client.Connect(serverIP, int.Parse(serverPort));

                        Console.WriteLine("Sunucu bağlantısı başarılı!");
                        connection = true;

                        stream = client.GetStream();

                        SendClientIP(stream);

                        Thread receiveThread = new Thread(() => ListenForMessages(stream));
                        receiveThread.Start();

                        while (client.Connected)
                        {
                            SendScreenShot(stream);
                            Thread.Sleep(200);
                        }

                        stream.Close();
                        client.Close();
                        receiveThread.Join();
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine($"Bağlantı hatası: {e.Message}");
                        connection = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Hata: {e.Message}");
                    }

                    Console.WriteLine("Bağlantı kesildi, tekrar bağlanılıyor...");
                    Thread.Sleep(5000);
                }
            }
            else
            {
                Console.WriteLine("Geçerli IP veya port bilgisi bulunamadı.");
            }
        }

        static void ListenForMessages(NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if (message.Trim().ToLower() == "exit")
                        {
                            Console.WriteLine("Çıkış mesajı alındı, bağlantı kesiliyor...");
                            stream.Close();
                            client.Close();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Mesaj dinlerken hata: {e.Message}");
            }
        }

        static void SendClientIP(NetworkStream stream)
        {
            try
            {
                string clientIP = GetLocalIPAddress();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(clientIP);
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"ClientIP: {clientIP}");
            }
            catch
            {
                stream.Close();
                client.Close();
            }
        }

        static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("0.0.0.0");
        }

        static void SendScreenShot(NetworkStream stream)
        {
            try
            {
                using (Bitmap screenshot = CaptureScreen())
                using (MemoryStream ms = new MemoryStream())
                {
                    screenshot.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    byte[] data = new byte[imageBytes.Length + 4];

                    BitConverter.GetBytes(imageBytes.Length).CopyTo(data, 0);
                    imageBytes.CopyTo(data, 4);

                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                }
            }
            catch
            {
                stream.Close();
                client.Close();
            }
        }

        static Bitmap CaptureScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }
            return screenshot;
        }
    }
}
