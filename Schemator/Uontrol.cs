using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schemator
{
    public partial class Uontrol : Form
    {
        private readonly SelectedCsv _selectedCsv;
        public Uontrol(SelectedCsv selectedCsv)
        {
            InitializeComponent();
            _selectedCsv = selectedCsv;
            openfilebtn.Click += openfilebtn_Click;
            openFileDialog1.Filter = "Text files(*.csv)|*.csv|All files(*.*)|*.*";
            
           
        }

        private void openfilebtn_Click(object sender, EventArgs e) 
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            { return; }
            string filename = openFileDialog1.FileName;
            string filetext = System.IO.File.ReadAllText(filename);
            MessageBox.Show($"Открыт файл {filename}");
            SelectedCsv selectedCsv = new SelectedCsv();
            _selectedCsv.csvfile= filetext;
            

            

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string text= _selectedCsv.csvfile;
            string[] rows = text.Split('\n');
            List<Room> rooms = new List<Room>();
            for (int i =1; i<rows.Count()-1; i++)
            {
               var  str = rows[i].ToString().Split(';');
               
               Room room = new Room(str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9], str[10], str[11],
                  str[12], str[13], str[14], str[15], str[16], str[17]);
                
                    
                rooms.Add(room);
                
              
            }
            MessageBox.Show(rooms.Count().ToString());
        }
    }
}
