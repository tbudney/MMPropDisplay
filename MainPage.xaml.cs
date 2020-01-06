using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MMPropDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string dbAddress;

        public MainPage()
        {
            this.InitializeComponent();
            createConfigFile();
            setDBAddress("Fake String");
            

        }

        
        private void TheButton_Click(object sender, RoutedEventArgs e)
        {

            getDBAddress();

            textBlock1.Text = dbAddress;
        }
        private string DbTestPull()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=MMPropSQL;User ID=test;Password=123456Test";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            System.Diagnostics.Debug.WriteLine("Connection Open  !");
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output ="";

            sql = "SELECT * FROM dbo.Displays"; //sql command
            command = new SqlCommand(sql, cnn); //send command with connection string
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + " - " + dataReader.GetValue(3) + " - " + dataReader.GetValue(4) + " - " + dataReader.GetValue(5) + " - " + dataReader.GetValue(6) +" - " + dataReader.GetValue(7) + "\n";
            }

            System.Diagnostics.Debug.WriteLine(Output);
            cnn.Close();
            return "test";
        }
        private string FindIP() //test method to practice updating fields in xaml
        {
            List<string> IPaddr = new List<string>();
            var HostNames = Windows.Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
            foreach (var Host in HostNames)
            {
                string IP = Host.DisplayName;
                IPaddr.Add(IP);
            }
            return IPaddr.Last();
        }
        private async void createConfigFile()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try { Windows.Storage.StorageFile configFile = await storageFolder.CreateFileAsync("config.txt", Windows.Storage.CreationCollisionOption.FailIfExists);
                System.Diagnostics.Debug.WriteLine("Creating Config File");
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("Configuration File Already Exists");
            }
           
            
        }
        private async void setDBAddress(String connectionString)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile configFile = await storageFolder.GetFileAsync("config.txt");
            await Windows.Storage.FileIO.WriteTextAsync(configFile, connectionString);
        }
        private async void getDBAddress()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile configFile = await storageFolder.GetFileAsync("config.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(configFile);
            dbAddress = text;

            System.Diagnostics.Debug.WriteLine(text);
        }
    }   
    
}
