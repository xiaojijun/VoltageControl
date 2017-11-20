using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoltageControl.src
{
    // 多线程安全操作
    class WaitingForConfirmList
    {
        private static WaitingForConfirmList m_instance;
        private static readonly object locker = new object();
        private List<WaitingForConfirm> m_PendingChannelList = new List<WaitingForConfirm>();

        public static WaitingForConfirmList Instance()
        {
            lock (locker)
            {
                if(null == m_instance)
                {
                    m_instance = new WaitingForConfirmList();
                }
            }
            return m_instance;
        }

        public List<WaitingForConfirm> GetPendingChannelList()
        {
            return m_PendingChannelList;
        }

        public void AddPendingChannel(WaitingForConfirm item)
        {
            m_PendingChannelList.Add(item);
        }

        public void RemovePendingChannel(int iIndex)
        {
            if (0 <= iIndex && m_PendingChannelList.Count > iIndex)
            {
                m_PendingChannelList.RemoveAt(iIndex);
            }
        }

    }


    class WaitingForConfirm
    {

        private byte m_channel = 0;
        private byte[] m_voltage = new byte[4];
        private DateTime m_SendDataTime;

        // disable construct without para
        private WaitingForConfirm()
        {
            ;
        }

        public WaitingForConfirm(byte channel, byte[] voltage)
        {
            m_channel = channel;
            m_voltage = voltage;
            m_SendDataTime = DateTime.Now;
        }

        public byte Getchannel()
        {
            return m_channel;
        }
        public byte[] GetVoltage()
        {
            return m_voltage;
        }

        public bool CheckRecieveChannel_ACK(byte channel, byte[] voltage)
        {
            if (channel == m_channel && voltage.Length == m_voltage.Length)
            {
                for(int i =0; i < m_voltage.Length; ++i)
                {
                    if (m_voltage[i] != voltage[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool CheckTimeOut()
        {
            TimeSpan timeSpan = DateTime.Now - m_SendDataTime;
            if (10 < timeSpan.TotalSeconds)
            {
                return true;
            }
            return false;
        }
    }
}
