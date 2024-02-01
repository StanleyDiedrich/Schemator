using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Schemator
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        static AddInId AddInId = new AddInId(new Guid("8F33A58A-A6A1-42E1-94B3-B7B65E642670"));
        private SelectedCsv SelectedCsv { get; set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SelectedCsv = new SelectedCsv();
            Uontrol window = new Uontrol(SelectedCsv);
            window.ShowDialog();
            var uiDocument = commandData.Application.ActiveUIDocument;
            var doc = uiDocument.Document;
            var csv = SelectedCsv.csvfile;
            var groupedrooms = SelectedCsv.rooms;




            FilteredElementCollector famtype = new FilteredElementCollector(doc).OfClass(typeof(Family));
            string FamilyName = "MARKS_Room_Stage_P##MARKS_Помещение_стадия_П1";
            Family family = famtype.FirstOrDefault<Element>(el => el.Name.Equals(FamilyName)) as Family;
            ISet<ElementId> elementSet = family.GetFamilySymbolIds();
            FamilySymbol familyType = doc.GetElement(elementSet.First()) as FamilySymbol;




            double XX = 0;
            foreach (var group in groupedrooms)
            {

                double x = 0;

                var fgroup = group.GroupBy(g => g.System1).ToList();
                foreach (var room in fgroup)
                {
                    foreach (var item in room)
                    {
                        if (item != null)
                        {
                            if (item.System1 != "-" || item.System2 != "-")
                            {
                                string rfloor = item.Floor;
                                int floorval;
                                if (rfloor == "-" || rfloor == null || rfloor == "")
                                {
                                    continue;
                                }
                                else
                                {
                                    floorval = Convert.ToInt32(rfloor.Select(dx => dx).Where(dx => char.IsDigit(dx)).First().ToString());
                                }
                                double y = floorval * 4000 / 304.8;
                                XYZ xYZ = new XYZ(XX + x, y, 0.0);
                                XYZ coordinate = xYZ;
                                x += 2500 / 304.8;
                                using (Transaction t = new Transaction(doc, "CreateScheme"))
                                {
                                    t.Start();

                                    if (!familyType.IsActive)
                                    {
                                        familyType.Activate();
                                    }


                                    try
                                    {
                                       
                                        FamilyInstance fI = doc.Create.NewFamilyInstance(coordinate, familyType, uiDocument.ActiveView);
                                        Parameter sys1 = fI.LookupParameter("Номер системы 01");
                                        sys1.Set(item.System1);
                                       t.Commit();

                                    }

                                    catch
                                    {
                                        t.RollBack();
                                    }
                                }
                            }

                        }
                    }



                    XX += 1000;
                }
                
            }
            return Result.Succeeded;
        }
    }
}
           
           


            /*foreach (var rooms in groupedrooms)
            {
                double X = 0;
                double Y = 0;
                double x = 0;
                double y = 0;

                foreach (var room in rooms)
                {

                    if (room != null)
                    {
                        string floor = room.Floor;
                        int floorval;
                        if (floor == "-" || floor == null || floor == "")
                        { 
                            continue;
                        }

                        else
                        {
                            List<string, List<Room> floorroom = new List<Room>();
                            try
                            {

                                floorval = Convert.ToInt32(floor.Select(dx => dx).Where(dx => char.IsDigit(dx)).First().ToString());
                                // floorval = int.Parse(floor);
                                x = X;
                                y = Y;
                                y = floorval * 4000 / 304.8 + y;
                                XYZ Point = new XYZ(x, y, 0);
                                list.Add(Point);

                            }

                            catch
                            {
                                //TaskDialog.Show("Revit pizdec", $"floor{floor}");
                                

                               
                                */
                                 /*  * *using (Transaction t = new Transaction(doc, "CreateScheme"))
                                {
                                    t.Start();
                                    if (!familyType.IsActive)
                                    {
                                        familyType.Activate();
                                    }

                                  
                                    try
                                    {
                                        FamilyInstance fI = doc.Create.NewFamilyInstance(Point, familyType, uiDocument.ActiveView);
                                        t.Commit();
                                        
                                    }

                                    catch
                                    {
                                       t.RollBack();
                                    }*/
                                   



                
          
    
/*string roomname = room.RoomName;
string roomnumber = room.RoomNummer;
string category = room.Category;
string floor = room.Floor;
string sys1 = room.System1;
string sys2 = room.System2;
string sys3= room.System3;
string sys4= room.System4;
string sys5= room.System5;
string sys6= room.System6;
string sys7= room.System7;
string sys8= room.System8;
string sys9 = room.System9;
string sys10 = room.System10;*/
/*  List<string> syslist = new List<string>() { sys1, sys2, sys3, sys4, sys5, sys6, sys7, sys8, sys9, sys10 };
  var count = syslist.Select(r => r).Where(r=>r!="-").Count();*/















// TaskDialog.Show("ReadCsv",groupedrooms.ToString());





