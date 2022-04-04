using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 事件执行的Activity接口
    /// </summary>
    public interface IEventWaitingExecute
    {
        string UserId { get;set;}
        string RoleId { get;set;}

        void Execute(string userId, string roleId, bool isUrgent);

        void Return(string userId, string roleId, bool isUrgent);

        void InitExecStatus();
    }
}
