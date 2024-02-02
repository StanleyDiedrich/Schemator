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

            List<SubElements> systems = new List<SubElements>();


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
                                double y = floorval * 4000 / 304.8+1000/304.8;
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
                                        Parameter roomname = fI.LookupParameter("Наименование помещения");
                                        Parameter roomnumber = fI.LookupParameter("Номер помещения");
                                        Parameter roomcategory = fI.LookupParameter("Категория");
                                        Parameter airterminal = fI.LookupParameter("Количество решеток");
                                        Parameter sys1 = fI.LookupParameter("Номер системы 01");
                                        Parameter sys2 = fI.LookupParameter("Номер системы 02");
                                        Parameter sys3 = fI.LookupParameter("Номер системы 03");
                                        Parameter sys4 = fI.LookupParameter("Номер системы 04");
                                        Parameter sys5 = fI.LookupParameter("Номер системы 05");
                                        Parameter sys6 = fI.LookupParameter("Номер системы 06");
                                        Parameter sys7 = fI.LookupParameter("Номер системы 07");
                                        Parameter sys8 = fI.LookupParameter("Номер системы 08");
                                        Parameter sys9 = fI.LookupParameter("Номер системы 09");
                                        Parameter sys10 = fI.LookupParameter("Номер системы 10");


                                        roomname.Set(item.RoomName);
                                        roomnumber.Set(item.RoomNummer);
                                        roomcategory.Set(item.Category);
                                        sys1.Set(item.System1);
                                        sys2.Set(item.System2);
                                        sys3.Set(item.System3);
                                        sys4.Set(item.System4);
                                        sys5.Set(item.System5);
                                        sys6.Set(item.System6);
                                        sys7.Set(item.System7);
                                        sys8.Set(item.System8);
                                        sys9.Set(item.System9);
                                        sys10.Set(item.System10);


                                       
                                        int count = 0;

                                        string s1 = item.System1;
                                        string s2 = item.System2;
                                        string s3 = item.System3;
                                        string s4 = item.System4;
                                        string s5 = item.System5;
                                        string s6 = item.System6;
                                        string s7 = item.System7;
                                        string s8 = item.System8;
                                        string s9 = item.System9;
                                        string s10 = item.System10;
                                        List<string> list = new List<string>()
                                        {
                                            s1,
                                            s2,
                                            s3,
                                            s4,
                                            s5,
                                            s6,
                                            s7,
                                            s8,
                                            s9,
                                            s10

                                        };
                                        foreach (var sys in list)
                                        {
                                            if (sys!="-"  )
                                            {
                                                count++;
                                            }
                                           
                                        }
                                        airterminal.Set(count);
                                        Parameter at1 = fI.LookupParameter("Решетка 01");
                                        Parameter at2 = fI.LookupParameter("Решетка 02");
                                        Parameter at3 = fI.LookupParameter("Решетка 03");
                                        Parameter at4 = fI.LookupParameter("Решетка 04");
                                        Parameter at5 = fI.LookupParameter("Решетка 05");
                                        Parameter at6 = fI.LookupParameter("Решетка 06");
                                        Parameter at7 = fI.LookupParameter("Решетка 07");
                                        Parameter at8 = fI.LookupParameter("Решетка 08");
                                        Parameter at9 = fI.LookupParameter("Решетка 09");
                                        Parameter at10 = fI.LookupParameter("Решетка 10");
                                        ElementId supplyterminal = new ElementId(257206);
                                        ElementId exhaustterminal = new ElementId(257205);
                                        ElementId localexterminal = new ElementId(257207);
                                        if (s1.StartsWith("П"))
                                        {
                                            at1.Set(supplyterminal);
                                        }
                                        else if (s1.StartsWith("В"))
                                        {
                                            at1.Set(exhaustterminal);
                                        }
                                        else if (s1.StartsWith("М"))
                                        {
                                            at1.Set(localexterminal);
                                        }
                                        

                                        if (s2.StartsWith("П"))
                                        {
                                            at2.Set(supplyterminal);
                                        }
                                        else if (s2.StartsWith("В"))
                                        {
                                            at2.Set(exhaustterminal);
                                        }
                                        else if (s2.StartsWith("М"))
                                        {
                                            at2.Set(localexterminal);
                                        }
                                        

                                        if (s3.StartsWith("П"))
                                        {
                                            at3.Set(supplyterminal);
                                        }
                                        else if (s3.StartsWith("В"))
                                        {
                                            at3.Set(exhaustterminal);
                                        }
                                        else if (s3.StartsWith("М"))
                                        {
                                            at3.Set(localexterminal);
                                        }
                                       

                                        if (s4.StartsWith("П"))
                                        {
                                            at4.Set(supplyterminal);
                                        }
                                        else if (s4.StartsWith("В"))
                                        {
                                            at4.Set(exhaustterminal);
                                        }
                                        else if (s4.StartsWith("М"))
                                        {
                                            at4.Set(localexterminal);
                                        }
                                       

                                        if (s5.StartsWith("П"))
                                        {
                                            at5.Set(supplyterminal);
                                        }
                                        else if (s5.StartsWith("В"))
                                        {
                                            at5.Set(exhaustterminal);
                                        }
                                        else if (s5.StartsWith("М"))
                                        {
                                            at5.Set(localexterminal);
                                        }
                                        


                                        if (s6.StartsWith("П"))
                                        {
                                            at6.Set(supplyterminal);
                                        }
                                        else if (s6.StartsWith("В"))
                                        {
                                            at6.Set(exhaustterminal);
                                        }
                                        else if (s6.StartsWith("М"))
                                        {
                                            at6.Set(localexterminal);
                                        }
                                        

                                        if (s7.StartsWith("П"))
                                        {
                                            at7.Set(supplyterminal);
                                        }
                                        else if (s7.StartsWith("В"))
                                        {
                                            at7.Set(exhaustterminal);
                                        }
                                        else if (s7.StartsWith("М"))
                                        {
                                            at7.Set(localexterminal);
                                        }
                                       

                                        if (s8.StartsWith("П"))
                                        {
                                            at8.Set(supplyterminal);
                                        }
                                        else if (s8.StartsWith("В"))
                                        {
                                            at8.Set(exhaustterminal);
                                        }
                                        else if (s8.StartsWith("М"))
                                        {
                                            at8.Set(localexterminal);
                                        }
                                        

                                        if (s9.StartsWith("П"))
                                        {
                                            at9.Set(supplyterminal);
                                        }
                                        else if (s9.StartsWith("В"))
                                        {
                                            at9.Set(exhaustterminal);
                                        }
                                        else if (s9.StartsWith("М"))
                                        {
                                            at9.Set(localexterminal);
                                        }
                                       

                                        if (s10.StartsWith("П"))
                                        {
                                            at10.Set(supplyterminal);
                                        }
                                        else if (s10.StartsWith("В"))
                                        {
                                            at10.Set(exhaustterminal);
                                        }
                                        else if (s10.StartsWith("М"))
                                        {
                                            at10.Set(localexterminal);
                                        }

                                        
                                        var subElements = fI.GetSubComponentIds();
                                        foreach ( var subElement in subElements )
                                        {
                                            if ( subElement != null )
                                            {
                                                var el= doc.GetElement(subElement);
                                                LocationPoint locpoint = el.Location as LocationPoint;
                                                XYZ loc = new XYZ(locpoint.Point.X, locpoint.Point.Y, locpoint.Point.Z);
                                                SubElements subelement = new SubElements(el.Id, el.LookupParameter("Система 1").AsString(), el.LookupParameter("Система 2").AsString(),
                                                    el.LookupParameter("Система 3").AsString(), el.LookupParameter("Система 4").AsString(),
                                                    el.LookupParameter("Система 5").AsString(), el.LookupParameter("Система 6").AsString(),
                                                    el.LookupParameter("Система 7").AsString(), el.LookupParameter("Система 8").AsString(),
                                                    el.LookupParameter("Система 9").AsString(),
                                                    el.LookupParameter("Система 10").AsString(), loc );
                                                systems.Add(subelement);
                                            }
                                        }

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
            var def 
            var collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            var familyInstances = collector.OfClass(typeof(FamilyInstance));
            foreach (var anElem in familyInstances)
            {
                if (anElem is FamilyInstance)
                {
                    FamilyInstance aFamilyInst = anElem as FamilyInstance;
                    // we need to skip nested family instances 
                    // since we already get them as per below
                    if (aFamilyInst.SuperComponent == null)
                    {
                        // this is a family that is a root family
                        // ie might have nested families 
                        // but is not a nested one
                       
                        if (subElements.Count == 0)
                        {
                            // no nested families
                            System.Diagnostics.Debug.WriteLine(aFamilyInst.Name + " has no nested families");
                        }
                        else
                        {
                            // has nested families
                            foreach (var aSubElemId in subElements)
                            {
                                var aSubElem = doc.GetElement(aSubElemId);
                                if (aSubElem is FamilyInstance)
                                {
                                    System.Diagnostics.Debug.WriteLine(aSubElem.Name + " is a nested family of " + aFamilyInst.Name);
                                }
                            }
                        }
                    }
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





