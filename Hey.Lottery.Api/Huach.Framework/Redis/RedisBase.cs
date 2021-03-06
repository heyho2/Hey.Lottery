﻿using ServiceStack.Redis;
using System;

namespace Huach.Framework.Redis
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        public IRedisClient IClient { get; private set; }
        public RedisBase()
        {
            IClient = RedisManager.GetClient();
        }

        //public static IRedisClient iClient { get; private set; }
        //static RedisBase()
        //{
        //    iClient = RedisManager.GetClient();
        //}

        public virtual void FlushAll()
        {
            IClient.FlushAll();
        }
        public virtual bool Remove(string key)
        {
            return IClient.Remove(key);
        }
        public bool ExpireEntryAt(string key, DateTime expireAt)
        {
            return IClient.ExpireEntryAt(key, expireAt);
        }
        public bool ExpireEntryIn(string key, TimeSpan expireIn)
        {
            return IClient.ExpireEntryIn(key, expireIn);
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    IClient.Dispose();
                    IClient = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            IClient.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            IClient.SaveAsync();
        }
    }
}
