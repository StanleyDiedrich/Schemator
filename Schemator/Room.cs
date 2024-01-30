using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schemator
{
    public  class Room
    {
        public string RoomId;
        public string RoomXlId;
        public string Category;
        public string Section;
        public string RoomNummer;
        public string ClassRoomName;
        public string RoomName;
        public string Floor;
        public string   TermQuantity;
        public string System1;
        public string System2;
        public string System3;
        public string System4;
        public string System5;
        public string System6;
        public string System7;
        public string System8;
        public string System9;
        public string System10;
        
        public Room( string roomxlId, string category,string section,string roomNummer, string classroomname,string roomname, string floor, string system1,
            string system2, string system3, string system4, string system5, string system6, string system7, string system8,
            string system9, string system10)
        {
          
            this.RoomXlId = roomxlId;
            this.Category = category;
            this.Section = section;
            this.RoomNummer = roomNummer;
            this.RoomName = roomname;
            this.ClassRoomName = classroomname;
            this.Floor = floor;
            this.System1 = system1;
            this.System2 = system2;
            this.System3 = system3;
            this.System4 = system4;
            this.System5 = system5;
            this.System6 = system6;
            this.System7 = system7;
            this.System8 = system8;
            this.System9 = system9;
            this.System10 = system10;
        }


    }
}
