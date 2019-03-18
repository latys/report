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
using System.Globalization;

namespace Report.Util
{
    class CodeService
    {
        ReportWindow window = null;
        List<DIC_Machines> dic_machine = new List<DIC_Machines>();
        List<DIC_ProcessType> dic_processType = new List<DIC_ProcessType>();
        List<D_UserInfo> user_info = new List<D_UserInfo>();
        List<string> process_name = new List<string>();
        List<string> user_name = new List<string>();
        FastReport.Report report = new FastReport.Report();

        public  CodeService(ReportWindow window)
        {
            this.window = window;
            this.user_info = window.user_info;
            this.user_name = window.user_name;
            this.dic_machine = window.dic_machine;
            this.process_name = window.process_name;
            this.report = window.report;
        }
        public bool getReport()
        {


            if (window == null)
            {
                return false;
            }

            Model1 db = new Model1();
            String sql;

            var machine_name = window.comMachineName.Text.ToString();
            var process_name = window.comProcessName.Text.ToString();
            var machineLeader_name = window.comMachineLeader.Text.ToString();

            if (machineLeader_name == "" || process_name == "")
            {

                MessageBox.Show("工序和机长不能为空");
                return false;
            }

            var stDate = DateTime.Parse(window.dpStartDate.Text).ToString("yyyy-MM-dd");
            var endDate = DateTime.Parse(window.dpEndDate.Text).ToString("yyyy-MM-dd");

            //DateTime t1, t2;
            //t1 = DateTime.Parse("2018/2/1");
            //t2 = DateTime.Parse("2018/12/1");
            //if(t2>=t1)
            //{
            //    Console.WriteLine("t2>=t1");
            //}

            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            DateTime date_start = Convert.ToDateTime(DateTime.Parse(window.dpStartDate.Text).ToString("yyyy-MM-dd"));
            DateTime date_end = DateTime.Parse(window.dpEndDate.Text, dtFormat);
            string machineNO = "";
            var machine_no = dic_machine.Where(x => x.machinename == machine_name).Select(x => x.machineno);
            var workunit = user_info.Where(x => x.username == machineLeader_name).Select(x => x.loginname).First();
            if (machine_no.Count() > 0)
            {
                machineNO = machine_no.ToList().ElementAt(0);
            }

            var processName = (from p in db.DIC_Machines where p.machinename == machine_name join p1 in db.DIC_ProcessType on p.processtype equals p1.ProcessType select p1.ProcessName).ToList<string>();

            FastReport.Preview.PreviewControl prew = new FastReport.Preview.PreviewControl();
            report.Load(@"report.frx");

            report.Parameters[0].Value = machine_name;
            //if(processName.Count>0)
            report.Parameters[1].Value = process_name;
            report.Parameters[2].Value = stDate + "--" + endDate;
            report.Parameters[4].Value = machineLeader_name;

            var boxCollect = (from p in db.D_BoxCollectResult where p.reportdate >= date_start && p.reportdate <= date_end && p.WorkUnit == workunit orderby p.StartCode select p).ToList<D_BoxCollectResult>();
            var WasteCode = (from p in db.D_VerWasteCode where p.ProductionDate >= date_start && p.ProductionDate <= date_end && (p.ProcessType == process_name ||p.code=="")  orderby p.codeRange,p.code select p).ToList<D_VerWasteCode>();

            //  var verWasteCode = (from p in db.D_VerWasteCode where p.ProductionDate >= date_start && p.ProductionDate <= date_end && p.MachineNo== machineNO group p by new { p.codeRange, p.Note }   into g select new { codeRange = g.Key.codeRange, note=g.Key.Note,_count = g.Count() }).ToList();
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            table.TableName = "rateView"; // 一定要设置表名称,rateView与实际表关联
            DataTable table_un_commite = new DataTable();
            table_un_commite.TableName = "unCommit"; // 一定要设置表名称,rateView与实际表关联

            ds.Tables.Add(table);
            ds.Tables.Add(table_un_commite);

            // 添加表中的列
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("ErrCode", typeof(string));
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("errCount", typeof(int));
            table.Columns.Add("rate", typeof(string));
            //
            table_un_commite.Columns.Add("CodeRange", typeof(string));
            table_un_commite.Columns.Add("Count", typeof(int));

            // 获取统计数据
            int total_count = 0;
            int total_err_count = 0;
            Dictionary<string, int> dic_waste_code = new Dictionary<string, int>();
            Dictionary<string, int> dic_waste_code_boxcollect = new Dictionary<string, int>();


            //将连续数据合并
            Dictionary<string, int> dic_waste_code_continue = new Dictionary<string, int>();
            //未提交的数据，即不连续的数据
            List<string> lst_uncommite_code = new List<string>();

            //该时间段内起始号
            string min_code = "";
            //该事件段内终止号
            string max_code = "";

            if (boxCollect.Count() > 0)
            {
                min_code = boxCollect[0].head+boxCollect[0].StartCode;
                max_code = boxCollect[boxCollect.Count() - 1].head+boxCollect[boxCollect.Count() - 1].EndCode;
            }

            foreach (D_VerWasteCode data in WasteCode)
            {
                if (dic_waste_code.ContainsKey(data.codeRange))
                {
                    dic_waste_code[data.codeRange]++;
                }
                else
                {
                    dic_waste_code.Add(data.codeRange, 0);
                    if (data.Note != "0")
                    {
                        dic_waste_code[data.codeRange]++;
                    }
                }

            }

            List<string> unique_code = new List<string>();
            foreach (D_BoxCollectResult boxcollect in boxCollect)
            {
                string codeRange = boxcollect.head + boxcollect.StartCode + "-" + boxcollect.head + boxcollect.EndCode;

                if (codeRange.Length < 5)
                    continue;
                if (!dic_waste_code_boxcollect.ContainsKey(codeRange))
                    dic_waste_code_boxcollect.Add(codeRange, -1);
                foreach (D_VerWasteCode data in WasteCode)
                {
                    string[] s = data.codeRange.Split('-');
                    if (s.Length == 2)
                    {
                        if (String.Compare(s[0], (boxcollect.head + boxcollect.StartCode)) <= 0 && String.Compare(s[1], (boxcollect.head + boxcollect.EndCode)) >= 0)
                        {
                            if (dic_waste_code_boxcollect[codeRange] == -1)
                            {
                                dic_waste_code_boxcollect[codeRange] = 0;
                            }
                        }
                    }
                    if (string.Compare(data.code, (boxcollect.head + boxcollect.EndCode)) <= 0 && string.Compare(data.code, (boxcollect.head + boxcollect.StartCode)) >= 0)
                    {
                        if (!unique_code.Contains(data.code))
                        {
                            unique_code.Add(data.code);
                            dic_waste_code_boxcollect[codeRange]++;
                        }
                        
                    }
                }
                

            }

            string start_code = "";
            string end_code = "";
            int code_err_count = 0;

            //if(dic_waste_code.Count()<=0)
            //{
            //    string uncommit = min_code + "-" + max_code;
            //    lst_uncommite_code.Add(uncommit);
            //}

            //foreach (string key in dic_waste_code.Keys)
            //{

            //    string[] s = key.Split('-');
            //    if (s.Count() == 2)
            //    {
            //        int start_code_tmp = int.Parse(s[0].Substring(2, s[0].Length - 2));
            //        if (end_code.Length > 2)
            //        {
            //            int end_code_tmp = int.Parse(end_code.Substring(2, end_code.Length - 2));
            //            if (start_code_tmp - end_code_tmp == 1)
            //            {
            //                end_code = s[1];
            //               // code_err_count += dic_waste_code[key];
            //            }
            //            else
            //            {
            //                string range = start_code + "-" + end_code;
            //                if (range != "")
            //                {
            //                   // dic_waste_code_continue.Add(range, code_err_count);

            //                }
            //                if (start_code_tmp - end_code_tmp > 1)
            //                {
            //                    string head = s[0].Substring(0, 2);
            //                    string uncommit = head + (end_code_tmp + 1).ToString("d7") + "-" + head + (start_code_tmp - 1).ToString("d7");
            //                    lst_uncommite_code.Add(uncommit);
            //                }
            //                start_code = s[0];
            //                end_code = s[1];
            //               // code_err_count = dic_waste_code[key];
            //            }
            //        }
            //        else
            //        {
            //            start_code = s[0];
            //            end_code = s[1];

            //            if (String.Compare(end_code, max_code) <=0 && String.Compare(start_code, min_code) >= 0 && end_code != "" && max_code != "")
            //            {
            //                try
            //                {
            //                    int tmp = int.Parse(start_code.Substring(2, start_code.Length - 2));
            //                    string uncommit = min_code + "-" + start_code.Substring(0, 2) + (tmp - 1).ToString("d7");
            //                    lst_uncommite_code.Add(uncommit);
            //                }
            //                catch (Exception e)
            //                {
            //                    LogHelper.WriteLog("", e);
            //                }

            //            }
            //            // code_err_count = dic_waste_code[key];
            //        }
            //    }
            //}
            ////if (start_code != min_code && start_code != "" && min_code != "")
            ////{
            ////    try
            ////    {
            ////        int tmp = int.Parse(start_code.Substring(2, start_code.Length - 2));
            ////        string uncommit = min_code + "-" + start_code.Substring(0,2)+(tmp-1).ToString("d7");
            ////        lst_uncommite_code.Add(uncommit);
            ////    }
            ////    catch(Exception e)
            ////    {
            ////        LogHelper.WriteLog("", e);
            ////    }

            ////}

            //if (String.Compare(end_code,max_code)<0 &&String.Compare(start_code,min_code)>0&& end_code != "" && max_code != "")
            //{
            //    try
            //    {
            //        int tmp = int.Parse(end_code.Substring(2, end_code.Length - 2));
            //        if((tmp+1)%1000000==0)
            //        {
            //            tmp = tmp - 1;
            //        }
            //        string uncommit = end_code.Substring(0,2)+(tmp+1).ToString("d7") + "-" + max_code;
            //        lst_uncommite_code.Add(uncommit);
            //    }
            //    catch (Exception e)
            //    {
            //        LogHelper.WriteLog("", e);
            //    }
            //    //string uncommit = end_code + "-" + max_code;
            //    //lst_uncommite_code.Add(uncommit);
            //}



            //start_code = "";
            //end_code = "";
            //code_err_count = 0;
            List<string> lst_uncommite_code_tmp = new List<string>();
            foreach (string key in dic_waste_code_boxcollect.Keys)
            {

                string[] s = key.Split('-');
                if (s.Count() == 2)
                {
                    if(dic_waste_code_boxcollect[key]==-1)
                    {
                        lst_uncommite_code_tmp.Add(key);
                        continue;
                    }
                    int start_code_tmp = 0;
                    bool ret=int.TryParse(s[0].Substring(2, s[0].Length - 2),out start_code_tmp);
                    if(!ret)
                    {
                        LogHelper.WriteLog("转换错误");
                        continue;
                    }
                    if (end_code.Length > 2)
                    {
                        int end_code_tmp = 0;
                         ret = int.TryParse(end_code.Substring(2, end_code.Length - 2),out end_code_tmp);
                        if (!ret)
                        {
                            LogHelper.WriteLog("转换错误");
                            continue;
                        }
                        
                        if (start_code_tmp - end_code_tmp == 1)
                        {
                            end_code = s[1];
                             code_err_count += dic_waste_code_boxcollect[key]==-1?0:dic_waste_code_boxcollect[key];
                        }
                        else
                        {
                            string range = start_code + "-" + end_code;
                            if (range != "")
                            {
                                 dic_waste_code_continue.Add(range, code_err_count);

                            }
                            if (start_code_tmp - end_code_tmp > 1)
                            {
                                //string head = s[0].Substring(0, 2);
                               // string uncommit = head + (end_code_tmp + 1).ToString() + "-" + head + (start_code_tmp - 1).ToString();
                               // lst_uncommite_code.Add(uncommit);
                            }
                            start_code = s[0];
                            end_code = s[1];
                            code_err_count = dic_waste_code_boxcollect[key] == -1 ? 0 : dic_waste_code_boxcollect[key];
                        }
                    }
                    else
                    {
                        start_code = s[0];
                        end_code = s[1];
                        code_err_count = dic_waste_code_boxcollect[key] == -1 ? 0 : dic_waste_code_boxcollect[key];
                    }
                }
            }

            string range1 = start_code + "-" + end_code;
            if (range1 != ""&&range1.Trim()!="-")
            {
                dic_waste_code_continue.Add(range1, code_err_count);
            }

            start_code = "";
            end_code = "";
           
            foreach (string key in lst_uncommite_code_tmp)
            {

                string[] s = key.Split('-');
                if (s.Count() == 2)
                {
                  
                    int start_code_tmp = 0;
                    bool ret = int.TryParse(s[0].Substring(2, s[0].Length - 2), out start_code_tmp);
                    if (!ret)
                    {
                        LogHelper.WriteLog("转换错误");
                        continue;
                    }
                    if (end_code.Length > 2)
                    {
                        int end_code_tmp = 0;
                        ret = int.TryParse(end_code.Substring(2, end_code.Length - 2), out end_code_tmp);
                        if (!ret)
                        {
                            LogHelper.WriteLog("转换错误");
                            continue;
                        }

                        if (start_code_tmp - end_code_tmp == 1)
                        {
                            end_code = s[1];
                            
                        }
                        else
                        {
                            string range = start_code + "-" + end_code;
                            if (range != "")
                            {
                                lst_uncommite_code.Add(range);

                            }
                        
                            start_code = s[0];
                            end_code = s[1];
                            
                        }
                    }
                    else
                    {
                        start_code = s[0];
                        end_code = s[1];
                        
                    }
                }
            }

            range1 = start_code + "-" + end_code;
            if (range1 != "-"&&range1.Trim()!="")
            {
                lst_uncommite_code.Add(range1);
            }

            foreach (string key in dic_waste_code_continue.Keys)
            //for (int i=0;i<verWasteCode.Count();i++)
            {
                DataRow row = table.NewRow();

                row["Code"] = key;

                row["ErrCode"] = "";

                int code_count = 0;
                string[] s = key.Split('-');
                if (s.Count() == 2)
                {
                    try
                    {
                        code_count = int.Parse(s[1].Substring(2, s[1].Length - 2)) - int.Parse(s[0].Substring(2, s[0].Length - 2)) + 1;

                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                }
                row["Count"] = code_count;
                row["errCount"] = dic_waste_code_continue[key];
                total_count += code_count;
                total_err_count += dic_waste_code_continue[key];

                try
                {
                    row["rate"] = (double.Parse(row["errCount"].ToString()) * 100 / code_count).ToString("f4") + "%";

                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("", ex);
                }
                table.Rows.Add(row);
            }

            foreach (string key in lst_uncommite_code)
            {
                Int64 code_count = 0;
                string[] s = key.Split('-');
                if (s.Count() == 2)
                {
                    try
                    {
                        Int64 end = 0;
                        Int64 start = 0;
                        Char[] bs = s[0].ToCharArray();
                        foreach (Char b in bs)
                        {
                            if (Char.IsLetter(b))
                            {
                                start = (start * 10 + b - 'A' + 10);
                            }
                            else
                            {
                                start =(start * 10 + b - '0');
                            }

                        }

                        bs = s[1].ToCharArray();
                        foreach (Char b in bs)
                        {
                            if (Char.IsLetter(b))
                            {
                                end =( end * 10 + b - 'A' + 10);
                            }
                            else
                            {
                                end = (end * 10 + b - '0');
                            }

                        }

                        // code_count = int.Parse(s[1].Substring(2, s[1].Length - 2)) - int.Parse(s[0].Substring(2, s[0].Length - 2)) + 1;
                        code_count = end - start + 1;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                }
                DataRow row = table_un_commite.NewRow();
                row["CodeRange"] = key;
                row["Count"] = code_count;
                table_un_commite.Rows.Add(row);
            }


            double total_rate = 0.0;
            try
            {
                total_rate = (double)total_err_count / total_count;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }

            report.Parameters[3].Value = (total_rate * 100).ToString("f4") + "%";

            report.RegisterData(ds);

            report.Preview = prew;
            report.Prepare();
            report.ShowPrepared();

            window.WinFormHost.Child = report.Preview;

            return true;
        }
    }
}
