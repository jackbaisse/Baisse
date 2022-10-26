using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Baisse.BaseDataCommon.RPC.RpcServer
{
    /// <summary>
    /// 缓冲区管理器
    /// </summary>
    public class BufferManager
    {
        int m_numBytes;                 //缓冲池控制的字节总数  公式：最大连接数*缓存区大小*2
        byte[] m_buffer;                // 缓冲区管理器维护的基础字节数组
        Stack<int> m_freeIndexPool;     //   后进先出集合
        int m_currentIndex;//初始长度
        int m_bufferSize;// 缓存区大小

        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;
            m_currentIndex = 0;
            m_bufferSize = bufferSize;
            m_freeIndexPool = new Stack<int>();
        }


        /// <summary>
        /// 分配缓冲池使用的缓冲区空间
        /// </summary>
        public void InitBuffer()
        {
            // 创建一个大的缓冲区并将其划分  
            // 输出到每个SocketAsyncEventArg
            m_buffer = new byte[m_numBytes];
        }


        /// <summary>
        ///将缓冲池中的缓冲区分配给指定的SocketAsyncEventArgs 
        /// </summary>
        /// <param name="args"></param>
        /// <returns>如果缓冲区设置成功，则为true，否则为false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            //集合数大于0，设置要与异步套接字方法一起使用的数据缓冲区。
            if (m_freeIndexPool.Count > 0)
            {
                //总字节数,删除顶部对象，缓存区大小
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else
            {
                //缓冲池控制的字节总数-缓冲区大小<初始长度
                if ((m_numBytes - m_bufferSize) < m_currentIndex)
                {
                    return false;
                }
                //总字节数，初始长度，缓冲区大小
                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                //增加初始长度
                m_currentIndex += m_bufferSize;
            }
            return true;
        }


        /// <summary>
        ///从SocketAsyncEventArg对象中移除缓冲区。  
        // 这会将缓冲区释放回缓冲池
        /// </summary>
        /// <param name="args"></param>
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            //添加到集合
            m_freeIndexPool.Push(args.Offset);
            //设置要与异步套接字方法一起使用的数据缓冲区。
            args.SetBuffer(null, 0, 0);
        }
    }
}
