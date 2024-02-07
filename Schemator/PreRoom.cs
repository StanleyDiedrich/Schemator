using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Schemator
{
    public class PreRoom
    {
        public string RoomXlId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public string Section {  get; set; }
        public int Floor {  get; set; }
        public string System1 { get; set; }
        public string System2 { get; set; }
        public string System3 { get; set; }
        public string System4 { get; set; }
        public string System5 { get; set; }
        public string System6 { get; set; }
        public string System7 { get; set; }
        public string System8 { get; set; }
        public string System9 { get; set; }
        public string System10 { get; set; }
        public XYZ Location { get; set; }

        public PreRoom(string roomId, string name, string number, string category, string section,   int floor, string sys1, string sys2, string sys3, string sys4, string sys5, string sys6, string sys7, string sys8, string sys9, string sys10)
        {
            RoomXlId = roomId;
            Name = name;
            Number = number;
            Category = category;
            Section = section;
            Floor = floor;
            System1 = sys1;
            System2 = sys2;
            System3 = sys3;
            System4 = sys4;
            System5 = sys5;
            System6 = sys6;
            System7 = sys7;
            System8 = sys8;
            System9 = sys9;
            System10 = sys10;
            
        }
    }
}
