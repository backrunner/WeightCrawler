﻿using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WeightCrawler
{
    
    public partial class WeightCrawler : Form
    {
        public WeightCrawler()
        {
            InitializeComponent();
        }

        //委托
        public delegate int GetFileLengthHandler(string filePath);
        public delegate void ImportFileHandler(string filePath);
        public delegate void ImportFinishHandler();
        public delegate void WorkHandler(int sleep);
        public delegate void WorkFinishedHandler();
        public delegate void WeightUpdateHandler(string weight);
        public delegate void CurrentProcessHandler(int processed);
        public delegate void ExportHandler(string path, ListView lv);

        public delegate void LogHandler(string s);
        public delegate void ProgressBarHandler(int value);
        public delegate void ThreadSignalHandler(bool signal);
        public delegate void DomainListHandler(string domain, string weight);

        public event LogHandler LogEvent;
        public event ProgressBarHandler ProgressBarUpdate;
        public event ThreadSignalHandler ThreadSignalUpdate;
        public event ImportFinishHandler ImportFinished;
        public event DomainListHandler DomainListInsert;
        public event WorkFinishedHandler WorkFinished;
        public event WeightUpdateHandler WeightUpdate;
        public event CurrentProcessHandler CurrentProcessUpdate;

        //公共变量
        private string filePath;
        private int fileLength;

        private Queue domains;
        private Queue _domains;

        private string[] apis =
        {
            "https://baidurank.aizhan.com/api/br?domain={domain}&style=text"
        };

        private int currentApi;
        private int threadCount;
        private int threadSleep;

        private int domainsCount;
        private static double processedCount { get; set; }

        //flags
        private bool workStarted { get; set; }
        private bool threadPause { get; set; }

        private int threadPauseTime { get; set; }

        private object _locker = new object();
        private object _locker_threadSignal = new object();
        private object _locker_count = new object();

        //窗体初始化
        private void WeightCrawler_Load(object sender, EventArgs e)
        {
            //bind events
            LogEvent += WeightCrawler_LogEvent;
            ProgressBarUpdate += WeightCrawler_ProgressBarUpdate;
            ThreadSignalUpdate += WeightCrawler_ThreadSignalUpdate;
            ImportFinished += WeightCrawler_ImportFinished;
            DomainListInsert += WeightCrawler_DomainListInsert;
            WorkFinished += WeightCrawler_WorkFinished;
            WeightUpdate += WeightCrawler_WeightUpdate;
            CurrentProcessUpdate += WeightCrawler_CurrentProcessUpdate;
            //设置默认值
            tb_thread.Text = "10";
            combo_api.SelectedIndex = 0;    //默认API
        }

        private void WeightCrawler_ThreadSignalUpdate(bool signal)
        {
            var update = new Action(() =>
            {
                if (signal)
                {
                    tb_count.BackColor = Color.Crimson;
                } else
                {
                    tb_count.BackColor = Color.LightGray;
                }
            });
            this.Invoke(update);
        }

        private void WeightCrawler_CurrentProcessUpdate(int processed)
        {
            var update = new Action(() => {
                tb_count.Text = processed.ToString();
            });
            this.Invoke(update);
        }

        private void UpdateCurrentProcess(int processed)
        {
            CurrentProcessUpdate?.Invoke(processed);
        }

        private void initStats()
        {
            lv_stats.Items.Clear();
            for (var i = 0; i < 10; i++)
            {
                ListViewItem _item = new ListViewItem("权" + i.ToString());
                _item.SubItems.Add("0");
                lv_stats.Items.Add(_item);
            }
            ListViewItem item = new ListViewItem("权n");
            item.SubItems.Add("0");
            lv_stats.Items.Add(item);
            item = new ListViewItem("错误");
            item.SubItems.Add("0");
            lv_stats.Items.Add(item);
        }

        private void WeightCrawler_WeightUpdate(string weight)
        {
            var update = new Action(() => {
                switch (weight)
                {
                    case "0":
                        lv_stats.Items[0].SubItems[1].Text = (int.Parse(lv_stats.Items[0].SubItems[1].Text)+1).ToString();
                        break;
                    case "1":
                        lv_stats.Items[1].SubItems[1].Text = (int.Parse(lv_stats.Items[1].SubItems[1].Text) + 1).ToString();
                        break;
                    case "2":
                        lv_stats.Items[2].SubItems[1].Text = (int.Parse(lv_stats.Items[2].SubItems[1].Text) + 1).ToString();
                        break;
                    case "3":
                        lv_stats.Items[3].SubItems[1].Text = (int.Parse(lv_stats.Items[3].SubItems[1].Text) + 1).ToString();
                        break;
                    case "4":
                        lv_stats.Items[4].SubItems[1].Text = (int.Parse(lv_stats.Items[4].SubItems[1].Text) + 1).ToString();
                        break;
                    case "5":
                        lv_stats.Items[5].SubItems[1].Text = (int.Parse(lv_stats.Items[5].SubItems[1].Text) + 1).ToString();
                        break;
                    case "6":
                        lv_stats.Items[6].SubItems[1].Text = (int.Parse(lv_stats.Items[6].SubItems[1].Text) + 1).ToString();
                        break;
                    case "7":
                        lv_stats.Items[7].SubItems[1].Text = (int.Parse(lv_stats.Items[7].SubItems[1].Text) + 1).ToString();
                        break;
                    case "8":
                        lv_stats.Items[8].SubItems[1].Text = (int.Parse(lv_stats.Items[8].SubItems[1].Text) + 1).ToString();
                        break;
                    case "9":
                        lv_stats.Items[9].SubItems[1].Text = (int.Parse(lv_stats.Items[9].SubItems[1].Text) + 1).ToString();
                        break;
                    case "n":
                        lv_stats.Items[10].SubItems[1].Text = (int.Parse(lv_stats.Items[10].SubItems[1].Text) + 1).ToString();
                        break;
                    case "error":
                        lv_stats.Items[11].SubItems[1].Text = (int.Parse(lv_stats.Items[11].SubItems[1].Text) + 1).ToString();
                        break;
                }
            });
            this.Invoke(update);
        }

        private void WeightCrawler_WorkFinished()
        {
            var update = new Action(() => {
                UpdateProgressBar(1);
                btn_start.Enabled = false;
                btn_stop.Enabled = false;
                btn_import.Enabled = true;
                btn_export.Enabled = true;
                combo_api.Enabled = true;
                tb_thread.Enabled = true;
            });
            this.Invoke(update);
        }

        private void WeightCrawler_DomainListInsert(string domain, string weight)
        {
            ListViewItem item = new ListViewItem(domain);
            item.SubItems.Add(weight);
            var update = new Action(() => {
                lv_domains.Items.Add(item);
            });
            this.Invoke(update);
        }

        private void InsertDomain(string domain, string weight)
        {
            DomainListInsert?.Invoke(domain, weight);
        }

        private void WeightCrawler_ProgressBarUpdate(int value)
        {
            var update = new Action(() => {
                progress_total.Value = value;
            });
            this.Invoke(update);
        }

        private void WeightCrawler_LogEvent(string s)
        {
            var update = new Action(() => {
                rb_output.Text += s + "\r\n";
                rb_output.ScrollToCaret();
            });
            this.Invoke(update);
        }

        private void Btn_selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择导入的文件";
            fileDialog.Filter = "文本文件(*.txt)|*.txt";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_file.Text = fileDialog.FileName;
                btn_import.Enabled = true;
            }
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            //设置按钮状态
            btn_start.Enabled = false;
            btn_import.Enabled = false;
            combo_api.Enabled = false;
            tb_thread.Enabled = false;
            btn_stop.Enabled = true;
            lv_domains.Items.Clear();
            //获取数据
            currentApi = combo_api.SelectedIndex;
            threadCount = int.Parse(tb_thread.Text);
            threadSleep = int.Parse(tb_sleep.Text);
            threadPauseTime = int.Parse(txt_threadPause.Text);

            //设置flag
            workStarted = true;

            //设置变量
            processedCount = 0;

            initStats();

            UpdateProgressBar(0);

            //开始工作
            WorkHandler handler = new WorkHandler(StartWork);
            handler.BeginInvoke(threadSleep, new AsyncCallback(WorkCallback), null);
        }

        private void Btn_stop_Click(object sender, EventArgs e)
        {
            workStarted = false;
            threadPause = false;
            btn_start.Enabled = true;
            btn_stop.Enabled = false;
            btn_import.Enabled = true;
            btn_export.Enabled = true;
            combo_api.Enabled = true;
            tb_thread.Enabled = true;
        }

        private void StartWork(int sleep)
        {
            Log("任务开始执行...");
            _domains = Queue.Synchronized(domains);
            ThreadPool.SetMinThreads(domains.Count, domains.Count);
            ThreadPool.SetMaxThreads(threadCount, threadCount);
            var tmr = new System.Timers.Timer();
            tmr.Interval = sleep;
            var i = 0;
            tmr.Elapsed += (s, arg) =>
            {
                i++;
                if (i > domains.Count)
                {
                    tmr.Stop();
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadWork), null);
            };
            tmr.Start();
        }

        private void WorkCallback(IAsyncResult result)
        {
            while (processedCount < domainsCount);
            workStarted = false;
            threadPause = false;
            Log("任务执行完成...");
            WorkFinished?.Invoke();
        }

        private void ThreadWork(object state)
        {
            if (!workStarted)
            {
                return;
            }    
            if (currentApi == 0)
            {
                while (threadPause)
                {
                    Thread.Sleep(threadPauseTime);
                    lock (_locker_threadSignal)
                    {
                        threadPause = false;
                        SetThreadSignal(false);
                    }
                }
                string domain;
                domain = (string)_domains.Dequeue();
                var api = apis[0].Replace("{domain}", domain);
                string weight = "";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(api);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3763.0 Safari/537.36 Edg/75.0.131.0";
                request.Timeout = 5000;
                try
                {
                    WebResponse wr = request.GetResponse();
                    StreamReader streamReader = new StreamReader(wr.GetResponseStream());
                    string response = streamReader.ReadToEnd();
                    string pattern = "[0-9]+</a>";
                    string res = Regex.Match(response, pattern).Value.Replace("</a>","");
                    if (res.Length > 0)
                    {
                        weight = res;
                    } else
                    {
                        weight = "error";
                    }
                }
                catch
                {
                    //do nothing
                    weight = "error";
                }
                InsertDomain(domain, weight);
                lock (_locker_count)
                {
                    processedCount++;
                }
                if (workStarted)
                {
                    UpdateProgressBar(processedCount / domainsCount);
                } else
                {
                    UpdateProgressBar(1);
                }
                UpdateWeight(weight);
                UpdateCurrentProcess((int)processedCount);
                if ((processedCount+1)%(360-threadCount) == 0)
                {
                    if (threadPauseTime > 0)
                    {
                        lock (_locker_threadSignal)
                        {
                            threadPause = true;
                            SetThreadSignal(true);
                            Log("防止API限制，缓速中……");
                        }
                    }
                }
            }
        }

        private void Btn_import_Click(object sender, EventArgs e)
        {
            btn_import.Enabled = false; //禁用按钮
            filePath = tb_file.Text.Trim(); //文件路径
            fileLength = 0;
            UpdateProgressBar(0);
            domains = Queue.Synchronized(new Queue());  //初始化domains
            Log("正在获取文件信息...");
            GetFileLengthHandler fileLengthHandler = new GetFileLengthHandler(FileOperation.GetFileLength);
            IAsyncResult result = fileLengthHandler.BeginInvoke(filePath, new AsyncCallback(GetFileLengthCallback), null);
        }

        private void GetFileLengthCallback(IAsyncResult result)
        {
            GetFileLengthHandler handler = (GetFileLengthHandler)((AsyncResult)result).AsyncDelegate;
            fileLength = handler.EndInvoke(result); //取得文件的长度，开始导入
            Log("获取到 " + fileLength.ToString()+" 行数据, 开始导入...");
            ImportFileHandler importFileHandler = new ImportFileHandler(ImportFile);
            IAsyncResult importResult = importFileHandler.BeginInvoke(filePath, new AsyncCallback(ImportFileCallback), null);
        }

        private void ImportFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                var s = sr.ReadLine();
                double lines = 0;
                while (s != null)
                {
                    s = s.Replace("&", "");
                    //处理文本
                    string pattern = "(([a-z0-9A-Z--]|[u4e00-u9fa5])+\\.)?([a-z0-9A-Z--]|[u4e00-u9fa5])+\\.(ac.cn|bj.cn|sh.cn|tj.cn|cq.cn|he.cn|sn.cn|sx.cn|nm.cn|ln.cn|jl.cn|hl.cn|js.cn|zj.cn|ah.cn|fj.cn|jx.cn|sd.cn|ha.cn|hb.cn|hn.cn|gd.cn|gx.cn|hi.cn|sc.cn|gz.cn|yn.cn|gs.cn|qh.cn|nx.cn|xj.cn|tw.cn|hk.cn|mo.cn|xz.cn" +
                    "|com.cn|net.cn|org.cn|gov.cn|.com.hk|我爱你|在线|中国|网址|网店|中文网|公司|网络|集团" +
                    "|com|cn|cc|org|net|gov|xin|xyz|vip|shop|top|club|wang|fun|info|online|tech|store|site|ltd|ink|biz|group|link|work|pro|mobi|ren|kim|name|tv|red|app|space|cloud|int" +
                    "|cool|team|live|pub|company|zone|today|video|art|chat|gold|guru|show|life|love|email|fund|city|plus|design|social|center|world|auto|rip|ceo|sale|hk|io|gg|tm|gs|us|tw)\\b";
                    var match = Regex.Match(s, pattern);
                    if (match.Value.Length > 0)
                    {
                        domains.Enqueue(match.Value);
                    }
                    s = sr.ReadLine();  //读取下一行
                    lines++;
                    UpdateProgressBar(lines / fileLength);
                }
            }
            //导入完成
            return;
        }

        private void ImportFileCallback(IAsyncResult result)
        {
            //import finished
            ImportFinished?.Invoke();
            domainsCount = domains.Count;
            Log("文件导入完成，共导入了 "+ domainsCount.ToString()+" 个域名...");
        }

        private void WeightCrawler_ImportFinished()
        {
            //导入完成后启用按钮
            var update = new Action(() => {
                btn_start.Enabled = true;
                btn_import.Enabled = true;
            });
            this.Invoke(update);
        }

        private void Log(string s)
        {
            s = DateTime.Now.ToString("HH:mm:ss ") + ": " + s;
            LogEvent?.Invoke(s);
        }

        private void UpdateProgressBar(double value)
        {
            var t = (int)(value * 100);
            if (t > 100)
            {
                t = 100;
            }
            ProgressBarUpdate?.Invoke(t);
        }
        private void SetThreadSignal(bool signal)
        {
            ThreadSignalUpdate?.Invoke(signal);
        }
        private void UpdateWeight(string weight)
        {
            WeightUpdate?.Invoke(weight);
        }

        private void Btn_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "逗号分隔符文件(*.csv)|*.csv";
            dialog.FileName = "output_" + DateTime.Now.ToString("yyyyMMddHHmmss")+".csv";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = dialog.FileName;
                UpdateProgressBar(0);
                var count = lv_domains.Items.Count;
                try
                {
                    using (FileStream fs = File.Open(file, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            //写入表头
                            sw.WriteLine(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes("域名,权重")));
                            for (var i = 0; i < count; i++)
                            {
                                if (lv_domains.Items[i].SubItems[1].Text != "error") {
                                    sw.WriteLine(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(lv_domains.Items[i].SubItems[0].Text + "," + lv_domains.Items[i].SubItems[1].Text)));
                                }
                                UpdateProgressBar((double)i / count);
                            }
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("导出时发生错误:\n" + ex.Message);
                    return;
                }
                UpdateProgressBar(1);
                MessageBox.Show("导出完成。");
            }
        }

        private void Btn_retry_Click(object sender, EventArgs e)
        {
            var count = lv_domains.Items.Count;
            Log("开始获取出错域名...");
            UpdateProgressBar(0);
            var errorcount = 0;
            domains = Queue.Synchronized(new Queue());  //初始化domains
            for (var i = 0; i < count; i++)
            {
                if (lv_domains.Items[i].SubItems[1].Text == "error")
                {
                    domains.Enqueue(lv_domains.Items[i].SubItems[0].Text);
                    errorcount++;
                }
                UpdateProgressBar((double)i / count);
            }
            Log("已导入 " + errorcount + " 个出错的域名...");
            btn_start.Enabled = true;
        }

        private void Btn_exporterror_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件(*.txt)|*.txt";
            dialog.FileName = "erroroutput_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = dialog.FileName;
                UpdateProgressBar(0);
                var count = lv_domains.Items.Count;
                try
                {
                    using (FileStream fs = File.Open(file, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            //写入表头
                            for (var i = 0; i < count; i++)
                            {
                                if (lv_domains.Items[i].SubItems[1].Text == "error")
                                {
                                    sw.WriteLine(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(lv_domains.Items[i].SubItems[0].Text)));
                                }
                                UpdateProgressBar((double)i / count);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出时发生错误:\n" + ex.Message);
                    return;
                }
                UpdateProgressBar(1);
                MessageBox.Show("导出完成。");
            }
        }
    }

    public static class FileOperation
    {
        public static int GetFileLength(string filePath)
        {
            var fileLength = 0;
            using (var sr = new StreamReader(filePath))
            {
                while (sr.ReadLine() != null)
                {
                    fileLength++;
                }
            }
            return fileLength;
        }
    }
}
