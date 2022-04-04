using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// �¼�ִ�е�Activity�ӿ�
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
