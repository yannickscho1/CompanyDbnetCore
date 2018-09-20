using Chayns.Backend.Api.Credentials;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Repository;
using CompanyNetCore.Interface;
using CompanyNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Helper
{
    public class MessageHelper : IMessageHelper
    {
        public ChaynsApiInfo _backendApiSettings;
        public MessageHelper(IOptions<ChaynsApiInfo> backendApiSettings)
        {
            _backendApiSettings = backendApiSettings.Value;
        }

        public bool SendIntercom(string message)
        {
            var secret = new SecretCredentials(_backendApiSettings.Secret, 430007);
            var intercomRepository = new IntercomRepository(secret);
            var intercomData = new IntercomData(158746)
            {
                Message = message,
                UserIds = new List<int>
                {
                    1988580
                }
            };
            var result = intercomRepository.SendIntercomMessage(intercomData);
            return result.Status.Success;
        }

        public Result<UacGroupResult> getUacGroups(int uacGroupId)
        {
            var uacGroups = new UacRepository(new SecretCredentials(_backendApiSettings.Secret, 430007));
            var group = new UacGroupDataGet(158746);
            var uacgroups = uacGroups.GetUacGroups(group);
            foreach (var groups in uacgroups.Data)
            {
                Console.WriteLine($"{groups.Name}"); 
            }
            return uacgroups;
        }

        public List<UacMemberResult> getUacMembers(int uacGroupId)
        {
            var uacMembers = new UacMemberRepository(new SecretCredentials(_backendApiSettings.Secret, 430007));
            var user = new UacMemberDataGet(158746)
            {
                UacGroupId = uacGroupId
            };
            var uacmembers = uacMembers.GetUacGroupMember(user);
            foreach (var members in uacmembers.Data)
            {
                Console.WriteLine($"{members.Name}");
            }
            return uacmembers.Data;
        }

        public List<UacGroupResult> getUacGroupsOfMember(int uacMemberId)
        {
            List<UacGroupResult> result = new List<UacGroupResult>();
            var uacGroups = new UacRepository(new SecretCredentials(_backendApiSettings.Secret, 430007));
            var group = new UacGroupDataGet(158746);
            var uacgroups = uacGroups.GetUacGroups(group);

            var uacMembers = new UacMemberRepository(new SecretCredentials(_backendApiSettings.Secret, 430007));
            var user = new UacMemberDataGet(158746)
            {
                UacGroupId = uacMemberId
            };
            var uacmembers = uacMembers.GetUacGroupMember(user);

            foreach (var groups in uacgroups.Data)
            {
                if (groups.CountMember > 0)
                {
                    foreach (var users in getUacMembers(groups.UserGroupId))
                    {
                        if(users.UserId == uacMemberId)
                        {
                            result.Add(groups);
                            Console.WriteLine(groups.ShowName);
                        }
                    }
                }
            }
            return result;
        }
    }
}
