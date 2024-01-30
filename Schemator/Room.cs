using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schemator
{
    public  class Room
    {
        string RoomId;
        string RoomXlId;
        string Category;
        string Section;
        string RoomNummer;
        string RoomName;
        string Floor;
        string   TermQuantity;
        string System1;
        string System2;
        string System3;
        string System4;
        string System5;
        string System6;
        string System7;
        string System8;
        string System9;
        string System10;
        
        public Room(string roomid, string roomxlId, string category,string section,string roomNummer, string roomname, string floor, string system1,
            string system2, string system3, string system4, string system5, string system6, string system7, string system8,
            string system9, string system10)
        {
            this.RoomId = roomid;
            this.RoomXlId = roomxlId;
            this.Category = category;
            this.Section = section;
            this.RoomNummer = roomNummer;
            this.RoomName = roomname;
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
