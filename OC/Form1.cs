using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;

//using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OC
{
    public partial class Form1 : Form
    {
        private string dir_now = Directory.GetCurrentDirectory();  //取得執行檔所在路徑
        private Settings1 set = new Settings1();  //設定config
        private String vbsfile = String.Empty;  //執行的vbs檔
        private String logname = DateTime.Now.ToString("yyyyMMdd") + "log.txt";   //預產生的log檔
        private StreamWriter log;

        public Form1()
        {
            InitializeComponent();
            progress.Text = "Please push 'Step1' to gather the SQL's command.";
        }

        private void act1_Click(object sender, EventArgs e)
        {
            if (act1.Text.ToString() == "Step1")
            {
                try
                {
                    act2.Enabled = false;
                    act1.Enabled = false;
                    check.Enabled = false;
                    String vbsfile = String.Empty;
                    DirectoryInfo dir = new DirectoryInfo(dir_now);
                    //Console.WriteLine("取得目錄-" + dir_now);
                    //Process vbs = new Process();
                    foreach (DirectoryInfo dChild in dir.GetDirectories("*"))   //取得所有子目錄底下檔案
                    {
                        //foreach (FileInfo dFile in dChild.GetFiles("oc.bat"))
                        //filefullname = dChild.FullName + "/oc.bat";
                        vbsfile = dChild.FullName + "\\" + set.VBSname;
                        //Console.WriteLine("要執行的檔案-" + filefullname);

                        if (File.Exists(vbsfile))   //若有存在vbs
                        {
                            FileInfo[] dFiles = dChild.GetFiles("*.sql");  //檢查有無多餘sql
                            if (dFiles.Length != 0)
                            {
                                foreach (FileInfo dFile in dFiles)
                                    File.Delete(dFile.FullName);     //若有多餘sql  則刪除
                                System.Threading.Thread.Sleep(1000);
                            }

                            _vbs(dChild);

                            for (uint i = 0; i < set.waitTime; i++)
                                System.Threading.Thread.Sleep(1000);

                            /*
                            Process vbs = new Process();
                            vbs.StartInfo.FileName = @"cscript";
                            vbs.StartInfo.Arguments = " " + vbsfile;
                            vbs.StartInfo.WorkingDirectory = dChild.FullName;
                            vbs.Start();
                            vbs.WaitForExit();
                            vbs.Close();
                             */
                        }
                        else
                        {
                            MessageBox.Show("File does not exist in " + dChild.FullName);
                            act1.Enabled = true;
                            act2.Enabled = true;
                            check.Enabled = true;
                        }
                    }
                    //vbs.Close();
                    //System.Threading.Thread.Sleep(3000);
                    act1.Text = "Ready";
                    progress.Text = "Step1 is over,please push Step2";
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error!!");
                    MessageBox.Show(er.Message);
                    //Console.WriteLine(er.ToString());
                    act2.Enabled = true;
                    act1.Enabled = true;
                    check.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please Restart!!");
                progress.Text = "Please Restart!!";
            }
            act1.Enabled = true;
            act2.Enabled = true;
            check.Enabled = true;
        }

        private void act2_Click(object sender, EventArgs e)
        {
            //Settings1 set = new Settings1();
            if (act1.Text == "Ready")
            {
                act1.Enabled = false;
                act2.Enabled = false;
                check.Enabled = false;
                String file_now = String.Empty;
                String line = String.Empty;
                //String logname = DateTime.Now.ToString("yyyyMMdd") + "log.txt";
                //StreamWriter log;

                if (!File.Exists(logname))   //檢查有無log檔
                {
                    log = new StreamWriter(logname);  //若沒有  產生一個
                    log.Close();
                }
                log = File.AppendText(logname);  //log寫入檔案尾端
                //SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Northwind;User Id=sa;Password=sa;");
                SqlConnection conn = new SqlConnection(set.connection);

                try
                {
                    log.WriteLine("--" + DateTime.Now.ToString("yyyy/MM/dd H:mm:ff") + "--START!!");
                    String sqlcomm = String.Empty;
                    SqlCommand cmd;
                    DirectoryInfo dir = new DirectoryInfo(dir_now);
                    //StreamReader sr;

                    conn.Open();

                    foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
                    {
                        foreach (FileInfo dFile in dChild.GetFiles("*.sql"))  //讀子目錄底下所有sql檔
                        {
                            StreamReader sr = File.OpenText(dFile.FullName);
                            file_now = dChild.ToString() + "/" + dFile.ToString();
                            progress.Text = file_now.ToString();
                            //progress.Refresh();
                            Application.DoEvents();
                            log.WriteLine("--" + dChild.ToString() + "/" + dFile.ToString());
                            while ((line = sr.ReadLine()) != null)
                            {
                                sqlcomm = line;
                                cmd = new SqlCommand(sqlcomm, conn);
                                cmd.ExecuteNonQuery();   //一行行寫入
                                //log.WriteLine(line);
                            }
                            sr.Close();
                        }
                    }

                    log.WriteLine("--" + DateTime.Now.ToString("yyyy/MM/dd H:mm:ff") + "--END");
                    progress.Text = "Done";
                    act2.Text = "Done";
                }
                catch (Exception rr)
                {
                    log.WriteLine(line + " Error!!");
                    log.WriteLine("--" + DateTime.Now.ToString("yyyyMMdd-H:mm:ff") + "--ERROR");
                    progress.Text = file_now.ToString() + "  error!!";
                    MessageBox.Show(progress.Text.ToString());
                    MessageBox.Show(rr.Message);
                }
                finally
                {
                    log.Close();
                    conn.Close();
                    act1.Enabled = true;
                    act2.Enabled = true;
                    check.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Step1 is NOT Ready!!");
                progress.Text = "Step1 is NOT Ready!!";
            }
        }

        private void check_Click(object sender, EventArgs e)
        {
            //Settings1 set = new Settings1();

            act1.Enabled = false;
            act2.Enabled = false;
            check.Enabled = false;

            DirectoryInfo dir = new DirectoryInfo(dir_now);
            String file_now = String.Empty;
            String vbsfile = String.Empty;
            //String logname = DateTime.Now.ToString("yyyyMMdd") + "log.txt";
            //StreamWriter log;

            if (!File.Exists(logname))
            {
                log = new StreamWriter(logname);
                log.Close();
            }
            log = File.AppendText(logname);

            foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
            {
                FileInfo[] dFiles = dChild.GetFiles("*.sql");  //檢查子目錄底下有無sql檔
                if (dFiles.Length != 0)  //若有sql檔
                {
                    foreach (FileInfo dFile in dFiles)   //檢查所有sql檔
                    {
                        StreamReader sr = File.OpenText(dFile.FullName);
                        file_now = dChild.ToString() + "/" + dFile.ToString();
                        progress.Text = file_now.ToString();
                        //progress.Refresh();
                        Application.DoEvents();
                        //log.WriteLine("--" + dChild.ToString() + "/" + dFile.ToString());
                        //while ((line = sr.ReadLine()) != null)
                        string[] srr = System.IO.File.ReadAllLines(dFile.FullName);   //個別讀完所有sql
                        //MessageBox.Show(srr.ToString());
                        //String idCheck = srr.ToString().Substring(25, 26);
                        String first2 = srr[srr.Length - 1].ToString().Substring(0, 2);  //取得各sql最後一行前兩字元
                        if (first2 == "de")  //表示產生不完全
                        {
                            sr.Close();
                            log.WriteLine(DateTime.Now.ToString("yyyyMMdd-H:mm:ff") + "->sql command is regenerated:" + dChild.Name);
                            foreach (FileInfo dFile2 in dChild.GetFiles("*.sql"))
                                File.Delete(dFile2.FullName);   //刪掉該子目錄底下所有sql檔

                            System.Threading.Thread.Sleep(3000);
                            _vbs(dChild);   //重新產生
                            /*
                            vbsfile = dChild.FullName + "\\" + set.VBSname;
                            Process vbs = new Process();
                            vbs.StartInfo.FileName = @"cscript";
                            vbs.StartInfo.Arguments = " " + vbsfile;
                            vbs.StartInfo.WorkingDirectory = dChild.FullName;
                            vbs.Start();
                            vbs.WaitForExit();
                            vbs.Close();
                             */
                            System.Threading.Thread.Sleep(3000);
                            //MessageBox.Show("file error");
                            break;
                        }
                        sr.Close();
                    }
                }
                else
                {
                    /*
                    vbsfile = dChild.FullName + "\\" + set.VBSname;
                    Process vbs = new Process();
                    vbs.StartInfo.FileName = @"cscript";
                    vbs.StartInfo.Arguments = " " + vbsfile;
                    vbs.StartInfo.WorkingDirectory = dChild.FullName;
                    vbs.Start();
                    vbs.WaitForExit();
                    vbs.Close();
                     */
                    _vbs(dChild);  //如果子目錄底下沒sql  一樣重新產生
                    System.Threading.Thread.Sleep(1000);
                }
            }

            foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
            {
                FileInfo[] dFiles = dChild.GetFiles("*.sql");  //檢查子目錄底下有無sql檔
                if (dFiles.Length != 0)  //若有sql檔
                {
                    string line = string.Empty;
                    int num;
                    bool isNum;
                    foreach (FileInfo dFile in dFiles)   //檢查所有sql檔
                    {
                        StreamReader sr = File.OpenText(dFile.FullName);
                        file_now = dChild.ToString() + "/" + dFile.ToString();
                        progress.Text = file_now.ToString();
                        //progress.Refresh();
                        Application.DoEvents();
                        //log.WriteLine("--" + dChild.ToString() + "/" + dFile.ToString());
                        //while ((line = sr.ReadLine()) != null)
                        //string[] srr = System.IO.File.ReadAllLines(dFile.FullName);   //個別讀完所有sql
                        //MessageBox.Show(srr.ToString());
                        //String idCheck = srr.ToString().Substring(25, 26);
                        //String first2 = srr[srr.Length - 1].ToString().Substring(0, 2);  //取得各sql最後一行前兩字元
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length > 23 && line.ToString().Substring(0, 23) == "insert into jobs values")
                            {
                                isNum = int.TryParse(line.ToString().Substring(25, 1), out num);
                                if (!isNum)
                                { MessageBox.Show(file_now.ToString() + " 有錯需修改==> " + line.ToString().Substring(25, 9)); }
                            }
                        }
                        sr.Close();
                    }
                }
            }

            log.Close();

            progress.Text = "Checked!";
            act1.Enabled = true;
            act2.Enabled = true;
            check.Enabled = true;
        }

        private void _vbs(DirectoryInfo _folder)   //執行子目錄底下vbs
        {
            vbsfile = _folder.FullName + "\\" + set.VBSname;
            Process vbs = new Process();
            vbs.StartInfo.FileName = @"cscript";
            vbs.StartInfo.Arguments = " " + vbsfile;
            vbs.StartInfo.WorkingDirectory = _folder.FullName;
            vbs.Start();
            vbs.WaitForExit();
            vbs.Close();
            //System.Threading.Thread.Sleep(TimeSpan.FromTicks(set.waitTime));
        }

        private void actF_Click(object sender, EventArgs e)
        {
            SqlConnection conn1 = new SqlConnection(set.connection);
            SqlConnection conn2 = new SqlConnection(set.connection);
            SqlConnection conn3 = new SqlConnection(set.connection);
            String sqlcomm1 = "select name,count(*)  from jobs where isprimary=1 group by name having count(*) >1";
            String sqlcomm2 = "select * from jobs where supervisorid='0'";
            String sqlcomm3 = "select A.NAME,B.NAME from(select distinct NAME from JOBS where ISPRIMARY = '0' and ID <> '0') A left outer join (select distinct NAME from JOBS where ISPRIMARY = '1') B on A.NAME = B.NAME where len(A.NAME) = 10 and B.NAME is null";

            SqlCommand cmd1 = new SqlCommand(sqlcomm1, conn1);
            SqlCommand cmd2 = new SqlCommand(sqlcomm2, conn2);
            SqlCommand cmd3 = new SqlCommand(sqlcomm3, conn3);

            try
            {
                conn1.Open();
                conn2.Open();
                conn3.Open();

                SqlDataReader r1 = cmd1.ExecuteReader();
                SqlDataReader r2 = cmd2.ExecuteReader();
                SqlDataReader r3 = cmd3.ExecuteReader();

                if (r1.HasRows || r2.HasRows || r3.HasRows)
                    MessageBox.Show("Something Wrong!");
                else
                    MessageBox.Show("Everything is goodgood~~");

                r1.Close();
                r2.Close();
                r3.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                cmd1.Dispose();
                cmd2.Dispose();
                cmd3.Dispose();
                conn1.Close();
                conn2.Close();
                conn3.Close();
            }
        }
    }
}