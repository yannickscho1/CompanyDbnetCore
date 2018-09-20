using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Interface
{
    public interface IMessageHelper
    {
        bool SendIntercom(string message);
        Result<UacGroupResult> getUacGroups(int uacGroupId);
        List<UacMemberResult> getUacMembers(int uacGroupId);
        List<UacGroupResult> getUacGroupsOfMember(int uacMemberId);
    }
}
