using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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




            List<PreRoom> rooms = new List<PreRoom>();
            foreach (var group in groupedrooms)
            {

                foreach (var element in group)
                {
                    if (element.RoomName==null || element.RoomName=="-" ||element.RoomName=="0"|| element.Floor=="-" || element.Floor==null)
                    { continue; }
                    else
                    {
                        string id = element.RoomId;
                        string name = element.RoomName;
                        string number = element.RoomNummer;
                        string category = element.Category;
                        string section = element.Section;
                        int floor = Convert.ToInt32(element.Floor.Select(dx => dx).Where(dx => char.IsDigit(dx)).First().ToString());
                        string sys1 = element.System1;
                        string sys2 = element.System2;
                        string sys3 = element.System3;
                        string sys4 = element.System4;
                        string sys5 = element.System5;
                        string sys6 = element.System6;
                        string sys7 = element.System7;
                        string sys8 = element.System8;
                        string sys9 = element.System9;
                        string sys10 = element.System10;

                        PreRoom proom = new PreRoom(id , name, number, category,section, floor, sys1, sys2, sys3, sys4, sys5, sys6, sys7, sys8, sys9, sys10);
                        rooms.Add(proom);
                    }
                    
                }
            }
            var orderedsystems = rooms.OrderBy(x=>x.Section).ThenBy(x => x.System1).ThenBy(x => x.System2).ThenBy(x => x.System3).ThenBy(x => x.System4);
            
            List<PreRoom> elevation = new List<PreRoom>();



            double X = 0;
            double Z = 0;
            foreach (var room in orderedsystems)
            {
                
                
                
               
                if (room.System1 == "-" || room.System2 == "-")
                { continue; }
                else
                {
                    double Y = room.Floor * 4500/304.8 + 500 / 304 / 8;
                    room.Location = new XYZ(X, Y, Z);
                    elevation.Add(room);
                    X += 2500 / 304.8;
                }
                   
                
               
                
            }

            foreach (var room in elevation)
            {
                
                    using (Transaction t = new Transaction(doc, "CreateScheme"))
                    {
                        t.Start();

                        if (!familyType.IsActive)
                        {
                            familyType.Activate();
                        }


                        try
                        {

                            FamilyInstance fI = doc.Create.NewFamilyInstance(room.Location, familyType, uiDocument.ActiveView);
                            Parameter roomid = fI.LookupParameter("MARKS_Пространство_ID");
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

                            roomid.Set(room.RoomXlId);
                            roomname.Set(room.Name);
                            roomnumber.Set(room.Number);
                            roomcategory.Set(room.Category);
                            sys1.Set(room.System1);
                            sys2.Set(room.System2);
                            sys3.Set(room.System3);
                            sys4.Set(room.System4);
                            sys5.Set(room.System5);
                            sys6.Set(room.System6);
                            sys7.Set(room.System7);
                            sys8.Set(room.System8);
                            sys9.Set(room.System9);
                            sys10.Set(room.System10);



                            int count = 0;

                            string s1 = room.System1;
                            string s2 = room.System2;
                            string s3 = room.System3;
                            string s4 = room.System4;
                            string s5 = room.System5;
                            string s6 = room.System6;
                            string s7 = room.System7;
                            string s8 = room.System8;
                            string s9 = room.System9;
                            string s10 = room.System10;
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
                            foreach (var sy in list)
                            {
                                if (sy != "-")
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


                            t.Commit();


                        }

                        catch
                        {
                            t.RollBack();
                        }

                    }
                




                
            }
            return Result.Succeeded;
        }
    }
}

           
           


           




















