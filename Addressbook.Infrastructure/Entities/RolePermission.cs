using System.Collections.Generic;

namespace Addressbook.Infrastructure.Entities
{
    public class RolePermission
    {
        public int RolePermissionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public ICollection<Role> Roles { get; set; } 

        public ICollection<Permission> Permissions { get; set; } 
    }
}