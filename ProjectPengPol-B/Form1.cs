using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Windows.Forms;

namespace ProjectPengPol_B
{

    public partial class Form1 : Form
    {
        public ArrayList list;
        public double treshold = 0.8;
        centroid[] c;
        public double sigma_centroid_baru = 0;
        public double sigma_centroid_lama = 0;
        int k;
        public double maxLatitude, maxLongitude, maxBrightness, maxConfidence;
        public double minLatitude, minLongitude, minBrightness, minConfidence;
        int iterasi = 0;

        private void button_tambahiterasi_Click(object sender, EventArgs e)
        {
            
            hitungCentroid();
            hitungjarak_tiapdata_ke_tiapcentroid();
            switchCluster();
            iterasi++;
            textBox_jumlahiterasi.Text = iterasi.ToString();
            tampilkan_TabelHasilAkhir();
            tampilkan_TabelCentroid();
            textBox_selisihlamadanbaru.Text = (Math.Abs(sigma_centroid_baru - sigma_centroid_lama)).ToString();


        }

        private void button_lanjutkan_Click(object sender, EventArgs e)
        {
            do
            {
                hitungCentroid();
                hitungjarak_tiapdata_ke_tiapcentroid();
                switchCluster();
                textBox_selisihlamadanbaru.Text = (Math.Abs(sigma_centroid_baru - sigma_centroid_lama)).ToString();
                iterasi++;
                textBox_jumlahiterasi.Text = iterasi.ToString();
            } while (Math.Abs(sigma_centroid_baru - sigma_centroid_lama) > treshold);

            textBox_jumlahiterasi.Text = iterasi.ToString();
            tampilkan_TabelHasilAkhir();
            tampilkan_TabelCentroid();
            


        }

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            list = new ArrayList();
            list.Add(new datakebakaran(1, 10.365, 123.033, 312.3, 51));
            list.Add(new datakebakaran(2, 13.353, 120.572, 314.4, 61));
            list.Add(new datakebakaran(3, 0.553, 102.297, 317, 41));
            list.Add(new datakebakaran(4, -1.396, 102.544, 310.2, 24));
            list.Add(new datakebakaran(5, -2.817, 103.541, 315.6, 44));
            list.Add(new datakebakaran(6, -3.481, 103.471, 315.4, 60));
            list.Add(new datakebakaran(7, -3.594, 104.28, 311, 31));
            list.Add(new datakebakaran(8, -3.719, 104.38, 318.9, 53));
            list.Add(new datakebakaran(9, -4.547, 104.307, 312.7, 55));
            list.Add(new datakebakaran(10, -4.546, 104.317, 312.6, 55));
            list.Add(new datakebakaran(11, -3.534, 127.938, 321, 56));
            list.Add(new datakebakaran(12, -3.532, 127.947, 317.2, 51));
            list.Add(new datakebakaran(13, -3.722, 104.386, 321.2, 65));
            list.Add(new datakebakaran(14, -3.721, 104.375, 350.3, 95));
            list.Add(new datakebakaran(15, -3.693, 104.396, 317, 40));
            list.Add(new datakebakaran(16, -2.517, 104.245, 323.6, 77));
            list.Add(new datakebakaran(17, 0.275, 102.341, 315.7, 52));
            list.Add(new datakebakaran(18, 0.568, 102.16, 312.5, 51));
            list.Add(new datakebakaran(19, -2.58, 121.389, 310.7, 39));
            list.Add(new datakebakaran(20, -6.579, 146.219, 315.5, 54));
            list.Add(new datakebakaran(21, -8.113, 118.07, 319.8, 51));
            list.Add(new datakebakaran(22, -8.112, 118.079, 319.8, 0));
            list.Add(new datakebakaran(23, -9.398, 119.737, 323.8, 40));
            list.Add(new datakebakaran(24, -0.798, 123.108, 312.1, 52));
            list.Add(new datakebakaran(25, -1.038, 123.29, 316.5, 57));
            list.Add(new datakebakaran(26, 3.009, 101.374, 315.7, 0));
            list.Add(new datakebakaran(27, -4.203, 122.195, 314.4, 57));
            list.Add(new datakebakaran(28, 3.836, 113.566, 325.6, 68));
            list.Add(new datakebakaran(29, 2.689, 113.127, 314.2, 40));
            list.Add(new datakebakaran(30, -2.391, 110.697, 317.4, 65));
            list.Add(new datakebakaran(31, -2.39, 110.708, 318.5, 70));
            list.Add(new datakebakaran(32, -2.394, 110.703, 326.3, 79));
            list.Add(new datakebakaran(33, -8.254, 113.37, 310.8, 43));
            list.Add(new datakebakaran(34, -7.589, 138.561, 314.1, 60));
            list.Add(new datakebakaran(35, 0.58, 101.981, 311.3, 35));
            list.Add(new datakebakaran(36, 2.701, 112.136, 330, 63));
            list.Add(new datakebakaran(37, 2.783, 112.144, 318.7, 70));
            list.Add(new datakebakaran(38, 2.793, 112.135, 335.1, 87));
            list.Add(new datakebakaran(39, 2.817, 113.293, 317.9, 37));
            list.Add(new datakebakaran(40, 2.817, 113.299, 318.4, 45));
            list.Add(new datakebakaran(41, 4.019, 117.308, 325.6, 79));
            list.Add(new datakebakaran(42, -8.106, 112.927, 312.1, 78));
            list.Add(new datakebakaran(43, 0.107, 117.473, 313, 81));
            list.Add(new datakebakaran(44, -2.566, 101.254, 323.5, 77));
            list.Add(new datakebakaran(45, -8.348, 147.822, 311.6, 50));
            list.Add(new datakebakaran(46, -8.346, 147.832, 334.4, 86));
            list.Add(new datakebakaran(47, 0.107, 117.471, 311.3, 74));
            list.Add(new datakebakaran(48, -2.584, 121.378, 310.5, 71));
            list.Add(new datakebakaran(49, -8.109, 112.917, 311, 73));
            tampilkan_TabelSoal();
            hitung_max_min_setiapfitur();
        }
        public void hitung_max_min_setiapfitur()
        {
            this.maxLatitude = double.MinValue;
            this.maxLongitude = double.MinValue;
            this.maxBrightness = double.MinValue;
            this.maxConfidence = double.MinValue;

            this.minLatitude = double.MaxValue;
            this.minLongitude = double.MaxValue;
            this.minBrightness = double.MaxValue;
            this.minConfidence = double.MaxValue;


            foreach (datakebakaran x in list)
            {
                if (x.latitude > this.maxLatitude)
                {
                    this.maxLatitude = x.latitude;
                }
                else if (x.latitude < this.minLatitude)
                {
                    this.minLatitude = x.latitude;
                }
                if (x.longitude > this.maxLongitude)
                {
                    this.maxLongitude = x.longitude;
                }
                else if (x.longitude < this.minLongitude)
                {
                    this.minLongitude = x.longitude;
                }
                if (x.brightness > this.maxBrightness)
                {
                    this.maxBrightness = x.brightness;
                }
                else if (x.brightness < this.minBrightness)
                {
                    this.minBrightness = x.brightness;
                }
                if (x.confidence > this.maxConfidence)
                {
                    this.maxConfidence = x.confidence;
                }
                else if (x.confidence < this.minConfidence)
                {
                    this.minConfidence = x.confidence;
                }

            }
            textbox_max_latitude.Text = this.maxLatitude.ToString();
            textbox_min_latitude.Text = this.minLatitude.ToString();
            textbox_max_longitude.Text = this.maxLongitude.ToString();
            textbox_min_longitude.Text = this.minLongitude.ToString();
            textbox_max_brightness.Text = this.maxBrightness.ToString();
            textbox_min_brightness.Text = this.minBrightness.ToString();
            textbox_max_confidence.Text = this.maxConfidence.ToString();
            textbox_min_confidence.Text = this.minConfidence.ToString();



        }
        public void tampilkan_TabelSoal()
        {            
            listView1.Items.Clear();
            foreach (datakebakaran x in list)
            {
                ListViewItem item = new ListViewItem(x.id.ToString());
                item.SubItems.Add(x.latitude.ToString());
                item.SubItems.Add(x.longitude.ToString());
                item.SubItems.Add(x.brightness.ToString());
                item.SubItems.Add(x.confidence.ToString());                
                listView1.Items.Add(item);                
            }
        }
        public void tampilkan_TabelHasilAkhir()
        {
            listView3.Items.Clear();
            foreach (datakebakaran x in list)
            {
                ListViewItem item = new ListViewItem(x.id.ToString());
                item.SubItems.Add(x.latitude.ToString());
                item.SubItems.Add(x.longitude.ToString());
                item.SubItems.Add(x.brightness.ToString());
                item.SubItems.Add(x.confidence.ToString());
                item.SubItems.Add(x.cluster.ToString());
                listView3.Items.Add(item);

            }
        }

        public void tampilkan_TabelHasilNormalisasi()
        {
            //int counter = 1;
            listView2.Items.Clear();
            foreach (datakebakaran x in list)
            {

                ListViewItem item = new ListViewItem(x.id.ToString());
                item.SubItems.Add(x.latitude.ToString());
                item.SubItems.Add(x.longitude.ToString());
                item.SubItems.Add(x.brightness.ToString());
                item.SubItems.Add(x.confidence.ToString());
                item.SubItems.Add(x.cluster.ToString());
                listView2.Items.Add(item);
                //counter++;
            }
        }

        public void tampilkan_TabelCentroid()
        {
            listView_centroid.Items.Clear();
            foreach (centroid c_i in c)
            {
                ListViewItem item = new ListViewItem(c_i.c_latitude.ToString());
                
                item.SubItems.Add(c_i.c_longitude.ToString());
                item.SubItems.Add(c_i.c_brightness.ToString());
                item.SubItems.Add(c_i.c_confidence.ToString());
                listView_centroid.Items.Add(item);
            }
        }

        

        private void button_submit_Click(object sender, EventArgs e)
        {
            k = Int32.Parse(textBox_k.Text); //inputan user 
            hitungNormalisasi_dan_tentukanRandomCluster();
            tampilkan_TabelHasilNormalisasi();
            c = new centroid[k];
        }
        public void hitungNormalisasi_dan_tentukanRandomCluster()
        {
            
            Random rnd = new Random();
            
                foreach (datakebakaran x in list)
                {
                    x.latitude = (x.latitude - this.minLatitude) / (this.maxLatitude - this.minLatitude);
                    x.longitude = (x.longitude - this.minLongitude) / (this.maxLongitude - this.minLongitude);
                    x.brightness = (x.brightness - this.minBrightness) / (this.maxBrightness - this.minBrightness);
                    x.confidence = (x.confidence - this.minConfidence) / (this.maxConfidence - this.minConfidence);
                    x.cluster = rnd.Next(1, k + 1);
                }
            
        }
        public void hitungCentroid (){            
            double total_latitude_cluster_i = 0;
            double total_longitude_cluster_i = 0;
            double total_brightness_cluster_i = 0;
            double total_confidence_cluster_i = 0;
            double banyak_anggota_cluster_i = 0;

            if (iterasi==0)
            {
                sigma_centroid_lama = 0;
            }
            else
            {
                sigma_centroid_lama = sigma_centroid_baru;
            }
            
            sigma_centroid_baru = 0;
            for (int i = 0; i < k; i++)
            {
                foreach (datakebakaran x in list)
                {

                    if (x.cluster == i+1)
                    {
                        total_latitude_cluster_i += x.latitude;
                        total_longitude_cluster_i += x.longitude;
                        total_brightness_cluster_i += x.brightness;
                        total_confidence_cluster_i += x.confidence;
                        banyak_anggota_cluster_i += 1;
                    }
                }
                c[i] = new centroid(total_latitude_cluster_i / banyak_anggota_cluster_i,
                                    total_longitude_cluster_i / banyak_anggota_cluster_i,
                                    total_brightness_cluster_i / banyak_anggota_cluster_i,
                                    total_confidence_cluster_i / banyak_anggota_cluster_i);
                sigma_centroid_baru = sigma_centroid_baru +
                                    (total_latitude_cluster_i / banyak_anggota_cluster_i) +
                                    (total_longitude_cluster_i / banyak_anggota_cluster_i) +
                                    (total_brightness_cluster_i / banyak_anggota_cluster_i) +
                                    (total_confidence_cluster_i / banyak_anggota_cluster_i);
            }

      
        }
        public void hitungjarak_tiapdata_ke_tiapcentroid()
        {
            double p = 0;
            double q = 0;
            double r = 0;
            double s = 0;
                        
                foreach (datakebakaran x in list)
                {
                    x.jarak_ke_centroid_i = new double[k];
                    for (int i = 0; i < k; i++)
                    {
                        p = Math.Pow(Math.Abs(x.latitude - c[i].c_latitude), 2);
                        q = Math.Pow(Math.Abs(x.longitude - c[i].c_longitude), 2);
                        r = Math.Pow(Math.Abs(x.brightness - c[i].c_brightness), 2);
                        s = Math.Pow(Math.Abs(x.confidence - c[i].c_confidence), 2);                        
                        x.jarak_ke_centroid_i[i] = Math.Sqrt(p + q + r + s);
                    }
                }
                        
        }
        public void switchCluster()
        {
            foreach (datakebakaran x in list)
            {
                
                double centroidTerdekat = x.jarak_ke_centroid_i.Min();
                for (int i = 0; i < k; i++)
                {
                    if (centroidTerdekat==x.jarak_ke_centroid_i[i])
                    {
                        x.cluster = i+1;
                    }
                }                
            }
            

        }
        public class datakebakaran
        {
            public double latitude, longitude, brightness, confidence;
            public int id;
            public int cluster;
            public double []jarak_ke_centroid_i;
            public datakebakaran()
            {

            }
            public datakebakaran(int id, double a, double b, double c, double d)
            {
                this.id = id;
                this.latitude = a;
                this.longitude = b;
                this.brightness = c;
                this.confidence = d;
                this.cluster = 0;
            }
        }
        public class centroid
        {
            public double c_latitude, c_longitude, c_brightness, c_confidence;
            public centroid()
            {

            }
            public centroid(double a, double b, double c, double d)
            {
                this.c_latitude = a;
                this.c_longitude = b;
                this.c_brightness = c;
                this.c_confidence = d;                
            }

        }
    }
}
