using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Baisse.StudyCommon.RPC.RpcServer
{
    /// <summary>
    /// 异步套接字连接池
    /// </summary>
    public class SocketEventPool
    {
        //表示的实例的可变大小后进先出（LIFO）集合
        //SocketAsyncEventArgs表示异步套接字操作。
        Stack<SocketAsyncEventArgs> m_pool;


        /// <summary>
        /// //套接字事件池
        /// </summary>
        /// <param name="capacity">最大连接数</param>
        public SocketEventPool(int capacity)
        {
            //实例化最大连接数集合大小
            m_pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        /// <summary>
        /// Stack集合添加值
        /// </summary>
        /// <param name="item"></param>
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (m_pool)
            {
                m_pool.Push(item);
            }
        }

        /// <summary>
        /// 删除集合对锡并返回
        /// </summary>
        /// <returns></returns>
        public SocketAsyncEventArgs Pop()
        {
            lock (m_pool)
            {
                return m_pool.Pop();
            }
        }

        /// <summary>
        /// 集合数量
        /// </summary>
        public int Count
        {
            get { return m_pool.Count; }
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            m_pool.Clear();
        }
    }
}
