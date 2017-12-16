using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using VoltageControl.CommonDEF;

namespace VoltageControl.src
{


    public struct RowInfo
    {
        public int iRowIndex;
        public string strUserPutIn;
        public RowInfo(int row, string strIn)
        {
            iRowIndex = row;
            strUserPutIn = strIn;
        }
    }



    class SendChannelData
    {
        public int ONE_FRAME_BYTES_WITH_ONE_PRE_BYTES = 10; // (FE) FE 68H CHANNEL CONTROL DATA*4 CS STOP
        public int ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES = 11; // FE FE 68H CHANNEL CONTROL DATA*4 CS STOP

        private SerialPort cSerialPort = null;
        //List<RowInfo> SendList = new List<RowInfo>();
        private List<RowInfo> SendList;
        private bool bIsInSendingData = false;
        // default delay time = 1000ms;
        private int iDelayTime = 300;
        public delegate void OutPutMessage(string s);
        public OutPutMessage SetLog;

        public delegate void SetRowState(int iRowIndex, RowState eRowState, string ThreadName);
        public SetRowState SetRowSending;

        public void SetDelayTime(int delay)
        {
            iDelayTime = delay;
        }
        public int GetDelayTime()
        {
            return iDelayTime;
        }


        public bool IsInSendingData()
        {
            return bIsInSendingData;
        }


        public SendChannelData()
        {
        }


        public bool InitialMultiRows(SerialPort cPort, List<RowInfo> RowList)
        {
            if (bIsInSendingData)
            {
                return false;
            }
            cSerialPort = cPort;
            SendList = RowList;
            return true;
        }


        public void WriteRichBox(string s)
        {
            SetLog(s);
        }
       
        public void SendMultiRows()
        {
            if (null == cSerialPort || !cSerialPort.IsOpen)
            {
                WriteRichBox("请先打开串口！\r\n");
                return;
            }
            // set sending state
            bIsInSendingData = true;

            foreach (RowInfo item in SendList)
            {
                SendData(item.strUserPutIn, item.iRowIndex);
                System.Threading.Thread.Sleep(iDelayTime);
            }

            // reset sending state
            bIsInSendingData = false;
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

        private void SendData(string strUserPutIn, int iRowsIndex)
        {

            float fUserIn = 0;
            if (!float.TryParse(strUserPutIn, out fUserIn))
            {
                WriteRichBox("发送失败：输入电压不正确！\r\n");
                return;
            }
            byte[] senderbuff = new byte[ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES];
            senderbuff[0] = 0xFE;
            senderbuff[1] = 0xFE;
            senderbuff[2] = 0x68; // start byte

            // channal  
            senderbuff[3] = Convert.ToByte(iRowsIndex + 1);

            // control byte
            // bit7:0b  data from computer to meu
            // bit6 bit5: 00b set voltage
            byte byControl = 0;
            senderbuff[4] = byControl;


            // DATA
            Byte data_integer = Convert.ToByte(fUserIn);
            senderbuff[5] = data_integer;
            float data_decimal = (fUserIn - (float)data_integer);
            int data_decimal_to_int = (int)(data_decimal * 0xFFFFFF);

            senderbuff[6] = (Byte)((data_decimal_to_int & 0xFF0000) >> 16);
            senderbuff[7] = (Byte)((data_decimal_to_int & 0x00FF00) >> 8);
            senderbuff[8] = (Byte)((data_decimal_to_int & 0x0000FF));

            // CS
            byte byCS = 0;
            for (int i = 2; i <= ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES - 2 - 1; ++i)
            {
                byCS += senderbuff[i];
            }
            senderbuff[9] = byCS;

            // stop
            senderbuff[10] = 0x16;

            // send data
            cSerialPort.Write(senderbuff, 0, ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES);

            // set pending channel
            byte[] VoltageByte = new byte[4];
            VoltageByte[0] = senderbuff[5];
            VoltageByte[1] = senderbuff[6];
            VoltageByte[2] = senderbuff[7];
            VoltageByte[3] = senderbuff[8];
            WaitingForConfirm cSendingChannel = new WaitingForConfirm(senderbuff[3], VoltageByte);
            // add the pending channel to list
            //cPendingChannel.Add(cSendingChannel);
            WaitingForConfirmList.Instance().AddPendingChannel(cSendingChannel);

            SetRowSending(iRowsIndex, RowState.ROW_STATE_SENDING, "ThreadSending");


            // set log
            WriteRichBox(DateTime.Now.ToLongTimeString() + ": 通道【" + senderbuff[3].ToString() + "】发送->  电压【" + strUserPutIn + "】\r\n" + ChangeArrayToHexString(senderbuff) + "\r\n\r\n");
       


            //test 
            senderbuff[4] = 0xC0;
            byCS = 0;
            for (int i = 2; i <= ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES - 2 - 1; ++i)
            {
                byCS += senderbuff[i];
            }
            senderbuff[9] = byCS;
            cSerialPort.Write(senderbuff, 0, ONE_FRAME_BYTES_WITH_TWO_PRE_BYTES);
        }

    }

}
