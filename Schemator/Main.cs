using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
namespace Schemator
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Main:IExternalCommand
    {
        static AddInId AddInId = new AddInId(new Guid("8F33A58A-A6A1-42E1-94B3-B7B65E642670"));
        private SelectedCsv SelectedCsv {  get; set; }
       
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SelectedCsv = new SelectedCsv();
           Uontrol window = new Uontrol(SelectedCsv);
            window.ShowDialog();
            var uiDocument = commandData.Application.ActiveUIDocument;
            var doc = uiDocument.Document;
            var csv = SelectedCsv.csvfile;
            var groupedrooms = SelectedCsv.rooms;
            
            foreach ( var rooms in groupedrooms )
            {
                foreach ( var room in rooms )
                {

                }
            }


           // TaskDialog.Show("ReadCsv",groupedrooms.ToString());

            return Result.Succeeded;
        }

        
    }
}
