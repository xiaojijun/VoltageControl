﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using VoltageControl.src;
using System.Collections;
using System.Threading;
using VoltageControl.CommonDEF;

namespace VoltageControl
{
    delegate void ResetChannel(byte channel_number, string strState);
    delegate void WriteTerminatorLog(string log);

    public partial class MainWindow : Form
    {


        public int ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES = 10; // (FE) FE 68H CHANNEL CONTROL DATA*4 CS STOP
        public int ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES = 11; // FE FE 68H CHANNEL CONTROL DATA*4 CS STOP

        // when cell validating set value, and when edit ending clear the value
        string strBeforeCellChangeValue = "";

        // pending channel 
        //private List<WaitingForConfirm> cPendingChannel = new List<WaitingForConfirm>();

        SerialPort cSerialPort = null;
        private System.Timers.Timer cTimer = null;
        // mark the timer elapesd states
        private bool bTimerInElapesing = false;

        SendChannelData cSendChannel = null;

        private bool bIsInReceiving = false;
        private bool bPrepareCloseMainWindow = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSerialPort();
            FillVoltageChannel(140);
            SetTimerInterval();
            InitalSendData();

        }


        private void InitalSendData()
        {
            cSendChannel = new SendChannelData();
            cSendChannel.SetLog = new SendChannelData.OutPutMessage(WriteLog);
            cSendChannel.SetRowSending = SetRowState;
        }

        // set timer interval
        private void SetTimerInterval()
        {
            
            cTimer = new System.Timers.Timer();
            // set interval
            cTimer.Interval = 2000;
            cTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElaped_CheckPendingChannel);
            cTimer.AutoReset = true;
            cTimer.Enabled = true;
        }

        private void TimerElaped_CheckPendingChannel(object sender, System.Timers.ElapsedEventArgs e)
        {
            bTimerInElapesing = true;
            List<WaitingForConfirm> cPendingChannel = WaitingForConfirmList.Instance().GetPendingChannelList();
            string str_log1 = "";
            for (int i = cPendingChannel.Count - 1; i >= 0; --i)
            {
                if (cPendingChannel[i].CheckTimeOut())
                {
                    // reset channel
                    ResetChannel resetChannel = new ResetChannel(RestDataGridViewRows);
                    dataGridView_channel.Invoke(resetChannel, cPendingChannel[i].Getchannel(), "超时：无回应！");
                    str_log1 += "通道【" + cPendingChannel[i].Getchannel().ToString() + "】：等待下位机回应超时！！！！\r\n";
                    // remove
                    WaitingForConfirmList.Instance().RemovePendingChannel(i);
                }
            }

            // write log
            if ("" != str_log1)
            {
                str_log1 += "\r\n";
                WriteTerminatorLog Log_delegate1 = new WriteTerminatorLog(WriteLog);
                terminator.Invoke(Log_delegate1, str_log1);
            }

            // time elpesed end
            bTimerInElapesing = false;

        }

        // set datagridview fill the window

        private void MainWindow_load(object ob, EventArgs e)
        {
            for (int i = 0; i < dataGridView_channel.ColumnCount; ++i)
            {
                dataGridView_channel.Columns[i].Width = panel2.Width / dataGridView_channel.ColumnCount;
            }
        }

        // Open serial port
        private void button_serialsetting_Click(object sender, EventArgs e)
        {
            if(bIsInReceiving || cSendChannel.IsInSendingData())
            {
                MessageBox.Show("串口正在收发数据，请稍后重试！");
                return;
            }

            if ("关闭串口" == button_serialsetting.Text)
            {
                cSerialPort.Close();
                cSerialPort.Dispose();
                cSerialPort = null;

                button_serialsetting.BackgroundImage = Properties.Resources.swith_grey;
                button_serialsetting.Text = "打开串口";
                comboBox_com.Enabled = true;
                comboBox_baudrate.Enabled = true;
                comboBox_checkbits.Enabled = true;
                comboBox_databits.Enabled = true;
                comboBox_stopbits.Enabled = true;


                return;
            }

            Int32 iBoadrate = Convert.ToInt32(comboBox_baudrate.Text);

            Parity eParity = Parity.None;
            switch (comboBox_checkbits.Text)
            {
                case "None":
                    eParity = Parity.None;
                    break;
                case "Odd":
                    eParity = Parity.Odd;
                    break;
                case "Even":
                    eParity = Parity.Even;
                    break;
                default:
                    break;
            }


            Int32 iDataBits = Convert.ToInt32(comboBox_databits.Text);

            StopBits eStopBits = StopBits.None;

            switch (comboBox_stopbits.Text)
            {
                case "None":
                    eStopBits = StopBits.None;
                    break;
                case "One":
                    eStopBits = StopBits.One;
                    break;
                case "Two":
                    eStopBits = StopBits.Two;
                    break;
                case "OnePointFive":
                    eStopBits = StopBits.OnePointFive;
                    break;
                default:
                    break;
            }

            cSerialPort = new SerialPort(comboBox_com.Text, iBoadrate, eParity, iDataBits, eStopBits);

            if (null == cSerialPort)
            {
                MessageBox.Show("串口打开失败！");
            }
            else
            {
                try
                {
                    cSerialPort.Open();
                    // serial port event setting
                    cSerialPort.ReceivedBytesThreshold = 1;
                    cSerialPort.DataReceived += new SerialDataReceivedEventHandler(Serial_ReceiveData);

                    // other setting
                    button_serialsetting.BackgroundImage = Properties.Resources.swith_green;
                    button_serialsetting.Text = "关闭串口";
                    comboBox_com.Enabled = false;
                    comboBox_baudrate.Enabled = false;
                    comboBox_checkbits.Enabled = false;
                    comboBox_databits.Enabled = false;
                    comboBox_stopbits.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cSerialPort = null;
                }

            }
        }

        // *********************************Serial port initialize**********************************************************

        public void InitializeSerialPort()
        {
            GetSeiralComList();
            GetBaudrateList();
            GetDataBitList();
            GetStopBitList();
            GetParityBitList();
        }

        public void GetSeiralComList()
        {
            comboBox_com.Items.Clear();
            string[] strComName = SerialPort.GetPortNames();

            foreach (string name in strComName)
            {
                comboBox_com.Items.Add(name);
            }
            if (0 >= comboBox_com.Items.Count)
            {
                comboBox_com.Items.Add("没有串口");
            }

            if (0 < comboBox_com.Items.Count)
            {
                comboBox_com.SelectedIndex = 0;
            }
        }

        public void GetBaudrateList()
        {
            comboBox_baudrate.Items.Add(2400);
            comboBox_baudrate.Items.Add(4800);
            comboBox_baudrate.Items.Add(9600);
            comboBox_baudrate.Items.Add(115200);
            comboBox_baudrate.SelectedIndex = 2;
        }

        public void GetDataBitList()
        {
            comboBox_databits.Items.Add(5);
            comboBox_databits.Items.Add(6);
            comboBox_databits.Items.Add(7);
            comboBox_databits.Items.Add(8);
            comboBox_databits.SelectedIndex = 3;
        }

        public void GetStopBitList()
        {
            comboBox_stopbits.Items.Add(StopBits.None);
            comboBox_stopbits.Items.Add(StopBits.One);
            comboBox_stopbits.Items.Add(StopBits.OnePointFive);
            comboBox_stopbits.Items.Add(StopBits.Two);
            comboBox_stopbits.SelectedIndex = 1;
        }

        public void GetParityBitList()
        {
            comboBox_checkbits.Items.Add(Parity.None);
            comboBox_checkbits.Items.Add(Parity.Odd);
            comboBox_checkbits.Items.Add(Parity.Even);
            comboBox_checkbits.SelectedIndex = 0;
        }

        // ********************************Serial port initialeze end***********************************************************

        private void FillVoltageChannel(Int32 iChannelCount)
        {
            for (int i = 0; i < iChannelCount; ++i)
            {
                int iRowIndex = dataGridView_channel.Rows.Add();

                dataGridView_channel.Rows[iRowIndex].Height = 35;

                // checkbox
                dataGridView_channel.Rows[iRowIndex].Cells[0].Value = "False";

                // channel
                dataGridView_channel.Rows[iRowIndex].Cells[1].Value = iRowIndex + 1;

                // voltage
                dataGridView_channel.Rows[iRowIndex].Cells[2].Value = 0.00.ToString("F2");

                // state
                dataGridView_channel.Rows[iRowIndex].Cells[3].Value = "正常";

                // button
                dataGridView_channel.Rows[iRowIndex].Cells[4].Value = "发送";
            }

            // set Width
            for (int i = 0; i < dataGridView_channel.ColumnCount; ++i)
            {
                dataGridView_channel.Columns[i].Width = panel2.Width / dataGridView_channel.ColumnCount - 2;
            }
        }

        private List<byte> receivebuff = new List<byte>(256);
        private void Serial_ReceiveData(object Sender, SerialDataReceivedEventArgs Event)
        {
            if (bPrepareCloseMainWindow)
            {
                return;
            }
            bIsInReceiving = true;
            int iDataByteToRead = cSerialPort.BytesToRead;
            byte[] readbuff = new byte[iDataByteToRead];
            int iActReadBytes = cSerialPort.Read(readbuff, 0, iDataByteToRead);
            receivebuff.AddRange(readbuff);
            //int iOneFrameByte = 8;
            byte STOP_BYTE = 0x16;
            int STOP_BYTE_index = ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES - 1; // 已经考虑了一个前导符
            int iCS_Byte_Index = STOP_BYTE_index - 1;
            while (receivebuff.Count >= ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES) // (FE) FE 68H CHANNEL CONTROL DATA*4 CS STOP
            {
                // Check data effect
                int iStartIndex = 0; // find the start byte with 0xFE 0x68H
                bool bFindStart = false;
                for (int i = 0; i + 1 < receivebuff.Count; ++i)
                {
                    // find start index
                    if (0xFE == receivebuff[i] && 0x68 == receivebuff[i + 1])
                    {
                        iStartIndex = i; // FE 68 的位置
                        bFindStart = true;
                        break;
                    }
                }

                if (bFindStart)
                {
                    receivebuff.RemoveRange(0, iStartIndex); // 0: FE, 1:68H
                    // check date count after remove useless byte
                    if (ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES > receivebuff.Count) // +1 是只起始符前面的一个FE,代表68H 是紧跟这前导符的
                    {
                        // the data not receive enought, waiting for next receive
                        break;
                    }
                    // calculate crs
                    byte byCS = 0;
                    for (int i = 1; i < iCS_Byte_Index; ++i)
                    {
                        byCS += receivebuff[i];
                    }
                    // check CRS and stop byte
                    if ((byCS != receivebuff[iCS_Byte_Index]) || (STOP_BYTE != receivebuff[STOP_BYTE_index]))
                    {
                        // CRS is failed or not find stop byte
                        // remove start position, drop this frame data

                        // write log
                        byte[] byte_log2 = new byte[ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES];
                        receivebuff.CopyTo(0, byte_log2, 0, ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES);
                        string str_log2 = "和校验失败【应为0x" + byCS.ToString("X2") + "】，或停止位【应为0x" + STOP_BYTE.ToString("X2") + "】错误，丢弃！ 接收数据：\r\n" + ChangeArrayToHexString(byte_log2) + "\r\n\r\n";
                        WriteTerminatorLog Log_delegate2 = new WriteTerminatorLog(WriteLog);
                        terminator.Invoke(Log_delegate2, str_log2);


                        receivebuff.RemoveRange(0, 2);
                        continue;
                    }

                    // get control byte
                    byte control = receivebuff[3];
                    // data from MEU to PC(bit7 = 1) and the data is ACK
                    if ((0 == (control & 0x80)) || (0x2 != (control & 0x60) >> 5))
                    {
                        // the control byte check fail
                        // remove start position, drop this frame data

                        // write log
                        byte[] byte_log1 = new byte[ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES];
                        receivebuff.CopyTo(0, byte_log1, 0, ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES);
                        string str_log1 = "控制位【0x" + control.ToString("X2") + "】校验失败，丢弃！ 接收数据：\r\n" + ChangeArrayToHexString(byte_log1) + "\r\n\r\n";
                        WriteTerminatorLog Log_delegate1 = new WriteTerminatorLog(WriteLog);
                        terminator.Invoke(Log_delegate1, str_log1);

                        receivebuff.RemoveRange(0, 2);
                        continue;
                    }

                    // here, the effect data get: FE 68H channel control byte_data*4 cs stop
                    byte channel = receivebuff[2];
                    byte[] voltage = new byte[4];
                    receivebuff.CopyTo(4, voltage, 0, 4);

                    // write log
                    double fVoltage = (double)voltage[0];
                    // calculate decimal
                    fVoltage += (double)(((int)voltage[1] << 16) | ((int)voltage[2] << 8) | ((int)voltage[3])) / 0xFFFFFF;
                    byte[] byte_log = new byte[ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES];
                    receivebuff.CopyTo(0, byte_log, 0, ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES);
                    string str_log = "通道【" + channel.ToString() + "】接受数据-> 电压【" + fVoltage.ToString("F2") + "】" + ChangeArrayToHexString(byte_log) + "\r\n";


                    bool bFindPendingChannel = false;

                    List<WaitingForConfirm> cPendingChannel = WaitingForConfirmList.Instance().GetPendingChannelList();
                    for (int i = cPendingChannel.Count - 1; i >= 0; --i)
                    {
                        if (cPendingChannel[i].CheckRecieveChannel_ACK(channel, voltage))
                        {
                            // set row to 
                            ResetChannel resetChannel = new ResetChannel(RestDataGridViewRows);

                            dataGridView_channel.Invoke(resetChannel, channel, "设置完成");
                            str_log += "通道【" + channel.ToString() + "】设置完成！！\r\n\r\n";
                            bFindPendingChannel = true;

                            // remove pengding channel
                            WaitingForConfirmList.Instance().RemovePendingChannel(i);
                        }
                    }

                    if (!bFindPendingChannel)
                    {
                        str_log += "通道【" + channel.ToString() + "】-> 数据无效，可能原因为：数据返回超时！\r\n\r\n";
                    }
                    WriteTerminatorLog Log_delegate = new WriteTerminatorLog(WriteLog);
                    terminator.Invoke(Log_delegate, str_log);

                    // remove one frame from revieve buff and the pre byte(FE)
                    receivebuff.RemoveRange(0, ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES);
                }
                else
                {
                    receivebuff.RemoveRange(0, receivebuff.Count - 1); // leave last byte data 
                }
            }
            bIsInReceiving = false;
        }



        private void CellValidating(object ob, DataGridViewCellValidatingEventArgs e)
        {
            if ("voltage" == dataGridView_channel.Columns[e.ColumnIndex].Name)
            {
                //save cell valued to sram, in order to check valude change
                strBeforeCellChangeValue = dataGridView_channel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                DataGridViewRow cRow = dataGridView_channel.Rows[e.RowIndex];
                string str_text = e.FormattedValue.ToString();

                // the string length must less than  6

                if (6 < str_text.Trim().Length)
                {
                    MessageBox.Show("请输入不要超过6个字符！");
                    e.Cancel = true;
                    dataGridView_channel.CancelEdit();
                    return;
                }

                //if (!Regex.IsMatch(str_text, @"^[-]?\d+[.]?\d*$"))
                if (!Regex.IsMatch(str_text, @"\d+[.]?\d*$"))
                {
                    string channel = dataGridView_channel.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                    MessageBox.Show("通道" + channel + " :请输入正确的电压值！:-->" + str_text);
                    //dataGridView_channel.CurrentCell = dataGridView_channel.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //dataGridView_channel.BeginEdit(false);
                    e.Cancel = true;
                    dataGridView_channel.CancelEdit();

                }
                else
                {
                    // Int32 iUser_in = Convert.ToInt32(sender_data);
                    float fUserIn = Convert.ToSingle(str_text);
                    if (0xFF < fUserIn)
                    {
                        MessageBox.Show("输入电压过大！！");
                        e.Cancel = true;
                        dataGridView_channel.CancelEdit();
                    }
                    else if (0 > fUserIn)
                    {
                        MessageBox.Show("输入正数的电压！！");
                        e.Cancel = true;
                        dataGridView_channel.CancelEdit();
                    }
                    else
                    {
                        //dataGridView_channel.Rows[e.RowIndex].Cells[0].Value = true;
                        e.Cancel = false;
                    }
                }
            }
        }

        private void SendSingleChannel_button_click(object ob, DataGridViewCellEventArgs e)
        {
            if ("operation" == dataGridView_channel.Columns[e.ColumnIndex].Name)
            {
                //dataGridView_channel.Rows[e.ColumnIndex];
                //MessageBox.Show(Convert.ToString("voltage:" + dataGridView_channel.Rows[e.RowIndex].Cells[2].Value) + ". row index" + e.RowIndex.ToString());
                if (null == cSerialPort || false == cSerialPort.IsOpen)
                {
                    MessageBox.Show("请先打开串口！");
                }
                else
                {
                    List<WaitingForConfirm> cPendingChannel = WaitingForConfirmList.Instance().GetPendingChannelList();
                    // check channel state, if the channel is waiting for respone, not send data 
                    for (int i = 0; i < cPendingChannel.Count; ++i)
                    {
                        // row index + 1 is the channel number
                        if ((byte)(e.RowIndex + 1) == cPendingChannel[i].Getchannel())
                        {
                            // write log
                            terminator.AppendText(DateTime.Now.ToLongTimeString() + ": 通道【" + cPendingChannel[i].Getchannel().ToString() + "】发送失败： 原因是正在等待下位机回应！\r\n\r\n");
                            return;
                        }
                    }

                    string sender_data = dataGridView_channel.Rows[e.RowIndex].Cells[2].Value.ToString();
                    List<RowInfo> SendList = new List<RowInfo>();
                    RowInfo rowInfo = new RowInfo();
                    rowInfo.iRowIndex = e.RowIndex;
                    rowInfo.strUserPutIn = sender_data;
                    SendList.Add(rowInfo);

                    if (!cSendChannel.InitialMultiRows(cSerialPort, SendList))
                    {
                        MessageBox.Show("发送串口忙，请稍后重试！");
                        return;
                    }
                    Thread SendingThread = new Thread(cSendChannel.SendMultiRows);
                    SendingThread.Start();

                }
            }

        }
        

        private string ChangeArrayToHexString(byte[] array)
        {
            string ret_string = "";
            for (int i = 0; i < array.Length; ++i)
            {
                ret_string += (array[i].ToString("X2") + " ");
            }
            return ret_string;
        }

        private void CellEndEdit_FormatNumber(object sender, DataGridViewCellEventArgs e)
        {
            if ("voltage" == dataGridView_channel.Columns[e.ColumnIndex].Name)
            {
                string Input_data = dataGridView_channel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                float InValtage = 0;
                if (float.TryParse(Input_data, out InValtage))
                {
                    dataGridView_channel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = InValtage.ToString("F2");
                    if (dataGridView_channel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != strBeforeCellChangeValue)
                    {
                        dataGridView_channel.Rows[e.RowIndex].Cells[0].Value = true;
                    }
                }
                else
                {
                    MessageBox.Show("输入正确的电压！！");
                }

                //initialize the saving cell valued before changed
                strBeforeCellChangeValue = "";
            }
        }

        public void RestDataGridViewRows(byte channel_num, string strState)
        {
            dataGridView_channel.Rows[channel_num - 1].DefaultCellStyle.BackColor = DefaultBackColor;
            dataGridView_channel.Rows[channel_num - 1].ReadOnly = false;
            dataGridView_channel.Rows[channel_num - 1].Cells[0].Value = false;
            dataGridView_channel.Rows[channel_num - 1].Cells[3].Value = strState;
        }


        private void terminator_TextChange(object ob, EventArgs e)
        {
            terminator.SelectionStart = terminator.TextLength;
            terminator.ScrollToCaret();
        }

        private void WriteLog(string log)
        {
            if (this.terminator.InvokeRequired)
            {
                this.Invoke(cSendChannel.SetLog, log);
            }
            else
            {
                terminator.AppendText(log);
            }
        }

        private void button_CleanLog_Click(object sender, EventArgs e)
        {
            terminator.Clear();
        }

        private void RenewComList(object sender, EventArgs e)
        {
            GetSeiralComList();
        }

        private void ResetGridView_Click(object sender, EventArgs e)
        {
            dataGridView_channel.Rows.Clear();
            FillVoltageChannel(140);
            SelectAllMemu.Text = "全部选中";
        }



        private void SendMultiChannel_Click(object sender, EventArgs e)
        {
            if (null == cSerialPort || !cSerialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            List<RowInfo> sendlist = new List<RowInfo>();
            string s = "";
            for (int row = 0; row < dataGridView_channel.Rows.Count; ++row)
            {
                string s1 = dataGridView_channel.Rows[row].Cells[0].EditedFormattedValue.ToString();
                string s2 = dataGridView_channel.Rows[row].Cells[3].Value.ToString();
                if ("True" == s1 && "已发送，等待状态返回……" != s2)
                {
                    RowInfo rowInfo = new RowInfo(row, dataGridView_channel.Rows[row].Cells[2].Value.ToString());
                    sendlist.Add(rowInfo);
                    s += "通道【" + (row + 1).ToString() + "】  电压【" + dataGridView_channel.Rows[row].Cells[2].Value.ToString() + "】\r\n";
                }
            }

            if (0 == sendlist.Count)
            {
                return;
            }


            MultiSendConfirmDiag diag = new MultiSendConfirmDiag(s);
            DialogResult result = diag.ShowDialog();
            if (DialogResult.OK != result)
            {
                terminator.AppendText("合并发送取消！\r\n");
                return;
            }

            // 发送串口忙
            if(false == cSendChannel.InitialMultiRows(cSerialPort ,sendlist))
            {
                MessageBox.Show("串口忙，请稍后重试！");
                return;
            }
            Thread SendThread = new Thread(cSendChannel.SendMultiRows);
            SendThread.Start();

        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            // 如果checkbox是当前焦点的话，checkbox的指会被formatvalud保持，需要改变焦点
            if (0 == dataGridView_channel.CurrentCell.ColumnIndex)
            {
                dataGridView_channel.CurrentCell = dataGridView_channel[1, dataGridView_channel.CurrentCell.RowIndex];
            }

            if ("全部选中" == SelectAllMemu.Text.ToString())
            {
                foreach (DataGridViewRow OneRow in dataGridView_channel.Rows)
                {
                    if ("True" != OneRow.Cells[0].Value.ToString())
                    {
                        OneRow.Cells[0].Value = "True";
                    }
                }
                SelectAllMemu.Text = "取消全选";
            }
            else
            {
                foreach (DataGridViewRow OneRow in dataGridView_channel.Rows)
                {
                    if ("True" == OneRow.Cells[0].Value.ToString())
                    {
                        OneRow.Cells[0].Value = "False";
                    }
                }
                SelectAllMemu.Text = "全部选中";
            }
        }


        // 设置datagridview状态

        private void SetRowState(int iRowIndex, RowState eRowState, string ThreadName)
        {
            if (this.dataGridView_channel.InvokeRequired)
            {  
                if ("ThreadSending" == ThreadName)
                {
                    this.dataGridView_channel.Invoke(cSendChannel.SetRowSending, iRowIndex, eRowState, ThreadName);
                }

            }
            else
            {
                switch(eRowState)
                {
                    case RowState.ROW_STATE_NORMAL:
                        {
                            dataGridView_channel.Rows[iRowIndex].Cells[0].Value = false;
                            // read only
                            dataGridView_channel.Rows[iRowIndex].ReadOnly = false;
                            // background color
                            dataGridView_channel.Rows[iRowIndex].DefaultCellStyle.BackColor = DefaultBackColor;
                            // channnel state
                            dataGridView_channel.Rows[iRowIndex].Cells[3].Value = "正常";

                        }
                        return;
                    case RowState.ROW_STATE_SENDING:
                        {
                            dataGridView_channel.Rows[iRowIndex].Cells[0].Value = true;
                            // read only
                            dataGridView_channel.Rows[iRowIndex].ReadOnly = true;
                            // background color
                            dataGridView_channel.Rows[iRowIndex].DefaultCellStyle.BackColor = Color.Gray;
                            // channnel state
                            dataGridView_channel.Rows[iRowIndex].Cells[3].Value = "已发送，等待状态返回……";

                        }
                        return;
                    case RowState.ROW_STATE_TIMEOUT:
                        {
                            dataGridView_channel.Rows[iRowIndex].Cells[0].Value = false;
                            // read only
                            dataGridView_channel.Rows[iRowIndex].ReadOnly = false;
                            // background color
                            dataGridView_channel.Rows[iRowIndex].DefaultCellStyle.BackColor = DefaultBackColor;
                            // channnel state
                            dataGridView_channel.Rows[iRowIndex].Cells[3].Value = "超时：无回应！";

                        }
                        return;
                    case RowState.ROW_STATE_SENDDONE:
                        {
                            dataGridView_channel.Rows[iRowIndex].Cells[0].Value = false;
                            // read only
                            dataGridView_channel.Rows[iRowIndex].ReadOnly = false;
                            // background color
                            dataGridView_channel.Rows[iRowIndex].DefaultCellStyle.BackColor = DefaultBackColor;
                            // channnel state
                            dataGridView_channel.Rows[iRowIndex].Cells[3].Value = "设置完成！";

                        }
                        return;
                    default:
                        return;
                }
            }
        }


        
        // 主程序关闭事件
        private void FormClosing_DeleteSource(object sender, FormClosingEventArgs e)
        {
            bPrepareCloseMainWindow = true;

            while (bIsInReceiving || cSendChannel.IsInSendingData())
            {
                MessageBox.Show("串口正在收发数据，请稍后关闭！");
                e.Cancel = true;
                bPrepareCloseMainWindow = false;
                return;
            }

            // stop timer
            if (null != cTimer)
            {
                // waiting for timer elpesd end
                while(!bTimerInElapesing)
                {
                    cTimer.Stop();
                    break;
                }
            }

            if (null != cSerialPort)
            {
                cSerialPort.Close();
                cSerialPort.Dispose();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void SetDelayTime_Click(object sender, EventArgs e)
        {
            string delay = delay_time.Text;
            Int32 iDelayTime = 0;
            if (int.TryParse(delay, out iDelayTime))
            {
                if (iDelayTime <= 10000 && iDelayTime >= 100)
                {
                    cSendChannel.SetDelayTime(iDelayTime);
                    delay_time.Text = iDelayTime.ToString();
                    return;
                }
                else
                {

                }
            }
            MessageBox.Show("请输入100 ~ 10000（0.1秒 到 10秒）时间！");
            int iDefault = cSendChannel.GetDelayTime();
            delay_time.Text = iDefault.ToString();
        }

    }
}

