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
             var groupedrooms = rooms.GroupBy(x=>x.System1).ToList();
            var grouproompair =new Dictionary<IGrouping<string,Room>, List<Room>>();
            foreach (var group  in groupedrooms)
            {
                List<Room> grouplist =new List<Room>();
                foreach (var room in group)
                {
                    grouplist.Add(room);
                }
                grouproompair.Add(group, grouplist);
            }

            
           /* List<string> systems = new List<string>();
            foreach (Room room in rooms)
            {
                var sys1 = room.System1;
                var sys2 = room.System2;
                var sys3 = room.System3;
                var sys4 = room.System4;
                var sys5 = room.System5;
                var sys6 = room.System6;
                var sys7 = room.System7;
                var sys8 = room.System8;
                var sys9 = room.System9;
                var sys10 = room.System10;

                if (sys1!="-")
                {
                    systems.Add(sys1);
                }

                if (sys2!="-")
                {
                    systems.Add(sys2);
                }
                if (sys3!="-")
                {
                    systems.Add(sys3);
                }
                if (sys4!="-")
                {
                    systems.Add(sys4);
                }
                if (sys5!="-")
                {
                    systems.Add(sys5);
                }
                if (sys6!="-")
                {
                    systems.Add(sys6);
                }
                if (sys7!="-")
                {
                    systems.Add(sys7);
                }
                if (sys8!="-")
                {
                    systems.Add(sys8);
                }
                if (sys9!="-")
                {
                    systems.Add(sys9);
                }
                if (sys10!="-")
                {
                    systems.Add(sys10);
                }
            }
            var filteredsystems =systems.Distinct().ToList();*/
            MessageBox.Show(groupedrooms.ToString());
        }
    }
}
