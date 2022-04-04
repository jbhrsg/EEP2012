using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    [Serializable]
	public enum SendToKind
	{
        Role, 

        RefRole, 

        Manager,

        RefManager,

        Applicate,

        ApplicateManager,

        AllRoles,

        User,

        RefUser,

        LastUser
	}
}
