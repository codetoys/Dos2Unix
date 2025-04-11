using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Web;
using System.Threading;
using System.Linq;

namespace Dos2Unix
{
	public partial class Form1 : Form
	{
		private bool isLanguageZH = false;//语言是否是简体中文
		private mySorter sorter;
		private string folder;
		private long file_count;
		private long file_text_count;
		private ListViewItem[] cacheItems = new ListViewItem[1000];//开始时逐条，超过则缓存，缓存满才加入
		private long cacheItemsCount = 0;
		private Thread work_thread;
		private bool bCancel = false;//取消任务

		public Form1()
		{
			InitializeComponent();
			sorter = new mySorter();
			//为ListViewItemSorter指定排序类
			listView1.ListViewItemSorter = sorter;
			sorter.SortOrder = SortOrder.Ascending;
			folder = "";

			isLanguageZH = Thread.CurrentThread.CurrentUICulture.Name.StartsWith("zh", StringComparison.OrdinalIgnoreCase);

		}
		public bool ProcessFile(string file, bool checkonly)
		{
			bool isASCII = true;
			FileStream filestream = new FileStream(file, FileMode.Open, FileAccess.Read);
			byte[] data = new byte[filestream.Length];
			filestream.Read(data, 0, (int)filestream.Length);
			filestream.Close();
			//MessageBox.Show(data.Length.ToString(),file);
			int i;
			for (i = 0; i < data.Length; ++i)
			{
				if (data[i] == 0)
				{
					//MessageBox.Show("非文本文件", file);
					isASCII = false;
					break;
				}
			}
			if (checkonly)
			{
				return isASCII;
			}
			else
			{
				File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);
				if (!isASCII) return isASCII;
			}

			FileStream outfile = new FileStream(file, FileMode.Truncate, FileAccess.Write);
			for (i = 0; i < data.Length; ++i)
			{
				if (i < data.Length - 1 && data[i] == '\r' && data[i + 1] == '\n') continue;
				outfile.WriteByte(data[i]);
			}
			//MessageBox.Show(outfile.Length.ToString(), file);
			outfile.Close();

			return isASCII;
		}
		public void thread_FindAllFile(string path, string bakfolder, bool checkonly, bool nolist)
		{
			this.button_browser.Enabled = false;
			this.button_do.Enabled = false;
			this.button_nolist.Enabled = false;
			bCancel = false;
			work_thread = new Thread(() =>
			{
				listView1.Items.Clear();
				file_count = 0;
				file_text_count = 0;

				FindAllFile(path, checkonly, nolist);
				_UpateUI();

				this.button_browser.Enabled = true;
				this.button_do.Enabled = true;
				this.button_nolist.Enabled = true;

				if (!checkonly)
				{
					if (isLanguageZH)
					{
						MessageBox.Show(this, "共有文件 " + file_count + " 个，共处理文本文件 " + file_text_count + " 个\n\n原始目录已经备份至 " + bakfolder, "转换完成");
					}
					else
					{
						MessageBox.Show(this, "Total files " + file_count + " ，text file " + file_text_count + " 个\n\nbackup " + bakfolder, "The conversion is complete");
					}
				}
			});
			work_thread.Start();
		}
		private void _UpateUI()
		{
			if(cacheItems.LongLength== cacheItemsCount)listView1.Items.AddRange(cacheItems);
			else
			{
				listView1.Items.AddRange(cacheItems.Take((int)cacheItemsCount).ToArray());
			}
			cacheItemsCount = 0;
		}
		private void UpdateUI(ListViewItem item)
		{
			if (null == item)
			{
				_UpateUI();
			}
			else
			{
				if (listView1.Items.Count < 100) listView1.Items.Add(item);
				else
				{
					cacheItems[cacheItemsCount++] = item;
					if (cacheItems.Length == cacheItemsCount)
					{
						_UpateUI();
					}
				}
			}
		}
		public void FindAllFile(string path, bool checkonly, bool nolist)
		{
			DirectoryInfo[] ChildDirectory;//子目录集
			FileInfo[] NewFileInfo;//当前所有文件
			DirectoryInfo FatherDirectory = new DirectoryInfo(path); //当前目录
			ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集
			NewFileInfo = FatherDirectory.GetFiles();//得到文件集，可以进行操作

			//MessageBox.Show(path, "当前目录");
			foreach (FileInfo fileInfo in NewFileInfo)
			{
				if (bCancel) break;
				bool istext = ProcessFile(fileInfo.FullName, checkonly);
				++file_count;
				if (istext) ++file_text_count;
				if (!nolist)
				{
					ListViewItem item = new ListViewItem(fileInfo.DirectoryName);
					item.SubItems.Add(fileInfo.Name);
					item.SubItems.Add(fileInfo.Extension);
					if (istext)
					{
						if (checkonly)
						{
							item.SubItems.Add(isLanguageZH ? "需转换" : "Conversion is required");
						}
						else
						{
							item.SubItems.Add("OK");
						}
					}
					else
					{
						item.SubItems.Add("");
					}
					UpdateUI(item);
				}
			}
			foreach (DirectoryInfo dirInfo in ChildDirectory)
			{
				if (bCancel) break;
				FindAllFile(dirInfo.FullName, checkonly, nolist);
			}
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == this.sorter.SortColumn)
			{
				if (this.sorter.SortOrder == SortOrder.Ascending)
					this.sorter.SortOrder = SortOrder.Descending;
				else
					if (this.sorter.SortOrder == SortOrder.Descending)
					this.sorter.SortOrder = SortOrder.Ascending;
				else
					return;
			}
			else
			{
				this.sorter.SortColumn = e.Column;
			}
			this.listView1.Sort();
		}

		private void do_dos2unix(bool nolist)
		{
			if (nolist || 0 == folder.CompareTo("") || 0 == listView1.Items.Count)
			{
				if (!browser()) return;
			}

			string bakfolder = folder + " - Dos2Unix bak " + DateTime.Now.ToString("yyyyMMdd hhmmss");
			Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(folder, bakfolder);

			thread_FindAllFile(folder, bakfolder, false, nolist);
		}
		private void button_do_Click(object sender, EventArgs e)
		{
			do_dos2unix(false);
		}
		private void button_nolist_Click(object sender, EventArgs e)
		{
			do_dos2unix(true);
		}

		private bool browser()
		{
			string Description = "";
			if (isLanguageZH)
			{
				Description = "Dox2Unix，文件格式转换\n请选择要转换的目录";
			}
			else
			{
				Description = "Dox2Unix, File format conversion\nPlease select the directory you want to convert";
			}

			folderBrowserDialog1.SelectedPath = Properties.Settings.Default.lastDir;
			folderBrowserDialog1.Description = Description;
			folderBrowserDialog1.ShowNewFolderButton = false;
			if (DialogResult.OK == folderBrowserDialog1.ShowDialog(this))
			{
				folder = folderBrowserDialog1.SelectedPath;
				Properties.Settings.Default.lastDir = folder;
				Properties.Settings.Default.Save();
				this.Text = "Dos2Unix - " + folder;
				return true;
			}
			else
			{
				return false;
			}
		}
		private void button_browser_Click(object sender, EventArgs e)
		{
			if (browser())
			{
				listView1.Items.Clear();
				thread_FindAllFile(folder, null, true, false);
			}
		}

		private void button_help_Click(object sender, EventArgs e)
		{
			if (isLanguageZH)
			{
				MessageBox.Show("Dos2Unix CodeToys 版权所有 2007-2025\n\n对一个目录下所有文本文件转换为UNIX格式\n\n转换前会自动备份", "Dos2Unix 1.0.1 帮助");
			}
			else
			{
				MessageBox.Show("Dos2Unix CodeToys Copyright 2007-2025\n\nConvert all text files in a directory to UNIX format\n\nAutomatically backed up before conversion", "Dos2Unix 1.0.1 Help");
			}
		}

		private NameValueCollection GetQueryStringParameters()
		{
			NameValueCollection nameValueTable = new NameValueCollection();

			if (ApplicationDeployment.IsNetworkDeployed)
			{
				string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
				MessageBox.Show(queryString, "URL");
				//nameValueTable = HttpUtility.ParseQueryString(queryString);
			}

			return (nameValueTable);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			NameValueCollection querystrings = GetQueryStringParameters();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (null != work_thread)
			{
				work_thread.Abort();
			}
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			bCancel = true;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.textBox_status.Text =listView1.Items.Count.ToString()+ " "+ file_count.ToString()+" "+ file_text_count.ToString();
		}
	}
	public class mySorter : System.Collections.IComparer
	{
		//private Comparer<object> comparer;
		private int sortColumn;
		private SortOrder sortOrder;
		public mySorter()
		{
			sortColumn = 0;
			sortOrder = SortOrder.None;
			//comparer = Comparer<object>.Default;
		}
		//指定进行排序的列
		public int SortColumn
		{
			get { return sortColumn; }
			set { sortColumn = value; }
		}
		//指定按升序或降序进行排序
		public SortOrder SortOrder
		{
			get { return sortOrder; }
			set { sortOrder = value; }
		}
		public int Compare(object x, object y)
		{
			int CompareResult;
			ListViewItem itemX = (ListViewItem)x;
			ListViewItem itemY = (ListViewItem)y;
			//在这里您可以提供自定义的排序
			CompareResult = itemX.SubItems[this.sortColumn].Text.CompareTo(itemY.SubItems[this.sortColumn].Text);
			if (this.SortOrder == SortOrder.Ascending)
				return CompareResult;
			else
				if (this.SortOrder == SortOrder.Descending)
				return (-CompareResult);
			else
				return 0;
		}
	}
}