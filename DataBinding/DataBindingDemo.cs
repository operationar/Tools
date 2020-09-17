using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBinding
{
    public partial class DataBindingDemo : Form
    {
        public DataBindingDemo()
        {
            InitializeComponent();
        }
        DataModel dataModel = new DataModel();
       
      

        private void DataBindingDemo_Load(object sender, EventArgs e)
        {
            label1.DataBindings.Add("Text", dataModel, "Data1");
            label2.DataBindings.Add("Text", dataModel, "data2");

            //

            List<TestData> t = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                t.Add(new TestData(i.ToString()));
            }
            foreach (var item in t)
            {
                bindingSource1.Add(item);
            }
            textBox3.DataBindings.Add("Text", bindingSource1, "data1");
            textBox4.DataBindings.Add("Text", bindingSource1, "data2");
            textBox5.DataBindings.Add("Text", bindingSource1, "data3");
            TestData str = (TestData)bindingSource1.Current;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataModel.Data1 = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataModel.Data2 = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
            for (int i = 0; i < bindingSource1.Count; i++)
            {
                if ((bindingSource1.List[i] as TestData).data1 == comboBox1.Text)
                {
                    break;
                }
                else
                {
                    bindingSource1.MoveNext();
                }
            }
        }
    }
    class TestData
    {
        public TestData(string t)
        {
            data1 = t;
            data2 = t;
            data3 = t;
        }
        public string  data1 { get; set; }
        public string  data2 { get; set; }
        public string  data3 { get; set; }

    }
    public class DataModel : INotifyPropertyChanged
    {
        private string  data1;

        public string  Data1
        {
            get { return data1; }
            set { data1 = value; NotifyPropertyChanged(); }
        }
        private string  data2;

        public string  Data2
        {
            get { return data2; }
            set { data2 = value; NotifyPropertyChanged(); }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged==null)
            {
                return;
            }
             PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
             
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
  
 
     
}
 