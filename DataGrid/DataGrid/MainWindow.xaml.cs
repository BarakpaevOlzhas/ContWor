using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace DataGrid
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            InitializeComponent();
            WebRequest request = WebRequest.Create("https://data.egov.kz/api/v2/valutalar_bagamdary4/v501?pretty");
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            List<Curse> data = new List<Curse>();

            
            using (StreamReader stream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                string JsonFormat = stream.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Curse>>(JsonFormat);
            }

            foreach (var i in data)
            {
                var Shop = new Curse
                {
                    Sootnowenie = i.Sootnowenie,
                    Name_Rus = "KZT",
                    Kurs = i.Kurs,
                    Kod = i.Kod
                };

                dataG.Items.Add(Shop);
            }           

        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
