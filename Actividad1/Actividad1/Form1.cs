using System;
using System.Drawing;
using System.Drawing.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Net;

namespace Actividad1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           


            String Username = Environment.UserName;
            lblUsername.Text = Username;
            string hostName = Dns.GetHostName();
            lblHostname.Text = hostName;
            //
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            //Detectar IPs IPv4 y IPv6 en la tarjeta de red activa
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if(adapter.Name.Equals("Wi-Fi") || adapter.Name.Equals("Ethernet"))
                {
                    if(adapter.OperationalStatus == OperationalStatus.Up)
                    {
                        string mac_address = adapter.GetPhysicalAddress().ToString();
                        lblmacaddress.Text = mac_address;

                        IPInterfaceProperties properties = adapter.GetIPProperties();

                        foreach (IPAddressInformation unicast in properties.UnicastAddresses)
                        {
                            if(unicast.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                lblip.Text = unicast.Address.ToString();
                            }
                        }
                        GatewayIPAddressInformationCollection addresses = properties.GatewayAddresses;
                        if (addresses.Count > 0)
                        {
                            //Muestra la dirreccion IP en formato IPv4
                            foreach (GatewayIPAddressInformation address in addresses)
                            {
                                lblGatewat.Text = address.Address.ToString();
                            }
                        }
                        //
                        Ping myPing = new Ping();
                        PingReply reply = myPing.Send("1.1.1.1", 1000);
                        int PingInternetCounter = 0;
                        for (int numpings = 0; numpings < 4; numpings++)
                        {
                            if(reply.Status == IPStatus.Success)
                            {
                                PingInternetCounter++;

                                if(PingInternetCounter.Equals(4))
                                {
                                    lblInternetConnection.Text = "Established";
                                    lblInternetConnection.ForeColor = Color.Green;
                                }
                                if(PingInternetCounter > 0 && PingInternetCounter < 4)
                                {
                                    lblInternetConnection.Text = "Unstable";
                                    lblInternetConnection.ForeColor = Color.Yellow;
                                }
                                if(reply.Status != IPStatus.Success)
                                {
                                    lblInternetConnection.Text = "Disconnected";
                                    lblInternetConnection.ForeColor = Color.Red;
                                }
                            }






                        }

                    }

                }

                var networks = NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected);
                foreach (var network in networks)
                {
                    if (network.IsConnected)
                    {
                        lblssidstatus.Text = "Connected";
                        lblssidstatus.ForeColor = Color.Green;
                    }else if (!network.IsConnected)
                    {
                        lblssidstatus.Text = "Disconnected";
                        lblssidstatus.ForeColor = Color.Red;
                    }
                    lblssid.Text = network.Name;
                }
                string strCmdText;
                //
                strCmdText = "/C" + "\"C:\\Program Files\\Oracle\\virtualbox\\VBoxManage.exe\" --version";

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = strCmdText;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                //
                string VBOX_Version = "";
                while (!process.HasExited)
                {
                    VBOX_Version = VBOX_Version + process.StandardOutput.ReadToEnd();
                }
                //

                //
                if(VBOX_Version != "")
                {
                    lblVirtualBoxInstalled.Text = "Yes";
                    lblVirtualBoxInstalled.ForeColor = Color.Green;
                    //
                    string vbox_cut_version = VBOX_Version.Substring(0, VBOX_Version.IndexOf("r"));
                    lblVBOXV.Text = vbox_cut_version;

                    if(vbox_cut_version.Equals("7.0.6"))
                    {
                        lblVBOXV.Text = vbox_cut_version + " " + "(Last Version)";
                        lblVBOXV.ForeColor = Color.Green;
                    }
                    if(!vbox_cut_version.Equals("7.0.6"))
                    {
                        lblVBOXV.Text = vbox_cut_version + " " + "(Outdated";
                        lblVBOXV.ForeColor = Color.Yellow;
                    }
                }
                if(VBOX_Version.Equals(""))
                {
                    lblVirtualBoxInstalled.Text = "NO";
                    lblVirtualBoxInstalled.ForeColor = Color.Red;
                    lblVBOXV.Text = "N/A";
                    lblVBOXV.ForeColor = Color.Red;
                }

            }
            /*private void cb_os_type_Click(object sender, EventArgs e)
            {
                OS_TYPE_CB();
            }*/
        }

        private Label IPHost;

        private void InitializeComponent()
        {
            this.IPHost = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblip = new System.Windows.Forms.Label();
            this.IPGateway = new System.Windows.Forms.Label();
            this.lblGatewat = new System.Windows.Forms.Label();
            this.Hostname = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblHostname = new System.Windows.Forms.Label();
            this.SSID = new System.Windows.Forms.Label();
            this.SSIDStatus = new System.Windows.Forms.Label();
            this.lblssid = new System.Windows.Forms.Label();
            this.lblssidstatus = new System.Windows.Forms.Label();
            this.MACAddress = new System.Windows.Forms.Label();
            this.InternetConnection = new System.Windows.Forms.Label();
            this.lblmacaddress = new System.Windows.Forms.Label();
            this.lblInternetConnection = new System.Windows.Forms.Label();
            this.VirtualBoxInstalled = new System.Windows.Forms.Label();
            this.VirtualBoxVersion = new System.Windows.Forms.Label();
            this.lblVirtualBoxInstalled = new System.Windows.Forms.Label();
            this.lblVBOXV = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // IPHost
            // 
            this.IPHost.AutoSize = true;
            this.IPHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPHost.Location = new System.Drawing.Point(3, 0);
            this.IPHost.Name = "IPHost";
            this.IPHost.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IPHost.Size = new System.Drawing.Size(45, 13);
            this.IPHost.TabIndex = 0;
            this.IPHost.Text = "IPHost";
            this.IPHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(391, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(392, 25);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Welcome to VirtualBox Management";
            this.lbl_Title.Click += new System.EventHandler(this.label2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.72067F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.27933F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel1.Controls.Add(this.lblip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.IPGateway, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblGatewat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Hostname, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Username, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblUsername, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblHostname, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.SSID, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.SSIDStatus, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblssid, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblssidstatus, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.MACAddress, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.InternetConnection, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblmacaddress, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblInternetConnection, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.VirtualBoxInstalled, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.VirtualBoxVersion, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblVirtualBoxInstalled, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblVBOXV, 9, 1);
            this.tableLayoutPanel1.Controls.Add(this.IPHost, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 54);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1141, 84);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // lblip
            // 
            this.lblip.AutoSize = true;
            this.lblip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblip.Location = new System.Drawing.Point(92, 0);
            this.lblip.Name = "lblip";
            this.lblip.Size = new System.Drawing.Size(37, 13);
            this.lblip.TabIndex = 4;
            this.lblip.Text = "lbl_ip";
            // 
            // IPGateway
            // 
            this.IPGateway.AutoSize = true;
            this.IPGateway.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPGateway.Location = new System.Drawing.Point(3, 42);
            this.IPGateway.Name = "IPGateway";
            this.IPGateway.Size = new System.Drawing.Size(68, 13);
            this.IPGateway.TabIndex = 3;
            this.IPGateway.Text = "IPGateway";
            // 
            // lblGatewat
            // 
            this.lblGatewat.AutoSize = true;
            this.lblGatewat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGatewat.Location = new System.Drawing.Point(92, 42);
            this.lblGatewat.Name = "lblGatewat";
            this.lblGatewat.Size = new System.Drawing.Size(74, 13);
            this.lblGatewat.TabIndex = 5;
            this.lblGatewat.Text = "lbl_gateway\r\n";
            // 
            // Hostname
            // 
            this.Hostname.AutoSize = true;
            this.Hostname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hostname.Location = new System.Drawing.Point(182, 42);
            this.Hostname.Name = "Hostname";
            this.Hostname.Size = new System.Drawing.Size(63, 13);
            this.Hostname.TabIndex = 7;
            this.Hostname.Text = "Hostname";
            this.Hostname.Click += new System.EventHandler(this.label7_Click);
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(182, 0);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(63, 13);
            this.Username.TabIndex = 6;
            this.Username.Text = "Username";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(265, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(83, 13);
            this.lblUsername.TabIndex = 8;
            this.lblUsername.Text = "lbl_Username";
            // 
            // lblHostname
            // 
            this.lblHostname.AutoSize = true;
            this.lblHostname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHostname.Location = new System.Drawing.Point(265, 42);
            this.lblHostname.Name = "lblHostname";
            this.lblHostname.Size = new System.Drawing.Size(83, 13);
            this.lblHostname.TabIndex = 9;
            this.lblHostname.Text = "lbl_Hostname";
            // 
            // SSID
            // 
            this.SSID.AutoSize = true;
            this.SSID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSID.Location = new System.Drawing.Point(359, 0);
            this.SSID.Name = "SSID";
            this.SSID.Size = new System.Drawing.Size(44, 13);
            this.SSID.TabIndex = 10;
            this.SSID.Text = "SSID :";
            // 
            // SSIDStatus
            // 
            this.SSIDStatus.AutoSize = true;
            this.SSIDStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSIDStatus.Location = new System.Drawing.Point(359, 42);
            this.SSIDStatus.Name = "SSIDStatus";
            this.SSIDStatus.Size = new System.Drawing.Size(72, 13);
            this.SSIDStatus.TabIndex = 11;
            this.SSIDStatus.Text = "SSIDStatus";
            // 
            // lblssid
            // 
            this.lblssid.AutoSize = true;
            this.lblssid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblssid.Location = new System.Drawing.Point(459, 0);
            this.lblssid.Name = "lblssid";
            this.lblssid.Size = new System.Drawing.Size(49, 13);
            this.lblssid.TabIndex = 12;
            this.lblssid.Text = "lbl_ssid";
            // 
            // lblssidstatus
            // 
            this.lblssidstatus.AutoSize = true;
            this.lblssidstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblssidstatus.Location = new System.Drawing.Point(459, 42);
            this.lblssidstatus.Name = "lblssidstatus";
            this.lblssidstatus.Size = new System.Drawing.Size(90, 13);
            this.lblssidstatus.TabIndex = 13;
            this.lblssidstatus.Text = "lbl_ssid_status";
            // 
            // MACAddress
            // 
            this.MACAddress.AutoSize = true;
            this.MACAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MACAddress.Location = new System.Drawing.Point(562, 0);
            this.MACAddress.Name = "MACAddress";
            this.MACAddress.Size = new System.Drawing.Size(78, 13);
            this.MACAddress.TabIndex = 14;
            this.MACAddress.Text = "MACAddress";
            // 
            // InternetConnection
            // 
            this.InternetConnection.AutoSize = true;
            this.InternetConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InternetConnection.Location = new System.Drawing.Point(562, 42);
            this.InternetConnection.Name = "InternetConnection";
            this.InternetConnection.Size = new System.Drawing.Size(107, 26);
            this.InternetConnection.TabIndex = 15;
            this.InternetConnection.Text = "lnternetConnection";
            // 
            // lblmacaddress
            // 
            this.lblmacaddress.AutoSize = true;
            this.lblmacaddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmacaddress.Location = new System.Drawing.Point(675, 0);
            this.lblmacaddress.Name = "lblmacaddress";
            this.lblmacaddress.Size = new System.Drawing.Size(101, 13);
            this.lblmacaddress.TabIndex = 16;
            this.lblmacaddress.Text = "lbl_mac_address";
            // 
            // lblInternetConnection
            // 
            this.lblInternetConnection.AutoSize = true;
            this.lblInternetConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternetConnection.Location = new System.Drawing.Point(675, 42);
            this.lblInternetConnection.Name = "lblInternetConnection";
            this.lblInternetConnection.Size = new System.Drawing.Size(142, 13);
            this.lblInternetConnection.TabIndex = 17;
            this.lblInternetConnection.Text = "lbl_Internet_Connection";
            // 
            // VirtualBoxInstalled
            // 
            this.VirtualBoxInstalled.AutoSize = true;
            this.VirtualBoxInstalled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VirtualBoxInstalled.Location = new System.Drawing.Point(837, 0);
            this.VirtualBoxInstalled.Name = "VirtualBoxInstalled";
            this.VirtualBoxInstalled.Size = new System.Drawing.Size(125, 13);
            this.VirtualBoxInstalled.TabIndex = 18;
            this.VirtualBoxInstalled.Text = "is VirtualBoxInstalled";
            // 
            // VirtualBoxVersion
            // 
            this.VirtualBoxVersion.AutoSize = true;
            this.VirtualBoxVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VirtualBoxVersion.Location = new System.Drawing.Point(837, 42);
            this.VirtualBoxVersion.Name = "VirtualBoxVersion";
            this.VirtualBoxVersion.Size = new System.Drawing.Size(106, 13);
            this.VirtualBoxVersion.TabIndex = 19;
            this.VirtualBoxVersion.Text = "VirtualBoxVersion";
            // 
            // lblVirtualBoxInstalled
            // 
            this.lblVirtualBoxInstalled.AutoSize = true;
            this.lblVirtualBoxInstalled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVirtualBoxInstalled.Location = new System.Drawing.Point(997, 0);
            this.lblVirtualBoxInstalled.Name = "lblVirtualBoxInstalled";
            this.lblVirtualBoxInstalled.Size = new System.Drawing.Size(132, 13);
            this.lblVirtualBoxInstalled.TabIndex = 20;
            this.lblVirtualBoxInstalled.Text = "lbl_VirtualBoxInstalled";
            this.lblVirtualBoxInstalled.Click += new System.EventHandler(this.label20_Click);
            // 
            // lblVBOXV
            // 
            this.lblVBOXV.AutoSize = true;
            this.lblVBOXV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVBOXV.Location = new System.Drawing.Point(997, 42);
            this.lblVBOXV.Name = "lblVBOXV";
            this.lblVBOXV.Size = new System.Drawing.Size(75, 13);
            this.lblVBOXV.TabIndex = 21;
            this.lblVBOXV.Text = "lbl_VBOX_V";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1161, 723);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lbl_Title);
            this.Name = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lbl_Title;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private TableLayoutPanel tableLayoutPanel1;

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private Label lblip;
        private Label IPGateway;
        private Label lblGatewat;
        private Label Hostname;
        private Label Username;
        private Label lblUsername;
        private Label lblHostname;
        private Label SSID;
        private Label SSIDStatus;
        private Label lblssid;
        private Label lblssidstatus;
        private Label MACAddress;
        private Label InternetConnection;
        private Label lblmacaddress;
        private Label lblInternetConnection;
        private Label VirtualBoxInstalled;
        private Label VirtualBoxVersion;
        private Label lblVirtualBoxInstalled;
        private Label lblVBOXV;

        
        

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
