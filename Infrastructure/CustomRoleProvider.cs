using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CruiseEntertainmentManagnmentSystem.Models;

namespace CruiseEntertainmentManagnmentSystem.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {
       // UsersContext userdbcontext = new UsersContext();
        public override bool IsUserInRole(string username, string roleName)
        {
            using (var DB = new CemsDbContext())
            {
                var user = DB.persons.SingleOrDefault(u => u.Email== username);
                if (user == null)
                    return false;
                var roles = DB.UserRoles.Where(x => x.UserID == user.ID);
                var roleInfo = DB.Roles.Where(x => x.RoleName == roleName);

                return roles != null && roleInfo != null;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var cemsDbContext = new CemsDbContext())
            {

                var user = cemsDbContext.persons.FirstOrDefault(u => u.Email== username);

                if (user == null)
                {

                    var adminuser = cemsDbContext.UserProfiles.Where(x=>x.UserName==username).SingleOrDefault();
                    if (adminuser == null)
                    {
                        return new string[] { };
                    }
                    else
                    {
                        //bool result=Roles.IsUserInRole(adminuser.UserName,"Admin");
                        var roles = cemsDbContext.UserRoles.Where(x => x.UserID == adminuser.UserId).Select(x => x.RoleID).ToArray();
                        var selectedrole = (from role in cemsDbContext.Roles where roles.Contains(role.RoleID) select role);
                        var roletoUser = selectedrole.Select(x => x.RoleName).ToArray();
                        return roles == null ? new string[] { } : roletoUser;
                    }





                    ///code to Give access to Role - Admin
                    ///--------------------------------------------------//////
                    // var users = FundRaisingDBContext.Distributors.SingleOrDefault(u => u.UserName == username);
                    //if(users==null)
                    //{
                    //    return new string[] { };
                    //}
                    //else
                    //{
                    //    var role = FundRaisingDBContext.UserRoles.Where(x => x.UserId == users.userID).Select(x => x.RoleId).ToArray();
                    //    var selectedroles = (from rol in FundRaisingDBContext.Roles where role.Contains(rol.RoleId) select rol);
                    //    var roletoUsers = selectedroles.Select(x => x.RoleName).ToArray();
                    //    //var rolesarray=allroles.ToArray();

                    //    return role == null ? new string[] { } : roletoUsers;
                    //}
                    ///--------------------------------------------------//////
                }
                else
                {
                    var roles = cemsDbContext.UserRoles.Where(x => x.UserID== user.ID).Select(x => x.RoleID).ToArray();
                    var selectedrole = (from role in cemsDbContext.Roles where roles.Contains(role.RoleID) select role);
                    var roletoUser = selectedrole.Select(x => x.RoleName).ToArray();
                    return roles == null ? new string[] { } : roletoUser;
                }

                //var rolesarray=allroles.ToArray();


                //return new string[] { };

            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            using (var cemsDbContext = new CemsDbContext())
            {
                return cemsDbContext.Roles.Select(r => r.RoleName).ToArray();
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}