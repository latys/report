using Report.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Report.View
{
    /// <summary>
    /// EditWaste.xaml 的交互逻辑
    /// </summary>
    public partial class EditWaste : Window
    {
        private D_VerWasteCode waste_code = null;
        public EditWaste()
        {
            InitializeComponent();
        }

        public EditWaste(D_VerWasteCode  wastecode)
        {
            InitializeComponent();
            waste_code =wastecode;
        }
        public EditWaste(int id)
        {
            InitializeComponent();
            if(id!=-1)
            {
                using (var db = new Model1())
                {
                    waste_code = (from p in db.D_VerWasteCode
                                  where p.id == id
                                  select p).SingleOrDefault();
                }
            }
       
        }


        private void btnPosApply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (waste_code != null)
            {

                using (var db = new Model1())
                {
                    var to_modify = (from p in db.D_VerWasteCode
                                     where p.id == waste_code.id
                                     select p).Single();

                    to_modify.code = tbcode.Text;
                    to_modify.MachineNo = tbMachineNo.Text;
                    to_modify.errType = tbErrType.Text;
                    to_modify.ProcessType = tbProcessType.Text;
                    to_modify.ProductionDate = DateTime.Parse(tbProductionTime.Text);
                    to_modify.workUnit = tbWorkUnit.Text;
                    to_modify.Note = tbNote.Text;
                    to_modify.CheckerNo = tbChecker.Text;
                    to_modify.CheckDate = DateTime.Parse(tbCheckTime.Text);
                    db.SaveChanges();

                }

            }
            else
            {
                using (var db = new Model1())
                {
                    D_VerWasteCode to_add = new D_VerWasteCode();

                    to_add.code = tbcode.Text;
                    to_add.MachineNo = tbMachineNo.Text;
                    to_add.errType = tbErrType.Text;
                    to_add.ProcessType = tbProcessType.Text;
                    to_add.ProductionDate = DateTime.Parse(tbProductionTime.Text);
                    to_add.workUnit = tbWorkUnit.Text;
                    to_add.Note = tbNote.Text;
                    to_add.CheckerNo = tbChecker.Text;
                    to_add.CheckDate = DateTime.Parse(tbCheckTime.Text);
                    db.D_VerWasteCode.Add(to_add);
                    db.SaveChanges();

                }
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(waste_code!=null)
            {
                tbcode.Text = waste_code.code;
                tbMachineNo.Text = waste_code.MachineNo;
                tbErrType.Text = waste_code.errType;
                tbProcessType.Text = waste_code.ProcessType;
                tbProductionTime.Text = waste_code.ProductionDate.ToString();
                tbWorkUnit.Text = waste_code.workUnit;
                tbNote.Text = waste_code.Note;
                tbChecker.Text = waste_code.CheckerNo;
                tbCheckTime.Text = waste_code.CheckDate.ToString();
            }
        }
    }
}
