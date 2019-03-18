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
using System.Data;
using FastReport;
using Common;
using Report.Model;
using Report.Util;
using Microsoft.International.Converters;
using Microsoft.International.Converters.PinYinConverter;
using System.Collections.ObjectModel;

namespace Report
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    /// 
    public class RateView
    {
        public string errRate { get; set; }
        public string workunit { get; set; }
        public string processType { get; set; }
    }

    public partial class ReportWindow : Page
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        public List<DIC_Machines> dic_machine = new List<DIC_Machines>();
        public List<DIC_ProcessType> dic_processType = new List<DIC_ProcessType>();
        public List<D_UserInfo> user_info = new List<D_UserInfo>();
        public List<string> process_name = new List<string>();
        public List<string> user_name = new List<string>();
        public FastReport.Report report = new FastReport.Report();


        public void page_load(object sender, RoutedEventArgs e)
        {
            try
            {
                Model1 db = new Model1();
                dic_machine = (from p in db.DIC_Machines select p).ToList<DIC_Machines>();
                dic_processType = (from p in db.DIC_ProcessType select p).ToList<DIC_ProcessType>();
                var machine_name = (from p in db.DIC_Machines select p.machinename).ToList<string>();
                user_info = (from p in db.D_UserInfo select p).ToList<D_UserInfo>();
                
                foreach(DIC_ProcessType dic in dic_processType)
                {
                    process_name.Add(dic.ProcessName);
                }

                foreach(D_UserInfo user in user_info)
                {
                    user_name.Add(user.username);
                }
                
                comMachineName.ItemsSource = machine_name;

               
                comProcessName.ItemsSource = process_name;

                comMachineLeader.ItemsSource = user_name;
            }
            catch(Exception ee)
            {
                LogHelper.WriteLog("", ee);
            }
          
            

        }
        private void GenReport_Click(object sender, RoutedEventArgs e)
        {
            //Common.ExportToExcel<RateView, List<RateView>> exporttoexcel =
            //new Common.ExportToExcel<RateView, List<RateView>>();
            ////实例化exporttoexcel对象
            //exporttoexcel.DataToPrint = (List<RateView>)dataGrid.ItemsSource;


            // create an instance of pdf export filter
            FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
            // show the export options dialog and do the export
            //if (export.ShowDialog())
            //    report.Export(export, "result.pdf");
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "报表"; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "pdf (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            string filename = string.Empty;

            // Process save file dialog box results
            if (result == true)
            {
                report.Export(export, dlg.FileName);
                LogHelper.WriteLog("导出报表成功:" + dlg.FileName);
            }
            else
            {
                return;
            }


        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            CodeService service = new CodeService(this);
            service.getReport();
            
          //  Model1 db = new Model1();
          //  String sql;

          //  var machine_name = comMachineName.Text.ToString();
          //  var process_name = comProcessName.Text.ToString();
          //  var machineLeader_name = comMachineLeader.Text.ToString();

          //  if(machineLeader_name==""||process_name=="")
          //  {

          //      MessageBox.Show("工序和机长不能为空");
          //      return;
          //  }

          //  var stDate = DateTime.Parse(dpStartDate.Text).ToString("yyyy-MM-dd");
          //  var endDate = DateTime.Parse(dpEndDate.Text).ToString("yyyy-MM-dd");

          //  DateTime date_start = DateTime.Parse(dpStartDate.Text);
          //  DateTime date_end = DateTime.Parse(dpEndDate.Text);
          //  string machineNO="";
          //  var machine_no = dic_machine.Where(x => x.machinename == machine_name).Select(x => x.machineno);
          //  var workunit = user_info.Where(x => x.username == machineLeader_name).Select(x => x.loginname).First();
          //  if (machine_no.Count()>0)
          //  {
          //      machineNO = machine_no.ToList().ElementAt(0);
          //  }

          //  var processName= (from p in db.DIC_Machines where p.machinename==machine_name join  p1 in db.DIC_ProcessType on p.processtype equals p1.ProcessType   select p1.ProcessName  ).ToList<string>();

          //  FastReport.Preview.PreviewControl prew = new FastReport.Preview.PreviewControl();
          //  report.Load(@"report.frx");

          //  report.Parameters[0].Value = machine_name;
          //  //if(processName.Count>0)
          //     report.Parameters[1].Value = process_name;
          //  report.Parameters[2].Value = stDate +"--" +endDate;

          //  var boxCollect= (from p in db.D_BoxCollectResult where p.reportdate >= date_start && p.reportdate <= date_end && p.WorkUnit == workunit orderby p.StartCode select p).ToList<D_BoxCollectResult>();
          //  var WasteCode = (from p in db.D_VerWasteCode where p.ProductionDate >= date_start && p.ProductionDate <= date_end && p.ProcessType == process_name &&p.workUnit==workunit orderby p.codeRange select p).ToList<D_VerWasteCode>();

          ////  var verWasteCode = (from p in db.D_VerWasteCode where p.ProductionDate >= date_start && p.ProductionDate <= date_end && p.MachineNo== machineNO group p by new { p.codeRange, p.Note }   into g select new { codeRange = g.Key.codeRange, note=g.Key.Note,_count = g.Count() }).ToList();
          //  DataSet ds = new DataSet();
          //  DataTable table = new DataTable();
          //  table.TableName = "rateView"; // 一定要设置表名称,rateView与实际表关联
          //  DataTable table_un_commite = new DataTable();
          //  table_un_commite.TableName = "unCommit"; // 一定要设置表名称,rateView与实际表关联

          //  ds.Tables.Add(table);
          //  ds.Tables.Add(table_un_commite);

          //  // 添加表中的列
          //  table.Columns.Add("Code", typeof(string));
          //  table.Columns.Add("ErrCode", typeof(string));
          //  table.Columns.Add("Count", typeof(int));
          //  table.Columns.Add("errCount", typeof(int));
          //  table.Columns.Add("rate", typeof(string));
          //  //
          //  table_un_commite.Columns.Add("CodeRange", typeof(string));
          //  table_un_commite.Columns.Add("Count", typeof(int));

          //  // 获取统计数据
          //  int total_count = 0;
          //  int total_err_count = 0;
          //  Dictionary<string, int> dic_waste_code = new Dictionary<string, int>();

          //  //将连续数据合并
          //  Dictionary<string, int> dic_waste_code_continue = new Dictionary<string, int>();
          //  //未提交的数据，即不连续的数据
          //  List<string> lst_uncommite_code = new List<string>();

          //  //该时间段内起始号
          //  string min_code = "";
          //  //该事件段内终止号
          //  string max_code = "";

          //  if(boxCollect.Count()>0)
          //  {
          //      min_code = boxCollect[0].StartCode;
          //      max_code = boxCollect[boxCollect.Count() - 1].EndCode;
          //  }

          //  foreach (D_VerWasteCode data in WasteCode)
          //  {
          //      if(dic_waste_code.ContainsKey(data.codeRange))
          //      {
          //          dic_waste_code[data.codeRange]++;
          //      }
          //      else
          //      {
          //          dic_waste_code.Add(data.codeRange, 0);
          //          if(data.Note!="0")
          //          {
          //              dic_waste_code[data.codeRange]++;
          //          }
          //      }
          //  }

          //  string start_code = "";
          //  string end_code = "";
          //  int code_err_count = 0;
          //  if(dic_waste_code.Count()>0)
          //  {
          //      if (dic_waste_code.Keys.ElementAt(0) != min_code&&min_code!="")
          //      {
          //          string uncommit = min_code + "-" + dic_waste_code.Keys.ElementAt(0);
          //          lst_uncommite_code.Add(uncommit);
          //      }
          //  }
          //  foreach (string key in dic_waste_code.Keys)
          //  {
                
          //      string[] s = key.Split('-');
          //      if (s.Count() == 2)
          //      {
          //          int start_code_tmp = int.Parse(s[0].Substring(2, s[0].Length-2));
          //          if(end_code.Length>2)
          //          {
          //              int end_code_tmp = int.Parse(end_code.Substring(2, end_code.Length-2));
          //              if (start_code_tmp- end_code_tmp==1)
          //              {
          //                  end_code = s[1];
          //                  code_err_count += dic_waste_code[key];
          //              }
          //              else
          //              {
          //                  string range = start_code + "-" + end_code;
          //                  if(range!="")
          //                  {
          //                      dic_waste_code_continue.Add(range, code_err_count);

          //                  }
          //                  if(start_code_tmp - end_code_tmp > 1)
          //                  {
          //                      string head = s[0].Substring(0, 2);
          //                      string uncommit = head + (end_code_tmp + 1).ToString() + "-" + head + (start_code_tmp - 1).ToString();
          //                      lst_uncommite_code.Add(uncommit);
          //                  }
          //                  start_code = s[0] ;
          //                  end_code = s[1];
          //                  code_err_count = dic_waste_code[key];
          //              }
          //          }
          //          else
          //          {
          //              start_code = s[0];
          //              end_code = s[1];
          //              code_err_count = dic_waste_code[key];
          //          }
          //      }
          //  }

          //  string range1 = start_code + "-" + end_code;
          //  if (range1 != "")
          //  {
          //      dic_waste_code_continue.Add(range1, code_err_count);
          //  }

          //  if(end_code!=max_code&&end_code!=""&&max_code!="")
          //  {
                
          //      string uncommit = end_code + "-"+ max_code;
          //      lst_uncommite_code.Add(uncommit);
          //  }

          //  foreach (string key in dic_waste_code_continue.Keys)
          //  //for (int i=0;i<verWasteCode.Count();i++)
          //  {
          //      DataRow row = table.NewRow();

          //      row["Code"] = key;

          //      row["ErrCode"] = "";

          //      int code_count = 0;
          //      string []s=key.Split('-');
          //      if(s.Count()==2)
          //      {
          //          try
          //          {
          //              code_count = int.Parse(s[1].Substring(2, s[1].Length - 2)) - int.Parse(s[0].Substring(2, s[0].Length - 2))+1;

          //          }
          //          catch(Exception ex)
          //          {
          //              LogHelper.WriteLog("", ex);
          //          }
          //      }
          //      row["Count"] = code_count;
          //      row["errCount"] = dic_waste_code_continue[key];
          //      total_count += code_count;
          //      total_err_count += dic_waste_code_continue[key];

          //      try
          //      {
          //          row["rate"] = (double.Parse(row["errCount"].ToString()) * 100 / code_count).ToString("f4") + "%";

          //      }
          //      catch(Exception ex)
          //      {
          //          LogHelper.WriteLog("", ex);
          //      }
          //      table.Rows.Add(row);
          //  }

          //  foreach(string key in lst_uncommite_code)
          //  {
          //      int code_count = 0;
          //      string[] s = key.Split('-');
          //      if (s.Count() == 2)
          //      {
          //          try
          //          {
          //              code_count = int.Parse(s[1].Substring(2, s[1].Length - 2)) - int.Parse(s[0].Substring(2, s[0].Length - 2)) + 1;

          //          }
          //          catch (Exception ex)
          //          {
          //              LogHelper.WriteLog("", ex);
          //          }
          //      }
          //      DataRow row = table_un_commite.NewRow();
          //      row["CodeRange"] = key;
          //      row["Count"] = code_count;
          //      table_un_commite.Rows.Add(row);
          //  }


          //  double total_rate = 0.0;
          //  try
          //  {
          //      total_rate = (double)total_err_count / total_count;
          //  }
          //  catch(Exception ex)
          //  {
          //      LogHelper.WriteLog("", ex);
          //  }

          //  report.Parameters[3].Value = (total_rate * 100).ToString("f4") + "%";

          //  report.RegisterData(ds);
            
          //  report.Preview = prew;
          //  report.Prepare();
          //  report.ShowPrepared();

          //  WinFormHost.Child = report.Preview;


//            sql = String.Format(@"SELECT LEFT(n.errnums*100.0/n.nums,5)+'%' as errRate,workunit,processType from 
//(SELECT a.nums,b.errnums,a.workunit,b.processType   from
//(select count(*) as nums, WorkUnit,processType from D_BoxCollectResult where RecordTime BETWEEN '2017-1-1' and '2017-1-30' GROUP BY WorkUnit,processType) a

//LEFT JOIN
//(select count(*) as errnums, WorkUnit,processType from D_ErrorCode where recordTime BETWEEN '2017-6-1' and '2017-6-30' GROUP BY WorkUnit,processType) b
//on a.workunit = b.WorkUnit) n");

            
//            var lsErrRate= db.Database.SqlQuery<RateView> (sql).ToList<RateView>();
//            dataGrid.ItemsSource = lsErrRate;


        }

        private void comProcessName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comProcessName.SelectedValue == null || comProcessName.SelectedValue == "")
                return;
            var process_name = comProcessName.SelectedValue;

            var process_type = dic_processType.Where(x => x.ProcessName == process_name).Select(x => x.ProcessType).FirstOrDefault();
            List<string> tmp = new List<string>();
            //user_info = (from p in db.D_UserInfo select p).ToList<D_UserInfo>();
            List<D_UserInfo> user = new List<D_UserInfo>();
            user = user_info.FindAll(delegate (D_UserInfo s) { return s.processtype == process_type; });

            foreach (D_UserInfo s in user)
            {
                tmp.Add(s.username);
            }
            comMachineLeader.ItemsSource = tmp;
        }

        private void comProcessName_KeyUp(object sender, KeyEventArgs e)
        {
            List<string> tmp = new List<string>();
            tmp = process_name.FindAll(delegate (string s) {
                if(s!=null){
                    return s.Contains(comProcessName.Text.Trim());
                }
                return false;
                
            });
            if(tmp!=null)
            {
                comProcessName.ItemsSource = tmp;
                comProcessName.IsDropDownOpen = true;
            }
      
        }

        private void comMachineLeader_KeyUp(object sender, KeyEventArgs e)
        {
            if (comMachineLeader.Text.Trim().Length <= 0)
            {

                return;
            }

            List<string> username = new List<string>();
            if (comProcessName.SelectedItem!=null&&comProcessName.SelectedItem.ToString() != "")
            {
                var process_name = comProcessName.Text.ToString();

                var process_type = dic_processType.Where(x => x.ProcessName == process_name).Select(x => x.ProcessType).First();
               
                //user_info = (from p in db.D_UserInfo select p).ToList<D_UserInfo>();
                List<D_UserInfo> user = new List<D_UserInfo>();
                user = user_info.FindAll(delegate (D_UserInfo s) { return s.processtype == process_type; });

                foreach (D_UserInfo s in user)
                {
                    username.Add(s.username);
                }
            }
            else
            {
                username = user_name;
            }
       


            List<string> tmp = new List<string>();
            tmp = username.FindAll(delegate (string s) {
                char[] chs= { ' '};
                StringBuilder str =new StringBuilder();
                foreach (char c in s.ToCharArray())
                {
                    if(ChineseChar.IsValidChar(c))
                    {
                        ChineseChar CC = new ChineseChar(c);
                        //将该汉字转化为拼音集合
                        ReadOnlyCollection<string> roc = CC.Pinyins;
                        //获取集合中第一个数据即为该汉字的拼音
                        chs = roc[0].ToLower().ToCharArray();
                        str.Append(chs[0]);
                        
                        
                    }
                }
                return str.ToString().IndexOf(comMachineLeader.Text.Trim())==0;
            });
            comMachineLeader.ItemsSource = tmp;
            comMachineLeader.IsDropDownOpen = true;


        }

        private void comMachineLeader_DropDownOpened(object sender, EventArgs e)
        {
            if (comMachineLeader.Text.Trim().Length <= 0)
            {

                if (comProcessName.Text == "")
                {
                    comMachineLeader.ItemsSource = user_name;
                    comMachineLeader.IsDropDownOpen = true;
                    return;

                }
                var process_name = comProcessName.Text.ToString();

                var process_type = dic_processType.Where(x => x.ProcessName == process_name).Select(x => x.ProcessType).FirstOrDefault();
                List<string> tmp = new List<string>();
                //user_info = (from p in db.D_UserInfo select p).ToList<D_UserInfo>();
                List<D_UserInfo> user = new List<D_UserInfo>();
                user = user_info.FindAll(delegate (D_UserInfo s) { return s.processtype == process_type; });

                foreach (D_UserInfo s in user)
                {
                    tmp.Add(s.username);
                }
                comMachineLeader.ItemsSource = tmp;

                comMachineLeader.IsDropDownOpen = true;
            }

        }
    }
}
