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
using System.Data.Entity;
using Common;
using Report.Model;
using Report.View;

namespace Report
{
    /// <summary>
    /// QueryVerWasteData.xaml 的交互逻辑
    /// </summary>
    public partial class QueryVerWasteData : Page
    {
        Model1 db = new Model1();

        List<D_VerWasteCode> queryUser=new List<D_VerWasteCode>();
        public QueryVerWasteData()
        {
            InitializeComponent();
            //D_VerWasteCode d = new D_VerWasteCode();
            //d.CheckerNo = "123";
            //db.D_VerWasteCode.Add(d);
            //db.SaveChanges();

        }
        public void page_load(object sender, RoutedEventArgs e)
        {
            var varErrType = (from p in db.DIC_ErrorType select p.errname).Distinct().ToList<string>();
            varErrType.Add("全部");
            comErrType.ItemsSource = varErrType;

            var varProcessType = (from p in db.DIC_ProcessType select p.ProcessName).ToList<string>();
            varProcessType.Add("全部");
            comProcessType.ItemsSource = varProcessType;


        }
        private void query_Click(object sender, RoutedEventArgs e)
        {
            query_data();

        }

        private void query_data()
        {
            var processName = comProcessType.Text;
            var errType = comErrType.Text;

            string a = dpStartDate.Text;
            var stDate = DateTime.Parse(dpStartDate.Text).ToString("yyyy-MM-dd");
            var endDate = DateTime.Parse(dpEndDate.Text).ToString("yyyy-MM-dd");

            //string sql = "select * from d_VerWasteCode  ";

            string sql = "select * from d_VerWasteCode where productionDate between '"+stDate+"' and '"+endDate+"'";
            
            if (processName != ""&& processName!="全部")
            {
                sql += " and processType='" + processName + "'";
            }

            if (errType != ""&& errType != "全部")
            {
                sql += " and errType='" + errType + "'";
            }

            try
            {
                queryUser = db.Database.SqlQuery<D_VerWasteCode>(sql).ToList<D_VerWasteCode>();
                dataGrid.ItemsSource = queryUser;
                LogHelper.WriteLog(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(sql, ex);
            }
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var index = dataGrid.SelectedIndex;

            if (MessageBox.Show("确定删除该数据", "提示",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    int id = queryUser[dataGrid.SelectedIndex].id;
                    var to_delete = (from p in db.D_VerWasteCode
                                     where p.id == id
                                     select p).Single();
                    db.D_VerWasteCode.Remove(to_delete);
                    db.SaveChanges();
                    LogHelper.WriteLog("删除数据成功" + queryUser[dataGrid.SelectedIndex].code);

                    query_data();

                }
                catch(Exception ex)
                {
                    LogHelper.WriteLog("删除数据失败", ex);
                }
                
            }

        }
        
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            
            var index = dataGrid.SelectedIndex;
            int id = queryUser[dataGrid.SelectedIndex].id;
         
            var to_modify = db.D_VerWasteCode.Where(p => p.id == id).FirstOrDefault<D_VerWasteCode>();
            
            if(to_modify!=null)
            {
                LogHelper.WriteLog("修改" + to_modify.ToString());
                to_modify.ProcessType = queryUser[dataGrid.SelectedIndex].ProcessType;
                to_modify.MachineNo = queryUser[dataGrid.SelectedIndex].MachineNo;
                to_modify.ProductionDate = queryUser[dataGrid.SelectedIndex].ProductionDate;
                to_modify.workUnit = queryUser[dataGrid.SelectedIndex].workUnit;
                to_modify.errType = queryUser[dataGrid.SelectedIndex].errType;
                to_modify.code = queryUser[dataGrid.SelectedIndex].code;
                to_modify.CheckDate = queryUser[dataGrid.SelectedIndex].CheckDate;
                to_modify.CheckerNo = queryUser[dataGrid.SelectedIndex].CheckerNo;
               
                db.SaveChanges();
                LogHelper.WriteLog("修改为" + to_modify.ToString());
                Console.WriteLine(to_modify.ToString());

            }
            else
            {
                var to_add = queryUser[dataGrid.SelectedIndex];
                db.D_VerWasteCode.Add(to_add);
                db.SaveChanges();
                LogHelper.WriteLog("增加："+to_add.ToString());
            }


        }
        

        private void update_Click(object sender, RoutedEventArgs e)
        {
                        
            var index = dataGrid.SelectedIndex;

            int id = queryUser[dataGrid.SelectedIndex].id;
         
            EditWaste window = new EditWaste(id);
            window.ShowDialog();

          
            try
            {
                query_data();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            var index = dataGrid.SelectedIndex;

            EditWaste window = new EditWaste(-1);
            window.ShowDialog();

            try
            {
                query_data();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }

        }

        private void comProcessType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comProcessType.SelectedItem.ToString()=="全部")
            {
                var varErrType = (from p in db.DIC_ErrorType select p.errname).Distinct().ToList<string>();
                varErrType.Add("全部");
                comErrType.ItemsSource = varErrType;
            }
            else
            {
                var varErrType = (from p in db.DIC_ErrorType  join pt in db.DIC_ProcessType  on p.processtype equals pt.ProcessType where pt.ProcessName == comProcessType.SelectedItem.ToString() select p.errname).Distinct().ToList<string>();
                varErrType.Add("全部");
                comErrType.ItemsSource = varErrType;
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var bindingdata = dataGrid.ItemsSource;
            if(bindingdata!=null)
            {
                var filter_data = ((List<D_VerWasteCode>)bindingdata).Where(delegate (D_VerWasteCode s) {
                    if(s.code==null||s.code.Length<2)
                    {
                        return false;
                    }
                    else
                    {
                        return s.code.Substring(s.code.Length - 2, 2) == "00" || s.code.Substring(s.code.Length - 2, 2) == "01"
                   || s.code.Substring(s.code.Length - 2, 2) == "02" || s.code.Substring(s.code.Length - 2, 2) == "49"
                   || s.code.Substring(s.code.Length - 2, 2) == "51" || s.code.Substring(s.code.Length - 2, 2) == "52"
                   || s.code.Substring(s.code.Length - 2, 2) == "99";
                    }
                   
                });
                dataGrid.ItemsSource = filter_data;
            }
        }
    }
}
